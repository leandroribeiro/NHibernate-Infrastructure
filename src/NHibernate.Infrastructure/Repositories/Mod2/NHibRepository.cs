using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NHibernate.Infrastructure.Repositories.Mod2 {
    public abstract class NHibRepository<T> : IRepository<T>, IDisposable
    where T : class, IEntity {
        protected NHibContext Context { get; private set; }
        protected NHibSession Session { get; private set; }

        protected NHibRepository(NHibContext context) {
            Context = context;
            Session = Context.CreateNewSession();
        }

        public void Dispose() {
            Disposer.TryDispose(Session);
        }

        public void Commit() {
            Session.Commit();
        }

        public void Rollback() {
            Session.Rollback();
        }

        public virtual void Insert(T item) {
            Session.Insert<T>(item);
        }

        public virtual void Update(T item) {
            Session.Update<T>(item);
        }

        public virtual void Delete(T item) {
            Session.Delete<T>(item);
        }

        public virtual T SingleBy(Expression<Func<T, bool>> query) {
            return Session.GetItemBy<T>(query);
        }

        public virtual IList<T> List() {
            return Session.GetList<T>();
        }

        public virtual IList<T> ListBy(Expression<Func<T, bool>> query) {
            return Session.GetListBy<T>(query);
        }
    }

}
