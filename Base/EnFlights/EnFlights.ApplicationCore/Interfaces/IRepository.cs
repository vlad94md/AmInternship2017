using EnFlights.ApplicationCore.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EnFlights.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(Guid id);
        List<T> GetAll(params Expression<Func<T, object>>[] properties);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
