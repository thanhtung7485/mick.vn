using System;
using System.Linq;

namespace EagleDigital.DbFirst.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly IEntitiesContext _context;

        public EntityRepository(IEntitiesContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
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

        public T InsertOnCommit(T entity)
        {
            _context.Set<T>().Add(entity);

            return entity;
        }

        public T Get(Guid key)
        {
            return _context.Set<T>().Find(key);
        }

        public T Get(string key)
        {
            return _context.Set<T>().Find(key);
        }

        public T Reload(T entity)
        {
            _context.Entry(entity).Reload();
            return entity;
        }
    }
}
