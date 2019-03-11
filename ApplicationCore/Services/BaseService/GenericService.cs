using ApplicationCore.Repository;
using ApplicationCore.UnitofWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.BaseService.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {

        private readonly IRepository<TEntity> _repository;
        public IUnitOfWork UnitOfWork { get; private set; }

        private bool _disposed;



        public GenericService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.Repository<TEntity>();
        }


        int IGenericService<TEntity>.Total(Expression<Func<TEntity, bool>> predicate)
        {

            return Count(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            _repository.Create(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            _repository.Create(entities);
        }

        public virtual void Delete(TEntity entity)
        {

            _repository.Delete(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {

            _repository.Delete(entities);
        }

        public virtual void Update(params TEntity[] entities)
        {
            _repository.Update(entities);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, bool track)
        {
            return _repository.Get(predicate, track);
        }

        public virtual TEntity Find(int id)
        {
            return _repository.Get(id);
        }

        public virtual List<TEntity> All(Expression<Func<TEntity, bool>> predicate, bool track)
        {

            return
                _repository.Fetch(predicate, track).ToList();
        }

        public virtual List<TEntity> All(bool track)
        {
            return
                _repository.Fetch(track).ToList();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {

            return _repository.Count(predicate);
        }

        public virtual TEntity GetIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties)
        {
            return _repository.GetAllIncluding(predicate, track, properties).FirstOrDefault();
        }

        public virtual IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties)
        {
            return _repository.GetAllIncluding(predicate, track, properties);
        }

        void IGenericService<TEntity>.Add(TEntity entity)
        {
            Add(entity);
        }

        void IGenericService<TEntity>.Add(IEnumerable<TEntity> entities)
        {
            Add(entities);
        }

        void IGenericService<TEntity>.Delete(IEnumerable<TEntity> entities)
        {
            Delete(entities);
        }

        List<TEntity> IGenericService<TEntity>.GetAll(Expression<Func<TEntity, bool>> predicate, bool track)
        {
            return All(predicate, track);
        }
        List<TEntity> IGenericService<TEntity>.GetAll(bool track)
        {
            return All(track);
        }
        TEntity IGenericService<TEntity>.GetById(int id)
        {
            return Find(id);
        }

        TEntity IGenericService<TEntity>.Get(Expression<Func<TEntity, bool>> predicate, bool track)
        {
            return Find(predicate, track);
        }

        void IGenericService<TEntity>.Update(params TEntity[] entities)
        {
            Update(entities);
        }

        public virtual IEnumerable<T> ExecuteProcedure<T>(string procedureName, params object[] extraQueries) where T : class
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("Exec {0} ", procedureName));

            var parameters = new List<object> { };

            foreach (var item in extraQueries)
                parameters.Add(item);

            var counter = parameters.Count();
            for (int i = 0; i < counter; i++)
            {
                sb.Append("@p" + i);
                if (i < counter - 1)
                    sb.Append(", ");
            }

            return UnitOfWork.Repository<T>().SqlQuery(sb.ToString(), parameters.ToArray());
        }

        IEnumerable<TEntity> IGenericService<TEntity>.ExecuteProcedure(string procedure, params object[] @params)
        {
            return ExecuteProcedure<TEntity>(procedure, @params);
        }

        public IEnumerable<dynamic> DynamicListFromSql(string Sql, params object[] parameters)
        {
            return _repository.DynamicListFromSql(Sql, parameters);
        }

        public IDataReader SqlToReader(string Sql, params object[] parameters)
        {
            return _repository.SqlToReader(Sql, parameters);
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
                UnitOfWork.Dispose();
            }
            _disposed = true;
        }

        TEntity IGenericService<TEntity>.GetIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties)
        {
            return GetIncluding(predicate, track, properties);
        }

        IQueryable<TEntity> IGenericService<TEntity>.GetAllIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties)
        {
            return GetAllIncluding(predicate, track, properties);
        }

        //public IQueryable<TEntity> IncludeFilter(params Expression<Func<TEntity, object>>[] predicate)
        //{
        //    return _repository.IncludeFilter(predicate);
        //}
    }
}
