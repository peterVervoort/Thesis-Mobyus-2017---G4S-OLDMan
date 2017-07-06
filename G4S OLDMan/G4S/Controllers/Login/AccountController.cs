//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Security.Cryptography;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.Cookies;
//using System.Linq;
//using G4S.Business.Repositories;
//using G4S.Entities.Pocos;
//using G4S.IdentityModels;
//using G4S.Models;

//namespace G4S.Controllers.Login
//{
//    [Authorize]
//    [RoutePrefix("api/Account")]
//    public class AccountController : ApiController
//    {
//        private const string LocalLoginProvider = "Local";
//        //private ApplicationUserManager _userManager;
//        //private readonly IReader<User> _userReader;

//        //public AccountController(IReader<User> userReader)
//        //{
//        //    _userReader = userReader;
//        //}

//        //public AccountController(ApplicationUserManager userManager,
//        //    ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
//        //{
//        //    UserManager = userManager;
//        //    AccessTokenFormat = accessTokenFormat;
//        //}

//        //public ApplicationUserManager UserManager
//        //{
//        //    get
//        //    {
//        //        return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
//        //    }
//        //    private set
//        //    {
//        //        _userManager = value;
//        //    }
//        //}

//        //public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }


        
        

//        // POST api/Account/Register
//        [AllowAnonymous]
//        [Route("Register")]
//        public async Task<IHttpActionResult> Register(UserModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }


//            var users = await _userReader.Search(g => g == model.Email);
//            if (users.Any())
//            {
//                return BadRequest("Email already exists");
//            }
             
//            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

//            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

//            if (!result.Succeeded)
//            {
//                return GetErrorResult(result);
//            }

//            result = UserManager.AddToRole(user.Id, model.Role);

//            if (!result.Succeeded)
//            {
//                return GetErrorResult(result);
//            }

//            return Ok();
//        }

       
//        private static class RandomOAuthStateGenerator
//        {
//            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

//            public static string Generate(int strengthInBits)
//            {
//                const int bitsPerByte = 8;

//                if (strengthInBits % bitsPerByte != 0)
//                {
//                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
//                }

//                int strengthInBytes = strengthInBits / bitsPerByte;

//                byte[] data = new byte[strengthInBytes];
//                _random.GetBytes(data);
//                return HttpServerUtility.UrlTokenEncode(data);
//            }
//        }
//    }
//}
