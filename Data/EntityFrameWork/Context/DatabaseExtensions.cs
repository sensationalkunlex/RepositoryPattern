using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Data.Database.Extensions
{
    public static class DatabaseExtensions
    {
        public static IDataReader SqlToReader(this System.Data.Entity.Database database, string sql, params object[] parameters)
        {
            using (var command = database.Connection.CreateCommand())
            {

                command.CommandText = sql;

                if (null != parameters
                && parameters.Length > 0)
                {
                    var dbParameters = new DbParameter[parameters.Length];

                    if (parameters.All(p => p is DbParameter))
                    {
                        for (var i = 0; i < parameters.Length; i++)
                        {
                            dbParameters[i] = (DbParameter)parameters[i];
                        }
                    }
                    else if (!parameters.Any(p => p is DbParameter))
                    {

                        var sb = new StringBuilder(sql).Append(" ");

                        var length = parameters.Length;
                        for (int i = 0; i < length; i++)
                        {
                            sb.Append("@p" + i);
                            if (i < length - 1)
                                sb.Append(", ");

                            dbParameters[i] = command.CreateParameter();
                            dbParameters[i].ParameterName = string.Format("p{0}", i);
                            dbParameters[i].Value = parameters[i] ?? DBNull.Value;
                        }

                        command.CommandText = sb.ToString();
                    }
                    else
                    {
                        throw new InvalidOperationException("couldn't mix dbparameter and other objects");
                    }

                    command.Parameters.AddRange(dbParameters);
                }

                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                var dataReader = command.ExecuteReader();

                return dataReader;
            }
        }

        //public static DataTable ExecuteDbCommand<T>(this System.Data.Entity.Database database, string sql, params object[] parameters)
        //{
        //    var response = new List<T>();

        //    using (var cmd = database.Connection.CreateCommand()) {

        //        var properties = typeof(T).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);

        //        cmd.CommandText = sql;
        //        cmd.Parameters.AddRange(parameters);

        //        if (cmd.Connection.State != ConnectionState.Open) {
        //            cmd.Connection.Open();
        //        }

        //        using (var dataReader = cmd.ExecuteReader()) {

        //            while (dataReader.Read()) {
        //                var row = new Dictionary<string, object>();

        //                for (var i = 0; i < dataReader.FieldCount; i++) {
        //                    var type = dataReader.GetFieldType(i);

        //                    row.Add(dataReader.GetName(i), TypeValueGetter(() => {
        //                        var val = dataReader.GetValue(i);
        //                        return val == DBNull.Value ? null : val;
        //                    }));
        //                }

        //                var instance = Activator.CreateInstance<T>();

        //                foreach (var column in row.Keys) {
        //                    foreach (var property in properties) {
        //                        if (column == property.Name)
        //                            property.SetValue(instance, row[column]);
        //                    }
        //                }

        //                response.Add(instance);
        //            }
        //        }
        //    }
        //    return response;
        //}

   
        public static IEnumerable<dynamic> DynamicListFromSql(this System.Data.Entity.Database Database, string Sql, params object[] parameters)
        {
            using (var cmd = Database.Connection.CreateCommand())
            {
                cmd.CommandText = Sql;
                if (cmd.Connection.State != ConnectionState.Open) { cmd.Connection.Open(); }

                if (null != parameters
                && parameters.Length > 0)
                {
                    var dbParameters = new DbParameter[parameters.Length];

                    if (parameters.All(p => p is DbParameter))
                    {
                        for (var i = 0; i < parameters.Length; i++)
                        {
                            dbParameters[i] = (DbParameter)parameters[i];
                        }
                    }
                    else if (!parameters.Any(p => p is DbParameter))
                    {

                        var sb = new StringBuilder(Sql).Append(" ");

                        var length = parameters.Length;
                        for (int i = 0; i < length; i++)
                        {
                            sb.Append("@p" + i);
                            if (i < length - 1)
                                sb.Append(", ");

                            dbParameters[i] = cmd.CreateParameter();
                            dbParameters[i].ParameterName = string.Format("p{0}", i);
                            dbParameters[i].Value = parameters[i] ?? DBNull.Value;
                        }

                        cmd.CommandText = sb.ToString();
                        cmd.CommandTimeout = 180;
                    }
                    else
                    {
                        throw new InvalidOperationException("couldn't mix dbparameter and other objects");
                    }

                    cmd.Parameters.AddRange(dbParameters);
                }

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var row = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            row.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
                        }
                        yield return row;
                    }
                }
            }
        }
    }
}