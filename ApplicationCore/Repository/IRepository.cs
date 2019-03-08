using ApplicationCore.Entities;
using ApplicationCore.UnitofWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repository
{
    public interface IRepository<TEntity> :IDisposable
    {
        void Create( TEntity entity );

        void Create( IEnumerable<TEntity> entities );
        void Update( params TEntity[] entities );
        void Delete( TEntity entity );

        void Delete( IEnumerable<TEntity> entities );

        TEntity Get( int id );

        TEntity Get( Expression<Func<TEntity, bool>> predicate, bool track = false );

        IQueryable<TEntity> GetAllIncluding( Expression<Func<TEntity, bool>> predicate, bool track, params Expression<Func<TEntity, object>>[] properties );

        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

        int Count( Expression<Func<TEntity, bool>> predicate );
        IEnumerable<TEntity> Fetch( Expression<Func<TEntity, bool>> predicate, bool track = false );
        IEnumerable<TEntity> Fetch( bool track = false );
        IEnumerable<TEntity> SqlQuery( string sql, params object[] parameters );

        // IQueryable<TEntity> IncludeFilter( params Expression<Func<TEntity, object>>[] predicate );
        IEnumerable<dynamic> DynamicListFromSql(string Sql, params object[] parameters);

        IDataReader SqlToReader(string Sql, params object[] parameters);
    }
}
