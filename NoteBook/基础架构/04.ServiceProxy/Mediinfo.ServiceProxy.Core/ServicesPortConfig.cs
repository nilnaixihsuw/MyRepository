using System.Xml;

namespace Mediinfo.ServiceProxy.Core
{
    /// <summary>
    /// 已废弃
    /// </summary>
    public class ServicesPortConfig
    {
        public static string GetValue(string serviceName)
        {
            string port = string.Empty;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath.Substring(0, System.Windows.Forms.Application.ExecutablePath.LastIndexOf('\\')) + "\\ServicesPort.config");
            XmlNode xNode;
            XmlElement xElem1;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='"+ serviceName + "']");
            if (xElem1 != null)
                port = xElem1.GetAttribute("value");
            return port;
        }
    }
}
