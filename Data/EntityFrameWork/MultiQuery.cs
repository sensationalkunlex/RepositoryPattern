//using Common.Utilities;
//using Data.Context;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.Core.Objects;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Z.EntityFramework.Plus;
//namespace Data.EntityFrameWork
//{
//    public class MultiQuery
//    {

//        public static class DefferedQueries: QueryDeferredExtensions
//        {

//        }
//        public class QueryRecord<T>
//        {
//            public BaseQueryFuture FutureQuery { get; set; }
//            public Type Type { get; set; }
//            public static BaseQueryFuture Create(IQueryable<T> query)
//            {
//                var result = new QueryRecord<T>();
//                result.Type = typeof(T);

//                if (result.Type.IsNumericType())
//                    result.FutureQuery = query.DeferredCount().FutureValue();
//                else
//                     result.Query.Future();

//                return new QueryRecord<T>() { Query = query, };
//            }

//            public BaseQueryFuture GetFuture()
//            {
              
//            }

//        }

//        #region Fields
//        private readonly IDbContext _context;
//        //List<QueryRecord<T> _queries;
//        #endregion

//        #region Constructors
//        public MultiQuery(IDbContext context)
//        {
//            _queries = new List<QueryRecord>();
//            _context = context;
//        }
//        #endregion

//        #region Public Methods
//        public MultiQuery Add<T>(IQueryable<T> query)
//        {
//            if (query == null)
//                throw new ArgumentNullException("query");
//            _queries.Add(QueryRecord.Create(query));
//            return this;
//        }

//        public static IEnumerable<object> ProcessQuery(this MultiQuery query)
//        {
//            foreach (var queryObj in query._queries)
//            {
//                if (queryObj.Type.IsNumericType())

//                    result.Query = queryObj.Query.FutureValue();
//            }
//        }

//        #endregion
//    }
//}
