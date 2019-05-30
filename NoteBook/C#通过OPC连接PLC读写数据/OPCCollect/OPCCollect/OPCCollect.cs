using opc;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPCCollect
{
    public class OPCCollect
    {
        private OPC opc;
        public OPCGroup group1;
        public OPCGroup group2 ;
        public OPCGroup group3 ;
        public OPCGroup group4 ;
        public OPCGroup group5 ;
        public OPCGroup group6 ;
        public OPCGroup group7 ;
        public OPCGroup group8 ;
        public OPCGroup group9 ;
        public OPCGroup group10 ;
        public OPCGroup group11 ;
        public OPCGroup group12 ;
        public OPCGroup group13 ;
        public OPCGroup group14 ;
        public OPCGroup group15;
        public OPCGroup group16 ;
        public OPCGroup group17;
        public OPCGroup group18 ;
        public OPCGroup group19 ;
        public OPCGroup group20 ;
        public OPCGroup group21 ;
        public OPCGroup group22 ;
        public List<OPCItem> items1 = new List<OPCItem>();
        public List<OPCItem> items2 = new List<OPCItem>();
        public List<OPCItem> items3 = new List<OPCItem>();
        public List<OPCItem> items4 = new List<OPCItem>();
        public List<OPCItem> items5 = new List<OPCItem>();
        public List<OPCItem> items6 = new List<OPCItem>();
        public List<OPCItem> items7 = new List<OPCItem>();
        public List<OPCItem> items8 = new List<OPCItem>();
        public List<OPCItem> items9 = new List<OPCItem>();
        public List<OPCItem> items10 = new List<OPCItem>();
        public List<OPCItem> items11 = new List<OPCItem>();
        public List<OPCItem> items12 = new List<OPCItem>();
        public List<OPCItem> items13 = new List<OPCItem>();
        public List<OPCItem> items14 = new List<OPCItem>();
        public List<OPCItem> items15 = new List<OPCItem>();
        public List<OPCItem> items16 = new List<OPCItem>();
        public List<OPCItem> items17 = new List<OPCItem>();
        public List<OPCItem> items18 = new List<OPCItem>();
        public List<OPCItem> items19 = new List<OPCItem>();
        public List<OPCItem> items20 = new List<OPCItem>();
        public List<OPCItem> items21 = new List<OPCItem>();
        public List<OPCItem> items22 = new List<OPCItem>();

        public OPCCollect()
        {
            opc = OPC.GetSingle;
            opc.ConnectOPC("KEPware.KEPServerEx.V6", "127.0.0.1");
        }
        public void AddGroups()
        {
            #region 添加组
            group1 = opc.AddGroup("MQF04", 100, true, GroupAsyncReadComplete, null, null);
            group2 = opc.AddGroup("MQF05", 100, true, GroupAsyncReadComplete, null, null);
            group3 = opc.AddGroup("MQF06", 100, true, GroupAsyncReadComplete, null, null);
            group4 = opc.AddGroup("MQZ01", 100, true, GroupAsyncReadComplete, null, null);
            group5 = opc.AddGroup("MQZ02", 100, true, GroupAsyncReadComplete, null, null);
            group6 = opc.AddGroup("MQZ03", 100, true, GroupAsyncReadComplete, null, null);
            group7 = opc.AddGroup("DP01", 100, true, GroupAsyncReadComplete, null, null);
            group8 = opc.AddGroup("DP02", 100, true, GroupAsyncReadComplete, null, null);
            group9 = opc.AddGroup("DP03", 100, true, GroupAsyncReadComplete, null, null);
            group10 = opc.AddGroup("DP04", 100, true, GroupAsyncReadComplete, null, null);
            group11 = opc.AddGroup("DP05", 100, true, GroupAsyncReadComplete, null, null);
            group12 = opc.AddGroup("DP06", 100, true, GroupAsyncReadComplete, null, null);
            group13 = opc.AddGroup("DP07", 100, true, GroupAsyncReadComplete, null, null);
            group14 = opc.AddGroup("HJGF", 100, true, GroupAsyncReadComplete, null, null);
            group15 = opc.AddGroup("HJGZ", 100, true, GroupAsyncReadComplete, null, null);
            group16 = opc.AddGroup("HJJBJF03", 100, true, GroupAsyncReadComplete, null, null);
            group17 = opc.AddGroup("HJJBJZ01", 100, true, GroupAsyncReadComplete, null, null);
            group18 = opc.AddGroup("HJJBJZ02", 100, true, GroupAsyncReadComplete, null, null);
            group19 = opc.AddGroup("JBFJT", 100, true, GroupAsyncReadComplete, null, null);
            group20 = opc.AddGroup("TBFJW", 100, true, GroupAsyncReadComplete, null, null);
            group21 = opc.AddGroup("TBZJT", 100, true, GroupAsyncReadComplete, null, null);
            group22 = opc.AddGroup("TBZJW", 100, true, GroupAsyncReadComplete, null, null);
            #endregion
        }
        public void AddItems()
        {
            #region 给各组添加项
            string s = "裁片.MQF04.PT";
            for (int i = 1; i <= 78; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items1.Add(group1.AddItem(s1, i - 1));
            }
            s = "裁片.MQF05.PT";
            for (int i = 1; i <= 78; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items2.Add(group2.AddItem(s1, i - 1));
            }
            s = "裁片.MQF06.PT";
            for (int i = 1; i <= 78; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items3.Add(group3.AddItem(s1, i - 1));
            }
            s = "裁片.MQZ01.PT";
            for (int i = 1; i <= 78; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items4.Add(group4.AddItem(s1, i - 1));
            }
            s = "裁片.MQZ02.PT";
            for (int i = 1; i <= 78; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items5.Add(group5.AddItem(s1, i - 1));
            }
            s = "裁片.MQZ03.PT";
            for (int i = 1; i <= 78; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items6.Add(group6.AddItem(s1, i - 1));
            }
            s = "叠片.DP01.PT";
            for (int i = 1; i <= 30; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items7.Add(group7.AddItem(s1, i - 1));
            }
            s = "叠片.DP02.PT";
            for (int i = 1; i <= 30; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items8.Add(group8.AddItem(s1, i - 1));
            }
            s = "叠片.DP03.PT";
            for (int i = 1; i <= 30; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items9.Add(group9.AddItem(s1, i - 1));
            }
            s = "叠片.DP04.PT";
            for (int i = 1; i <= 30; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items10.Add(group10.AddItem(s1, i - 1));
            }
            s = "叠片.DP05.PT";
            for (int i = 1; i <= 30; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items11.Add(group11.AddItem(s1, i - 1));
            }
            s = "叠片.DP06.PT";
            for (int i = 1; i <= 30; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items12.Add(group12.AddItem(s1, i - 1));
            }
            s = "叠片.DP07.PT";
            for (int i = 1; i <= 30; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items13.Add(group13.AddItem(s1, i - 1));
            }
            s = "合浆.HJGF.PT";
            for (int i = 1; i <= 4; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items14.Add(group14.AddItem(s1, i - 1));
            }
            s = "合浆.HJGZ.PT";
            for (int i = 1; i <= 4; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items15.Add(group15.AddItem(s1, i - 1));
            }
            s = "合浆.HJJBJF03.PT";
            for (int i = 1; i <= 40; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items16.Add(group16.AddItem(s1, i - 1));
            }
            s = "合浆.HJJBJZ01.PT";
            for (int i = 1; i <= 40; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items17.Add(group17.AddItem(s1, i - 1));
            }
            s = "合浆.HJJBJZ02.PT";
            for (int i = 1; i <= 40; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items18.Add(group18.AddItem(s1, i - 1));
            }
            s = "涂布.TBFJT.PT";
            for (int i = 1; i <= 133; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items19.Add(group19.AddItem(s1, i - 1));
            }
            s = "涂布.TBFJW.PT";
            for (int i = 1; i <= 132; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items20.Add(group20.AddItem(s1, i - 1));
            }
            s = "涂布.TBZJT.PT";
            for (int i = 1; i <= 136; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items21.Add(group21.AddItem(s1, i - 1));
            }
            s = "涂布.TBZJW.PT";
            for (int i = 1; i <= 135; i++)
            {
                string s1 = s;
                if (i < 10) s1 += "0";
                s1 += i.ToString();
                items22.Add(group22.AddItem(s1, i - 1));
            }
            #endregion
        }
        public void GroupAsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            string result = "";
            for (int i = 1; i <= NumItems; i++)
            {
                result += $",{ItemValues.GetValue(i)}";
            }
            result.TrimEnd(',');
            //Console.WriteLine(result);
            Console.WriteLine($"{TransactionID},{NumItems}");
            Program.counter++;
            Program.state = false;
        }
    }
}
