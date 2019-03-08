
using ApplicationCore.Entities.Interfaces;
using ApplicationCore.Repository;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Data.EntityFrameWork
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        
        private readonly IDbContext _context;

        private bool _disposed;

     

        public Repository(IDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("Context cannot be null");
        }
        
        protected string GetFullErrorText(System.Data.Entity.Validation.DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
        }

        protected virtual IEnumerable<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return _context.SqlQuery<TEntity>(sql, parameters);
        }

        protected virtual IDbContext Context
        {
            get { return _context; }
        }

        protected virtual IDbSet<TEntity> Entities
        {
            get { return Context.Set<TEntity>(); }
        }

        protected virtual void Create(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                //InsertDateTrackDetails(entity, true);
                //InsertUserTrackDetails(entity, true);

                Entities.Add(entity);
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        protected virtual void Create(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entity");

                foreach (var entity in entities)
                {
                    //InsertDateTrackDetails(entity, true);
                    //InsertUserTrackDetails(entity, true);
                    Entities.Add(entity);
                }

                _context.SaveChanges();

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        protected virtual void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                //InsertDateTrackDetails(entity, false);
                //InsertUserTrackDetails(entity, false);

                var deletable = entity as ISoftDeletable;
                if (deletable != null)
                {
                    deletable.IsDeleted = true;
                    Update(entity);
                    return;
                }


                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        protected virtual void Delete(IEnumerable<TEntity> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                foreach (var entity in entities)
                {
                    var deletable = entity as ISoftDeletable;
                    //InsertDateTrackDetails(entity, false);
                    //InsertUserTrackDetails(entity, false);

                    if (deletable != null)
                    {
                        deletable.IsDeleted = true;
                        Update(entity);
                        return;
                    }
                    Entities.Remove(entity);
                }


                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }


        protected virtual void Update(params TEntity[] entities)
        {
            try
            {
                if (entities == null || !entities.Any())
                    throw new ArgumentNullException("entities");

                //foreach (var entity in entities)
                //{
                //    InsertDateTrackDetails(entity, false);
                //    InsertUserTrackDetails(entity, false);
                //}

                _context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        protected virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Fetch(predicate)
                     .AsNoTracking()
                          .Count();
        }

        protected virtual int Count()
        {
            return TableNoTracking.Count();
        }

        protected virtual TEntity Get(object id)
        {
            return Entities.Find(id);
        }

        protected virtual TEntity Get(Expression<Func<TEntity, bool>> predicate, bool track = false)
        {
            return
                track ? Fetch(predicate).SingleOrDefault() :
                    Fetch(predicate).AsNoTracking()
                                    .SingleOrDefault();
        }

        protected virtual IQueryable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        protected virtual IQueryable<TEntity> Fetch()
        {
            return Entities;
        }

        IQueryable<TEntity> IRepository<TEntity>.Table
        {
            get
            {
                return Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        protected virtual IQueryable<TEntity> TableNoTracking
        {
            get
            {
                return Entities.AsNoTracking();
            }
        }

        IQueryable<TEntity> IRepository<TEntity>.TableNoTracking => TableNoTracking;

        protected virtual IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties)
        {
            var entities = IncludeProperties(properties);

            return
                track ? entities.Where(predicate) :
                    entities.Where(predicate).AsNoTracking();
        }

       
        int IRepository<TEntity>.Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Count(predicate);
        }

        void IRepository<TEntity>.Create(TEntity entity)
        {
            Create(entity);
        }

        void IRepository<TEntity>.Create(IEnumerable<TEntity> entities)
        {
            Create(entities);
        }

        void IRepository<TEntity>.Delete(TEntity entity)
        {
            Delete(entity);
        }

        IEnumerable<TEntity> IRepository<TEntity>.Fetch(Expression<Func<TEntity, bool>> predicate, bool track)
        {
            return
                track ?
                Fetch(predicate)
                                .ToList()
                                     .AsReadOnly()
               : Fetch(predicate)
                              .AsNoTracking()
                                  .ToList()
                                     .AsReadOnly();
        }

        TEntity IRepository<TEntity>.Get(Expression<Func<TEntity, bool>> predicate, bool track)
        {
            return Get(predicate, track);
        }

        TEntity IRepository<TEntity>.Get(int id)
        {
            return Get(id);
        }

        void IRepository<TEntity>.Update(params TEntity[] entities)
        {
            Update(entities);
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
        IEnumerable<TEntity> IRepository<TEntity>.SqlQuery(string sql, params object[] parameters)
        {
            return SqlQuery(sql, parameters);
        }

        protected IEnumerable<TEntity> Fetch(bool track)
        {
            return
                track ?
                Fetch()
                      .ToList()
                       .AsReadOnly()
               : Fetch()
                        .AsNoTracking()
                         .ToList()
                          .AsReadOnly();
        }

        IQueryable<TEntity> IRepository<TEntity>.GetAllIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties)
        {
            return GetAllIncluding(predicate, track, properties);
        }

        protected IDbContext GetContext()
        {
            return _context;
        }

        //IQueryable<TEntity> IRepository<TEntity>.IncludeFilter(params Expression<Func<TEntity, object>>[] predicate)
        //{
        //    return IncludeFilters(predicate);
        //}

        protected virtual IQueryable<TEntity> IncludeFilters(Expression<Func<TEntity, object>>[] predicate)
        {
            IQueryable<TEntity> entities = Entities;
            foreach (var exp in predicate)
            {
                entities = entities.IncludeOptimized(exp);
            }
            return entities;
        }
        

        void IRepository<TEntity>.Delete(IEnumerable<TEntity> entities)
        {
            Delete(entities);
        }


        IEnumerable<TEntity> IRepository<TEntity>.Fetch(bool track)
        {
            return Fetch(track);
        }

        public IEnumerable<dynamic> DynamicListFromSql(string Sql, params object[] parameters)
        {
            return _context.DynamicListFromSql(Sql, parameters);
        }

        public IDataReader SqlToReader(string Sql, params object[] parameters)
        {
            return _context.SqlToReader(Sql, parameters);
        }

        #region private fields

        private IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = Entities;
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.IncludeOptimized(includeProperty);
            }
            return entities;
        }


        //private void InsertDateTrackDetails(TEntity entity, bool IsCreated)
        //{
        //    var datenow = Helper.GetCurrentDate();
        //    var entityToAdd = entity as IDateTrackable;
        //    if (entityToAdd != null)
        //    {
        //        if (IsCreated)
        //            entityToAdd.CreatedDate = datenow;

        //        entityToAdd.ModifiedDate = datenow;
        //    }
        //}

        //private void InsertUserTrackDetails(TEntity entity, bool isCreated)
        //{

        //    var entityToAdd = entity as IUserTrackable;
        //    if (entityToAdd != null)
        //    {
        //        var _userId = Convert.ToInt32(Helper.CurrentUserId() ?? 1);
        //        if (_userId == 0)
        //            throw new EntityNullException("You are not authorized to perform this action");

        //        if (isCreated)
        //            entityToAdd.CreatedById = _userId;

        //        entityToAdd.LastModifiedById = _userId;

        //    }


        //}
        #endregion




    }
}
