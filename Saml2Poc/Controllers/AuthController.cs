using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saml2Poc.Utils;
using System.Text;

namespace Saml2Poc.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration cfg)
        {
            _config = cfg;
        }

        [Route("login")]
        public ActionResult Login()
        {
            string logSnippet = new StringBuilder("[")
                                .Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"))
                                .Append("][AuthController][Login] => ")
                                .ToString();

            string assertionConsumerServiceUrl = $"https://{Request.Host}{Request.PathBase}/{SamlConstants.ASSERTION_CONSUMER_SERVICE_URI}";
            Console.WriteLine(logSnippet + $"{logSnippet} (assertionConsumerServiceUrl): '{assertionConsumerServiceUrl}'");

            // SamlConfig
            //SamlConfig samlConfig = new SamlConfig(_config);

            // SamlRequest 
            //SamlRequest samlRequest = new SamlRequest(samlConfig, assertionConsumerServiceUrl);

            //Console.WriteLine(logSnippet + "BEGIN (SamlRequest) =>");
            //Console.WriteLine(samlRequest.Saml2Request.OuterXml);
            //Console.WriteLine(logSnippet + "<= END (SamlRequest)");


            string url = "https://trial-1098158.okta.com/app/trial-1098158_saml2poc_1/exk3ie3sb1xK554EB697/sso/saml";
            return Redirect(url);
        }

        [Route("AssertionConsumerService")]
        public ActionResult AssertionConsumerService()
        {
            string logSnippet = new StringBuilder("[")
                                .Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"))
                                .Append("][AuthController][AssertionConsumerService] => ")

                                .ToString();

            string encodedSamlResponse = String.Empty;
            if (Request.Form != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{logSnippet} Request.Form[\"SamlResponse\"]): '{Request.Form["SamlResponse"]}'");
                Console.WriteLine();

                encodedSamlResponse = Request.Form["SamlResponse"];


                Console.WriteLine();
                Console.WriteLine($"{logSnippet} (encodedSamlResponse): '{encodedSamlResponse}'");
                Console.WriteLine();
            }


            return RedirectToAction("Page2", "Home");
        }
    }
}
