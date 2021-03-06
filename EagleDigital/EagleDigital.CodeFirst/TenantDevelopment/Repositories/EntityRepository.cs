﻿using System.Collections.Generic;
using System.Linq;

namespace EagleDigital.CodeFirst.TenantDevelopment.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {

        private readonly IEntitiesContext _context;

        public EntityRepository(IEntitiesContext context)
        {
            _context = context;
        }

        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return _context.Set<T>().SqlQuery(query, parameters).ToList();
        }

        public void CommitChanges()
        {
            _context.SaveChanges();
        }

        public void DeleteOnCommit(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public T Get(int key)
        {
            return _context.Set<T>().Find(key);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T InsertOnCommit(T entity)
        {
            _context.Set<T>().Add(entity);
            //entity.InsAt = DateTime.Now;
            //entity.UpdAt = DateTime.Now;
            return entity;
        }
    }
}
