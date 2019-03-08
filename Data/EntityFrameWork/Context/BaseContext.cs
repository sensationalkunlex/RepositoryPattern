using Data.Database.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;

namespace Data.Context
{
    public class BaseContext : DbContext, IDbContext
    {
        private IDbTransaction _transaction;

        public BaseContext() : this("DefaultConnection")
        {

        }

        public BaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }


        void IDbContext.BeginTransaction()
        {
            Configuration.AutoDetectChangesEnabled = false;
            if (Database.Connection.State != ConnectionState.Open)
                Database.Connection.Open();

            _transaction = Database.BeginTransaction().UnderlyingTransaction;
        }

        int IDbContext.Commit()
        {
            ChangeTracker.DetectChanges();
            var result = SaveChanges();
            _transaction.Commit();

            return result;
        }

        void IDbContext.Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return Database.SqlQuery<T>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable<dynamic> DynamicListFromSql(string Sql, params object[] parameters)
        {
            return Database.DynamicListFromSql(Sql, parameters);
        }

        public IDataReader SqlToReader(string Sql, params object[] parameters)
        {
            return Database.SqlToReader(Sql, parameters);
        }

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                InsertDateTrackDetails(entry.Entity, entry.State == EntityState.Added);
                InsertUserTrackDetails(entry.Entity, entry.State == EntityState.Added);
            }
            return base.SaveChanges();
        }


        #region private fields

        private void InsertDateTrackDetails(object entity, bool IsCreated)
        {
            //var datenow = CommonHelper.GetCurrentDate();
            //var entityToAdd = entity as IDateTrackable;
            //if (entityToAdd != null)
            //{
            //    if (IsCreated)
            //        entityToAdd.CreatedDate = datenow;

            //    entityToAdd.ModifiedDate = datenow;
            //}
        }

        private void InsertUserTrackDetails(object entity, bool isCreated)
        {

           // var entityToAdd = entity as IUserTrackable;

            //if (entityToAdd == null)
            //    return;

            //if (entityToAdd.CreatedById == 0 || entityToAdd.LastModifiedById == null)
            //{
            //    if (entityToAdd != null)
            //    {
            //        var userId = CommonHelper.CurrentUserId();
            //        var _userId = userId ?? 0;

            //        if (isCreated && entityToAdd.CreatedById == 0)
            //        {
            //            if (_userId == 0)
            //                throw new DbEntityValidationException("You are not authorized to perform this action");

            //            entityToAdd.CreatedById = _userId;
            //        }

            //        if (_userId != 0)
            //            entityToAdd.LastModifiedById = _userId;

            //    }
            //}

        }
        #endregion
    }


}
