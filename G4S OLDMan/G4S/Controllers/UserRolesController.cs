using G4S.Entities.Pocos;
using G4S.Models;
using System.Web.Http;
using System.Threading.Tasks;
using G4S.Business.Repositories;
using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using G4S.Business.Writers;
using System.Collections.Generic;
using G4S.Entities.Enums;

namespace G4S.Controllers
{
    //[Authorize(Roles = "UserRoles")]
    public class UserRolesController : BaseController<UserRole, UserRoleModel, UserRolePostModel, UserRoleSearchModel>
    {
        private const string LocalLoginProvider = "Local";

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        private readonly IReader<User> _userReader;
        private readonly IWriter<User> _userWriter;
        private readonly IReader<UserRoleGroup> _userRoleGroupReader;
        private readonly IUserRoleWriter _userRoleWriter;

        public UserRolesController(IWriter<User> userWriter, 
            IReader<User> userReader, 
            IReader<UserRoleGroup> userRoleGroupReader, 
            IUserRoleWriter userRoleWriter)
        {
            _userReader = userReader;
            _userWriter = userWriter;
            _userRoleGroupReader = userRoleGroupReader;
            _userRoleWriter = userRoleWriter;
        }


        
        public override async Task<IHttpActionResult> Post([FromBody] UserRolePostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool roleAlreadyExists = (await EntityReader.Search(g => g.RoleName.ToLower() == model.RoleName.ToLower())).Any();

                    if (!roleAlreadyExists)
                    {
                        var identityRole = await RoleManager.FindByNameAsync(model.RoleName);
                        if (identityRole != null) roleAlreadyExists = true;
                    }

                    if (roleAlreadyExists) return BadRequest("Role already exists");

                    var entity = Mapper.Map<UserRole>(model);
                    var result = await EntityWriter.InsertAsync(entity);

                    if (result.Code == Business.Helpers.ResultCode.Success)
                    {
                        //Save in identity tables
                        IdentityResult identityResult = await RoleManager.CreateAsync(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(model.RoleName));
                        if (identityResult.Succeeded) return Ok(result.Entity);
                        await EntityWriter.DeleteAsync(result.Entity.Id);
                        return InternalServerError();
                    }

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

        [Route("~/api/users/{userId:int}/roles")]
        [HttpGet]
        [AllowAnonymous]
        [OverrideAuthorization]
        [Authorize]
        public async Task<IHttpActionResult> GetAllRolesForUser(int userId)
        {
            try
            {
                var user = await _userReader.GetById(
                    userId, 
                    nameof(Entities.Pocos.User.RoleGroup), 
                    $"{nameof(Entities.Pocos.User.RoleGroup)}.{nameof(Entities.Pocos.UserRoleGroup.Roles)}"
                );
                if (user == null) return NotFound();
                var models = Mapper.Map<IEnumerable<UserRoleModel>>(user.RoleGroup?.Roles);
                
                return Ok(models);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        [Route("~/api/userrolegroups/{groupId:int}/roles")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllRolesForGroup(int groupId)
        {
            try
            {
                var group = await _userRoleGroupReader.GetById(
                    groupId,
                    nameof(Entities.Pocos.UserRoleGroup.Roles)
                );
                if (group == null) return NotFound();
                var models = Mapper.Map<IEnumerable<UserRoleModel>>(group.Roles);

                return Ok(models);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("~/api/userrolegroups/{groupId:int}/roles")]
        [HttpPost]
        public async Task<IHttpActionResult> PostRoleForUser(int groupId, [FromBody]UserRolePostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRoleWriter.AddUserRoleToGroup(model.Id, groupId);
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

        [Route("~/api/userrolegroups/{groupId:int}/roles/{roleId:int}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRoleForUser(int groupId, int roleId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRoleWriter.RemoveUserRoleFromGroup(roleId, groupId);
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

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _roleManager != null)
            {
                _roleManager.Dispose();
                _roleManager = null;
            }

            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }
    }
}