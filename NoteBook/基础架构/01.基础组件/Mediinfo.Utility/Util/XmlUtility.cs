using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Mediinfo.Utility.Util
{
    /// <summary>
    /// xml工具类
    /// </summary>
    public class XmlUtility
    {
        /// <summary>
        /// 将自定义对象序列化为XML字符串
        /// </summary>
        /// <param name="myObject">自定义对象实体</param>
        /// <returns>序列化后的XML字符串</returns>
        public static string SerializeToXml<T>(T myObject)
        {
            if (myObject != null)
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));

                MemoryStream stream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                writer.Formatting = Formatting.None;//缩进
                // 去掉要结点的 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" 属性
                XmlSerializerNamespaces _namespaces = new XmlSerializerNamespaces(
                    new XmlQualifiedName[] {
                        new XmlQualifiedName(string.Empty, "aa")
                 });
                xs.Serialize(writer, myObject, _namespaces);

                stream.Position = 0;
                StringBuilder sb = new StringBuilder();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                    reader.Close();
                }
                writer.Close();
                return sb.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 将XML字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xml">XML字符</param>
        /// <returns></returns>
        public static T DeserializeToObject<T>(string xml)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xml);
                ms.Write(bytes, 0, bytes.Length);
                ms.Flush();
                ms.Position = 0;
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(ms);
            }

            //T myObject;
            //XmlSerializer serializer = new XmlSerializer(typeof(T));
            //StringReader reader = new StringReader(xml);
            //myObject = (T)serializer.Deserialize(reader);
            //reader.Close();
            //return myObject;
        }
    }
}