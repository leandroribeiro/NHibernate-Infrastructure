using System.Collections.Generic;

namespace NHibernate.Infrastructure.Repositories.Mod1 {
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

        //public virtual T SingleBy(Expression<Func<T, bool>> query) {
        //    T result;

        //    using (var uow = Context.CreateUnitOfWork()) {
        //        result = uow.GetItemBy<T>(query);
        //    }

        //    return result;
        //}

        public virtual IList<T> List() {
            IList<T> result;

            using (var uow = Context.CreateUnitOfWork()) {
                result = uow.GetList<T>();
            }

            return result;
        }
        //public virtual IList<T> ListBy(Expression<Func<T, bool>> query) {
        //    IList<T> result;

        //    using (var uow = Context.CreateUnitOfWork()) {
        //        result = uow.GetListBy<T>(query);
        //    }

        //    return result;
        //}
    }

}
