using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POS.Repositories.Common
{
    public interface IBaseRepository<T> where T:class
    {
        void Create(T entity);
        void Create(IList<T> entities);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetBy(Expression<Func<T, bool>> expression);
        void Update(T entity);            
        void Delete(T entity);        
        void Delete(T entity, bool isHardDeleted);
        void DeleteRange(IEnumerable<T> entity);
    }
}
