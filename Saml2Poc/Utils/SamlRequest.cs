using System;
using System.Xml;
using System.Text;
using Saml2Poc.Utils;

namespace Saml2Poc.Utils
{
    public class SamlRequest
    {
        public XmlElement Saml2Request { get; }

        public SamlRequest(SamlConfig samlConfig, string assertionConsumerServiceUrlValue)
        {

            XmlDocument xmlDoc = new XmlDocument();
            this.Saml2Request = xmlDoc.CreateElement("q1:AuthnRequest", "urn:oasis:names:tc:SAML:2.0:protocol");

            // ID Attribute
            XmlAttribute idAttribute = xmlDoc.CreateAttribute("ID");
            idAttribute.Value = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
            this.Saml2Request.Attributes.Append(idAttribute);

            // Version Attribute
            XmlAttribute versionAttribute = xmlDoc.CreateAttribute("Version");
            versionAttribute.Value = SamlConstants.Version;
            this.Saml2Request.Attributes.Append(versionAttribute);

            // IssueInstant Attribute
            XmlAttribute issueInstantAttribute = xmlDoc.CreateAttribute("IssueInstant");
            //issueInstantAttribute.Value = DateTime.UtcNow.ToString();
            issueInstantAttribute.Value = DateTime.UtcNow.ToString("o");
            this.Saml2Request.Attributes.Append(issueInstantAttribute);

            // Destination Attribute
            XmlAttribute destinationAttribute = xmlDoc.CreateAttribute("Destination");
            destinationAttribute.Value = samlConfig.Destination;
            this.Saml2Request.Attributes.Append(destinationAttribute);

            // ForceAuthn Attribute
            XmlAttribute forceAuthnAttribute = xmlDoc.CreateAttribute("ForceAuthn");
            forceAuthnAttribute.Value = "false";
            this.Saml2Request.Attributes.Append(forceAuthnAttribute);

            // IsPassive Attribute
            XmlAttribute isPassiveAttribute = xmlDoc.CreateAttribute("IsPassive");
            isPassiveAttribute.Value = "false";
            this.Saml2Request.Attributes.Append(isPassiveAttribute);

            // ProtocolBinding Attribute
            XmlAttribute protocolBindingAttribute = xmlDoc.CreateAttribute("ProtocolBinding");
            protocolBindingAttribute.Value = samlConfig.ProtocolBinding;
            this.Saml2Request.Attributes.Append(protocolBindingAttribute);

            // AssertionConsumerServiceURL Attribute
            XmlAttribute assertionConsumerServiceURLAttribute = xmlDoc.CreateAttribute("AssertionConsumerServiceURL");
            assertionConsumerServiceURLAttribute.Value = assertionConsumerServiceUrlValue;
            this.Saml2Request.Attributes.Append(assertionConsumerServiceURLAttribute);

            // Issuer Element
            XmlElement issuerElement = xmlDoc.CreateElement("Issuer", "urn:oasis:names:tc:SAML:2.0:assertion");
            issuerElement.InnerText = samlConfig.EntityId;
            this.Saml2Request.AppendChild(issuerElement);

            // Conditions Element
            XmlElement conditionsElement = xmlDoc.CreateElement("Conditions", "urn:oasis:names:tc:SAML:2.0:assertion");
            conditionsElement.InnerText = samlConfig.EntityId;
            this.Saml2Request.AppendChild(conditionsElement);

            // AudienceRestriction Element
            XmlElement audienceRestrictionElement = xmlDoc.CreateElement("AudienceRestriction", "urn:oasis:names:tc:SAML:2.0:assertion");
            conditionsElement.AppendChild(audienceRestrictionElement);

            // Audience Element
            XmlElement audienceElement = xmlDoc.CreateElement("Audience");
            audienceElement.InnerText = samlConfig.EntityId;
            audienceRestrictionElement.AppendChild(audienceElement);
        }
    }
}
