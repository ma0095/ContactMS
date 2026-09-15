using ContactMS.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMS.Framework.Data.Services
{

    public abstract class BaseDataService : IDataService
    {
        public IUnitOfWork UnitOfWork;

        public BaseDataService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                UnitOfWork.Dispose();
            }

            _disposed = true;
        }
    }
}
