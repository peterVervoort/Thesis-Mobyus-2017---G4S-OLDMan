using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class RepairReasonsController : BaseController<RepairReason, RepairReasonModel, RepairReasonPostModel, RepairReasonSearchModel>
    {
        [Authorize(Roles = SystemUserRole.RepairReasonEdit)]
        public override Task<IHttpActionResult> Post([FromBody] RepairReasonPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.RepairReasonEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] RepairReasonPostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.RepairReasonDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
