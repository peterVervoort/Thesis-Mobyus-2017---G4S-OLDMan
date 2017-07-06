using System.Threading.Tasks;
using G4S.Business.Helpers;
using G4S.Entities.Pocos;

namespace G4S.Business.Writers
{
    public interface IUserRoleGroupWriter : IWriter<UserRoleGroup>
    {
        Task<EntityResult<StateChange>> AddUserRoleGroupToStateChange(int userRoleGroupId, int stateChangeId);
        Task<EntityResult> RemoveUserRoleGroupFromStateChange(int userRoleGroupId, int stateChangeId);
        
        Task<EntityResult<OrderStateChange>> AddUserRoleGroupToOrderStateChange(int userRoleGroupId, int orderstateChangeId);
        Task<EntityResult> RemoveUserRoleGroupFromOrderStateChange(int userRoleGroupId, int orderstateChangeId);
    }
}