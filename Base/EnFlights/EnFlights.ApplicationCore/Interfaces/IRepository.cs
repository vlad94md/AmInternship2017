using EnFlights.ApplicationCore.Entities.Base;
using System;
using System.Collections.Generic;

namespace EnFlights.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : IBaseEntity
    {
        T GetById(Guid id);
        List<T> GetAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
