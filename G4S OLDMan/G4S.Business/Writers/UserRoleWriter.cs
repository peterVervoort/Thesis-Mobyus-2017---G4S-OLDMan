using G4S.Business.Helpers;
using G4S.DataAccess.UnitOfWork;
using G4S.Entities.Pocos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace G4S.Business.Writers
{
    public class UserRoleWriter : Writer<UserRole>, IUserRoleWriter
    {
        private readonly IUowProvider _uowProvider;

        public UserRoleWriter(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        public async Task<EntityResult<UserRole>> AddUserRoleToGroup(int userRoleId, int userRoleGroupId)
        {
            EntityResult<UserRole> result = new EntityResult<UserRole>(ResultCode.Failed);
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    //lookup userrole
                    var urRepo = uow.GetRepository<UserRole>();
                    UserRole userRole = await urRepo.GetAsync(userRoleId);
                    if (userRole == null) result.Exception = new System.Exception("UserRole not found in database");

                    //lookup group
                    var urgRepo = uow.GetRepository<UserRoleGroup>();
                    UserRoleGroup userRoleGroup = await urgRepo.GetAsync(userRoleGroupId);
                    if (userRoleGroup == null) result.Exception = new System.Exception("UserRoleGroup not found in database");

                    //add role to group
                    userRoleGroup.Roles.Add(userRole);
                    await uow.SaveChangesAsync();
                    result.Entity = await urRepo.GetAsync(userRoleId);
                    result.Code = ResultCode.Success;
                }
            }
            catch (System.Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public async Task<EntityResult> RemoveUserRoleFromGroup(int userRoleId, int userRoleGroupId) {
            EntityResult result = new EntityResult(ResultCode.Failed);
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    //lookup group
                    var urgRepo = uow.GetRepository<UserRoleGroup>();
                    UserRoleGroup userRoleGroup = await urgRepo.GetAsync(userRoleGroupId);
                    if (userRoleGroup == null) result.Exception = new System.Exception("UserRoleGroup not found in database");

                    var userRole = userRoleGroup.Roles.FirstOrDefault(ur => ur.Id == userRoleId);
                    if (userRole == null) result.Exception = new System.Exception($"UserRoleGroup does not contain userrole with id {userRoleId}");

                    //add role to group
                    userRoleGroup.Roles.Remove(userRole);
                    await uow.SaveChangesAsync();
                    result.Code = ResultCode.Success;
                }
            }
            catch (System.Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

    }
}
