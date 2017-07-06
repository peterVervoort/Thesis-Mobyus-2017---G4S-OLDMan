using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Net.Http;
using System.Web.Http;

namespace G4S.Controllers
{
    public class AuthenticationController : ApiController
    {
        // POST api/Account/Logout
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        //INFO:: Login provided in /api/authentication/token endpoint of account => see OAuthOptions property in Startup.Auth.cs
        //INFO:: Token itself can be found in ApplicationOAuthProvider.cs

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }
    }
}
