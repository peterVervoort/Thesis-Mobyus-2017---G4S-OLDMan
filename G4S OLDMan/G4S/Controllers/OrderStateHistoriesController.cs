using AutoMapper;
using G4S.Business.Repositories;
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
    [Authorize]
    public class OrderStateHistoriesController : BaseController<OrderItemHistory, OrderItemHistoryModel, OrderItemHistoryPostModel, OrderItemHistorySearchModel>
    {
        [Dependency]
        public IReader<OrderItem> _orderItemReader { get; set; }
        

        [Route("~/api/orderitems/{orderItemId:int}/states")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllStatesForDevice(int orderItemId)
        {
            try
            {
                var orderItem = await _orderItemReader.GetById(orderItemId);
                if (orderItem == null) return NotFound();
                var models = Mapper.Map<IEnumerable<OrderItemHistoryModel>>(orderItem.ItemChanges.OrderByDescending(rc => rc.ChangeDate));

                return Ok(models);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        
        [Route("~/api/orderitems/{orderItemId:int}/states")]
        [HttpPost]
        [Authorize(Roles = SystemUserRole.OrderStateEdit)]
        public async Task<IHttpActionResult> PostState(int orderItemId, OrderItemHistoryPostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = Mapper.Map<OrderItemHistory>(model);
                    entity.OrderItemId = orderItemId;
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

        [Authorize(Roles = SystemUserRole.OrderStateEdit)]
        public override Task<IHttpActionResult> Post([FromBody] OrderItemHistoryPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.OrderStateEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] OrderItemHistoryPostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.OrderStateDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

    }
}
