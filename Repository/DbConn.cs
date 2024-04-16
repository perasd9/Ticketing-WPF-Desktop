using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DbConn : IDisposable
    {
        readonly SqlConnection _connection;
        SqlTransaction _transaction;

        public DbConn()
        {
            _connection = new SqlConnection(@Environment.GetEnvironmentVariable("ConnectionStringTicketing", EnvironmentVariableTarget.User));
        }

        public async Task OpenConnectionAsync()
        {
            await _connection?.OpenAsync();
        }
        public void CloseConnection()
        {
            _connection?.Close();
        }
        public void BeginTransaction()
        {
            _transaction = _connection?.BeginTransaction();
        }
        public void Commit()
        {
            _transaction?.Commit();
        }
        public void Rollback()
        {
            _transaction?.Rollback();
        }
        public SqlCommand CreateCommand()
        {
            return new SqlCommand("", _connection, _transaction);
        }
        public void Dispose()
        {
            _connection?.Dispose();
            _transaction?.Dispose();
        }
    }
}
