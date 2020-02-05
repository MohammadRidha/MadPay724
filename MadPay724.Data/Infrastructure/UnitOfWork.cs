using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay724.Data.Infrastructure
{
    // In Syntext pishnahadi khode "Microsoft"
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        #region ctor

        protected readonly DbContext _db;
        public UnitOfWork()
        {
            _db = new TContext();
        }
        #endregion

        #region Save
        public void Save()
        {
            _db.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _db.SaveChangesAsync();
        }
        #endregion

        #region Dispose

        private bool disposed = false;
        public virtual  void Dispose(bool desposing)
        {
            if (!disposed)
            {
                if (desposing)
                {
                    _db.Dispose();
                }
            }

            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);

            //in GC cleanUp mikone vasamon
            GC.SuppressFinalize(this);
        }

        //mokhareb ctor
         ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion

    }
}
