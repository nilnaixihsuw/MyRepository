using OPCAutomation;
using OpMonitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpMonitor
{
    /// <summary>
    /// OPC数据分组，
    /// 带有1、OPC组信息
    ///     2、组下子项信息
    /// </summary>
    public class DaGroup : TagsChangedListener, IGroup
    {
        #region Group
        /// <summary>
        /// OpcGroup,通过泛型转换方式
        /// </summary>
        public OPCGroup OPCGroup { get; private set; }
        /// <summary>
        /// GroupName
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="group"></param>
        public DaGroup(OPCGroup group, string groupName)
        {
            this.OPCGroup = group;
            this.GroupName = groupName;
            //this.OPCGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(this.KepGroup_DataChange);
            //为组增加监听，当集合数量发生变化时，调用TagsCollectionChanged事件,绑定tags
            this.Tags.CollectionChanged += this.TagsCollectionChanged;
            //this.OPCGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(KepGroup_AsyncWriteComplete); 
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="group"></param>     
        public DaGroup(OPCGroup group)
        {
            this.OPCGroup = group;
            this.GroupName = group.Name;
            this.OPCGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(this.KepGroup_DataChange);
            //为组增加监听，当集合数量发生变化时，调用TagsCollectionChanged事件,绑定tags
            this.Tags.CollectionChanged += this.TagsCollectionChanged;

        }

        #endregion
        #region GroupItems

        /// <summary>
        /// 组内数据项集合
        /// 采用动态数据集合ObservableCollection
        /// </summary>
        private ObservableCollection<Tag> tags = new ObservableCollection<Tag>();
        /// <summary>
        ///  OPCTags 信号集合
        ///  监听 ObservableCollection<T> 集合的“增加元素”事件，然后，监听这个T对象的属性修改事件，属性修改（value发生变化时）,调用广播事件TriggerEvent.obj_PropertyChanged
        /// </summary>
        public ObservableCollection<Tag> Tags { get { return tags; } }

        public bool AddItem(Tag bi)
        {
            try
            {
                #region*********集合已包含此Tag,则退出*************************
                foreach (Tag item in this.Tags)
                {
                    if (item.TagName == bi.TagName)
                    {
                        MessageBox.Show("Tag重复：在在重复的Tag点", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                #endregion

                OPCItems items = this.OPCGroup.OPCItems;//根据指定的组类别，取出组下的标签集合 
                Func<Tag, int> f = p => { if (p != null) { return p.ExtraAs<DaExtra>().ItmHandleClient + 1; } return 0; };
                int itmHandleBegin = f(Tags.LastOrDefault());//itmHandleBegin的逻辑为最后一个标签itmHandle值+1                
                //关键，此步骤将标签添加入OPCclient的监听列表中
                OPCItem ki = items.AddItem(bi.OpcTagName, itmHandleBegin);
                bi.ExtraAs<DaExtra>().ItmHandleClient = itmHandleBegin;
                bi.DataType = Enum.GetName(typeof(OpcTypes.CanonicalDataTypesForDa), ki.CanonicalDataType);
                bi.ExtraAs<DaExtra>().ItmHandleServer = ki.ServerHandle;
                Tags.Add((Tag)(Object)bi);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddItems(List<Tag> biList)
        {
            try
            {
                #region 传入的标签集合为空时，直接返回
                if (biList.Count == 0) return false;
                #endregion
                #region 当前标签集合已包含此Tag,则退出
                if (Tags.Count(p => biList.Select(b => b.TagName).Contains(p.TagName)) != 0)
                {
                    MessageBox.Show("Tag重复：在在重复的Tag点", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }


                #endregion

                //根据组名，取出组下的子集合
                OPCItems items = this.OPCGroup.OPCItems;
                int itmHandleBegin;
                int NUMITEMS;
                Array OPCItemIDs;
                Array ItemClientHandles;
                Array ItemServerHandles;
                Array AddItemServerErrors;
                Func<Tag, int> f = (p => p != null ? p.ExtraAs<DaExtra>().ItmHandleClient : 0);

                itmHandleBegin = f(Tags.LastOrDefault());//itmHandleBegin的逻辑为最后一个标签itmHandle值                
                NUMITEMS = biList.Count + 1;//OPCitem集合元素是以1为数组的基数,例：如果新增5个标签的话，就需要创建一个长度为6的标签， 
                OPCItemIDs = Array.CreateInstance(typeof(string), NUMITEMS);
                ItemClientHandles = Array.CreateInstance(typeof(int), NUMITEMS);
                ItemServerHandles = Array.CreateInstance(typeof(int), NUMITEMS);
                AddItemServerErrors = default(System.Array);

                //标签赋值后加入daTags
                for (int i = 1; i < NUMITEMS; i++)
                {
                    OPCItemIDs.SetValue(biList[i - 1].OpcTagName, i);
                    ItemClientHandles.SetValue(itmHandleBegin + i, i);
                    biList[i - 1].ExtraAs<DaExtra>().ItmHandleClient = itmHandleBegin + i;
                    Tags.Add((Tag)(Object)biList[i - 1]);
                }

                //关键，此步骤将标签添加入OPCclient的监听列表中
                items.AddItems(biList.Count, ref OPCItemIDs, ref ItemClientHandles, out ItemServerHandles, out AddItemServerErrors);

                #region ***********判断tag点是否正常监听************
                for (short i = 1; i < NUMITEMS; i++)
                {
                    if ((int)AddItemServerErrors.GetValue(i) == 0) //If the item was added successfully then allow it to be used.
                    {
                        Tags[i - 1].Message = "OPC Add Item Successful";
                        Tags[i - 1].Enabled = true;

                        //设置标签的数据类型及ServerHandle
                        OPCItem ki = items.GetOPCItem((int)ItemServerHandles.GetValue(i));
                        biList[i - 1].DataType = Enum.GetName(typeof(OpcTypes.CanonicalDataTypesForDa), ki.CanonicalDataType);
                        biList[i - 1].ExtraAs<DaExtra>().ItmHandleServer = (int)ItemServerHandles.GetValue(i);
                    }
                    else
                    {
                        Tags[i - 1].Message = "OPC Add Item Fail";
                        Tags[i - 1].Enabled = false;
                        //ItemServerHandles(i) = 0; // If the handle was bad mark it as empty                       
                        //OPCItemValue(i).Enabled = false;
                        //OPCItemValue(i).Text = "OPC Add Item Fail";
                    }
                }
                #endregion
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void RemoveItemsAll()
        {
            if (Tags.Count == 0) return;//监控的集合为空时，直接返回

            OPCItems items = this.OPCGroup.OPCItems;//根据组名，取出分组下的标签集合
            List<int> tempHandles = new List<int>();//临时标签句柄集合，保存需要删除的每个标签的服务端句柄
            Array ItemServerHandles;//保存需要删除的标签句柄集合，由临时句柄集合转换而来
            Array Errors;

            #region*********将需要删除的标签句柄压入数组ItemServerHandles******************
            tempHandles.Add(0); //因为opc的传入参数特殊性，数组第一位置留空                
            tempHandles.AddRange(Tags.Where(p => p.Enabled == true).Select(p => p.ExtraAs<DaExtra>().ItmHandleServer));
            ItemServerHandles = tempHandles.ToArray();//由临时标签集合转换
            Tags.Clear();//监控集合删除所有位置的标签
            #endregion
            try
            {
                //移除
                items.Remove(ItemServerHandles.Length - 1, ref ItemServerHandles, out Errors);//OPC集合删除指定位置的元素                
            }
            catch (System.AccessViolationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void RemoveItems(List<Tag> biList)
        {
            try
            {
                if (Tags.Count == 0) return;//监控的集合为空时，直接返回

                OPCItems items = this.OPCGroup.OPCItems;//根据组名，取出分组下的标签集合
                List<int> tempHandles = new List<int>();//临时标签句柄集合，保存需要删除的每个标签的服务端句柄
                Array ItemServerHandles;//保存需要删除的标签句柄集合，由临时句柄集合转换而来
                Array Errors;

                #region*********将需要删除的标签句柄压入数组ItemServerHandles******************
                tempHandles.Add(0);//因为opc的传入参数特殊性，数组第一位置留空  
                foreach (var item in Tags
                    .Where(a => biList
                    .Select(p => p.TagName)
                    .Contains(a.TagName)))
                {
                    tempHandles.Add(item.ExtraAs<DaExtra>().ItmHandleServer);
                    Tags.Remove(item);//监控标签集合删除指定位置的标签
                }

                ItemServerHandles = tempHandles.ToArray();//由临时标签集合转换
                #endregion
                //DaOpc从组中移除标签操作
                items.Remove(ItemServerHandles.Length - 1, ref ItemServerHandles, out Errors);

            }
            catch (System.AccessViolationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveItem(Tag bi)
        {
            try
            {
                if (Tags.Count == 0) return;//标签不存在时，返回false

                OPCItems items = this.OPCGroup.OPCItems;//根据组名，取出分组下的标签集合
                List<int> tempHandles = new List<int>();//临时标签句柄集合，保存需要删除的每个标签的服务端句柄
                Array ItemServerHandles;//保存需要删除的标签句柄集合，由临时句柄集合转换而来
                Array Errors;

                #region*********将需要删除的标签句柄压入数组ItemServerHandles******************
                //因为opc的传入参数特殊性，数组第一位置留空
                tempHandles.Add(0);
                foreach (var item in Tags.Where(p => p.TagName == bi.TagName))
                {
                    tempHandles.Add(item.ExtraAs<DaExtra>().ItmHandleServer);
                    Tags.Remove(item);//监控标签集合删除指定位置的标签
                }

                ItemServerHandles = tempHandles.ToArray();//由临时集合转换而来
                #endregion
                //DaOpc从组中移除标签操作
                items.Remove(tempHandles.Count - 1, ref ItemServerHandles, out Errors);
            }
            catch (System.AccessViolationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Tag GetTagValue(Tag bi)
        {
            return Tags.Where(t => t.TagName == bi.TagName).FirstOrDefault();
        }

        public List<Tag> GetTagValues(List<Tag> biList)
        {
            return Tags.Where(t => biList
                 .Select(b => b.TagName)
                 .Contains(t.TagName)).ToList();
        }
        #endregion
        #region DataChange
        /// <summary>
        /// 每当项数据有变化时执行的事件(事件数据)
        /// </summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">TAG值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        public void KepGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            //为了测试，所以加了控制台的输出，来查看事物ID号
            //Console.WriteLine("********" + NumItems.ToString() + "********" + TransactionID.ToString() + "*********");
            try
            {
                for (int i = 1; i <= NumItems; i++)
                {
                    int HandleClient = Convert.ToInt32(ClientHandles.GetValue(i));//标签唯一标识
                    Tag tag = Tags.Where(p => p.ExtraAs<DaExtra>().ItmHandleClient == HandleClient).FirstOrDefault();//根据句柄搜索对应的标签
                    //标签值
                    tag.Value = ItemValues.GetValue(i).ToString();
                    //标签时间戳
                    tag.TimeStamps = TimeStamps.GetValue(i).ToString();
                    //标签质量
                    switch (Convert.ToInt32(Qualities.GetValue(i)))
                    {
                        case (int)(OPCQuality.OPCQualityGood):
                            tag.Qualities = "Good";
                            break;
                        case (int)(OPCQuality.OPCQualityUncertain):
                            tag.Qualities = "Uncertain";
                            break;
                        case (int)(OPCQuality.OPCQualityBad):
                            tag.Qualities = "Bad";
                            break;
                        default:
                            break;
                    }
                }
                //Console.WriteLine("**********" + daTagsData[0].Value + "*******" + daTagsData[1].Value + "*******" + daTagsData[2].Value + "*******" + daTagsData[3].Value);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }
        #endregion
        #region IDisposable

        /// <summary>
        /// 获取是否已释放
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 关闭和释放所有相关资源
        /// </summary>
        public void Dispose()
        {
            // 如果资源未释放 这个判断主要用了防止对象被多次释放
            if (this.IsDisposed == false)
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
            this.IsDisposed = true;
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~DaGroup()
        {
            this.Dispose(false);
        }
        /// <summary>
        /// 释放资源
        ///参数为true表示释放所有资源，只能由使用者调用
        //参数为false表示释放非托管资源，只能由垃圾回收器自动调用
        //如果子类有自己的非托管资源，可以重载这个函数，添加自己的非托管资源的释放
        //但是要记住，重载此函数必须保证调用基类的版本，以保证基类的资源正常释放
        /// </summary>
        /// <param name="disposing">是否也释放托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
            //释放非托管资源
            //this.OPCGroup.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(this.KepGroup_DataChange);
            //this.Tags.CollectionChanged -= this.TagsCollectionChanged;
            //this.OPCGroup = null;            
            // 释放托管资源(一般用不到，不需要手动释放，依赖垃圾回收器自动回收)
            if (disposing)
            {
                //暂无
            }
        }
        #endregion
    }
}
