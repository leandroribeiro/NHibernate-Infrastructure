using System.Linq;

namespace NHibernate.Infrastructure.Repositories.Mod5 {
    public interface IRepository<T> where T : class {
        void Save(T entity);
        void Delete(T entity);
        T GetById(object objId);
        T GetById<U>(U objId);
        IQueryable<TEntity> ToList<TEntity>();
    }

}
