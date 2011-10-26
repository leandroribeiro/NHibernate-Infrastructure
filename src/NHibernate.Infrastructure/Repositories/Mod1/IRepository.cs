using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NHibernate.Infrastructure.Repositories.Mod1 {
    //public interface IRepository<T> {
    //    void Save(T obj);
    //    void Update(T obj);
    //    void Delete(T obj);
    //    T Load<T>(object id);
    //    T GetReference<T>(object id);
    //    void DeleteAll(IList<T> objs);
    //    void UpdateAll(IList<T> objs);
    //    void InsertAll(IList<T> objs);
    //    IList<T> GetAll<T>();
    //    IList<T> GetAllOrdered<T>(string propertyName, bool ascending);
    //    IList<T> Find<T>(IList<string> criteria);
    //    void Detach(T item);
    //    IList<T> GetAll<T>(int pageIndex, int pageSize);
    //    void Commit();
    //    void Rollback();
    //    void BeginTransaction();
    //}

    public interface IRepository<T> where T : class {
        ITransaction CreateTransaction();

        void Insert(T item);
        void Update(T item);
        void Delete(T item);

        T SingleBy(Expression<Func<T, bool>> query);

        IList<T> List();
        IList<T> ListBy(Expression<Func<T, bool>> query);
    }


}
