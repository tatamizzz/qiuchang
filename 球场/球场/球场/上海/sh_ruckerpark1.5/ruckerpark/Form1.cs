using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Net;
using System.IO;
using System.Xml;
namespace ruckerpark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static DataTable mydt1 = new DataTable();
        static DataTable mydt2 = new DataTable();
        static DataTable courts = new DataTable();
        static DateTime mydatetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        static DateTime start_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void 点卡销售ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void 点卡续费ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f4=new Form4();
            f4.ShowDialog();
        }

        private void 统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.ShowDialog();
        }

        private void 借球ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.ShowDialog();
        }

        private void 订货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            goods_order go = new goods_order();
            go.ShowDialog();
        }

        private void 添加管理员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adduser adu = new adduser();
            adu.ShowDialog();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void 销售ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saledrink sd = new saledrink();
            sd.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripMenuItem2.ShortcutKeys = Keys.F1;
            借球ToolStripMenuItem.ShortcutKeys = Keys.F2;
            点卡销售ToolStripMenuItem.ShortcutKeys = Keys.F3;
            点卡续费ToolStripMenuItem.ShortcutKeys = Keys.F4;
            会员信息查询ToolStripMenuItem.ShortcutKeys = Keys.F5;
            new_court.ShortcutKeys=Keys.F6;
            court_jz.ShortcutKeys = Keys.F7;
            sale.ShortcutKeys =Keys.F8;
            initdg();  
            parametersf.dgf1 =parametersf.dgf2 = false;
            timer1.Start();
            this.update_sale();
            this.current_model();
        }

        private void initdg()
        {
            dataGridView1.AllowUserToOrderColumns = false;
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);

            string commstr = "select cardid as '卡号',convert(varchar(50),begdatetime,25) as '开卡时间',enddatetime,ball_number as '球号' from card_consumption where cardid in(select cardid from vipcard) and begdatetime<'" + mydatetime.ToString() + "' and begdatetime>='" + DateTime.Now.Date + "' order by begdatetime desc";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);

            mydt1.Columns.Add("行号");
            
            new SqlDataAdapter(mycomm).Fill(mydt1);
            dataGridView1.DataSource = mydt1;
            mydt1.Columns.Add("状态");
            dataGridView1.Columns[3].Visible = false;
            //标题行居中未解决
            for (int i = 0; i < mydt1.Rows.Count; i++)
            {
                if (mydt1.Rows[i][3].ToString() != "")
                {
                    mydt1.Rows[i][5] = "已结账";
                    mydt1.Rows[i][0] = i + 1;
                    mydt1.Rows[i][2] = Convert.ToDateTime(mydt1.Rows[i][2].ToString()).ToString("d-H:m");
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.SlateGray;
                    dataGridView1.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    mydt1.Rows[i][5] = "在场内";
                    mydt1.Rows[i][2] = Convert.ToDateTime(mydt1.Rows[i][2].ToString()).ToString("d-H:m");
                    mydt1.Rows[i][0] = i + 1;
                    dataGridView1.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            //非会员列表
            mydt2.Columns.Add("行号");
            commstr = "select cardid as '卡号',convert(varchar(50),begdatetime,25) as '开卡时间',enddatetime,ball_number as '球号' from card_consumption where cardid not in(select cardid from vipcard) and begdatetime<'" + mydatetime.ToString() + "' and begdatetime>='" + DateTime.Now.Date + "' order by begdatetime desc";
            mycomm.CommandText = commstr;
            new SqlDataAdapter(mycomm).Fill(mydt2);
            dataGridView2.DataSource = mydt2;
            mydt2.Columns.Add("状态");
            dataGridView2.Columns[3].Visible = false;
            //标题行居中未解决
            for (int i = 0; i < mydt2.Rows.Count; i++)
            {
              
                if (mydt2.Rows[i][3].ToString() != "")
                {
                    mydt2.Rows[i][5] = "已结账";
                    mydt2.Rows[i][2] = Convert.ToDateTime(mydt2.Rows[i][2].ToString()).ToString("d-H:m");
                    mydt2.Rows[i][0] = i + 1;
                    dataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.SlateGray;
                    dataGridView2.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    mydt2.Rows[i][5] = "在场内";
                    mydt2.Rows[i][2] = Convert.ToDateTime(mydt2.Rows[i][2].ToString()).ToString("d-H:m");
                    mydt2.Rows[i][0] = i + 1;
                    dataGridView2.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }

            commstr = "select court_name as '包场人',court_number as '场地号',court_price as '包场价格',court_tel as '联系电话',convert(varchar(50),court_start,25) as '开始包场时间',court_end from court where court_start<='" + mydatetime.ToString() + "' and court_start>='" + DateTime.Now.Date + "' order by court_start desc";
            mycomm.CommandText = commstr;

            new SqlDataAdapter(mycomm).Fill(courts);
            court_list.DataSource = courts;
            court_list.Columns[5].Visible = false;
            courts.Columns.Add("状态");
            //标题行居中未解决
            for (int i = 0; i < courts.Rows.Count; i++)
            {
                if (courts.Rows[i][5].ToString() != "")
                {
                    courts.Rows[i][6] = "已结账";
                    courts.Rows[i][4] = Convert.ToDateTime(courts.Rows[i][4].ToString()).ToString("d-H:m"); ;
                    court_list.Rows[i].DefaultCellStyle.ForeColor = Color.SlateGray;
                    court_list.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    courts.Rows[i][6] = "在场内";
                    courts.Rows[i][4] = Convert.ToDateTime(courts.Rows[i][4].ToString()).ToString("d-H:m"); ;
                    court_list.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
            this.current_total();

        }


        public void reflashdg()
        {
            dataGridView1.AllowUserToOrderColumns = false;
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select cardid as '卡号',convert(varchar(50),begdatetime,25) as '开卡时间',enddatetime,ball_number as '球号' from card_consumption where cardid in(select cardid from vipcard) and begdatetime<'" + mydatetime.ToString() + "' and begdatetime>='" + DateTime.Now.Date + "' order by begdatetime desc";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);

            mydt1.Clear();
            new SqlDataAdapter(mycomm).Fill(mydt1);
            dataGridView1.DataSource = mydt1;
            
            dataGridView1.Columns[3].Visible = false;
            //标题行居中未解决
            for (int i = 0; i < mydt1.Rows.Count; i++)
            {
                if (mydt1.Rows[i][3].ToString() != "")
                {
                    mydt1.Rows[i][5] = "已结账";
                    mydt1.Rows[i][0] = i + 1;
                    mydt1.Rows[i][2] = Convert.ToDateTime(mydt1.Rows[i][2].ToString()).ToString("d-H:m");
                    dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.SlateGray;
                    dataGridView1.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    mydt1.Rows[i][5] = "在场内";
                    mydt1.Rows[i][2] = Convert.ToDateTime(mydt1.Rows[i][2].ToString()).ToString("d-H:m");
                    mydt1.Rows[i][0] = i + 1;
                    dataGridView1.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }

            //非会员列表
            mydt2.Clear();
            commstr = "select cardid as '卡号',convert(varchar(50),begdatetime,25) as '开卡时间',enddatetime,ball_number as '球号' from card_consumption where cardid not in(select cardid from vipcard) and begdatetime<'" + mydatetime.ToString() + "' and begdatetime>='" + DateTime.Now.Date + "' order by begdatetime desc";
            mycomm.CommandText = commstr;
            new SqlDataAdapter(mycomm).Fill(mydt2);
            dataGridView2.DataSource = mydt2;
            
            dataGridView2.Columns[3].Visible = false;
            //标题行居中未解决
            for (int i = 0; i < mydt2.Rows.Count; i++)
            {
                if (mydt2.Rows[i][3].ToString() != "")
                {
                    mydt2.Rows[i][5] = "已结账";
                    mydt2.Rows[i][2] = Convert.ToDateTime(mydt2.Rows[i][2].ToString()).ToString("d-H:m");
                    mydt2.Rows[i][0] = i + 1;
                    dataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.SlateGray;
                    dataGridView2.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    mydt2.Rows[i][5] = "在场内";
                    mydt2.Rows[i][2] = Convert.ToDateTime(mydt2.Rows[i][2].ToString()).ToString("d-H:m");
                    mydt2.Rows[i][0] = i + 1;
                    dataGridView2.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }

            courts.Clear();
            commstr = "select court_name as '包场人',court_number as '场地号',court_price as '包场价格',court_tel as '联系电话',convert(varchar(50),court_start,25) as '开始包场时间',court_end from court where court_start<='" + mydatetime.ToString() + "' and court_start>='" + DateTime.Now.Date + "' order by court_start desc";
            mycomm.CommandText = commstr;
            new SqlDataAdapter(mycomm).Fill(courts);
            court_list.DataSource = courts;
            court_list.Columns[5].Visible = false;
            //标题行居中未解决
            for (int i = 0; i < courts.Rows.Count; i++)
            {
                if (courts.Rows[i][5].ToString() != "")
                {
                    courts.Rows[i][6] = "已结账";
                    court_list.Rows[i].DefaultCellStyle.ForeColor = Color.SlateGray;
                    courts.Rows[i][4] = Convert.ToDateTime(courts.Rows[i][4].ToString()).ToString("d-H:m");
                    court_list.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    courts.Rows[i][6] = "在场内";
                    courts.Rows[i][4] = Convert.ToDateTime(courts.Rows[i][4].ToString()).ToString("d-H:m");
                    court_list.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
            this.current_total();
            this.current_model();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            reflashdg();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (parametersf.dgf1)
            {
                reflashdg();
                parametersf.dgf1 = false;
                parametersf.dgf2 = false;
                this.current_model();
            }
        }

        private void 会员信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            user_info_find uif = new user_info_find();
            uif.ShowDialog();
        }

        private void 会员信息全览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            all_members_info ami = new all_members_info();
            ami.ShowDialog();
        }

        private void 商品信息录入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.ShowDialog();
        }

        private void 商品信息修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            goods_info_modify gim = new goods_info_modify();
            gim.ShowDialog();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView2_Sorted(object sender, EventArgs e)
        {
            reflashdg();
        }

        private void 验货ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            goods_order_check goc = new goods_order_check();
            goc.ShowDialog();
        }

        private void 当前库存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            goods_stock_now gsn = new goods_stock_now();
            gsn.ShowDialog();
        }

        private void 补卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fill_card fc = new fill_card();
            fc.ShowDialog();
        }

        private void new_court_Click(object sender, EventArgs e)
        {
            Form10 court = new Form10();
            court.ShowDialog();
        }

        private void all_court_Click(object sender, EventArgs e)
        {
            Form11 court = new Form11();
            court.ShowDialog();
        }

        private void current_total()
        {
            string commstr = "select sum(xf_point) as point,sum(xf_money) as account from card_consumption where begdatetime<'" + mydatetime.ToString() + "' and begdatetime>='" + DateTime.Now.Date + "'";
            string connstr = ConfigurationManager.AppSettings["connectionstring"];

            SqlConnection conn =new SqlConnection(connstr);
            SqlCommand comm = new SqlCommand(commstr, conn);
            comm.CommandText = commstr;
            conn.Open();
            SqlDataReader rs = comm.ExecuteReader();
            rs.Read();

            string point = rs[0].ToString();
            string money = rs[1].ToString();
            if (point == "") point = "0";
            if (money == "") money = "0";
            commstr = "select sum(xf_money) as court from court where court_start>'" + DateTime.Now.Date + "' and court_start<='" + mydatetime.ToString() + "'";
            comm.CommandText = commstr;
            rs.Close();
            string court_total = comm.ExecuteScalar().ToString();
            double court = court_total!=""?double.Parse(court_total):0;
            if (court < 0) court = 0;
            Total.Text = "当前会员收款：" + point + "元，散客：" + money + "元" + "，包场费用：" + court.ToString();
        }

        private void SubmitAllData_Click(object sender, EventArgs e)
        {
            this.SubmitCardConsum();
            this.Submitgoods_sale();
            this.Submitgoods();
            this.SubmitVipCard();
            this.SubmitVipCard_cz();
            this.SubmitVipCard_sale();
            this.SubmitCourt();
            MessageBox.Show("数据提交成功", "提示");
        }

        private bool Submitgoods()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string court = ConfigurationManager.AppSettings["court"];
            string posturl = "http://peak.hoopchina.com/index.php?c=request&a=goods";
            string postdata = "court=" + court;

            string lastsubmit = ConfigurationManager.AppSettings["goods"];
            if (lastsubmit == "") lastsubmit = "0";

            SqlConnection myconn = new SqlConnection(connstr);

            string commstr = "select goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_price,adddate,id from goods_info where id>'" + lastsubmit + "' order by id";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            mycomm.CommandText = commstr;
            myconn.Open();

            string data = "";
            string rows = "";

            SqlDataReader reader = mycomm.ExecuteReader();

            while (reader.Read())
            {
                rows = "";
                rows = String.Format("{0}::::{1}::::{2}::::{3}::::{4}::::{5}::::{6}||", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6]);
                data = data + rows;
                lastsubmit = reader[6].ToString();
            }
            postdata = postdata + "&data=" + data;
            bool result = false;
            if (this.PostData(posturl, postdata) == "ok") result = true;
            if (result) this.SetValue("goods", lastsubmit);
            return result;
        }

        private bool Submitgoods_sale()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string court = ConfigurationManager.AppSettings["court"];
            string posturl = "http://peak.hoopchina.com/index.php?c=request&a=goodsale";
            string postdata = "court=" + court;
            string lastsubmit = ConfigurationManager.AppSettings["goods_sale"];

            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select [name],num,price,cardid,num_day,id from sale where id>'" + lastsubmit + "' order by id";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            mycomm.CommandText = commstr;
            myconn.Open();

            string data = "";
            string rows = "";

            SqlDataReader reader = mycomm.ExecuteReader();

            while (reader.Read())
            {
                rows = "";
                rows = String.Format("{0}::::{1}::::{2}::::{3}::::{4}::::{5}||", reader[0], reader[1], reader[2], reader[3], reader[4],reader[5]);
                data = data + rows;
                lastsubmit = reader[4].ToString();
            }
            postdata = postdata + "&data=" + data;
            bool result = false;
            if (this.PostData(posturl, postdata) == "ok") result = true;
            if (result) this.SetValue("goods_sale", lastsubmit);
            return result;
        }

        private bool SubmitCardConsum()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string court = ConfigurationManager.AppSettings["court"];
            string posturl = "http://peak.hoopchina.com/index.php?c=request&a=consum";
            string postdata = "court=" + court;
            string lastsubmit = ConfigurationManager.AppSettings["cardconsum"];
            if (lastsubmit == "" || lastsubmit == null) lastsubmit = "0";

            SqlConnection myconn = new SqlConnection(connstr);

            string commstr = "select a.cardid,a.begdatetime,a.enddatetime,a.ball_number,a.xf_point,a.xf_money,b.cardid as uid,a.id from card_consumption as a left join vipcard as b on b.cardid=a.cardid and a.enddatetime is not null and a.id>'" + lastsubmit + "' order by a.id";

            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            mycomm.CommandText = commstr;
            myconn.Open();

            string data = "";
            string rows = "";

            SqlDataReader reader = mycomm.ExecuteReader();

            while (reader.Read())
            {
                rows = "";
                rows = String.Format("{0}::::{1}::::{2}::::{3}::::{4}::::{5}::::{6}::::{7}||", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7]);
                data = data + rows;
                lastsubmit = reader[7].ToString();
            }
            postdata = postdata + "&data=" + data;

            bool result = false;
            if (this.PostData(posturl, postdata) == "ok") result = true;
            if (result) this.SetValue("cardconsum", lastsubmit);
            return result;
        }

        private bool SubmitVipCard_sale()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string court = ConfigurationManager.AppSettings["court"];
            string posturl = "http://peak.hoopchina.com/index.php?c=request&a=cardsale";
            string postdata = "court=" + court;
            string lastsubmit = ConfigurationManager.AppSettings["cardsale"];
            if (lastsubmit == "") lastsubmit = "0";
            SqlConnection myconn = new SqlConnection(connstr);

            string commstr = "select cardid,salemoney,adddatetime,id from vipcard_sale where id>'" + lastsubmit + "' order by id";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            mycomm.CommandText = commstr;
            myconn.Open();

            string data = "";
            string rows = "";

            SqlDataReader reader = mycomm.ExecuteReader();

            while (reader.Read())
            {
                rows = "";
                rows = String.Format("{0}::::{1}::::{2}::::{3}||", reader[0], reader[1], reader[2], reader[3]);
                data = data + rows;
                lastsubmit = reader[3].ToString();
            }
            postdata = postdata + "&data=" + data;
            bool result = false;
            if (this.PostData(posturl, postdata) == "ok") result = true;
            if (result) this.SetValue("cardsale", lastsubmit);
            return result;
        }

        private bool SubmitVipCard_cz()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string court = ConfigurationManager.AppSettings["court"];
            string posturl = "http://peak.hoopchina.com/index.php?c=request&a=cardcz";
            string postdata = "court=" + court;
            string lastsubmit = ConfigurationManager.AppSettings["cardcz"];
            if (lastsubmit == "") lastsubmit = "0";
            SqlConnection myconn = new SqlConnection(connstr);

            string commstr = "select cardid,chongzhi,adddatetime,id from vipcard_cz where id>'" + lastsubmit + "' order by id";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            mycomm.CommandText = commstr;
            myconn.Open();

            string data = "";
            string rows = "";

            SqlDataReader reader = mycomm.ExecuteReader();

            while (reader.Read())
            {
                rows = "";
                rows = String.Format("{0}::::{1}::::{2}::::{3}||", reader[0], reader[1], reader[2], reader[3]);
                data = data + rows;
                lastsubmit = reader[3].ToString();
            }
            postdata = postdata + "&data=" + data;

            bool result = false;
            if (this.PostData(posturl, postdata) == "ok") result = true;
            if (result) this.SetValue("cardcz", lastsubmit);
            return result;
        }

        private bool SubmitVipCard()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string court = ConfigurationManager.AppSettings["court"];
            string posturl = "http://peak.hoopchina.com/index.php?c=request&a=vipcard";
            string postdata = "court=" + court;
            string lastsubmit = ConfigurationManager.AppSettings["vip"];
            if (lastsubmit == "") lastsubmit = "0";
            SqlConnection myconn = new SqlConnection(connstr);

            string commstr = "select cardid,m_name,m_age,m_qq,m_mobile,adddate,point,id from vipcard where id>'" + lastsubmit + "' order by id";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            mycomm.CommandText = commstr;
            myconn.Open();

            string data = "";
            string rows = "";

            SqlDataReader reader = mycomm.ExecuteReader();

            while (reader.Read())
            {
                rows = "";
                rows = String.Format("{0}::::{1}::::{2}::::{3}::::{4}::::{5}::::{6}::::{7}||", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7]);
                data = data + rows;
                lastsubmit = reader[7].ToString();
            }
            postdata = postdata + "&data=" + data;
            bool result = false;
            if (this.PostData(posturl, postdata) == "ok") result = true;
            if (result) this.SetValue("vip", lastsubmit);
            return result;
        }

        private bool SubmitCourt()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string court = ConfigurationManager.AppSettings["court"];
            string posturl = "http://peak.hoopchina.com/index.php?c=request&a=courts";
            string postdata = "court=" + court;
            string lastsubmit = ConfigurationManager.AppSettings["courts"];
            if (lastsubmit == "") lastsubmit = "0";
            SqlConnection myconn = new SqlConnection(connstr);

            string commstr = "select court_name,court_number,court_per_num,court_price,court_tel,court_start,court_end,xf_money,court_id from court where court_end is not null and court_id>'" + lastsubmit + "' order by court_id";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            mycomm.CommandText = commstr;
            myconn.Open();

            string data = "";
            string rows = "";

            SqlDataReader reader = mycomm.ExecuteReader();

            while (reader.Read())
            {
                rows = "";
                rows = String.Format("{0}::::{1}::::{2}::::{3}::::{4}::::{5}::::{6}::::{7}::::{8}||", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7],reader[8]);
                data = data + rows;
                lastsubmit = reader[7].ToString();
            }
            postdata = postdata + "&data=" + data;
            bool result = false;
            if (this.PostData(posturl, postdata) == "ok") result = true;
            if (result) this.SetValue("vip", lastsubmit);
            return result;
        }

        private string PostData(string strUrl, string strParm)
        {
            Encoding encode = System.Text.Encoding.Default;
            byte[] arrB = encode.GetBytes(strParm);
            string strBaseUrl = null;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(strUrl);
            myReq.Method = "POST";
            myReq.ContentType = "application/x-www-form-urlencoded";
            myReq.ContentLength = arrB.Length;
            Stream outStream = myReq.GetRequestStream();
            outStream.Write(arrB, 0, arrB.Length);
            outStream.Close();
            WebResponse myResp = null;
            try
            {
                myResp = myReq.GetResponse();
            }
            catch (Exception e)
            {
                int ii = 0;
            }
            Stream ReceiveStream = myResp.GetResponseStream();
            StreamReader readStream = new StreamReader(ReceiveStream, encode);
            Char[] read = new Char[256];
            int count = readStream.Read(read, 0, 256);
            string str = null;
            while (count > 0)
            {
                str += new String(read, 0, count);
                count = readStream.Read(read, 0, 256);
            }
            readStream.Close();
            myResp.Close();
            return str;
        }

        private void current_model()
        {
            bool holiday = this.check_holiday();
            DateTime dt = DateTime.Now;
            int nowdate = int.Parse(DateTime.Now.ToString("Hmm"));
            string key="";
            string text = "";
            if (holiday == true) {
                key = "holiday";
                text = "当前是：节假日收费模式";
            }
            if (nowdate >= 1730 && holiday == false) {
                key = "pm";
                text = "当前是：非节假日晚上收费模式";
            }

            if (nowdate < 1730 && holiday == false) {
                key = "am";
                text = "当前是：非节假日白天收费模式";
            }
            string money=ConfigurationManager.AppSettings[key];
            string [] moneys = money.Split('|');
            text = text + moneys[0]+"元/小时,最低消费"+moneys[1]+"元,畅打"+moneys[2]+"元,超时每十分钟"+moneys[3]+"元";
            module.Text = text;
        }

        private string current_money()
        {
            bool holiday = this.check_holiday();
            DateTime dt = DateTime.Now;
            int nowdate = int.Parse(DateTime.Now.ToString("Hmm"));
            string key = "";
            if (holiday == true) key = "holiday";
            if (nowdate >= 1730 && holiday == false) key = "pm";
            if (nowdate < 1730 && holiday == false) key = "am";
            return key;
        }

        private bool check_holiday()
        {
            int date = int.Parse(DateTime.Now.ToString("Md"));
            DateTime dt = DateTime.Now;
            string holiday = ConfigurationManager.AppSettings["holiday_list"];
            string [] holiday_list=holiday.Split(',');
            foreach (string day in holiday_list)
            {
                int fday = int.Parse(day);
                if (fday == date) return true;
            }
            string week = Convert.ToDateTime(dt).DayOfWeek.ToString();
            if (week == "Sunday" || week == "Saturday") return true;
            return false;
        }

        private void court_jz_Click(object sender, EventArgs e)
        {
            Form12 fm = new Form12();
            fm.ShowDialog();
        }

        private void integral_Click(object sender, EventArgs e)
        {
            user_info_find uif = new user_info_find();
            uif.ShowDialog();
        }

        private void integral_manage_Click(object sender, EventArgs e)
        {
            integral_manage integral = new integral_manage();
            integral.ShowDialog();
        }

        private void sale_Click(object sender, EventArgs e)
        {
            //testdbutton tt = new testdbutton();
            //tt.ShowDialog();
            sale salecs = new sale();
            salecs.ShowDialog();
        }

        private void update_sale()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            string sql = "select id,num_day from sale where len(num_day)<8";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader rs = comm.ExecuteReader();
            string query = "";
            while (rs.Read())
            {

                string numday = rs[1].ToString();
                if (numday.Length == 6 || numday.Length == 7)
                {
                    if (numday.Length == 6) numday = numday.Substring(0, 4) + "0" + numday.Substring(4, 1) + "0" + numday.Substring(5, 1);
                    if (numday.Length == 7) numday = numday.Substring(0, 4) + "0" + numday.Substring(4, 1) + "" + numday.Substring(5, 2);
                    query = query + "update sale set num_day=" + numday + "where id=" + rs[0].ToString() + ";";
                }
            }
            rs.Close();
            if (query != "") {
                comm.CommandText = query;
                comm.ExecuteNonQuery();
            }
            
        }

        private void SetValue(string AppKey, string AppValue)
        {
            XmlDocument xDoc = new XmlDocument();
            string file = Application.StartupPath + "\\ruckerpark.exe.config";

            xDoc.Load(file);
            XmlNode xNode;
            XmlElement xElem1;
            XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null)
            {
                xElem1.SetAttribute("value", AppValue);
            }
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", AppKey);
                xElem2.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(file);
        }
    }
}