using G4S.Business.Helpers;
using G4S.Entities.Pocos;
using System.Threading.Tasks;

namespace G4S.Business.Writers
{
    internal interface IUserWriter : IWriter<User>
    {
        Task<EntityResult<User>> AddLoginSiteToUser(int userId, int loginSiteId);
        Task<EntityResult<User>> RemoveLoginSiteFromUser(int userId, int loginSiteId);
    }
}