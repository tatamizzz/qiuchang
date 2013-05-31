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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        DataSet myds = new DataSet();

        private void Form9_Load(object sender, EventArgs e)
        {
            countdata();
            checkdata();
        }

        private void checkdata()
        {
            myds.Clear();
            myds.Dispose();
            myds.Tables.Clear();
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myds.Tables.Add();
            commstr = "select tagname as 项目,num as 数量,total as 金额 from total order by sort asc";
            mycomm.CommandText = commstr;
            mycomm.CommandType = CommandType.Text;
            new SqlDataAdapter(mycomm).Fill(myds.Tables[0]);
            showdata();
            printDocument1.DefaultPageSettings.Landscape = true;
        }

        private void countdata()
        {
            string startdate = dateTimePicker1.Value.ToString("yyyy-MM-dd")+" 00:00:00";
            string enddate = dateTimePicker2.Value.ToString("yyyy-MM-dd")+" 23:59:59";
            string start_day = dateTimePicker1.Value.ToString("yyyyMMdd");
            string end_day = dateTimePicker2.Value.ToString("yyyyMMdd");

            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            string query = "delete from total";
            SqlCommand comm = new SqlCommand(query, conn);
            comm.ExecuteNonQuery();

            query = "";
            string sql = "";
            sql = "select count(id) as num,sum(xf_money) as total from card_consumption where begdatetime>='" + startdate + "' and enddatetime<='" + enddate + "' and xf_money is not null";
            comm.CommandText = sql;
            SqlDataReader rs = comm.ExecuteReader();
            rs.Read();
            if (Convert.ToInt32(rs[0].ToString())>=1)
            {
                query = "insert into total(tagname,total,num,sort)values('非会员打球消费'," + rs[1].ToString() + "," + rs[0].ToString() + ",1)";
            }
            rs.Close();
            if (query != "")
            {
                comm.CommandText = query;
                comm.ExecuteNonQuery();
            }

            sql = "select count(id) as num,sum(xf_point) as total from card_consumption where begdatetime>='" + startdate + "' and enddatetime<='" + enddate + "' and xf_point is not null";
            comm.CommandText = sql;
            if (Convert.ToInt32(comm.ExecuteScalar()) >= 1)
            {
                rs = comm.ExecuteReader();
                rs.Read();
                query = "insert into total(tagname,total,num,sort)values('会员打球消费'," + rs[1].ToString() + "," + rs[0].ToString() + ",2)";
                rs.Close();
                comm.CommandText = query;
                comm.ExecuteNonQuery();
                
            }

            sql = "select count(id) as num,sum(salemoney) as total from vipcard_sale where adddatetime>='" + startdate + "' and adddatetime<='" + enddate + "'";
            comm.CommandText = sql;
            if (Convert.ToInt32(comm.ExecuteScalar()) >= 1)
            {
                rs = comm.ExecuteReader();
                rs.Read();
                query = "insert into total(tagname,total,num,sort)values('会员卡销售'," + rs[1].ToString() + "," + rs[0].ToString() + ",3)";
                rs.Close();
                comm.CommandText = query;
                comm.ExecuteNonQuery();
            }

            sql = "select count(id) as num,sum(chongzhi) as total from vipcard_cz where adddatetime>='" + startdate + "' and adddatetime<='" + enddate + "'";
            comm.CommandText = sql;
            if (Convert.ToInt32(comm.ExecuteScalar()) >= 1)
            {
                rs = comm.ExecuteReader();
                rs.Read();
                query = "insert into total(tagname,total,num,sort)values('会员卡充值'," + rs[1].ToString() + "," + rs[0].ToString() + ",4)";
                rs.Close();
                comm.CommandText = query;
                comm.ExecuteNonQuery();
            }

            sql = "select count(id) as num from card_consumption where begdatetime>='" + startdate + "'and ball_number is not null and xf_money is not null and begdatetime<='" + enddate + "'";
            comm.CommandText = sql;
            if (Convert.ToInt32(comm.ExecuteScalar()) >= 1)
            {
                rs = comm.ExecuteReader();
                rs.Read();
                query = "insert into total(tagname,total,num,sort)values('非会员借球'," + (Convert.ToInt16(rs[0].ToString())*5).ToString() + "," + rs[0].ToString() + ",5)";
                rs.Close();
                comm.CommandText = query;
                comm.ExecuteNonQuery();
            }

            sql = "select count(court_id) as num,sum(xf_money) as total from court where court_start>='" + startdate + "'and court_start<='" + enddate + "'";
            comm.CommandText = sql;
            if (Convert.ToInt32(comm.ExecuteScalar()) >= 1)
            {
                rs = comm.ExecuteReader();
                rs.Read();
                query = "insert into total(tagname,total,num,sort)values('包场消费'," + rs[1].ToString() + "," + rs[0].ToString() + ",5)";
                rs.Close();
                comm.CommandText = query;
                comm.ExecuteNonQuery();
            }

            sql = "select count(id) as a,sum(goods_num) as num,sum(goods_num*goods_price) as total from vipcard_goods_sale where sale_date>='" + startdate + "' and sale_date<='" + enddate + "'";
            comm.CommandText = sql;
            if (Convert.ToInt32(comm.ExecuteScalar()) >= 1)
            {
                rs = comm.ExecuteReader();
                rs.Read();
                query = "insert into total(tagname,total,num,sort)values('会员购物消费'," + rs[2].ToString() + "," + rs[1].ToString() + ",5)";
                rs.Close();
                comm.CommandText = query;
                comm.ExecuteNonQuery();
            }

            sql = "select sum(num) as num,price,name from sale where num_day>='" + start_day + "' and num_day<='" + end_day + "' group by name,price;";
            comm.CommandText = sql;
            rs = comm.ExecuteReader();
            int total = 0;
            query = "";
            while (rs.Read())
            {
                total = int.Parse(rs[0].ToString()) * int.Parse(rs[1].ToString());
                query = query + "insert into total(tagname,total,num,sort)values('"+rs[2]+"'," + total.ToString() + "," + rs[0].ToString() + ",6);";
            }
            rs.Close();
            if (query != "")
            {
                comm.CommandText = query;
                comm.ExecuteNonQuery();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            Font myfont = new Font("宋体", 8);
            Font singlefont = new Font("宋体", 14, FontStyle.Bold);
            Pen mypen = new Pen(Color.Black, 3);

            int x = 94;
            int y = 70;
            int tmp = 94;
            int cx = 95;
            double user_bball = 0;
            double user_shop = 0;
            double total_money = 0;
            string filed = "";
            for (int a = 0; a < myds.Tables[0].Columns.Count; a++)
            {
                e.Graphics.DrawString(myds.Tables[0].Columns[a].ToString(), myfont, Brushes.Black, cx, 50);
                cx = cx + 120;
            }

            for (int b = 0; b < myds.Tables[0].Rows.Count; b++)
            {

                for (int i = 0; i < myds.Tables[0].Columns.Count; i++)
                {
                    e.Graphics.DrawString(myds.Tables[0].Rows[b][i].ToString(), myfont, Brushes.Black, x, y);
                    if (i == 2)
                    {
                        e.Graphics.DrawString(Math.Round(Convert.ToDouble(myds.Tables[0].Rows[b][i]), 2).ToString(), myfont, Brushes.Black, x, y);
                    }
                    else
                    {
                        e.Graphics.DrawString(myds.Tables[0].Rows[b][i].ToString(), myfont, Brushes.Black, x, y);
                    }
                    x = x + 120;
                }
                filed = myds.Tables[0].Rows[b][0].ToString();
                filed = filed.Trim();
                if (filed == "会员购物消费" || filed == "会员打球消费")
                {
                    if (filed == "会员打球消费") user_bball = Math.Round(Convert.ToDouble(myds.Tables[0].Rows[b][2]), 2);
                    if (filed == "会员购物消费") user_shop = Math.Round(Convert.ToDouble(myds.Tables[0].Rows[b][2]), 2);
                }
                else {
                    total_money = total_money + Convert.ToDouble(myds.Tables[0].Rows[b][2]);
                }
                y = y + 20;
                x = tmp + 3;
            }
            x = x + 150;
            total_money = total_money - user_shop;
            e.Graphics.DrawLine(mypen, 50,60,50,60);
            e.Graphics.DrawString("总计：" + total_money.ToString(), myfont, Brushes.Black, x, y);
            e.Graphics.DrawString("负责人签名：_____________", singlefont, Brushes.Black, x, y+20);
            e.Graphics.DrawString(DateTime.Now.ToString(), singlefont, Brushes.Black, x, y+40);
        }

        private void showdata()
        {
            Image myimg = new Bitmap(3500, 2400);
            Graphics g = Graphics.FromImage(myimg);
            Font myfont = new Font("宋体", 10);
            Font singlefont = new Font("宋体", 14, FontStyle.Bold);
            Pen mypen = new Pen(Color.Black, 3);

            int x = 94;
            int y = 70;
            int tmp = 94;
            int cx = 95;
            string filed = "";
            double user_bball = 0;
            double user_shop = 0;
            double total_money = 0;

            for (int a = 0; a < myds.Tables[0].Columns.Count; a++)
            {
                g.DrawString(myds.Tables[0].Columns[a].ToString(), myfont, Brushes.Black, cx, 50);
                cx = cx + 120;
            }

            for (int b = 0; b < myds.Tables[0].Rows.Count; b++)
            {

                for (int i = 0; i < myds.Tables[0].Columns.Count; i++)
                {
                    if (i == 2)
                    {
                        g.DrawString(Math.Round(Convert.ToDouble(myds.Tables[0].Rows[b][i]),2).ToString(), myfont, Brushes.Black, x, y);
                    }
                    else
                    {
                        g.DrawString(myds.Tables[0].Rows[b][i].ToString(), myfont, Brushes.Black, x, y);
                    }
                    x = x + 120;
                }
                filed = myds.Tables[0].Rows[b][0].ToString();
                filed = filed.Trim();
                if (filed == "会员购物消费" || filed == "会员打球消费")
                {
                    if (filed == "会员打球消费") user_bball = Math.Round(Convert.ToDouble(myds.Tables[0].Rows[b][2]), 2);
                    if (filed == "会员购物消费") user_shop = Math.Round(Convert.ToDouble(myds.Tables[0].Rows[b][2]), 2);
                }
                else
                {
                    total_money = total_money + Convert.ToDouble(myds.Tables[0].Rows[b][2]);
                }
                y = y + 20;
                x = tmp + 3;
            }
            x = x + 150;
            total_money=total_money-user_shop;
            g.DrawLine(mypen, 50, 60, 50, 60);
            g.DrawString("总计："+total_money.ToString(), myfont, Brushes.Black, x,  y);
            g.DrawString("负责人签名：_____________", singlefont, Brushes.Black, x,  y+20);
            g.DrawString(DateTime.Now.ToString(), singlefont, Brushes.Black, x,  y+40);
            pictureBox1.Image=myimg;
            pictureBox1.Show();
            pictureBox1.Refresh();
            if (y > pictureBox1.Height)
            {
                pictureBox1.Height = y + 100;
                this.Height = y + 180;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            countdata();
            checkdata();
        }
    }
}