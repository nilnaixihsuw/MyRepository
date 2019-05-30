using OpLink.test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;


namespace OpLink
{
    public partial class FormTest1 : Form
    {
        private IOpcClient<DaExtra> client;
        public FormTest1()
        {
            InitializeComponent();
        }

        private void FormTest1_Load(object sender, EventArgs e)
        {
            client = new DaOpc<DaExtra>();
            List<string> list = client.GetServerList();
            foreach (string turn in list)
            {
                cmbServerName.Items.Add(turn);
            }
            cmbServerName.SelectedIndex = 0;
        }

        private void btnConnServer_Click(object sender, EventArgs e)
        {
            client.Connect(cmbServerName.Text);
            List<string> branches = client.ShowBranches();
            client.CreateGroupData();

            listBoxGroups.Items.Clear();
            foreach (string item in branches)
            {
                listBoxGroups.Items.Add(item);
            }
            btnDisconnServer.Enabled = true;
            btnConnServer.Enabled = false;
           
        }

        private void listBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> leafs = client.ShowLeafs(listBoxGroups.SelectedItem.ToString());
            listView1.Items.Clear();
            foreach (string item in leafs)
            {
                listView1.Items.Add(item);
            }
            label11.Text = "个数: " + listView1.Items.Count.ToString();
            
        }

        private void btnDisconnServer_Click(object sender, EventArgs e)
        {
            client.DisConnect();
            listView1.Items.Clear();
            listBoxGroups.Items.Clear();
            dataGridView1.Rows.Clear();
            btnConnServer.Enabled = true;
            btnDisconnServer.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<DaTag> listIn = new List<DaTag>();

            foreach (ListViewItem item in listView1.CheckedItems)
            {
                DaTag bi = new DaTag();
                bi.OpcTagName = item.Text;
                bi.TagName = item.Text;
                bi.TimeStamps = "";
                bi.Value = "";
                bi.Qualities = "";
                listIn.Add(bi);
            }
            client.AddItems(listIn, OpcTypes.TagTypes.TAG_DATA);

            List<DaTag> listOut = client.GetTagValues(ref listIn, OpcTypes.TagTypes.TAG_DATA);

            for (int i = 0; i < listOut.Count; i++)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["OpcTagName"].Value = ((DaTag)listOut[i]).OpcTagName;
                dataGridView1.Rows[i].Cells["TagName"].Value = ((DaTag)listOut[i]).TagName;
                dataGridView1.Rows[i].Cells["DataType"].Value = ((DaTag)listOut[i]).DataType;
                dataGridView1.Rows[i].Cells["Value"].Value = ((DaTag)listOut[i]).Value;
                dataGridView1.Rows[i].Cells["Qualities"].Value = ((DaTag)listOut[i]).Qualities;
                dataGridView1.Rows[i].Cells["TimeStamps"].Value = ((DaTag)listOut[i]).TimeStamps;
            }

            //List<DaOpcItem> l= new List<DaOpcItem>();
            //List<DaOpcItem> p = new List<DaOpcItem>();
            //DaOpcItem d=new DaOpcItem();
            //d.OpcTagName="Channel_0_User_Defined.User.User1";
            //d.TagName = "Channel_0_User_Defined.User.User1";
            //d.GroupName = "testdata";

            //DaOpcItem f = new DaOpcItem();
            //f.OpcTagName = "Channel_0_User_Defined.User.User2";
            //f.TagName = "Channel_0_User_Defined.User.User2";
            //f.GroupName = "testdata";


            //DaOpcItem g = new DaOpcItem();
            //g.OpcTagName = "Channel_0_User_Defined.User.User3";
            //g.TagName = "Channel_0_User_Defined.User.User3";
            //g.GroupName = "testdata";


            //DaOpcItem h = new DaOpcItem();
            //h.OpcTagName = "Channel_0_User_Defined.User.User4";
            //h.TagName = "Channel_0_User_Defined.User.User4";
            //h.GroupName = "testdata";

            //l.Add(d);
            //l.Add(f);
            ////p.Add(g);
            ////p.Add(h);

            //client.AddItems("testdata", l);
            //client.AddItem("testdata", g);
            //client.AddItem("testdata", h);

            //client.AddItems("testdata", p);


            //client.AddItem("testdata", f);
            //client.AddItem("testdata", g);
            //client.AddItem("testdata", h);


        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            List<DaTag> list = new List<DaTag>();
            DaTag d = new DaTag();
            d.OpcTagName = "Channel_0_User_Defined.User.User1";
            d.TagName = "Channel_0_User_Defined.User.User1";
            d.GroupName = "testdata";
            d = client.GetTagValue(ref d, OpcTypes.TagTypes.TAG_DATA);
            
            DaTag f = new DaTag();
            f.OpcTagName = "Channel_0_User_Defined.User.User2";
            f.TagName = "Channel_0_User_Defined.User.User2";
            f.GroupName = "testdata";
            f = client.GetTagValue(ref f, OpcTypes.TagTypes.TAG_DATA);


            DaTag g = new DaTag();
            g.OpcTagName = "Channel_0_User_Defined.User.User3";
            g.TagName = "Channel_0_User_Defined.User.User3";
            g.GroupName = "testdata";
            g = client.GetTagValue(ref g, OpcTypes.TagTypes.TAG_DATA);


            Tag<DaExtra> h = new Tag<DaExtra>();
            h.OpcTagName = "Channel_0_User_Defined.User.User4";
            h.TagName = "Channel_0_User_Defined.User.User4";
            h.GroupName = "testdata";
            h = client.GetTagValue(ref h, OpcTypes.TagTypes.TAG_DATA);

            list.Add(h);
            list.Add(g);
            list.Add(f);
            list.Add(d);  

            dataGridView1.Rows.Clear();
            //2.赋值新数据  
            for (int i = 0; i < list.Count; i++)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["OpcTagName"].Value = ((Tag<DaExtra>)list[i]).OpcTagName;
                dataGridView1.Rows[i].Cells["TagName"].Value = ((Tag<DaExtra>)list[i]).TagName;
                dataGridView1.Rows[i].Cells["DataType"].Value = ((Tag<DaExtra>)list[i]).DataType;
                dataGridView1.Rows[i].Cells["Value"].Value = ((Tag<DaExtra>)list[i]).Value;
                dataGridView1.Rows[i].Cells["Qualities"].Value = ((Tag<DaExtra>)list[i]).Qualities;
                dataGridView1.Rows[i].Cells["TimeStamps"].Value = ((Tag<DaExtra>)list[i]).TimeStamps;
            } 
        }

        private void btnSetGroupPro_Click(object sender, EventArgs e)
        {

        }

        private void btnReConnServer_Click(object sender, EventArgs e)
        {
            client.Connect(cmbServerName.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            XMLReaderBase.CreateBaseXml();

            //XElement xGroups = new XElement("Groups");
            //XElement group = new XElement("Group");
            //XElement tag = new XElement("tag", new XElement("Name", "Remote"), new XElement("Age", "23"));
            //xGroups.Add(group);
            //group.Add(tag);
            //string path = string.Format("{0}\\Config\\tags.xml", Environment.CurrentDirectory);
            //xGroups.Save(path);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = string.Format("{0}\\Config\\tags.xml", Environment.CurrentDirectory);
            XElement xGroups = XElement.Load(path);
            XElement group = xGroups.Elements("Group").FirstOrDefault();
            XElement tag = group.Elements("tag").Where(p => p.Element("Name").Value == "Remote1").FirstOrDefault();
            tag.SetElementValue("Name", "Remoteppp");
            xGroups.Save(path);
        }
    }
}
