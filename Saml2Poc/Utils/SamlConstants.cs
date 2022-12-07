using Microsoft.AspNetCore.Http;

namespace Saml2Poc.Utils
{
    public class SamlConstants
    {
        public static readonly string Version = "2.0";
        public static readonly string ASSERTION_CONSUMER_SERVICE_URI = "Auth/AssertionConsumerService";


        // XML Response Elements
        public static readonly string ElementNameForStatus = "saml2p:Status";
        public static readonly string ElementNameForAssertion = "saml2:Assertion";
        public static readonly string ElementNameForSubject = "saml2:Subject";
        public static readonly string ElementNameForAuthnStatement = "saml2:AuthnStatement";
        public static readonly string ElementNameForAttributeStatement = "saml2:AttributeStatement";
        public static readonly string ElementNameForAttributeValue = "saml2:AttributeValue";
        public static readonly string ElementNameForSubjectNameIdValue = "saml2:NameID";

        // XML Response Attributes
        public static readonly string AttributeNameForStatusCodeValue = "Value";

        // XML Response Values
        public static readonly string StatusCodeSuccessValue = "urn:oasis:names:tc:SAML:2.0:status:Success";
    }


}
