using System.Threading.Tasks;
using G4S.Business.Helpers;
using G4S.Entities.Pocos;

namespace G4S.Business.Writers
{
    public interface IUserRoleWriter : IWriter<UserRole>
    {
        Task<EntityResult<UserRole>> AddUserRoleToGroup(int userRoleId, int userRoleGroupId);
        Task<EntityResult> RemoveUserRoleFromGroup(int userRoleId, int userRoleGroupId);
        
    }
}