using G4S.Entities.Pocos;
using G4S.Models;
using System.Web.Http;
using System.Threading.Tasks;
using G4S.Business.Repositories;
using System.Linq;
using AutoMapper;
using G4S.IdentityModels;
using Microsoft.AspNet.Identity;
using System;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using G4S.Entities.Enums;

namespace G4S.Controllers
{
    //[Authorize]
    public class UsersController : BaseController<User, UserModel, UserPostModel, UserSearchModel>
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        private IReader<User> _userReader;

        public UsersController(IReader<User> userReader)
        {
            _userReader = userReader;
            IncludeFields = new string[] {
                nameof(Entities.Pocos.User.Language),
                nameof(Entities.Pocos.User.RoleGroup)
            };
        }

        [HttpGet]
        [Route("~/api/loginsites/{loginSiteId:int}/users")]
        public async Task<IHttpActionResult> GetUsersForLoginSites(int loginSiteId)
        {
            try
            {
                var users = await EntityReader.Search(u => u.LoginSites.Any(ls => ls.Id == loginSiteId), includes: IncludeFields);
                var models = Mapper.Map<IOrderedEnumerable<UserModel>>(users);
                return Ok(models);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("~/api/userrolegroups/{urgId:int}/users")]
        public async Task<IHttpActionResult> GetUsersForRoleGroup(int urgId)
        {
            try
            {
                var users = await EntityReader.Search(u => u.RoleGroupId == urgId, includes: IncludeFields);
                var models = Mapper.Map<IOrderedEnumerable<UserModel>>(users);
                return Ok(models);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("~/api/languages/{langId:int}/users")]
        public async Task<IHttpActionResult> GetUsersForLanguage(int langId)
        {
            try
            {
                var users = await EntityReader.Search(u => u.LanguageId == langId, includes: IncludeFields);
                var models = Mapper.Map<IOrderedEnumerable<UserModel>>(users);
                return Ok(models);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = SystemUserRole.UsersEdit)]
        public override async Task<IHttpActionResult> Post([FromBody] UserPostModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool userAlreadyExists = (await _userReader.Search(g => g.Email.ToLower() == model.Email.ToLower())).Any();

                    if (!userAlreadyExists)
                    {
                        var identityUser = await UserManager.FindByNameAsync(model.Email);
                        if (identityUser != null) userAlreadyExists = true;
                    }

                    if (userAlreadyExists) return BadRequest("User already exists");

                    var entity = Mapper.Map<User>(model);
                    var result = await EntityWriter.InsertAsync(entity);

                    if (result.Code == Business.Helpers.ResultCode.Success)
                    {
                        //Save in identity tables
                        IdentityResult identityResult = await UserManager.CreateAsync(new ApplicationUser() { UserName = model.Email, Email = model.Email }, model.Password);
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

        [Authorize(Roles = SystemUserRole.UsersDelete)]
        public override async Task<IHttpActionResult> Delete(int id)
        {
            var user = await EntityReader.GetById(id);
            if (user == null) return NotFound();

            var identityUser = await UserManager.FindByEmailAsync(user.Email);
            if (identityUser != null)
            {
                await UserManager.DeleteAsync(identityUser);
            }

            return await base.Delete(id);
        }

        [Authorize(Roles = SystemUserRole.UsersEdit)]
        public override async Task<IHttpActionResult> Put(int id, [FromBody] UserPostModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.PasswordRepeat))
                {
                    if (!model.Password.Equals("unchanged", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var user = await _userReader.GetById(id);
                        if (user == null) return NotFound();

                        var identityUser = await UserManager.FindByEmailAsync(user.Email);
                        await UserManager.RemovePasswordAsync(identityUser.Id);
                        var result = await UserManager.AddPasswordAsync(identityUser.Id, model.Password);
                        if (!result.Succeeded) return InternalServerError();
                    }
                }
                return await base.Put(id, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("~/api/users/{userId:int}/passwords")]
        public async Task<IHttpActionResult> ChangePassword(int userId, PasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userReader.GetById(userId);
            if (user == null) return NotFound();

            var identityUser = await UserManager.FindByEmailAsync(user.Email);
            IdentityResult result = await UserManager.ChangePasswordAsync(identityUser.Id, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(string.Join(";", result.Errors));
            }

            return Ok();
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }


    }
}