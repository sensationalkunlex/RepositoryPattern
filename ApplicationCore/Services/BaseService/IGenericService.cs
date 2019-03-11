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
    public interface IGenericService<TEntity> : IDisposable where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        void Update(params TEntity[] entities);




        int Total(Expression<Func<TEntity, bool>> predicate);

        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool track = false);
        List<TEntity> GetAll(bool track = false);
        TEntity GetById(int id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, bool track = false);
        IEnumerable<TEntity> ExecuteProcedure(string procedure, params object[] @params);
        TEntity GetIncluding(Expression<Func<TEntity, bool>> predicate, bool track = false, params Expression<Func<TEntity, object>>[] properties);
        IQueryable<TEntity> GetAllIncluding(Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties);
        //IQueryable<TEntity> IncludeFilter(Expression<Func<TEntity, object>>[] predicate);
        IEnumerable<dynamic> DynamicListFromSql(string Sql, params object[] parameters);

        IDataReader SqlToReader(string Sql, params object[] parameters);
    }

}
