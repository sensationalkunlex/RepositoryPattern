using ApplicationCore.Repository;
using ApplicationCore.UnitofWork;
using Data.Context;
using Data.EntityFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Data.EntityFrameWork.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly IDbContext _context;
        private bool _disposed;
        private Hashtable _repositories;

        public UnitOfWork(IDbContext context)
        {
            _context = context;
        }
        public void BeginTransaction()
        {
            _context.BeginTransaction();
        }
        public int Commit()
        {
            return _context.Commit();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }
            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }
            var repositoryType = typeof(Repository<>);
            if (!_repositories.Contains(type))
                _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));
            return (IRepository<TEntity>)_repositories[type];
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();

                if (_repositories != null)
                {
                    foreach (IDisposable repository in _repositories.Values)
                        repository.Dispose();// dispose all repositries
                }
            }
            _disposed = true;
        }
    }
}
