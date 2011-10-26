using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace EBXDashboardsModel.Infra.Repositories {
    public class NHibSession
    : IDisposable {
        private ISession InnerSession { get; set; }
        private ITransaction Transaction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibSession"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public NHibSession(ISession session) {
            InnerSession = session;
            InnerSession.FlushMode = FlushMode.Commit;
            SetupNewTransaction();
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if (disposing) {
                if (Transaction != null) {
                    Transaction.Rollback();
                    Transaction.Dispose();
                    Transaction = null;
                }

                if (InnerSession != null) {
                    InnerSession.Dispose(); //InnerSession.Close(); Do not use! It breaks when outer transactionscopes are active! Should only use Dispose.
                    InnerSession = null;
                }
            }
        }

        ~NHibSession() {
            Dispose(false);
        }

        public void Commit() {
            Transaction.Commit();
            SetupNewTransaction();
        }

        public void Rollback() {
            Transaction.Rollback();
            SetupNewTransaction();
        }

        private void SetupNewTransaction() {
            if (Transaction != null)
                Transaction.Dispose();

            Transaction = InnerSession.BeginTransaction();
        }

        /// <summary>
        /// Creates and returns a Query.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <returns></returns>
        public IQuery CreateQuery(string queryString) {
            return InnerSession.CreateQuery(queryString);
        }

        /// <summary>
        /// Creates and returns a Criteria.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ICriteria CreateCriteria<T>()
            where T : class {
            return InnerSession.CreateCriteria<T>();
        }

        /// <summary>
        /// Gets an item that matches sent expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public T GetItemBy<T>(Expression<Func<T, bool>> query) {
            return InnerSession.Linq<T>().SingleOrDefault(query);
        }

        /// <summary>
        /// Returns item via Id.
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public TReturn GetItemById<TReturn, TId>(TId id) {
            return InnerSession.Get<TReturn>(id);
        }

        /// <summary>
        /// Returns item via NHibernate Criterions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criterions"></param>
        /// <returns></returns>
        public T GetItemByCriterions<T>(params ICriterion[] criterions) {
            return AddCriterions(InnerSession.CreateCriteria(typeof(T)), criterions).UniqueResult<T>();
        }

        /// <summary>
        /// Returns a list of ALL items.
        /// </summary>
        /// <remarks>ALL items are returned.</remarks>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetList<T>() {
            return GetListByCriterions<T>(null);
        }

        /// <summary>
        /// Returns a list of items matching sent expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IList<T> GetListBy<T>(Expression<Func<T, bool>> query = null) {
            return InnerSession.Linq<T>().Where(query).ToList();
        }

        /// <summary>
        /// Returns list of item matching sent criterions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criterions"></param>
        /// <returns></returns>
        public IList<T> GetListByCriterions<T>(params ICriterion[] criterions) {
            ICriteria criteria = AddCriterions(InnerSession.CreateCriteria(typeof(T)), criterions);
            IList<T> result = criteria.List<T>();

            return result ?? new List<T>(0);
        }

        /// <summary>
        /// Deletes sent item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Delete<T>(T obj) {
            InnerSession.Delete(obj);
        }

        /// <summary>
        /// Deletes sent item by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        public void DeleteById<T, TId>(TId id) {
            Delete(GetItemById<T, TId>(id));
        }

        /// <summary>
        /// Deletes by query.
        /// </summary>
        /// <param name="query"></param>
        public void DeleteByQuery(string query) {
            InnerSession.Delete(query);
        }

        /// <summary>
        /// Inserts sent item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Insert<T>(T obj) {
            InnerSession.Save(obj);
        }

        /// <summary>
        /// Updates sent item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Update<T>(T obj) {
            InnerSession.Update(obj);
        }

        private static ICriteria AddCriterions(ICriteria criteria, ICriterion[] criterions) {
            if (criterions != null)
                for (int c = 0; c < criterions.Length; c++)
                    criteria = criteria.Add(criterions[c]);

            return criteria;
        }
    }

}
