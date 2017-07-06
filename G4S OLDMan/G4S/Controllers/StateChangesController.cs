using AutoMapper;
using G4S.Business.Handlers;
using G4S.Business.Repositories;
using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class StateChangesController : BaseController<StateChange, StateChangeModel, StateChangePostModel, StateChangeSearchModel>
    {
        [Dependency]
        public IReader<MobileDevice> _mobileDeviceReader { get; set; }
        [Dependency]
        public IDeviceStateHistoryHandler _deviceStateHandler { get; set; }


        [Route("~/api/mobiledevices/{deviceId:int}/possiblestatechanges")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPossibleStateChangesForDevice(int deviceId)
        {
            try
            {
                var entities = await _deviceStateHandler.GetPossibleStateChanges(deviceId);
                var models = Mapper.Map<IEnumerable<StateChangeModel>>(entities);
                return Ok(models);
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

        [Authorize(Roles = SystemUserRole.DeviceStatesFlowEdit)]
        public override Task<IHttpActionResult> Post([FromBody] StateChangePostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.DeviceStatesFlowEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] StateChangePostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.DeviceStatesFlowDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
