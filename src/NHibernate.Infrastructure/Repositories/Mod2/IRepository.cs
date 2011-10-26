using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NHibernate.Infrastructure.Repositories.Mod2 {
    public interface IRepository<T> where T : class, IEntity {
        void Commit();
        void Rollback();

        void Insert(T item);
        void Update(T item);
        void Delete(T item);

        T SingleBy(Expression<Func<T, bool>> query);

        IList<T> List();
        IList<T> ListBy(Expression<Func<T, bool>> query);
    }

}
