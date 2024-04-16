using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    internal interface IRepository<IEntity> where IEntity : class
    {
        Task AddAsync(IEntity parameter);
        Broker GetAll(IEntity parameter);
        Broker Join(IEntity parameter1, IEntity parameter2);
        Broker Where(IEntity parameter, string statement);
        Task<IEntity> GetById(object id, IEntity parameter);
        Task Delete(IEntity parameter);
        Task Update(IEntity parameter, object id);
        Task<List<IEntity>> ToList(Type entityType);
    }
}
