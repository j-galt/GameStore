using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        T Get(Expression<Func<T, bool>> predicate, 
            params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetAll();

        void Create(T entity);
        T Edit(int id, T updatedEntity);

        void Delete(T entity);
    }
}
