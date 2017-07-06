using G4S.Business.Writers;
using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace G4S.Controllers
{
    [Authorize]
    public class UserRoleGroupsController : BaseController<UserRoleGroup, UserRoleGroupModel, UserRoleGroupPostModel, UserRoleGroupSearchModel>
    {
        private IUserRoleGroupWriter _userRoleGroupWriter;

        public UserRoleGroupsController(IUserRoleGroupWriter userRoleGroupWriter)
        {
            _userRoleGroupWriter = userRoleGroupWriter;
        }

        [Route("~/api/statechanges/{stateChangeId:int}/userrolegroups")]
        [HttpPost]
        [Authorize(Roles = SystemUserRole.UserRoleGroupEdit)]
        public async Task<IHttpActionResult> PostGroupForStateChange(int stateChangeId, [FromBody]UserRoleGroupPostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRoleGroupWriter.AddUserRoleGroupToStateChange(model.Id, stateChangeId);
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

        [Route("~/api/statechanges/{stateChangeId:int}/userrolegroups/{groupId:int}")]
        [HttpDelete]
        [Authorize(Roles = SystemUserRole.UserRoleGroupDelete)]
        public async Task<IHttpActionResult> DeleteGroupForStateChange(int stateChangeId, int groupId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRoleGroupWriter.RemoveUserRoleGroupFromStateChange(groupId, stateChangeId);
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

        [Route("~/api/orderstatechanges/{orderstateChangeId:int}/userrolegroups")]
        [HttpPost]
        [Authorize(Roles = SystemUserRole.UserRoleGroupEdit)]
        public async Task<IHttpActionResult> PostGroupForOrderStateChange(int orderstateChangeId, [FromBody]UserRoleGroupPostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRoleGroupWriter.AddUserRoleGroupToOrderStateChange(model.Id, orderstateChangeId);
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

        [Route("~/api/orderstatechanges/{orderstateChangeId:int}/userrolegroups/{groupId:int}")]
        [HttpDelete]
        [Authorize(Roles = SystemUserRole.UserRoleGroupDelete)]
        public async Task<IHttpActionResult> DeleteGroupForOrderStateChange(int orderstateChangeId, int groupId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRoleGroupWriter.RemoveUserRoleGroupFromOrderStateChange(groupId, orderstateChangeId);
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

        [Authorize(Roles = SystemUserRole.UserRoleGroupEdit)]
        public override Task<IHttpActionResult> Post([FromBody] UserRoleGroupPostModel model)
        {
            return base.Post(model);
        }

        [Authorize(Roles = SystemUserRole.UserRoleGroupEdit)]
        public override Task<IHttpActionResult> Put(int id, [FromBody] UserRoleGroupPostModel model)
        {
            return base.Put(id, model);
        }

        [Authorize(Roles = SystemUserRole.UserRoleGroupDelete)]
        public override Task<IHttpActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
