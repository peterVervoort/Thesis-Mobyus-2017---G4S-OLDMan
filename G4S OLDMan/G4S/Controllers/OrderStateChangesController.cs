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
    public class OrderStateChangesController : BaseController<OrderStateChange, OrderStateChangeModel, OrderStateChangePostModel, OrderStateChangeSearchModel>
    {
        [Dependency]
        public IReader<OrderItem> _OrderItemReader { get; set; }
        [Dependency]
        public IOrderItemStateHistoryHandler _orderItemStateHandler { get; set; }


        [Route("~/api/orderitems/{orderItemId:int}/possiblestatechanges")]
        [HttpGet]
        public async Task<IHttpActionResult> GetPossibleStateChangesForOrderItem(int orderItemId)
        {
            try
            {
                var entities = await _orderItemStateHandler.GetPossibleStateChanges(orderItemId);
                var models = Mapper.Map<IEnumerable<OrderStateChangeModel>>(entities);
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

        [Authorize(Roles = SystemUserRole.OrderStatesFlowEdit)]
        public override Task<IHttpActionResult> Post([FromBody] OrderStateChangePostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.OrderStatesFlowEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] OrderStateChangePostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.OrderStatesFlowDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }

}
