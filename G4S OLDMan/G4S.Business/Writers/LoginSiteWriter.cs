using G4S.Business.Helpers;
using G4S.Business.Repositories;
using G4S.Business.Services;
using G4S.DataAccess.UnitOfWork;
using G4S.Entities.Pocos;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace G4S.Business.Writers
{
    public class LoginSiteWriter : Writer<LoginSite>
    {
        [Dependency]
        internal IUowProvider _uowProvider { get; set; }
        [Dependency]
        internal IUserWriter UserWriter { get; set; }
        [Dependency]
        internal IReader<User> UserReader { get; set; }

        public override async Task<EntityResult<LoginSite>> InsertAsync(LoginSite entity)
        {
            EntityResult<LoginSite> result = await base.InsertAsync(entity);
            if (result.Code == ResultCode.Success)
            {
                var usersToUpdate = await UserReader.Search(u => u.RoleGroup.AutoLinkEveryGroup);
                foreach (var user in usersToUpdate)
                {
                    await UserWriter.AddLoginSiteToUser(user.Id, result.Entity.Id);
                }
            }
            return result;
        }

    }
}
