using MadPay724.Repo.Repositories.Interface;
using MadPay724.Repo.Repositories.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay724.Repo.Infrastructure
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

        #region PrivateRepository

        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(_db);
                }
                return userRepository;
            }
        }
        #endregion


        #region Save
        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return  await _db.SaveChangesAsync();
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
