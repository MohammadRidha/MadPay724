using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MadPay724.Data.Infrastructure
{
    public class Repository<TEnity> : IRepository<TEnity>, IDisposable where TEnity : class
    {

        private readonly DbContext _db;
        private readonly DbSet<TEnity> _dbSet;

        #region ctor

        public Repository(DbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEnity>();
        }
        #endregion

        #region normal
        public void Insert(TEnity entity)
        {
            _db.Add(entity);
        }
        public void Update(TEnity entity)
        {

            if (entity == null)
                throw new ArgumentException("there is no entity");
            _dbSet.Update(entity);
        }
        public void Delete(object id)
        {
            var entity = GetById(id);
            if (entity == null)
                throw new ArgumentException("there is no entity");
            _dbSet.Remove(entity);
        }
        public void Delete(TEnity entity)
        {
            _dbSet.Remove(entity);
        }
        public void Delete(Expression<Func<TEnity, bool>> where)
        {
            IEnumerable<TEnity> objs = _dbSet.Where(where).AsEnumerable();
            foreach (TEnity item in objs)
            {
                _dbSet.Remove(item);
            }
        }
        public TEnity GetById(object id)
        {
            return _dbSet.Find(id);
        }
        public TEnity Get(Expression<Func<TEnity, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault();
        }
        public IEnumerable<TEnity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }
        public IEnumerable<TEnity> GetMany(Expression<Func<TEnity, bool>> where)
        {
            return _dbSet.Where(where).AsEnumerable();
        }

        #endregion

        #region async
        public async Task InserAsync(TEnity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<TEnity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEnity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEnity> GetAsync(Expression<Func<TEnity, bool>> where)
        {
            return await _dbSet.Where(where).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEnity>> GetManyAsync(Expression<Func<TEnity, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();
        }


        #endregion

        #region dispose
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            Dispose(false);
        }

        #endregion
    }
}
