using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Mediinfo.Utility
{
    public class XMLHelper
    {
        #region 构造函数  
        public XMLHelper()
        {
        }

        public XMLHelper(string filePath)
        {
            FilePath = filePath;
            OpenXML();
        }
        #endregion

        #region 对象定义  

        private XmlDocument xmlDoc = new XmlDocument();
        XmlNode xmlnode;
        XmlElement xmlelem;
        DataSet xmlds = new DataSet();
        
        #endregion



        #region 属性定义  
        private string filePath;
        public string FilePath
        {
            set { filePath = value; }
            get { return filePath; }
        }

        #endregion

        #region   

        /// <summary>  
        /// 创建XML操作对象  
        /// </summary>  
        public void OpenXML()
        {
            try
            {
                if (!string.IsNullOrEmpty(FilePath))
                {
                    xmlDoc.Load(filePath);
                }
                else
                {
                    throw new Exception("XML 解析失败，未设定需要操作的XML路径！！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("XML 解析失败，" + ex.Message);
            }
        }
        /// <summary>
        /// 加载xml操作对象
        /// </summary>
        /// <param name="xmlContent"></param>
        public void LoadXML(string xmlContent)
        {
            try
            {
                if (!string.IsNullOrEmpty(xmlContent))
                {
                    xmlDoc.LoadXml(xmlContent);
                }
                else
                {
                    throw new Exception("XML 解析失败，传入的xml内容为空！！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("XML 解析失败，" + ex.Message);
            }
        }
        #endregion


        #region 创建Xml 文档  
        /// <summary>  
        /// 创建一个带有根节点的Xml 文件  
        /// </summary>  
        /// <param name="FileName">Xml 文件名称</param>  
        /// <param name="rootName">根节点名称</param>  
        /// <param name="Encode">编码方式:gb2312，UTF-8 等常见的</param>  
        /// <returns></returns>  
        public bool CreatexmlDocument(string FileName, string rootName, string Encode)
        {
            try
            {
                XmlDeclaration xmldecl;
                xmldecl = xmlDoc.CreateXmlDeclaration("1.0", Encode, null);
                xmlDoc.AppendChild(xmldecl);
                xmlelem = xmlDoc.CreateElement("", rootName, "");
                xmlDoc.AppendChild(xmlelem);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        //获取值  

        #region xml转数据集
        public void ChangeXmlToDataSet()
        {
            StringReader read = new StringReader(xmlDoc.DocumentElement.OuterXml);
            xmlds.ReadXml(read);
        }
        #endregion

        #region xml转数据集取值
        /// <summary>
        /// 根据表名，列名，行从数据集里取出对应的值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public string GetColumnValue(string tableName, string columnName, int rowNum = 0)
        {
            string returnVal = "";
            if (xmlds != null && xmlds.Tables.Contains(tableName))
            {
                var table = xmlds.Tables[tableName];
                if (table.Columns.Contains(columnName) && table.Rows.Count >= rowNum)
                {
                    returnVal = table.Rows[rowNum][columnName].ToString();
                }
            }
            return returnVal;
        }

        public string GetDocumentInnerXml()
        {
           return xmlDoc.DocumentElement.InnerXml;
        }


        /// <summary>
        /// 对象实例集转成xml
        /// </summary>
        /// <param name="items">对象实例集</param>
        /// <returns></returns>
        public  string EntityToXml<T>(List<T> items)
        {
            //创建XmlDocument文档
            XmlDocument doc = new XmlDocument();
            //创建根元素
            XmlElement root = doc.CreateElement(items[0].GetType().Name);
            //添加根元素的子元素集
            foreach (var item in items)
            {
                EntityToXml(doc, root, item);
            }
            //向XmlDocument文档添加根元素
            doc.AppendChild(root);

            return doc.InnerXml;
        }
        private  void EntityToXml<T>(XmlDocument doc, XmlElement root, T item)
        {
            //创建元素
            XmlElement xmlItem = doc.CreateElement(item.GetType().Name);
            //对象的属性集

            System.Reflection.PropertyInfo[] propertyInfo =
            item.GetType().GetProperties(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);



            foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
            {
                if (pinfo != null)
                {
                    //对象属性名称
                    string name = pinfo.Name;
                    //对象属性值
                    string value = String.Empty;

                    if (pinfo.GetValue(item, null) != null)
                        value = pinfo.GetValue(item, null).ToString();//获取对象属性值
                    //设置元素的属性值
                    xmlItem.SetAttribute(name, value);
                }
            }
            //向根添加子元素
            root.AppendChild(xmlItem);
        }

        #region Xml转成实体类

        /// <summary>
        /// Xml转成对象实例
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        public static T XmlToEntity<T>(string xml)where T:class,new()
        {
            IList<T> items = XmlToEntityList<T>(xml);
            if (items != null && items.Count > 0)
                return items[0];
            else return default(T);
        }

        /// <summary>
        /// Xml转成对象实例集
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        public static List<T> XmlToEntityList<T>(string xml)where T:class,new()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
            }
            catch
            {
                return null;
            }
            if (doc.ChildNodes.Count != 1)
                return null;
            if (doc.ChildNodes[0].Name.ToLower() != typeof(T).Name.ToLower() + "s")
                return null;

            XmlNode node = doc.ChildNodes[0];

            List<T> items = new List<T>();

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.ToLower() == typeof(T).Name.ToLower())
                    items.Add(XmlNodeToEntity<T>(child));
            }

            return items;
        }

        private static T XmlNodeToEntity<T>(XmlNode node)where T:class,new()
        {
            T item = new T();

            if (node.NodeType == XmlNodeType.Element)
            {
                XmlElement element = (XmlElement)node;

                System.Reflection.PropertyInfo[] propertyInfo =
            typeof(T).GetProperties(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);

                foreach (XmlAttribute attr in element.Attributes)
                {
                    string attrName = attr.Name.ToLower();
                    string attrValue = attr.Value.ToString();
                    foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
                    {
                        if (pinfo != null)
                        {
                            string name = pinfo.Name.ToLower();
                            Type dbType = pinfo.PropertyType;
                            if (name == attrName)
                            {
                                if (String.IsNullOrEmpty(attrValue))
                                    continue;
                                switch (dbType.ToString())
                                {
                                    case "System.Int32":
                                        pinfo.SetValue(item, Convert.ToInt32(attrValue), null);
                                        break;
                                    case "System.Boolean":
                                        pinfo.SetValue(item, Convert.ToBoolean(attrValue), null);
                                        break;
                                    case "System.DateTime":
                                        pinfo.SetValue(item, Convert.ToDateTime(attrValue), null);
                                        break;
                                    case "System.Decimal":
                                        pinfo.SetValue(item, Convert.ToDecimal(attrValue), null);
                                        break;
                                    case "System.Double":
                                        pinfo.SetValue(item, Convert.ToDouble(attrValue), null);
                                        break;
                                    default:
                                        pinfo.SetValue(item, attrValue, null);
                                        break;
                                }
                                continue;
                            }
                        }
                    }
                }
            }
            return item;
        }

        #endregion
        /// <summary>
        /// 根据数据集中对应的表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        public DataTable GetTable(string tableName)
        {
            DataTable table = null;
            if (xmlds != null && xmlds.Tables.Contains(tableName))
            {
                table = xmlds.Tables[tableName];

            }
            return table;
        }
        #endregion


        #region 读取指定节点的指定属性值  
        /// <summary>  
        /// 功能:  
        /// 读取指定节点的指定属性值  
        /// </summary>  
        /// <param name="strNode">节点名称(相对路径：//+节点名称)</param>  
        /// <param name="strAttribute">此节点的属性</param>  
        /// <returns></returns>  
        public string GetXmlNodeValue(string strNode, string strAttribute)
        {
            string strReturn = "";
            try
            {
                //根据指定路径获取节点  
                XmlNode xmlNode = xmlDoc.SelectSingleNode(strNode);
                //获取节点的属性，并循环取出需要的属性值  
                XmlAttributeCollection xmlAttr = xmlNode.Attributes;

                for (int i = 0; i < xmlAttr.Count; i++)
                {
                    if (xmlAttr.Item(i).Name == strAttribute)
                    {
                        strReturn = xmlAttr.Item(i).Value;
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }
        #endregion

        #region
        /// <summary>
        /// 获取节点文本
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public string GetElementsTextByTagName(string nodeName, int rowNumber = 0)
        {
            string nodeValue = "";
            var node = xmlDoc.GetElementsByTagName(nodeName);
            if (node != null && node.Count > 0)
            {
                nodeValue = node[rowNumber].InnerText;
            }
            return nodeValue;
        }

        /// <summary>
        /// 获取节点中的xml内容，带格式的
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public string GetElementsXmlByTagName(string nodeName, int rowNumber = 0)
        {
            string nodeValue = "";
            var node = xmlDoc.GetElementsByTagName(nodeName);
            if (node != null && node.Count > 0)
            {
                nodeValue = node[rowNumber].InnerXml;
            }
            return nodeValue;
        }
        #endregion

        #region 读取指定节点的值  
        /// <summary>  
        /// 功能:  
        /// 读取指定节点的值  
        /// </summary>  
        /// <param name="strNode">节点名称</param>  
        /// <returns></returns>  
        public string GetXmlNodeValue(string strNode)
        {
            string strReturn = String.Empty;
            try
            {
                //根据路径获取节点  
                XmlNode xmlNode = xmlDoc.SelectSingleNode(strNode);
                strReturn = xmlNode.InnerText;
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return strReturn;
        }
        #endregion

        #region 获取XML文件的根元素  
        /// <summary>  
        /// 获取XML文件的根元素  
        /// </summary>  
        public XmlNode GetXmlRoot()
        {
            return xmlDoc.DocumentElement;
        }
        #endregion

        #region 获取XML节点值  
        /// <summary>  
        /// 获取XML节点值  
        /// </summary>  
        /// <param name="nodeName"></param>  
        /// <returns></returns>  
        public string GetNodeValue(string nodeName)
        {
            XmlNodeList xnl = xmlDoc.ChildNodes;
            foreach (XmlNode xnf in xnl)
            {
                XmlElement xe = (XmlElement)xnf;
                XmlNodeList xnf1 = xe.ChildNodes;
                foreach (XmlNode xn2 in xnf1)
                {
                    if (xn2.InnerText == nodeName)
                    {
                        XmlElement xe2 = (XmlElement)xn2;
                        return xe2.GetAttribute("value");
                    }
                }
            }
            return null;
        }
        #endregion

        //添加或插入  

        #region 设置节点值  
        /// <summary>  
        /// 功能:  
        /// 设置节点值  
        /// </summary>  
        /// <param name="strNode">节点的名称</param>  
        /// <param name="newValue">节点值</param>  
        public void SetXmlNodeValue(string xmlNodePath, string xmlNodeValue)
        {
            try
            {
                //根据指定路径获取节点  
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xmlNodePath);
                //设置节点值  
                xmlNode.InnerText = xmlNodeValue;
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion

        #region 添加父节点  

        /// <summary>  
        /// 在根节点下添加父节点  
        /// </summary>  
        public void AddParentNode(string parentNode)
        {
            XmlNode root = GetXmlRoot();
            XmlNode parentXmlNode = xmlDoc.CreateElement(parentNode);

            root.AppendChild(parentXmlNode);
        }
        #endregion

        #region 向一个已经存在的父节点中插入一个子节点  
        /// <summary>  
        /// 向一个已经存在的父节点中插入一个子节点  
        /// </summary>  
        public void AddChildNode(string parentNodePath, string childNodePath)
        {
            XmlNode parentXmlNode = xmlDoc.SelectSingleNode(parentNodePath);
            XmlNode childXmlNode = xmlDoc.CreateElement(childNodePath);

            parentXmlNode.AppendChild(childXmlNode);
        }
        #endregion

        #region 向一个节点添加属性  
        /// <summary>  
        /// 向一个节点添加属性  
        /// </summary>  
        public void AddAttribute(string NodePath, string NodeAttribute)
        {
            XmlAttribute nodeAttribute = xmlDoc.CreateAttribute(NodeAttribute);
            XmlNode nodePath = xmlDoc.SelectSingleNode(NodePath);
            nodePath.Attributes.Append(nodeAttribute);
        }
        #endregion

        #region 插入一个节点和它的若干子节点  
        /// <summary>  
        /// 插入一个节点和它的若干子节点  
        /// </summary>  
        /// <param name="NewNodeName">插入的节点名称</param>  
        /// <param name="HasAttributes">此节点是否具有属性，True 为有，False 为无</param>  
        /// <param name="fatherNode">此插入节点的父节点</param>  
        /// <param name="htAtt">此节点的属性，Key 为属性名，Value 为属性值</param>  
        /// <param name="htSubNode"> 子节点的属性， Key 为Name,Value 为InnerText</param>  
        /// <returns>返回真为更新成功，否则失败</returns>  
        public bool InsertNode(string NewNodeName, bool HasAttributes, string fatherNode, Hashtable htAtt, Hashtable htSubNode)
        {
            try
            {
                XmlNode root = xmlDoc.SelectSingleNode(fatherNode);
                xmlelem = xmlDoc.CreateElement(NewNodeName);
                if (htAtt != null && HasAttributes)//若此节点有属性，则先添加属性  
                {
                    SetAttributes(xmlelem, htAtt);
                    AddNodes(xmlelem.Name, xmlDoc, xmlelem, htSubNode);//添加完此节点属性后，再添加它的子节点和它们的InnerText  
                }
                else
                {
                    AddNodes(xmlelem.Name, xmlDoc, xmlelem, htSubNode);//若此节点无属性，那么直接添加它的子节点  
                }
                root.AppendChild(xmlelem);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region 设置节点属性  
        /// <summary>  
        /// 设置节点属性  
        /// </summary>  
        /// <param name="xe">节点所处的Element</param>  
        /// <param name="htAttribute">节点属性，Key 代表属性名称，Value 代表属性值</param>  
        public void SetAttributes(XmlElement xe, Hashtable htAttribute)
        {
            foreach (DictionaryEntry de in htAttribute)
            {
                xe.SetAttribute(de.Key.ToString(), de.Value.ToString());
            }
        }
        #endregion

        #region 增加子节点到根节点下  
        /// <summary>  
        /// 增加子节点到根节点下  
        /// </summary>  
        /// <param name="rootNode">上级节点名称</param>  
        /// <param name="xmlDoc">Xml 文档</param>  
        /// <param name="rootXe">父根节点所属的Element</param>  
        /// <param name="SubNodes">子节点属性，Key 为Name 值，Value 为InnerText 值</param>  
        public void AddNodes(string rootNode, XmlDocument xmlDoc, XmlElement rootXe, Hashtable SubNodes)
        {
            foreach (DictionaryEntry de in SubNodes)
            {
                xmlnode = xmlDoc.SelectSingleNode(rootNode);
                XmlElement subNode = xmlDoc.CreateElement(de.Key.ToString());
                subNode.InnerText = de.Value.ToString();
                rootXe.AppendChild(subNode);
            }
        }
        #endregion

        #region 增加内容到节点后面  
        /// <summary>
        /// 将传入的类型增加到xml节点中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="nodeNumber"></param>
        public void AddListToXML<T>(List<T> list,int nodeNumber=0)
        {
            if (xmlDoc.DocumentElement.ChildNodes.Count > nodeNumber)
            {
                xmlDoc.DocumentElement.ChildNodes[nodeNumber].InnerXml = xmlDoc.DocumentElement.ChildNodes[nodeNumber].InnerXml + EntityToXml<T>(list);
            }
        }
        #endregion

        //更新  

        #region 设置节点的属性值  
        /// <summary>  
        /// 功能:  
        /// 设置节点的属性值  
        /// </summary>  
        /// <param name="xmlNodePath">节点名称</param>  
        /// <param name="xmlNodeAttribute">属性名称</param>  
        /// <param name="xmlNodeAttributeValue">属性值</param>  
        public void SetXmlNodeValue(string xmlNodePath, string xmlNodeAttribute, string xmlNodeAttributeValue)
        {
            try
            {
                //根据指定路径获取节点  
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xmlNodePath);

                //获取节点的属性，并循环取出需要的属性值  
                XmlAttributeCollection xmlAttr = xmlNode.Attributes;
                for (int i = 0; i < xmlAttr.Count; i++)
                {
                    if (xmlAttr.Item(i).Name == xmlNodeAttribute)
                    {
                        xmlAttr.Item(i).Value = xmlNodeAttributeValue;
                        break;
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        #endregion

        #region 更新节点  
        /// <summary>  
        /// 更新节点  
        /// </summary>  
        /// <param name="fatherNode">需要更新节点的上级节点</param>  
        /// <param name="htAtt">需要更新的属性表，Key 代表需要更新的属性，Value 代表更新后的值</param>  
        /// <param name="htSubNode">需要更新的子节点的属性表，Key 代表需要更新的子节点名字Name,Value 代表更新后的值InnerText</param>  
        /// <returns>返回真为更新成功，否则失败</returns>  
        public bool UpdateNode(string fatherNode, Hashtable htAtt, Hashtable htSubNode)
        {
            try
            {
                XmlNodeList root = xmlDoc.SelectSingleNode(fatherNode).ChildNodes;
                UpdateNodes(root, htAtt, htSubNode);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region 更新节点属性和子节点InnerText 值  
        /// <summary>  
        /// 更新节点属性和子节点InnerText 值  
        /// </summary>  
        /// <param name="root">根节点名字</param>  
        /// <param name="htAtt">需要更改的属性名称和值</param>  
        /// <param name="htSubNode">需要更改InnerText 的子节点名字和值</param>  
        public void UpdateNodes(XmlNodeList root, Hashtable htAtt, Hashtable htSubNode)
        {
            foreach (XmlNode xn in root)
            {
                xmlelem = (XmlElement)xn;
                if (xmlelem.HasAttributes)//如果节点如属性，则先更改它的属性  
                {
                    foreach (DictionaryEntry de in htAtt)//遍历属性哈希表  
                    {
                        if (xmlelem.HasAttribute(de.Key.ToString()))//如果节点有需要更改的属性  
                        {
                            xmlelem.SetAttribute(de.Key.ToString(), de.Value.ToString());//则把哈希表中相应的值Value 赋给此属性Key  
                        }
                    }
                }
                if (xmlelem.HasChildNodes)//如果有子节点，则修改其子节点的InnerText  
                {
                    XmlNodeList xnl = xmlelem.ChildNodes;
                    foreach (XmlNode xn1 in xnl)
                    {
                        XmlElement xe = (XmlElement)xn1;
                        foreach (DictionaryEntry de in htSubNode)
                        {
                            if (xe.Name == de.Key.ToString())//htSubNode 中的key 存储了需要更改的节点名称，  
                            {
                                xe.InnerText = de.Value.ToString();//htSubNode中的Value存储了Key 节点更新后的数据  
                            }
                        }
                    }
                }
            }
        }
        #endregion

        //删除  

        #region 删除一个节点的属性  
        /// <summary>  
        /// 删除一个节点的属性  
        /// </summary>  
        public void DeleteAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            XmlNodeList nodePath = xmlDoc.SelectSingleNode(NodePath).ChildNodes;

            foreach (XmlNode xn in nodePath)
            {
                XmlElement xe = (XmlElement)xn;

                if (xe.GetAttribute(NodeAttribute) == NodeAttributeValue)
                {
                    xe.RemoveAttribute(NodeAttribute);//删除属性  
                }
            }
        }

        #endregion

        #region 删除一个节点  
        /// <summary>  
        /// 删除一个节点  
        /// </summary>  
        public void DeleteXmlNode(string tempXmlNode)
        {
            XmlNode xmlNodePath = xmlDoc.SelectSingleNode(tempXmlNode);
            xmlNodePath.ParentNode.RemoveChild(xmlNodePath);
        }

        #endregion

        #region 删除指定节点下的子节点  
        /// <summary>  
        /// 删除指定节点下的子节点  
        /// </summary>  
        /// <param name="fatherNode">制定节点</param>  
        /// <returns>返回真为更新成功，否则失败</returns>  
        public bool DeleteNodes(string fatherNode)
        {
            try
            {
                xmlnode = xmlDoc.SelectSingleNode(fatherNode);
                xmlnode.RemoveAll();
                return true;
            }
            catch (XmlException xe)
            {
                throw new XmlException(xe.Message);
            }
        }
        #endregion

        //内部函数与保存  

        #region 私有函数  

        private string functionReturn(XmlNodeList xmlList, int i, string nodeName)
        {
            string node = xmlList[i].ToString();
            string rusultNode = "";
            for (int j = 0; j < i; j++)
            {
                if (node == nodeName)
                {
                    rusultNode = node.ToString();
                }
                else
                {
                    if (xmlList[j].HasChildNodes)
                    {
                        functionReturn(xmlList, j, nodeName);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return rusultNode;

        }

        #endregion

        #region 保存XML文件  
        /// <summary>  
        /// 功能:   
        /// 保存XML文件  
        ///   
        /// </summary>  
        public void SaveXmlDocument()
        {
            try
            {
                xmlDoc.Save(FilePath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        /// <summary>  
        /// 功能: 保存XML文件  
        /// </summary>  
        /// <param name="XMLFilePath"></param>  
        public void SaveXmlDocument(string xMLFilePath)
        {
            try
            {
                xmlDoc.Save(xMLFilePath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion


        public  string GetXml(object spin)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("", "");
                new XmlSerializer(spin.GetType()).Serialize(new XmlTextWriter(ms, Encoding.UTF8), spin, xmlSerializerNamespaces);
                ms.Flush();
                ms.Position = 0;
                using (StreamReader sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
