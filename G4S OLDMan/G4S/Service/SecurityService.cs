using System;
using G4S.Entities.Pocos;
using G4S.Business.Services;
using System.Web;
using G4S.Business.Repositories;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace G4S.Services
{
    public class SecurityService : ISecurityService
    {
        [Dependency]
        public IReader<User> UserReader { get; set; }
        [Dependency]
        public IReader<UserRoleGroup> GroupReader { get; set; }
        [Dependency]
        public IReader<UserRole> UserRoleReader { get; set; }


        public async Task<User> GetCurrentUser()
        {
            ClaimsIdentity identity = HttpContext.Current?.User?.Identity as ClaimsIdentity;
            if (identity == null) return null;
            if (!identity.Claims.Any()) return null;
            var email = identity.Claims.FirstOrDefault().Value;
            var user = await UserReader.Search(u => u.Email == email);
            return user.FirstOrDefault();
        }

        public async Task<int?> GetCurrentUserId()
        {
            return (await GetCurrentUser())?.Id;
        }

        public async Task<string> GetCurrentUserName()
        {
            var user = await GetCurrentUser();
            if (user == null) return null;
            return $"{user.FirstName} {user.Name}";
        }

        public async Task<UserRoleGroup> GetCurrentUserRoleGroup()
        {
            var user = await GetCurrentUser();
            if (user == null) return null;
            if (!user.RoleGroupId.HasValue) return null;
            return await GroupReader.GetById(user.RoleGroupId.Value);
        }

        public async Task<IEnumerable<UserRole>> GetCurrentUserRoles()
        {
            var user = await GetCurrentUser();
            if (user == null) return null;
            if (!user.RoleGroupId.HasValue) return null;
            return await UserRoleReader.Search(ur => ur.Groups.Any(g => g.Id == user.RoleGroupId));
        }

        public async Task<bool> HasUserRole(string role)
        {
            var roles = await GetCurrentUserRoles();
            if (roles == null) return false;
            if (!roles.Any()) return false;
            return roles.Any(r => r.RoleName == role);
        }
    }
}
