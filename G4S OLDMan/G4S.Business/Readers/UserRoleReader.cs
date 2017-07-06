using G4S.Business.Repositories;
using G4S.DataAccess.UnitOfWork;
using G4S.Entities.Pocos;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Readers
{
    public class UserRoleReader : ReaderBase<UserRole>, IUserRoleReader
    {
        [Dependency]
        public IUowProvider UowProvider { get; set; }

        public async Task<bool> DoesUserHaveRole(string roleName, string Email)
        {
            try
            {
                using (var uow = UowProvider.CreateUnitOfWork(false))
                {
                    return await uow.GetRepository<UserRole>().Any(ur => ur.Groups.Any(g => g.Users.Any(u => u.Email == Email)));
                }
            }
            catch (Exception ex)
            {
                //TODO
                return false;
            }
        }
    }
}
