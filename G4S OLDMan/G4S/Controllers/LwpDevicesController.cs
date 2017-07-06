using AutoMapper;
using G4S.Business.Handlers;
using G4S.Business.Repositories;
using G4S.Business.Services;
using G4S.Business.Writers;
using G4S.Controllers.Base;
using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using G4S.Models.LwpDevice;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class LwpDevicesController : ReturnValuesApiController<MobileDevice, LwpDeviceModel>
    {
        [Dependency]
        protected ILwpDeviceHandler Handler { get; set; }
        [Dependency]
        protected IReader<MobileDevice> EntityReader { get; set; }



        // GET: api/TEntity/5
        [Route("api/lwpdevices/{id:int}")]
        [HttpGet]
        public virtual async Task<IHttpActionResult> GetById(int id)
        {
            try
            {
                var entity = await EntityReader.GetById(id);
                if (entity == null) return NotFound();
                return Ok(Mapper.Map<LwpDeviceModel>(entity));
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


        // POST: api/TEntity
        [Route("api/lwpdevices")]
        [Authorize(Roles = SystemUserRole.DeviceEdit)]
        [HttpPost]
        public virtual async Task<IHttpActionResult> Post([FromBody]LwpDevicePostModel model)
        {
            try
            {
                if (model == null) return BadRequest("Model not found");
                if (ModelState.IsValid)
                {
                    var device = Mapper.Map<MobileDevice>(model.MobileDevice);
                    var lwp = Mapper.Map<LwpSetting>(model.LwpSetting);
                    var result = await Handler.Create(device, lwp);
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

        // PUT: api/TEntity/5
        [Route("api/lwpdevices/{id:int}")]
        [Authorize(Roles = SystemUserRole.DeviceEdit)]
        [HttpPut]
        public virtual async Task<IHttpActionResult> Put(int id, [FromBody]LwpDevicePostModel model)
        {
            try
            {
                if (model == null) return BadRequest("Model not found");
                if (id != model.Id) return BadRequest("Id not matching");
                if (ModelState.IsValid)
                {
                    var device = Mapper.Map<MobileDevice>(model.MobileDevice);
                    var lwp = Mapper.Map<LwpSetting>(model.LwpSetting);
                    var result = await Handler.Update(id, device, lwp);
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


        // DELETE: api/TEntity/5
        [Route("api/lwpdevices/{id:int}")]
        [Authorize(Roles = SystemUserRole.DeviceDelete)]
        [HttpDelete]
        public virtual async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var result = await Handler.Delete(id);
                if (result.Code == Business.Helpers.ResultCode.Failed)
                {
                    return InternalServerError(result.Exception);
                }
                return NoContent();
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

    }
}