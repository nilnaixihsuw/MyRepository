using DevExpress.XtraEditors.Repository;

using System.Collections;

namespace Mediinfo.WinForm.HIS.Controls
{
    public class CreateTabHeaderControlHelper
    {
        static CreateTabHeaderControlHelper()
        {
            if (Instance == null)
            {
                lock (obj)
                {
                    if (Instance == null)
                    {
                        Instance = new CreateTabHeaderControlHelper();
                    }
                }
            }

        }
        private static readonly object obj = new object();

        public static CreateTabHeaderControlHelper Instance;

        /// <summary>
        /// 控件集合
        /// </summary>
        public ControlCollection controlCollection = new ControlCollection();
    }

    public class ControlCollection : CollectionBase
    {
        public ControlCollection()
        {

        }

        public ControlCollection(ControlCollection value)
        {
            this.AddRange(value);
        }

        public ControlCollection(TabHeaderControl[] value)
        {
            this.AddRange(value);
        }

        public TabHeaderControl this[int index]
        {
            get { return ((TabHeaderControl)(List[index])); }
            set { List[index] = value; }
        }

        public int Add(TabHeaderControl value)
        {
            return List.Add(value);
        }

        public void AddRange(TabHeaderControl[] value)
        {
            for (int Counter = 0; Counter < value.Length; Counter = Counter + 1)
            {
                if (value[Counter] == null) continue;   // add by zhukunpin 增加非空排除，避免报错
                this.Add(value[Counter]);
            }
        }

        public void AddRange(ControlCollection value)
        {
            for (int Counter = 0; Counter < value.Count; Counter = Counter + 1)
            {
                this.Add(value[Counter]);
            }
        }

        public void Remove(TabHeaderControl value)
        {
            List.Remove(value);
        }

        public void Insert(int index, TabHeaderControl value)
        {
            List.Insert(index, value);
        }

        public bool Contains(TabHeaderControl value)
        {
            return List.Contains(value);
        }

        public int IndexOf(TabHeaderControl value)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if ((TabHeaderControl)List[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public string FindById(int documentId)
        {
            foreach (string document in List)
            {
                if (document == (string)List[documentId])
                {
                    return document;
                }
            }
            return null;
        }

        public void CopyTo(TabHeaderControl[] array, int index)
        {
            List.CopyTo(array, index);
        }

        public new ControlCollectionEnumerator GetEnumerator()
        {
            return new ControlCollectionEnumerator(this);
        }
    }

    public class ControlCollectionEnumerator : IEnumerator
    {
        private IEnumerator Base;
        private IEnumerable Local;

        public object Current
        {
            get { return Base.Current; }
        }

        public ControlCollectionEnumerator(ControlCollection Controls)
        {
            Local = Controls;
            Base = Local.GetEnumerator();
        }

        public bool MoveNext()
        {
            return Base.MoveNext();
        }

        public void Reset()
        {
            Base.Reset();
        }
    }

    /// <summary>
    /// 头部控件创建类
    /// </summary>
    public class TabHeaderControl
    {
        /// <summary>
        /// 控件
        /// </summary>
        public RepositoryItem RepositoryItem { get; set; }

        /// <summary>
        /// 控件信息
        /// </summary>
        public EditorControlInfo EditorControlInfo { get; set; }
    }
}
