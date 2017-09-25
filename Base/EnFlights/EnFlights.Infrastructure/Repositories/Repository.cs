using EnFlights.ApplicationCore.Entities.Base;
using EnFlights.ApplicationCore.Interfaces;
using EnFlights.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EnFlights.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        public Repository(string connection)
        {
            _dbContext = new ApplicationDbContext(connection);
        }

        public T GetById(Guid id)
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public List<T> GetAll(params Expression<Func<T, object>>[] properties)
        {
            return _dbContext.Set<T>().ToList();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
