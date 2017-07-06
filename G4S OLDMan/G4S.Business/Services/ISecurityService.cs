using G4S.Entities.Pocos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G4S.Business.Services
{
    public interface ISecurityService
    {
        Task<User> GetCurrentUser();
        Task<int?> GetCurrentUserId();
        Task<string> GetCurrentUserName();
        Task<UserRoleGroup> GetCurrentUserRoleGroup();
        Task<IEnumerable<UserRole>> GetCurrentUserRoles();
        Task<bool> HasUserRole(string role);


    }
}
