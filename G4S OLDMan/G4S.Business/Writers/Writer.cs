using G4S.Business.Helpers;
using G4S.Business.Services;
using G4S.Business.Validators;
using G4S.DataAccess.Repositories;
using G4S.DataAccess.UnitOfWork;
using G4S.Entities.Pocos;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace G4S.Business.Writers
{
    public class Writer<TEntity> : IWriter<TEntity> where TEntity : EntityBase
    {
        [Dependency]
        protected IRepository<TEntity> Repository { get; set; }
        [Dependency]
        protected ISecurityService SecurityService { get; set; }
        [Dependency]
        protected IValidator<TEntity> Validator { get; set; }
        [Dependency]
        protected IUowProvider UowProvider { get; set; }


        public virtual async Task<EntityResult<TEntity>> InsertAsync(TEntity entity)
        {
            EntityResult<TEntity> result = new EntityResult<TEntity>(ResultCode.Success);
            result.Entity = entity;
            try
            {
                var validationResult = await Validator.ValidateInsertAsync(entity);
                if (validationResult.Result != ValidationResultCode.Valid)
                {
                    return new EntityResult<TEntity>(ResultCode.ValidationError, validationResult.Messages.ToArray());
                }

                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repo = uow.GetRepository<TEntity>();
                    entity = repo.Create(entity, await SecurityService.GetCurrentUserName());
                    await uow.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failed;
                result.Exception = ex;
            }

            return result;
        }

        public virtual async Task<EntityResult<TEntity>> UpdateAsync(TEntity entity)
        {
            EntityResult<TEntity> result = new EntityResult<TEntity>(ResultCode.Success);
            result.Entity = entity;
            try
            {
                var validationResult = await Validator.ValidateUpdateAsync(entity);

                if (validationResult.Result != ValidationResultCode.Valid)
                {
                    return new EntityResult<TEntity>(ResultCode.ValidationError, validationResult.Messages.ToArray());
                }

                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repo = uow.GetRepository<TEntity>();
                    entity = await repo.UpdateAsync(entity, await SecurityService.GetCurrentUserName());
                    await uow.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failed;
                result.Exception = ex;
            }

            return result;
        }

        public virtual async Task<EntityResult<TEntity>> DeleteAsync(int id)
        {
            EntityResult<TEntity> result = new EntityResult<TEntity>(ResultCode.Success);

            try
            {
                var entity = await Repository.GetAsync(id);
                result.Entity = entity;

                var validationResult = await Validator.ValidateDeleteAsync(entity);
                if (validationResult.Result != ValidationResultCode.Valid)
                {
                    return new EntityResult<TEntity>(ResultCode.ValidationError, validationResult.Messages.ToArray());
                }
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repo = uow.GetRepository<TEntity>();
                    await repo.DeleteAsync(id, await SecurityService.GetCurrentUserName());
                    await uow.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result.Code = ResultCode.Failed;
                result.Exception = ex;
            }

            return result;
        }
        public virtual async Task<EntityResult<TEntity>> RestoreAsync(int id)
        {
            EntityResult<TEntity> result = new EntityResult<TEntity>(ResultCode.Success);

            try
            {
                var entity = await Repository.GetAsync(id, includeDeleted: Entities.Enums.DeleteOption.OnlyDeleted);
                result.Entity = entity;

                var validationResult = await Validator.ValidateRestoreAsync(entity);
                if (validationResult.Result != ValidationResultCode.Valid)
                {
                    return new EntityResult<TEntity>(ResultCode.ValidationError, validationResult.Messages.ToArray());
                }
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repo = uow.GetRepository<TEntity>();
                    await repo.RestoreAsync(id, await SecurityService.GetCurrentUserName());
                    await uow.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                result.Code = ResultCode.Failed;
                result.Exception = ex;
            }

            return result;
        }
    }
}
