using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class ToBeTreatedMobileDevicesController : BaseController<ToBeTreatedMobileDevice, ToBeTreatedMobileDeviceModel, ToBeTreatedMobileDevicePostModel, ToBeTreatedMobileDeviceSearchModel>
    {
        [Authorize(Roles = SystemUserRole.ToBeTreatedMobileDeviceEdit)]
        public override Task<IHttpActionResult> Post([FromBody] ToBeTreatedMobileDevicePostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.ToBeTreatedMobileDeviceEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] ToBeTreatedMobileDevicePostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.ToBeTreatedMobileDeviceDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}