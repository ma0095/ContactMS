using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ContactMS.Framework.Data;
using ContactMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.FrameWork
{


    public class Repository<TContract, TEntity> : IRepository<TContract>
        where TContract : IEntity
         where TEntity : class, TContract
    {

        private IQueryable<TEntity>? _queryable = null;


        private readonly DbContext _dbContext;


        private readonly DbSet<TEntity> _dbSet;


        private static readonly object LockObject = new();


        private static readonly List<Expression<Func<TEntity, object>>> navigationproperties
          = new();


        protected readonly ILogger _logger;


        public Repository(DbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            @_queryable = _dbSet;
            _logger = logger;
        }

        private IQueryable<TContract> GetQuerable()
        {
            return @_queryable;
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }


        public TContract Add(TContract entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.EditedDate = DateTime.UtcNow;
            _dbContext.Entry((TEntity)entity).State = EntityState.Added;
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> entityItem = _dbSet.Add((TEntity)entity);
            return entityItem.Entity;
        }

        public void Update(TContract entity)
        {
            entity.EditedDate = DateTime.UtcNow;
            //_dbSet.Attach((TEntity)entity);
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> entry = _dbContext.Entry((TEntity)entity);
            entry.State = EntityState.Modified;
            entry.Property(x => x.CreatedDate).IsModified = false;
        }

        public void Insert(IEnumerable<TContract> entities)
        {
            lock (LockObject)
            {
                IEnumerable<TEntity> items = entities.Cast<TEntity>();
                foreach (TEntity item in items)
                {
                    item.CreatedDate = DateTime.UtcNow;
                    item.EditedDate = DateTime.UtcNow;
                }
                _dbSet.AddRange(items);
            }
        }


        public void Delete(TContract entity)
        {
            if (_dbContext.Entry((TEntity)entity).State == EntityState.Detached)
            {
                _ = _dbSet.Attach((TEntity)entity);
            }

            _ = _dbSet.Remove((TEntity)entity);
        }


        public void DeleteAll(IEnumerable<TContract> entity)
        {
            lock (LockObject)
            {
                IEnumerable<TEntity> items = entity.Cast<TEntity>();
                foreach (TEntity item in items)
                {
                    if (_dbContext.Entry(item).State == EntityState.Detached)
                    {
                        _ = _dbSet.Attach(item);
                    }
                    _ = _dbSet.Remove(item);
                }
            }
        }


        public async Task<IEnumerable<TContract>> GetAllAsync()
        {
            IEnumerable<TContract> items = await _dbSet.ToListAsync();
            return items;
        }
        public async Task<IEnumerable<TContract>> GetAllAsync(Expression<Func<TContract, bool>> condition)
        {
            IEnumerable<TContract> items = await _dbSet.AsNoTracking().Where(condition).ToListAsync();
            return items;
        }


        public async Task<TContract> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }


        public IQueryable<TContract> Entities
        {
            get
            {
                IQueryable<TContract> entities = GetQuerable()
                    .AsQueryable<TContract>();
                return entities;
            }
        }


        public void Include(Expression<Func<TEntity, object>> navigationProperty)
        {
            _queryable = _queryable.Include(navigationProperty);
        }

        public async Task UpdateAsync(Expression<Func<TContract, bool>> condition, Action<TContract> updation)
        {
            await _dbSet.Where(condition)
                .Select(x => x).ForEachAsync(updation);
        }
        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _dbContext.Dispose();
                }

                //// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                //// TODO: set large fields to null.

                disposedValue = true;
            }
        }


        public void Dispose()
        {
            //// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            //// TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
