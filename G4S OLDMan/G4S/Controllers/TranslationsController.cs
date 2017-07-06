using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class TranslationsController : BaseController<Translation, TranslationModel, TranslationPostModel, TranslationSearchModel>
    {
        public TranslationsController()
        {
            IncludeFields = new string[] { nameof(Translation.Language) };
        }

        [AllowAnonymous]
        public override Task<IHttpActionResult> Search([FromBody] TranslationSearchModel model)
        {
            return base.Search(model);
        }

        [AllowAnonymous]
        public override Task<IHttpActionResult> SearchCount([FromBody] TranslationSearchModel model)
        {
            return base.SearchCount(model);
        }

        public override Task<IHttpActionResult> Post([FromBody] TranslationPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.TranslationEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] TranslationPostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.TranslationDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
