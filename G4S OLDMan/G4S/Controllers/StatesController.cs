using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class StatesController : BaseController<State, StateModel, StatePostModel, StateSearchModel>
    {
        [Authorize(Roles = SystemUserRole.StatesEdit)]
        public override Task<IHttpActionResult> Post([FromBody] StatePostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.StatesEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] StatePostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.StatesDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
