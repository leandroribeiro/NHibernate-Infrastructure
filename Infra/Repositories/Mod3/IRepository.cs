using System;
using System.Linq;

namespace EBXDashboardsModel.Infra.Repositories {
    public interface IRepository {
        void Save(object obj);
        void Delete(object obj);
        object GetById(Type objType, object objId);
        IQueryable<TEntity> ToList<TEntity>();
    }

}
