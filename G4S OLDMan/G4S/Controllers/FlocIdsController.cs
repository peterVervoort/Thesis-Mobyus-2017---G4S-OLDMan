using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Web.Http;
using System.Threading.Tasks;

namespace G4S.Controllers
{
    [Authorize]
    public class FlocIdsController : BaseController<FlocId, FlocIdModel, FlocIdPostModel, FlocIdSearchModel>
    {
        [Authorize(Roles = SystemUserRole.FlocIdEdit)]
        public override Task<IHttpActionResult> Post([FromBody] FlocIdPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.FlocIdEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] FlocIdPostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.FlocIdDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}