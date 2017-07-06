using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class LanguagesController : BaseController<Language, LanguageModel, LanguagePostModel, LanguageSearchModel> 
    {
        [Authorize(Roles = SystemUserRole.LanguageEdit)]
        public override Task<IHttpActionResult> Post([FromBody] LanguagePostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.LanguageEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] LanguagePostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.LanguageDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
