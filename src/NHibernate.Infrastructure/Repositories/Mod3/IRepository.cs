using System;
using System.Linq;

namespace NHibernate.Infrastructure.Repositories.Mod3 {
    public interface IRepository {
        void Save(object obj);
        void Delete(object obj);
        object GetById(System.Type objType, object objId);
        IQueryable<TEntity> ToList<TEntity>();
    }

}
