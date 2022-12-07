using System;
using System.Xml;
using System.Text;

namespace Saml2Poc.Utils
{
    public class SamlResponse
    {
        public string SubjectNameIdValue { get; } = String.Empty;

        public SamlResponse(string encodedSamlResponse)
        {
             string logSnippet = new StringBuilder("[")
                    .Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"))
                    .Append("][SamlResponse][Constructor] => ")
                    .ToString();

            string decodedSamlResponse = Encoding.UTF8.GetString(Convert.FromBase64String(encodedSamlResponse));
            this.writeToLog(decodedSamlResponse);

            XmlDocument xmlDoc = new XmlDocument
            {
                XmlResolver = null,
                PreserveWhitespace = true
            };

            xmlDoc.LoadXml(decodedSamlResponse);
            XmlElement xmlDocElement = xmlDoc.DocumentElement!;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Status > StatusCode
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string statusCodeValue = extractStatusCodeValue(xmlDocElement);
            Console.WriteLine($"{logSnippet} (statusCodeValue): '{statusCodeValue}'");

            if (statusCodeValue.Equals(SamlConstants.StatusCodeSuccessValue) == false)
            {
                StringBuilder sb = new StringBuilder("Saml2Response did not contain a successful status code value.");
                sb.Append(" Expected: \"");
                sb.Append(SamlConstants.StatusCodeSuccessValue);
                sb.Append("\". Received: \"");
                sb.Append(statusCodeValue);
                sb.Append("\". Redirecting user to 'Authentication Failed' page.");

                throw new Exception(sb.ToString());
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Assertion > Subject > NameID
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            this.SubjectNameIdValue = extractAssertionSubjectNameIdValue(xmlDocElement);
            Console.WriteLine($"{logSnippet} ( SubjectNameIdValue): '{this.SubjectNameIdValue}'");

            if (String.IsNullOrEmpty(this.SubjectNameIdValue) || String.IsNullOrWhiteSpace(this.SubjectNameIdValue))
            {
                throw new Exception($"Saml2Response did not contain a '{SamlConstants.ElementNameForSubjectNameIdValue}' value.");
            }
        }

        private string extractStatusCodeValue(XmlElement xmlDocElement)
        {
            XmlNodeList xmlStatusNodeList = xmlDocElement.GetElementsByTagName(SamlConstants.ElementNameForStatus);
            XmlNode xmlStatusNode = xmlStatusNodeList[0]!;
            XmlNode xmlStatusCodeNode = xmlStatusNode.FirstChild!;
            XmlAttribute xmlStatusCodeValueAttribute = xmlStatusCodeNode.Attributes![SamlConstants.AttributeNameForStatusCodeValue!]!;
            return xmlStatusCodeValueAttribute.Value;
        }

        private string extractAssertionSubjectNameIdValue(XmlElement xmlDocElement)
        {
            XmlNodeList xmlAssertionNodeList = xmlDocElement.GetElementsByTagName(SamlConstants.ElementNameForAssertion);
            XmlNode xmlAssertionNode = xmlAssertionNodeList[0]!;

            XmlElement xmlSubjectElement = xmlAssertionNode[SamlConstants.ElementNameForSubject]!;
            XmlNode xmlNameIdNode = xmlSubjectElement.FirstChild!;
            return xmlNameIdNode.InnerText;

        }

        private void writeToLog(string decodedSamlResponse)
        {
             string logSnippet = new StringBuilder("[")
                   .Append(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"))
                   .Append("][SamlResponse][Constructor][writeToLog] => ")
                   .ToString();

            Console.WriteLine(logSnippet + "\nBEGIN decodedSamlResponse:\n");
            Console.WriteLine(decodedSamlResponse);
            Console.WriteLine(logSnippet + "\n:END decodedSamlResponse\n");
        }
    }
}
