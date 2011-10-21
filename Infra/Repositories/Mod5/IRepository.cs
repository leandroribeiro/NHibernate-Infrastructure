using System.Linq;

namespace EBXDashboardsModel.Infra.Repositories {
    public interface IRepository<T> where T : class {
        void Save(T entity);
        void Delete(T entity);
        T GetById(object objId);
        T GetById<U>(U objId);
        IQueryable<TEntity> ToList<TEntity>();
    }

}
