using G4S.Business.Helpers;
using G4S.DataAccess.UnitOfWork;
using G4S.Entities.Pocos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace G4S.Business.Writers
{
    public class UserWriter : Writer<User>, IUserWriter
    {
        private readonly IUowProvider _uowProvider;

        public UserWriter(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        public async Task<EntityResult<User>> AddLoginSiteToUser(int userId, int loginSiteId)
        {
            EntityResult<User> result = new EntityResult<User>(ResultCode.Failed);
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    //lookup stateChange
                    var uRepo = uow.GetRepository<User>();
                    User user = await uRepo.GetAsync(userId, includeDeleted: Entities.Enums.DeleteOption.Both);
                    if (user == null)
                    {
                        result.Exception = new System.Exception("User not found in database");
                        return result;
                    }

                    //lookup group
                    var lsRepo = uow.GetRepository<LoginSite>();
                    LoginSite loginSite = await lsRepo.GetAsync(loginSiteId);
                    if (loginSite == null)
                    {
                        result.Exception = new System.Exception("LoginSite not found in database");
                        return result;
                    }

                    //add role to group
                    user.LoginSites.Add(loginSite);
                    await uow.SaveChangesAsync();
                    result.Entity = await uRepo.GetAsync(userId);
                    result.Code = ResultCode.Success;
                }
            }
            catch (System.Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public async Task<EntityResult<User>> RemoveLoginSiteFromUser(int userId, int loginSiteId)
        {
            EntityResult<User> result = new EntityResult<User>(ResultCode.Failed);
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    //lookup stateChange
                    var uRepo = uow.GetRepository<User>();
                    User user = await uRepo.GetAsync(userId, includeDeleted: Entities.Enums.DeleteOption.Both);
                    if (user == null)
                    {
                        result.Exception = new System.Exception("User not found in database");
                        return result;
                    }

                    //lookup group
                    var lsRepo = uow.GetRepository<LoginSite>();
                    LoginSite loginSite = await lsRepo.GetAsync(loginSiteId);
                    if (loginSite == null)
                    {
                        result.Exception = new System.Exception("LoginSite not found in database");
                        return result;
                    }

                    //add role to group
                    if (!user.LoginSites.Any(ls => ls.Id == loginSiteId))
                    {
                        result.Exception = new System.Exception("User does not contain given loginsite");
                        return result;
                    }

                    user.LoginSites.Remove(loginSite);
                    await uow.SaveChangesAsync();
                    result.Entity = await uRepo.GetAsync(userId);
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
