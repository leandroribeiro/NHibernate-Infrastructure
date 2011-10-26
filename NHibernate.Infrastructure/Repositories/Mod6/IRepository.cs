using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBXDashboardsModel.Infra.Repositories.Mod6 {
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class {
        bool Add(TEntity entity);
        bool Add(IEnumerable<TEntity> items);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        bool Delete(IEnumerable<TEntity> entities);
    }
}
