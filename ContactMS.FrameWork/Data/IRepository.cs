using ContactMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Framework.Data
{

    public interface IRepository<TEntity> : IDisposable where TEntity : IEntity
    {
        IQueryable<TEntity> Entities { get; }
        //Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> condition);
        //Task<TEntity> GetByIdAsync(int id);
        TEntity Add(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        //Task UpdateAsync(Expression<Func<TEntity, bool>> condition, Action<TEntity> updation);
        //void Delete(TEntity entity);
        //void DeleteAll(IEnumerable<TEntity> entities);
    }
}
