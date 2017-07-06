using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class LoginLicencesController : BaseController<LoginLicence, LoginLicenceModel, LoginLicencePostModel, LoginLicenceSearchModel>
    {
        [Authorize(Roles = SystemUserRole.LoginLicenceEdit)]
        public override Task<IHttpActionResult> Post([FromBody] LoginLicencePostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.LoginLicenceEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] LoginLicencePostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.LoginLicenceDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}