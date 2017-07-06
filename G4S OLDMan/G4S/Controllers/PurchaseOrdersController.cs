using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class PurchaseOrdersController : BaseController<PurchaseOrder, PurchaseOrderModel, PurchaseOrderPostModel, PurchaseOrderSearchModel>
    {
        [Authorize(Roles = SystemUserRole.PoEdit)]
        public override Task<IHttpActionResult> Post([FromBody] PurchaseOrderPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.PoEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] PurchaseOrderPostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.PoDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}