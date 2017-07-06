using AutoMapper;
using G4S.Business.Handlers;
using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class ToBeTreatedLwpSettingsController : BaseController<ToBeTreatedLwpSetting, ToBeTreatedLwpSettingModel, ToBeTreatedLwpSettingPostModel, ToBeTreatedLwpSettingSearchModel>
    {
        [Dependency]
        public ICsvHandler Handler { get; set; }



        [Authorize(Roles = SystemUserRole.DeviceEdit)]
        public override Task<IHttpActionResult> Post([FromBody] ToBeTreatedLwpSettingPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.DeviceEdit)]
        public override async Task<IHttpActionResult> Put(int id, [FromBody] ToBeTreatedLwpSettingPostModel model)
        {
            try
            {
                if (model == null) return BadRequest("Model not found");
                if (id != model.Id) return BadRequest("Id not matching");
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<ToBeTreatedLwpSetting>(model);
                    var result = await EntityWriter.UpdateAsync(entity);
                    if (result.Code == Business.Helpers.ResultCode.Success)
                    {
                        await Handler.HandleToBeTreated(id);
                    }
                    return OkEntityResult(result);
                }
                else
                {
                    return BadRequest(ModelState);
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
        }

        [Authorize(Roles = SystemUserRole.DeviceDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
