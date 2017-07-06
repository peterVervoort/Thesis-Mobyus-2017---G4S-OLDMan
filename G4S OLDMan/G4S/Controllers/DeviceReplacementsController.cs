using G4S.Business.Handlers;
using G4S.Models.DeviceReplacements;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class DeviceReplacementsController : ApiController
    {
        [Dependency]
        public IDeviceReplacementHandler Handler { get; set; }

        // GET: api/TEntity
        [HttpPost]
        [Route("api/devicereplacements")]
        public async Task<IHttpActionResult> Post(DeviceReplacementPostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await Handler.ReplaceDevice(model.OldMobileDeviceId, model.NewMobileDeviceId, model.OldStateId, model.NewStateId);
                    if (result.Code == Business.Helpers.ResultCode.Success) return Ok();
                } else
                {
                    return BadRequest();
                }
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return InternalServerError();
        }
    }
}
