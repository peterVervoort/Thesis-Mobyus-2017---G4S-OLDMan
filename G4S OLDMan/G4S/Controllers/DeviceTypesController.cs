using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class DeviceTypesController : BaseController<DeviceType, DeviceTypeModel, DeviceTypePostModel, DeviceTypeSearchModel>
    {
        [Authorize(Roles = SystemUserRole.DeviceTypeEdit)] 
        public override Task<IHttpActionResult> Post([FromBody] DeviceTypePostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.DeviceTypeEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] DeviceTypePostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.DeviceTypeDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
