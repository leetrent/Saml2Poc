using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saml2Poc.Utils;
using System.Security.Authentication;
using System.Text;

namespace Saml2Poc.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public class AuthController : Controller
    {
        [Route("login")]
        public ActionResult Login()
        {
            string logSnippet = new StringBuilder("[")
                                .Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"))
                                .Append("][AuthController][Login] => ")
                                .ToString();
            string assertionConsumerServiceUrl = $"https://{Request.Host}{Request.PathBase}/{Saml2Constants.ASSERTION_CONSUMER_SERVICE_URI}";
            Console.WriteLine(logSnippet + $"(assertionConsumerServiceUrl): '{assertionConsumerServiceUrl}'");

            string url = "https://trial-1098158.okta.com/app/trial-1098158_saml2poc_1/exk3ie3sb1xK554EB697/sso/saml";

            return Redirect(url);
        }

        [Route("AssertionConsumerService")]
        public ActionResult AssertionConsumerService()
        {
            return RedirectToAction("Page2", "Home");
        }
    }
}
