using Microsoft.Extensions.Configuration;

namespace Saml2Poc.Utils
{
    public class SamlConfig
    {
        public string EntityId { get; }
        public string Destination { get; }
        public string ProtocolBinding { get; }

        public SamlConfig(IConfiguration config)
        {
            this.EntityId = config["Saml2:MaxGov:ServiceProviderConfiguration:EntityId"];
            this.Destination = config["Saml2:MaxGov:IdentityProviderConfiguration:SingleSignOnService"];
            this.ProtocolBinding = config["Saml2:MaxGov:IdentityProviderConfiguration:ProtocolBinding"];
        }
    }
}
