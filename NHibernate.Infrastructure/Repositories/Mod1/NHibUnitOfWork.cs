using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace EBXDashboardsModel.Infra.Repositories {
    /// <summary>
    /// An Unit-of-work implementation against NHibernate.
    /// </summary>
    /// <remarks>
    /// If disposed, the injected session is also disposed.
    /// </remarks>
    public class NHibUnitOfWork
        : IDisposable {
        private readonly object _lock = new object();

        /// <summary>
        /// Indicates if there are any un-flushed changes.
        /// </summary>
        private bool HasChanges { get; set; }

        /// <summary>
        /// Original NHibernate session that is wrapped.
        /// </summary>
        private ISession InnerSession { get; set; }

        public NHibUnitOfWork(ISession session) {
            this.HasChanges = false;
            this.InnerSession = session;
            this.InnerSession.FlushMode = FlushMode.Never;
        }

        #region " Object lifetime, Disposing "

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if (disposing)
                this.FreeManagedResources();
        }

        ~NHibUnitOfWork() {
            this.Dispose(false);
        }

        /// <summary>
        /// Frees managed resources (.Net, other items implementing IDisposable)
        /// </summary>
        private void FreeManagedResources() {
            if (this.InnerSession == null) return;

            this.InnerSession.Close();
            this.InnerSession.Dispose();
            this.InnerSession = null;
        }

        #endregion

        /// <summary>
        /// Creates and returns a Query.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <returns></returns>
        public IQuery CreateQuery(string queryString) {
            return this.InnerSession.CreateQuery(queryString);
        }

        /// <summary>
        /// Creates and returns a Criteria.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ICriteria CreateCriteria<T>()
            where T : class {
            return this.InnerSession.CreateCriteria<T>();
        }

        /// <summary>
        /// Returns item via Id.
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public TReturn GetItemById<TReturn, TId>(TId id) {
            return this.InnerSession.Get<TReturn>(id);
        }

        /// <summary>
        /// Returns item via NHibernate Criterions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criterions"></param>
        /// <returns></returns>
        public T GetItemByCriterions<T>(params ICriterion[] criterions) {
            return AddCriterions(this.InnerSession.CreateCriteria(typeof(T)), criterions).UniqueResult<T>();
        }

        /// <summary>
        /// Returns a list of ALL items.
        /// </summary>
        /// <remarks>ALL items are returned.</remarks>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> GetList<T>() {
            return this.GetListByCriterions<T>(null);
        }

        /// <summary>
        /// Returns list of item matching sent criterions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criterions"></param>
        /// <returns></returns>
        public IList<T> GetListByCriterions<T>(params ICriterion[] criterions) {
            ICriteria criteria = AddCriterions(this.InnerSession.CreateCriteria(typeof(T)), criterions);
            IList<T> result = criteria.List<T>();

            return result ?? new List<T>(0);
        }

        /// <summary>
        /// Deletes sent item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Delete<T>(T obj) {
            this.InnerSession.Delete(obj);
            this.HasChanges = true;
        }

        /// <summary>
        /// Deletes sent item by id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="id"></param>
        public void DeleteById<T, TId>(TId id) {
            this.Delete<T>(this.GetItemById<T, TId>(id));
            this.HasChanges = true;
        }

        /// <summary>
        /// Deletes by query.
        /// </summary>
        /// <param name="query"></param>
        public void DeleteByQuery(string query) {
            this.InnerSession.Delete(query);
            this.HasChanges = true;
        }

        /// <summary>
        /// Inserts sent item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Insert<T>(T obj) {
            this.InnerSession.Save(obj);
            this.HasChanges = true;
        }

        ///// <summary>
        ///// Saves (inserts or updates) the item.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        //public void SaveItem<T>(T obj)
        //{
        //    this.InnerSession.SaveOrUpdate(obj);
        //}

        /// <summary>
        /// Updates sent item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Update<T>(T obj) {
            this.InnerSession.Update(obj);
            this.HasChanges = true;
        }

        /// <summary>
        /// Clears the UnitOfWork from unsaved changes.
        /// </summary>
        public void Clear() {
            lock (this._lock) {
                this.InnerSession.Clear();
                this.HasChanges = false;
            }
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges() {
            if (!this.HasChanges) return;

            lock (this._lock) {
                this.InnerSession.Flush();
                this.HasChanges = false;
            }
        }

        private static ICriteria AddCriterions(ICriteria criteria, ICriterion[] criterions) {
            if (criterions != null)
                for (int c = 0; c < criterions.Length; c++)
                    criteria = criteria.Add(criterions[c]);

            return criteria;
        }

    }

}
