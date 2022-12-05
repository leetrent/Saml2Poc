using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Saml2Poc.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public class AuthController : Controller
    {
        [Route("login")]
        public ActionResult Login()
        {
            return RedirectToAction("Page2", "Home");
        }
    }
}
