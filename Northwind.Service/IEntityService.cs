using Northwind.Models.Domain.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Service {
    public interface IEntityService<TEntity> : IDisposable where TEntity : EntityBase {
        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByKeyAsync(params object[] keys);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetByParamAsync(/*DataSourceParameter dataSourceParam*/);

        Task<int> SaveAsync(TEntity entityToSave);

        bool Exists(int id);

        bool Exists(params object[] keys);
    }
}
