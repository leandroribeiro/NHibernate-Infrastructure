using System;
using System.Collections.Generic;

namespace NHibernate.Infrastructure.Repositories.Mod1 {


    //public class NHibernateRepository<T> : IRepository<T> {
    //    private ISession session;

    //    public NHibernateRepository() {
    //        session = NHibernateSessionManager.Instance.GetSession();
    //    }

    //    #region IRepository<T> Members

    //    public void Save(T obj) {
    //        session.Save(obj);
    //    }

    //    public void Update(T obj) {
    //        session.Update(obj);
    //    }

    //    public void Delete(T obj) {
    //        session.Delete(obj);
    //    }

    //    public T Load<T>(object id) {
    //        return session.Load<T>(id);
    //    }

    //    public T GetReference<T>(object id) {
    //        return session.Get<T>(id);
    //    }

    //    public void DeleteAll(IList<T> objs) {
    //        for (Int32 I = 0; I < objs.Count; ++I) {
    //            Delete(objs[I]);
    //        }
    //    }

    //    public void UpdateAll(IList<T> objs) {
    //        for (Int32 I = 0; I < objs.Count; ++I) {
    //            Update(objs[I]);
    //        }
    //    }

    //    public void InsertAll(IList<T> objs) {
    //        for (Int32 I = 0; I < objs.Count; ++I) {
    //            Save(objs[I]);
    //        }
    //    }

    //    public void Detach(T item) {
    //        session.Evict(item);
    //    }

    //    internal void Flush() {
    //        session.Flush();
    //    }

    //    public IList<T> GetAll<T>(int pageIndex, int pageSize) {
    //        ICriteria criteria = session.CreateCriteria(typeof(T));
    //        criteria.SetFirstResult(pageIndex * pageSize);
    //        if (pageSize > 0) {
    //            criteria.SetMaxResults(pageSize);
    //        }
    //        return criteria.List<T>();
    //    }

    //    public IList<T> GetAll<T>() {
    //        return GetAll<T>(0, 0);
    //    }

    //    public IList<T> Find<T>(IList<string> strs) {
    //        IList<ICriterion> objs = new List<ICriterion>();
    //        foreach (string s in strs) {
    //            ICriterion cr1 = Expression.Sql(s);
    //            objs.Add(cr1);
    //        }
    //        ICriteria criteria = session.CreateCriteria(typeof(T));
    //        foreach (ICriterion rest in objs)
    //            session.CreateCriteria(typeof(T)).Add(rest);

    //        criteria.SetFirstResult(0);
    //        return criteria.List<T>();
    //    }

    //    public void Commit() {
    //        if (session.Transaction.IsActive) {
    //            session.Transaction.Commit();
    //        }
    //    }

    //    public void Rollback() {
    //        if (session.Transaction.IsActive) {
    //            session.Transaction.Rollback();
    //            session.Clear();
    //        }
    //    }

    //    public void BeginTransaction() {
    //        session.BeginTransaction();
    //    }

    //    #endregion

    //    #region IRepository<T> Members

    //    public IList<T> GetAllOrdered<T>(string propertyName, bool ascending) {
    //        Order cr1 = new Order(propertyName, Ascending);
    //        IList<T> objsResult = session.CreateCriteria(typeof(T)).AddOrder(cr1).List<T>();
    //        return objsResult;

    //    }

    //    #endregion
    //}

    public abstract class NHibRepository<T> : IRepository<T>
    where T : class, new() {
        protected NHibContext Context { get; private set; }

        protected NHibRepository(NHibContext context) {
            Context = context;
        }

        public virtual ITransaction CreateTransaction() {
            return new Transaction();
        }

        public virtual void Insert(T item) {
            using (var uow = Context.CreateUnitOfWork()) {
                uow.Insert<T>(item);
                uow.SaveChanges();
            }
        }

        public virtual void Update(T item) {
            using (var uow = Context.CreateUnitOfWork()) {
                uow.Update<T>(item);
                uow.SaveChanges();
            }
        }

        public virtual void Delete(T item) {
            using (var uow = Context.CreateUnitOfWork()) {
                uow.Delete<T>(item);
                uow.SaveChanges();
            }
        }

        public virtual T SingleBy(Expression<Func<T, bool>> query) {
            T result;

            using (var uow = Context.CreateUnitOfWork()) {
                result = uow.GetItemBy<T>(query);
            }

            return result;
        }

        public virtual IList<T> List() {
            IList<T> result;

            using (var uow = Context.CreateUnitOfWork()) {
                result = uow.GetList<T>();
            }

            return result;
        }
        public virtual IList<T> ListBy(Expression<Func<T, bool>> query) {
            IList<T> result;

            using (var uow = Context.CreateUnitOfWork()) {
                result = uow.GetListBy<T>(query);
            }

            return result;
        }
    }


}
