using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingCommon.Model;
using TicketingCommon.Model.Attributes;
using System.ComponentModel.DataAnnotations;
using Repository.Interfaces;

namespace Repository
{
    public class Broker : IDisposable, IRepository<IEntity>
    {
        readonly DbConn connection;
        List<IEntity> entities;
        SqlCommand cmd;

        public Broker()
        {
            connection = new DbConn();
        }
        public async Task OpenConnectionAsync()
        {
            await connection?.OpenConnectionAsync();
        }
        public void CloseConnection()
        {
            connection?.CloseConnection();
        }
        public void Commit()
        {
            connection?.Commit();
        }
        public void Rollback()
        {
            connection?.Rollback();
        }
        public void BeginTransaction()
        {
            connection?.BeginTransaction();
        }
        public async Task AddAsync(IEntity parameter)
        {
            using (cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"insert into {parameter.TableName}" +
                    $" output inserted.{parameter.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().Name}" +
                    $" values({parameter.Parameters})";

                foreach (var propertyInfo in parameter.GetType().GetProperties().Where(prop => !Attribute.IsDefined(prop, typeof(InsertAttribute))))
                {
                    cmd.Parameters.AddWithValue($"@{propertyInfo.Name}", propertyInfo.GetValue(parameter));
                }
                parameter.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().SetValue(parameter, await cmd.ExecuteScalarAsync());
            }

        }
        public Broker GetAll(IEntity parameter) 
        {
            cmd = connection.CreateCommand();

            cmd.CommandText = $"select * from {parameter.TableName}";

            return this;
        }
        public Broker Join(IEntity parameter1, IEntity parameter2)
        {
            cmd.CommandText += $" join {parameter2.TableName} on ({parameter1.TableName}.{parameter2.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().Name}" +
                $"= {parameter2.TableName}.{parameter2.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().Name})";
            return this;
        }
        public Broker Where(IEntity parameter, string statement)
        {
            if (cmd.CommandText.Contains("where"))
                cmd.CommandText += $" and {parameter.TableName}.{statement}";
            else
                cmd.CommandText += $" where {parameter.TableName}.{statement}";
            return this;
        }
        public async Task<IEntity> GetById(object id, IEntity parameter)
        {
            using (cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"select * from {parameter.TableName} where {parameter.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)))}";

                var reader = await cmd.ExecuteReaderAsync();

                await reader.ReadAsync();

                IEntity entity = (IEntity)Activator.CreateInstance(parameter.GetType());
                int conuntOfProperty = 0;
                foreach (var propertyInfo in entity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(SelectAttribute))))
                {
                    entity.GetType().GetProperty(propertyInfo.Name).SetValue(entity, reader[conuntOfProperty++]);
                }
                return entity;
            }
        }
        public async Task Delete(IEntity parameter)
        {
            using(cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"delete from {parameter.TableName} where {parameter.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().Name} = @identifier";
                cmd.Parameters.AddWithValue("@identifier", parameter.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().GetValue(parameter));
                foreach (var prop in parameter.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).Skip(1))
                {
                    cmd.CommandText += $" and {prop.Name} = {prop.GetValue(parameter)}";
                    }

                await cmd.ExecuteNonQueryAsync();
            }
        }
        public async Task Update(IEntity parameter, object id) 
        {
            using (cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"update {parameter.TableName} set {parameter.UpdateParameters}" +
                    $" where {parameter.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First().Name} = @identifier";
                    
                cmd.Parameters.AddWithValue("@identifier", id);

                foreach (var prop in parameter.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).Skip(1))
                {
                    cmd.CommandText += $" and {prop.Name} = {prop.GetValue(parameter)}";
                }


                foreach (var propertyInfo in parameter.GetType().GetProperties().Where(prop => !Attribute.IsDefined(prop, typeof(InsertAttribute))))
                {
                    cmd.Parameters.AddWithValue($"@{propertyInfo.Name}", propertyInfo.GetValue(parameter));
                }
                await cmd.ExecuteNonQueryAsync();
            }
        }
        public async Task<List<IEntity>> ToList(Type entityType)
        {
            
            entities = new List<IEntity>();
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var entity = Activator.CreateInstance(entityType);
                int conuntOfProperty = 0;
                foreach(var propertyInfo in entity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(SelectAttribute))))
                {
                    entity.GetType().GetProperty(propertyInfo.Name).SetValue(entity, reader[conuntOfProperty++]);
                }
                foreach(var propertyInfo in entity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(JoinAttribute))))
                {
                    if (cmd.CommandText.Contains(propertyInfo.Name))
                    {
                        var joinEntity = Activator.CreateInstance(propertyInfo.PropertyType);
                        foreach (var propertyInfo2 in joinEntity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(SelectAttribute))))
                        {
                            joinEntity.GetType().GetProperty(propertyInfo2.Name)?.SetValue(joinEntity, reader[conuntOfProperty++]);
                        }
                            entity.GetType().GetProperty(propertyInfo.Name).SetValue(entity, joinEntity);
                    }
                }
                    entities.Add((IEntity)entity);
            }
            cmd.Dispose();
            reader.Close();
            return entities;
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
