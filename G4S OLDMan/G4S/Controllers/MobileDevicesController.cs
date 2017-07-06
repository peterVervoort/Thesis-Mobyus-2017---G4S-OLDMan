using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class MobileDevicesController : BaseController<MobileDevice, MobileDeviceModel, MobileDevicePostModel, MobileDeviceSearchModel>
    {
        [Authorize(Roles = SystemUserRole.DeviceEdit)]
        public override Task<IHttpActionResult> Post([FromBody] MobileDevicePostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.DeviceEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] MobileDevicePostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.DeviceDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}