﻿
namespace EBXDashboardsModel.Infra.Repositories.Mod6 {
    public interface IIntKeyedRepository<TEntity> : IRepository<TEntity> where TEntity : class {
        TEntity FindBy(int id);
    }

}
