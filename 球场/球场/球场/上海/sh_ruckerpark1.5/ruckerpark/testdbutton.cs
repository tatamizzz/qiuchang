using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace ruckerpark
{
    public partial class testdbutton : Form
    {

        Button[] mybutton;
        Label[] mylabel;
        string[] prices;

        public testdbutton()
        {
            InitializeComponent();
        }

        private void testdbutton_Load(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string sqlstr = "select goods_name,goods_price from goods_info";
            SqlConnection myconn = new SqlConnection(connstr);
            DataTable mydt = new DataTable();
            new SqlDataAdapter(sqlstr, myconn).Fill(mydt);

            mybutton=new Button[mydt.Rows.Count];
            mylabel=new Label[mydt.Rows.Count];
            prices = new String[mydt.Rows.Count];
            
            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                Button a = new Button();
                a.Text = mydt.Rows[i][0].ToString();
                a.Name = "BTN" + i;
                a.Left = i % 5 * 80;
                a.Top = i / 5 * 60;
                a.Click += new EventHandler(Button_Click);
                mybutton[i] = a;
                panel1.Controls.Add(a);

                Label l = new Label();
                l.Text = "0";
                a.Name = "Lab" + i;
                l.Width = 40;
                l.Left = i % 5 * 80+30;
                l.Top = i / 5 * 60+30;
                mylabel[i] = l;
                panel1.Controls.Add(l);

                prices[i] = mydt.Rows[i][1].ToString();
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button tmpb=((Button)sender);
            mylabel[int.Parse(tmpb.Name.Substring(3))].Text = (int.Parse(mylabel[int.Parse(tmpb.Name.Substring(3))].Text) + 1).ToString();
            check_goods_stock(tmpb.Text, int.Parse(tmpb.Name.Substring(3)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vipcardid = "8888";
            for (int i = 0; i < mylabel.Length; i++)
            {
                if(mylabel[i]!=null && mylabel[i].Text!="0")
                    label1.Text = "insert into sale(num,name,price,num_day,cardid)values('" + mylabel[i].Text + "','" + mybutton[i].Text + "','" + prices[i] + "','" + DateTime.Now.ToString("yyyyMMdd") + "','" + vipcardid + "')";
            }
        }

        private void check_goods_stock(string goods_name,int i)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string sqlstr = "select goods_stock from goods_stock where goods_stock.goods_huohao=(select goods_info.goods_huohao from goods_info where goods_name='"+mybutton[i].Text+"')";
            SqlConnection myconn = new SqlConnection(connstr);
            SqlCommand mycomm = new SqlCommand(sqlstr, myconn);
            myconn.Open();
            int tmp_goods_stock=int.Parse(mycomm.ExecuteScalar().ToString());
            if (int.Parse(mylabel[i].Text) >= tmp_goods_stock)
            {
                mylabel[i].Text = tmp_goods_stock.ToString();
                mybutton[i].Enabled = false;
            }
        }
    }
}
