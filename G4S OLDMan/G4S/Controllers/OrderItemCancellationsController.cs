using G4S.Business.Handlers;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class OrderItemCancellationsController : ApiController
    {
        [Dependency]
        public IOrderItemHandler Handler { get; set; }

        // GET: api/TEntity
        [HttpPost]
        [Route("api/orderitemcancellations")]
        public async Task<IHttpActionResult> Post([FromBody]int id)
        {
            try
            {
                var result = await Handler.CancelOrderItem(id);
                if (result.Code == Business.Helpers.ResultCode.Success) return Ok();
                else return BadRequest();
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
