using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class ProductTypesController : BaseController<ProductType, ProductTypeModel, ProductTypePostModel, ProductTypeSearchModel>
    {
        [Authorize(Roles = SystemUserRole.ProductTypeEdit)]
        public override Task<IHttpActionResult> Post([FromBody] ProductTypePostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.ProductTypeEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] ProductTypePostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.ProductTypeDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
