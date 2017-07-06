using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class OrderItemsController : BaseController<OrderItem, OrderItemModel, OrderItemPostModel, OrderItemSearchModel>
    {
        [Authorize(Roles = SystemUserRole.ItemLineEditItemLine)]
        public override Task<IHttpActionResult> Post([FromBody] OrderItemPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.ItemLineEditItemLine)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] OrderItemPostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.ItemLineDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}