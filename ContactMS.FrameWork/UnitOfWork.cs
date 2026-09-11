using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using ContactMS.Framework.Data;
using ContactMS.Framework.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.FrameWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members        


        private readonly DbContext _dbContext;


        private IServiceProvider serviceProvider { get; set; }

        private readonly ILogger _logger;

        private IDbContextTransaction? _transaction = null;
        #endregion

        #region Constructor        

        public UnitOfWork(DbContext dbContext, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger("logs");
        }
        #endregion

        #region Public Methods

        public IEnumerable<TEntity> Exec<TEntity>(string query, params object[] parameters)
        {
            FormattableString sql = FormattableStringFactory.Create(query, parameters);
            List<TEntity> entities = _dbContext.Database.SqlQuery<TEntity>(sql).ToList();
            return entities.Select(i => i).AsEnumerable();
        }


        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }


        public int Commit()
        {
            lock (_lock)
            {
                try
                {
                    int result = _dbContext.SaveChanges();
                    _transaction.Commit();
                    return result;
                }
                catch
                {
                    _transaction.Rollback();
                    return 0;
                }
                finally
                {

                }
            }
        }


        private static readonly object _lock = new();


        public async Task<int> CommitAsync()
        {
            //lock (_lock)
            //{
            try
            {
                int result = await _dbContext.SaveChangesAsync();
                return result;
            }
            finally
            {
                //_dbContext.ChangeTracker.Entries()
                //    .ToList()
                //    .ForEach(x => x.State = EntityState.Detached);
            }
            //  }
        }

        public int CommitTransaction()
        {
            lock (_lock)
            {
                try
                {
                    int result = _dbContext.SaveChangesAsync().Result;
                    _transaction.Commit();
                    return result;
                }
                finally
                {
                    //_dbContext.ChangeTracker.Entries()
                    //    .ToList()
                    //    .ForEach(x => x.State = EntityState.Detached);
                }
            }
        }


        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            object? instance = serviceProvider.GetService(typeof(TEntity));
            Type instanceType = instance.GetType();
            MethodInfo setMethod = GetType().GetTypeInfo()
                        .GetMethod("CreateRepository").MakeGenericMethod(typeof(TEntity), instanceType);
            IRepository<TEntity>? repository = (IRepository<TEntity>)setMethod.Invoke(this, new object[] { });
            //Repositories[keyType] = repository;
            return repository;
        }


        public virtual object CreateRepository<TContract, TEntity>()
          where TContract : IEntity
          where TEntity : class, TContract
        {
            Repository<TContract, TEntity> repository = new(_dbContext, _logger);
            return repository;
        }


        public void Rollback()
        {
            _transaction.Rollback();
        }
        #endregion

        #region IDisposable Support        

        private bool disposedValue = false; // To detect redundant calls

        public virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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
