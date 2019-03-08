using ApplicationCore.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        void BeginTransaction();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        int Commit();
        void Rollback();
  
    }
}
