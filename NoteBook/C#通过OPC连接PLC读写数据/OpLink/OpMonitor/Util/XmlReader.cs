using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using OpMonitor;
using OpMonitor.FileRead;

namespace OpMonitor
{
    class XmlReader
    {
        public static string Path
        {
            get
            {
                string path = string.Format("{0}" + LoadIniPath(), Environment.CurrentDirectory);
                return path;
            }
        }

        /// <summary>
        /// 获取XML内容
        /// </summary>
        /// <returns></returns>
        public static XElement Load()
        {
            return XElement.Load(Path);
        }

        #region 新增
        /// <summary>
        /// 创建tags.xml并保存
        /// 基础结构包括GroupData，GroupTrigger
        /// </summary>
        public static void CreateTagsXml()
        {
            if (File.Exists(Path))
            {
                if (MessageBox.Show("tags.xml已在在，是否需要重新初始化？？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    XElement xGroups = new XElement("Groups");
                    XElement xGroupTrigger = new XElement("Group", "GroupTrigger");
                    XElement xGroupData = new XElement("Group", "GroupData");
                    xGroups.Add(xGroupData);
                    xGroups.Add(xGroupTrigger);
                    string path = string.Format("{0}\\Config\\tags.xml", Environment.CurrentDirectory);
                    xGroups.Save(Path);
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 在组内创建块
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="blockName"></param>
        public static bool CreateBlock(string groupName, string blockName)
        {
            XElement xGroups = XElement.Load(Path);
            XElement xGroup = xGroups.Elements("Group").Where(p => p.Attribute("Name").Value == groupName).FirstOrDefault();

            if (!ExistBlock(blockName))
            {
                //不存在则新增
                XElement xBlock = new XElement("Block");
                xBlock.SetAttributeValue("Name", blockName);
                xGroup.Add(xBlock);
                xGroups.Save(Path);
                return true;
            }
            else
            {
                //在在则取消
                MessageBox.Show(blockName + "已在在，请修正名称后再尝试", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }

        /// <summary>
        /// 在块内创建Tag
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="blockName"></param>
        public static bool CreateTag(string groupName, string blockName, List<Tag> tags)
        {
            XElement xGroups = XElement.Load(Path);
            XElement xGroup = xGroups.Elements("Group").Where(p => p.Attribute("Name").Value == groupName).FirstOrDefault();
            XElement xBlock = xGroup.Elements("Block").Where(p => p.Attribute("Name").Value == blockName).FirstOrDefault();


            foreach (Tag tag in tags)
            {
                if (ExistTag(tag.TagName))
                {

                    //在在则取消
                    MessageBox.Show(tag.TagName + "已在在，请修正名称后再尝试", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            //不存在则新增
            //XElement xTag = new XElement("Tag");
            IEnumerable<XElement> xTags = CollectionToXml(tags);
            xBlock.Add(xTags);
            xGroups.Save(Path);
            return true;
        }
        #endregion    
        #region 判断、查询
        /// <summary>
        /// 如果所有Blocks中已存在相同名称block，则返回true
        /// </summary>
        /// <param name="blockName"></param>
        /// <returns></returns>
        public static bool ExistBlock(string blockName)
        {
            //实例化所有组
            XElement xGroups = XElement.Load(Path);
            IEnumerable<XElement> list = xGroups.Elements("Group");

            //IEnumerable<XElement> xGroup = xGroups.Elements("Group");
            foreach (XElement group in list)
            {
                //组内判断是否有相同名称的Block
                IEnumerable<XElement> Blocks = group.Elements("Block").Where(p => p.Attribute("Name").Value == blockName);

                if (Blocks.Count() != 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 如果所有Tags中已存在相同名称tag，则返回true
        /// </summary>
        /// <param name="blockName"></param>
        /// <returns></returns>
        public static bool ExistTag(string tagName)
        {
            //实例化所有组
            XElement xGroups = XElement.Load(Path);
            IEnumerable<XElement> xBlocks = xGroups.Elements("Group");

            //IEnumerable<XElement> xGroup = xGroups.Elements("Group");
            foreach (XElement group in xBlocks)
            {
                foreach (XElement block in group.Elements("Block"))
                {
                    //组内判断是否有相同名称的Block
                    IEnumerable<XElement> tags = block.Elements("Tag").Where(p => p.Attribute("TagName").Value == tagName);

                    if (tags.Count() != 0)
                    {
                        return true;
                    }
                }              
            }
            return false;
        }

        /// <summary>
        /// 查询xml配置文件中的tags
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="groupName"></param>
        /// <param name="blockName"></param>
        /// <returns></returns>
        public static List<T> QueryTags<T>(string groupName, string blockName) where T : new()
        {
            XElement xGroups = XElement.Load(Path);
            XElement xGroup = xGroups.Elements("Group").Where(p => p.Attribute("Name").Value == groupName).FirstOrDefault();
            XElement xBlock = xGroup.Elements("Block").Where(p => p.Attribute("Name").Value == blockName).FirstOrDefault();

            return XmlToCollection<T>(xBlock);
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除Block
        /// </summary>
        /// <param name="blockName"></param>
        /// <returns></returns>
        public static bool DelBlock(string blockName)
        {
            //实例化所有组
            XElement xGroups = XElement.Load(Path);
            IEnumerable<XElement> list = xGroups.Elements("Group");

            //IEnumerable<XElement> xGroup = xGroups.Elements("Group");
            foreach (XElement group in list)
            {
                //组内判断是否有相同名称的Block
                IEnumerable<XElement> Blocks = group.Elements("Block").Where(p => p.Attribute("Name").Value == blockName);

                if (Blocks.Count() != 0)
                {
                    Blocks.Remove();
                    xGroups.Save(Path);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 删除Tag
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="blockName"></param>
        public static bool DelTag(string groupName, string blockName, Tag tag)
        {
            XElement xGroups = XElement.Load(Path);
            XElement xGroup = xGroups.Elements("Group").Where(p => p.Attribute("Name").Value == groupName).FirstOrDefault();
            XElement xBlock = xGroup.Elements("Block").Where(p => p.Attribute("Name").Value == blockName).FirstOrDefault();
            IEnumerable<XElement> xTags = xBlock.Elements("Tag").Where(p => p.Attribute("TagName").Value == tag.TagName);

            if (ExistTag(tag.TagName))
            {

                if (xTags.Count() != 0)
                {
                    xTags.Remove();
                    xGroups.Save(Path);
                    return true;
                }
                else
                {
                    MessageBox.Show(tag.TagName + "在" + blockName + "中不存在", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

            }
            else
            {
                MessageBox.Show(tag.TagName + "已不存在，请再尝试", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        #endregion
        #region 函数
        /// <summary>
        /// 集合转换成数据表
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="TCollection">泛型集合</param>
        /// <returns>集合的XML格式字符串</returns>
        public static List<XElement> CollectionToXml<T>(IEnumerable<T> TCollection)
        {
            //定义元素数组
            var elements = new List<XElement>();
            //把集合中的元素添加到元素数组中
            foreach (var item in TCollection)
            {
                //获取泛型的具体类型
                Type type = typeof(T);
                //定义属性数组，XObject是XAttribute和XElement的基类
                var attributes = new List<XObject>();
                //获取类型的所有属性，并把属性和值添加到属性数组中
                foreach (var property in type.GetProperties())
                    //只搜索带有Config特性的属性
                    if (property.IsDefined(typeof(SaveAttribute), true))
                    {
                        //获取属性名称和属性值，添加到属性数组中(也可以作为子元素添加到属性数组中，只需把XAttribute更改为XElement)
                        attributes.Add(new XAttribute(property.Name, property.GetValue(item, null)));
                    }
                //把属性数组添加到元素中
                elements.Add(new XElement(type.Name, attributes));
            }
            //初始化根元素，并把元素数组作为根元素的子元素，返回根元素的字符串格式(XML)
            //return new XElement("Root", elements).ToString();
            return elements;
        }

        /// <summary>
        /// 数据表转换成集合
        /// </summary>
        /// <typeparam name="T">泛型参数(集合成员的类型)</typeparam>
        /// <param name="TCollection">泛型集合</param>
        /// <returns>集合的XML格式字符串</returns>
        public static List<T> XmlToCollection<T>(XElement xBlock) where T:new()
        {
            List<T> list = new List<T>();
            //获取泛型的具体类型
            Type type = typeof(T);
            PropertyInfo[] propinfos = type.GetProperties();

            foreach (XElement tag in xBlock.Elements("Tag"))
            {
                T entity = new T();
                ///填充entity类的属性
                foreach (var property in propinfos)
                {
                    //获取属性名称和属性值，添加到属性数组中(也可以作为子元素添加到属性数组中，只需把XAttribute更改为XElement)   
                    if (tag.Attribute(property.Name) != null)
                    {
                        string v = tag.Attribute(property.Name).Value;
                        if (v != null)
                        {
                            property.SetValue(entity, Convert.ChangeType(v, property.PropertyType), null);
                        }
                    }
                }
                //把属性数组添加到元素中
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 创建注释
        /// </summary>
        public static void CreateComment()
        {
            XElement xGroups = new XElement("Groups");
            XElement group = new XElement("Group");
            XElement tag = new XElement("tag", new XElement("Name", "Remote"), new XElement("Age", "23"));
            xGroups.Add(group);
            group.Add(tag);

            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("提示"),
                new XElement("item", "asd")
                );
            doc.Save(Path);
        }

        /// <summary>
        /// 创建属性
        /// </summary>
        public static void CreteAttribute()
        {
            XAttribute xa = new XAttribute("V2", "2");
            XElement xele = new XElement(
                "Root",
                new XElement("Item",
                    new XAttribute("V1", "1"),
                    xa
                    ));
            xele.Save(Path);
        }

        private static string LoadIniPath()
        {
            //读取配置文件
            IniReader.LoadProfile();
            return  IniReader.G_TAGPATH;
        }
        #endregion
        /// <summary>
        /// 查询xml配置文件中的tags
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="groupName"></param>
        /// <param name="blockName"></param>
        /// <returns></returns>
        //public static List<T> QueryTags_bak<T>(string groupName, string blockName) where T:new()
        //{
        //    List<T> list = new List<T>();
        //    XElement xGroups = XElement.Load(Path);
        //    XElement xGroup = xGroups.Elements("Group").Where(p => p.Attribute("Name").Value == groupName).FirstOrDefault();
        //    XElement xBlock = xGroup.Elements("Block").Where(p => p.Attribute("Name").Value == blockName).FirstOrDefault();

        //     //获取泛型的具体类型
        //    Type type=typeof(T);
        //    PropertyInfo[] propinfos = type.GetProperties();

        //    foreach (XElement tag in xBlock.Elements("Tag"))
        //    {
        //        T entity = new T();
        //        //T entity = default(T);
        //        //定义属性数组，XObject是XAttribute和XElement的基类
        //        var attributes = new List<XObject>();
        //        ///填充entity类的属性
        //        foreach (var property in propinfos)
        //        {
        //            //获取属性名称和属性值，添加到属性数组中(也可以作为子元素添加到属性数组中，只需把XAttribute更改为XElement)   
        //            if (tag.Attribute(property.Name) != null)
        //            {
        //                string v = tag.Attribute(property.Name).Value;
        //                if (v != null)
        //                {
        //                    property.SetValue(entity, Convert.ChangeType(v, property.PropertyType), null);
        //                }
        //            }                   
        //        }
        //        //把属性数组添加到元素中
        //        list.Add(entity);
        //    }
        //    return list;
        //}
    }
}
