using Microsoft.EntityFrameworkCore;
using POS.Domain.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace POS.Repositories.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly POSDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(POSDbContext dbContext)
        {
            this._dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public void Create(T entity)
        {
            _dbContext.Add<T>(entity);
        }

        public void Create(IList<T> entities)
        {
            foreach(var entity in entities)
            {
                _dbContext.Add<T>(entity);
            }
        }

        public void Delete(T entity)
        {
            _dbContext.Update<T>(entity);
        }

        public void Delete(T entity, bool isHardDeleted)
        {
            _dbContext.Remove<T>(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking().Where(expression).AsEnumerable();
        }

        public IEnumerable<T> GetBy(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AsNoTracking().Where(expression).AsEnumerable();
        }

        public void Update(T entity)
        {
            _dbContext.Update<T>(entity);
        }
    }
}
