using ContactMS.Framework.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Framework.Mappers
{

    
    public abstract class APIDataMapper<TEntity, TObject> where TEntity : IEntity where TObject : class, new()
    {
        #region Private Members

        protected IServiceProvider Services { get; set; }

        #endregion

        #region Constructor


        protected APIDataMapper(IServiceProvider Services)
        {
            this.Services = Services;
        }

        #endregion


        public abstract TObject ToObject(TEntity? entity);


        public abstract TEntity ToEntity(TObject value);

        protected TEntity? CreateEntity()
        {
            TEntity? entity = (TEntity?)Services.GetRequiredService(typeof(TEntity));
            return entity;
        }


        public IEnumerable<TObject> ToObjects(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                yield return ToObject(entity);
            }
        }


        public IEnumerable<TEntity> ToEntities(IEnumerable<TObject> items)
        {
            foreach (TObject item in items)
            {
                yield return ToEntity(item);
            }
        }
    }
}
