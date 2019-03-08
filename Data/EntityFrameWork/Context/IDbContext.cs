using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Text;

namespace Data.Context
{
    public interface IDbContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        void BeginTransaction();
        int Commit();
        void Rollback();
        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, params object[] parameters);

        IDataReader SqlToReader(string Sql, params object[] parameters);
        IEnumerable<dynamic> DynamicListFromSql(string Sql, params object[] parameters);
    }
}
