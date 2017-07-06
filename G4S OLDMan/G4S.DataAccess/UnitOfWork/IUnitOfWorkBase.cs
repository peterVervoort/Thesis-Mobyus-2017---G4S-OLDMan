using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace G4S.DataAccess.UnitOfWork
{
    public interface IUnitOfWorkBase : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase;
        TRepository GetCustomRepository<TRepository>();
    }
}