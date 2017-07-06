using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;

namespace G4S.Controllers
{
    [Authorize]
    public class PlatformsController : BaseController<Platform, PlatformModel, PlatformPostModel, PlatformSearchModel>
    {
        [Authorize(Roles = SystemUserRole.PlatformEdit)]
        public override Task<IHttpActionResult> Post([FromBody] PlatformPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.PlatformEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] PlatformPostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.PlatformDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}