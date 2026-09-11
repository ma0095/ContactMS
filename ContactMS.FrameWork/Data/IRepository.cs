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
        /// <summary>
        /// Gets the entities.
        /// </summary> 
        IQueryable<TEntity> Entities { get; }
        /// <summary>
        /// Gets all asynchronous.
        /// </summary> 
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary> 
        Task<TEntity> GetByIdAsync(int id);
        /// <summary>
        /// Adds the specified entity.
        /// </summary> 
        TEntity Add(TEntity entity);
        /// <summary>
        /// Inserts the specified entities.
        /// </summary> 
        void Insert(IEnumerable<TEntity> entities);
        /// <summary>
        /// Updates the specified entity.
        /// </summary> 
        void Update(TEntity entity);
        /// <summary>
        /// Updates the specified condition.
        /// </summary> 
        Task UpdateAsync(Expression<Func<TEntity, bool>> condition, Action<TEntity> updation);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary> 
        void Delete(TEntity entity);
        /// <summary>
        /// Deletes all.
        /// </summary> 
        void DeleteAll(IEnumerable<TEntity> entities);
    }
}
