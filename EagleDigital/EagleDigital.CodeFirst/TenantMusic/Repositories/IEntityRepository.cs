using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleDigital.CodeFirst.TenantMusic.Repositories
{
    public interface IEntityRepository<T>
          where T : class
    {

        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);
        void CommitChanges();
        void DeleteOnCommit(T entity);
        T Get(int key);
        IQueryable<T> GetAll();
        T InsertOnCommit(T entity);
    }
}
