using AutoMapper;
using G4S.Business.Handlers;
using G4S.Business.Repositories;
using G4S.Business.Writers;
using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    public class DeviceStateHistoriesController : BaseController<DeviceStateHistory, DeviceStateHistoryModel, DeviceStateHistoryPostModel, DeviceStateHistorySearchModel>
    {
        [Dependency]
        public IReader<MobileDevice> _mobileDeviceReader { get; set; }
        

        [Route("~/api/mobiledevices/{deviceId:int}/states")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllStatesForDevice(int deviceId)
        {
            try
            {
                var device = await _mobileDeviceReader.GetById(
                    deviceId,
                    nameof(Entities.Pocos.MobileDevice.RepairChanges));
                if (device == null) return NotFound();
                var models = Mapper.Map<IEnumerable<DeviceStateHistoryModel>>(device.RepairChanges.OrderByDescending(rc => rc.ChangeDate));

                return Ok(models);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        
        [Route("~/api/mobiledevices/{deviceId:int}/states")]
        [HttpPost]
        [Authorize(Roles = SystemUserRole.DeviceStateEdit)]
        public async Task<IHttpActionResult> PostState(int deviceId, DeviceStateHistoryPostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<DeviceStateHistory>(model);
                    entity.MobileDeviceId = deviceId;
                    var result = await EntityWriter.InsertAsync(entity);
                    return OkEntityResult(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = SystemUserRole.DeviceStateEdit)]
        public override Task<IHttpActionResult> Post([FromBody] DeviceStateHistoryPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.DeviceStateEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] DeviceStateHistoryPostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.DeviceStateDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

    }
}
