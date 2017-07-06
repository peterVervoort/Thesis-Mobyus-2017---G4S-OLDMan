using G4S.Business.Writers;
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
    public class LoginSitesController : BaseController<LoginSite, LoginSiteModel, LoginSitePostModel, LoginSiteSearchModel>
    {
        [Dependency]
        public UserWriter UserWriter { get; set; }

        [Authorize(Roles = SystemUserRole.LoginSiteEdit)]
        public override Task<IHttpActionResult> Post([FromBody] LoginSitePostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.LoginSiteEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] LoginSitePostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.LoginSiteDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

        [Route("~/api/users/{userId:int}/loginsites")]
        [HttpPost]
        [Authorize(Roles = SystemUserRole.UserAddLoginSite)]
        public async Task<IHttpActionResult> PostGroupForStateChange(int userId, [FromBody]LoginSitePostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await UserWriter.AddLoginSiteToUser(userId, model.Id);
                    return OkEntityResult(result);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("~/api/users/{userId:int}/loginsites/{loginSiteId:int}")]
        [HttpDelete]
        [Authorize(Roles = SystemUserRole.UserAddLoginSite)]
        public async Task<IHttpActionResult> PostGroupForStateChange(int userId, int loginSiteId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await UserWriter.RemoveLoginSiteFromUser(userId, loginSiteId);
                    return OkEntityResult(result);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}