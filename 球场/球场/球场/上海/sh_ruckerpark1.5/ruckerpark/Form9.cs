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

            checkdata();
        }

        private void checkdata()
        {
            myds.Clear();
            myds.Dispose();
            myds.Tables.Clear();
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "total_count_new";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            mycomm.CommandType = CommandType.StoredProcedure;
            SqlParameter startdate = new SqlParameter("@starttime", dateTimePicker1.Value.Date);
            mycomm.Parameters.Add(startdate);

            SqlParameter enddate = mycomm.Parameters.Add("@endtime", SqlDbType.DateTime);
            enddate.Value = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day, 23, 59, 59);

            myconn.Open();
            mycomm.ExecuteNonQuery();
            myconn.Close();
            myds.Tables.Add();
            myds.Tables[0].Columns.Add("标签");
            commstr = "select * from total_table_new";
            mycomm.CommandText = commstr;
            mycomm.CommandType = CommandType.Text;
            new SqlDataAdapter(mycomm).Fill(myds.Tables[0]);

            myds.Tables[0].Rows[0][0] = "数量";
            myds.Tables[0].Rows[1][0] = "金额";
            this.sale_total();
            showdata();
            printDocument1.DefaultPageSettings.Landscape = true;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            Font myfont = new Font("宋体", 8);
            Font singlefont = new Font("宋体", 14, FontStyle.Bold);
            Pen mypen = new Pen(Color.Black, 3);

            int zk = 16;//字宽
            float leftk = 50;//左边距
            float topg = 50;//顶边距
            double total_money=0;//总计
            for (int i = 0; i < myds.Tables[0].Columns.Count; i++)
            {
                
                    if (i == 0)
                    {
                        int zs = myds.Tables[0].Columns[i].Caption.Length;//字数
                        e.Graphics.DrawString(myds.Tables[0].Columns[i].Caption, myfont, Brushes.Black, leftk, topg);
                        e.Graphics.DrawString(myds.Tables[0].Rows[0][i].ToString(), myfont, Brushes.Black, leftk , topg + 20);
                        e.Graphics.DrawString(myds.Tables[0].Rows[1][i].ToString(), myfont, Brushes.Black, leftk, topg + 40);
                        leftk += zs * zk;
                    }
                    else
                    {
                        int zs = myds.Tables[0].Columns[i].Caption.Length;//字数
                        e.Graphics.DrawString(myds.Tables[0].Columns[i].Caption, myfont, Brushes.Black, leftk, topg);
                        e.Graphics.DrawString(myds.Tables[0].Rows[0][i].ToString(), myfont, Brushes.Black, leftk + zs * zk / 3 - myds.Tables[0].Rows[0][i].ToString().Length * 3, topg + 20);
                        e.Graphics.DrawString(myds.Tables[0].Rows[1][i].ToString(), myfont, Brushes.Black, leftk + zs * zk / 3 - myds.Tables[0].Rows[1][i].ToString().Length * 3, topg + 40);
                        leftk += zs * zk;
                    }
                    if (myds.Tables[0].Columns[i].Caption.Trim() != "标签" && myds.Tables[0].Columns[i].Caption != "会员消费" && myds.Tables[0].Columns[i].Caption != "销售")
                    {
                        total_money += Convert.ToDouble(myds.Tables[0].Rows[1][i].ToString());
                    }
            }
            e.Graphics.DrawLine(mypen, 50, topg + 60, leftk, topg + 60);
            e.Graphics.DrawString("总计：" + total_money.ToString(), myfont, Brushes.Black, leftk - 75, topg + 80);
            e.Graphics.DrawString("负责人签名：_____________", singlefont, Brushes.Black, leftk - 250, topg + 140);
            e.Graphics.DrawString(DateTime.Now.ToString(), singlefont, Brushes.Black, leftk - 210, topg + 180);
            
            
            
        }

        private void showdata()
        {
            Image myimg = new Bitmap(3500, 2400);
            Graphics g = Graphics.FromImage(myimg);
            Font myfont = new Font("宋体", 10);
            Font singlefont = new Font("宋体", 14, FontStyle.Bold);
            Pen mypen = new Pen(Color.Black, 3);

            int zk = 16;//字宽
            float leftk = 50;//左边距
            float topg = 50;//顶边距
            double total_money=0;//总计
            for (int i = 0; i < myds.Tables[0].Columns.Count; i++)
            {
                
                    if (i == 0)
                    {
                        int zs = myds.Tables[0].Columns[i].Caption.Length;//字数
                        g.DrawString(myds.Tables[0].Columns[i].Caption, myfont, Brushes.Black, leftk, topg);
                        g.DrawString(myds.Tables[0].Rows[0][i].ToString(), myfont, Brushes.Black, leftk, topg + 20);
                        g.DrawString(myds.Tables[0].Rows[1][i].ToString(), myfont, Brushes.Black, leftk, topg + 40);
                        leftk += zs * zk;
                    }
                    else
                    {
                        int zs = myds.Tables[0].Columns[i].Caption.Length;//字数
                        g.DrawString(myds.Tables[0].Columns[i].Caption, myfont, Brushes.Black, leftk, topg);
                        g.DrawString(myds.Tables[0].Rows[0][i].ToString(), myfont, Brushes.Black, leftk + zs * zk / 3 - myds.Tables[0].Rows[0][i].ToString().Length * 3, topg + 20);
                        g.DrawString(myds.Tables[0].Rows[1][i].ToString(), myfont, Brushes.Black, leftk + zs * zk / 3 - myds.Tables[0].Rows[1][i].ToString().Length * 3, topg + 40);
                        leftk += zs * zk;
                    }
                    if (myds.Tables[0].Columns[i].Caption.Trim() != "标签" && myds.Tables[0].Columns[i].Caption != "会员消费")
                    {
                        total_money += Convert.ToDouble(myds.Tables[0].Rows[1][i].ToString());
                    }
            }
            double sale_total = myds.Tables[0].Rows[1][1].ToString()==""?0:double.Parse(myds.Tables[0].Rows[1][1].ToString());
            g.DrawLine(mypen, 50, topg + 60, leftk, topg + 60);
            g.DrawString("总计："+total_money.ToString(), myfont, Brushes.Black, leftk - 75, topg + 80);
            g.DrawString("负责人签名：_____________", singlefont, Brushes.Black, leftk - 250, topg + 140);
            g.DrawString(DateTime.Now.ToString(), singlefont, Brushes.Black, leftk - 210, topg + 180);
            
            
            pictureBox1.Image=myimg;
            pictureBox1.Show();
            pictureBox1.Refresh();
        }

        private void sale_total()
        {
            DateTime startday = dateTimePicker1.Value;
            DateTime endday = dateTimePicker2.Value;
            string start_day = startday.ToString("yyyyMMdd");
            string end_day = endday.ToString("yyyyMMdd");
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string sql="select sum(num) as num,price,name from sale where num_day>='"+start_day+"' and num_day<='"+end_day+"' group by name,price;";
            SqlConnection myconn = new SqlConnection(connstr);
            myconn.Open();
            SqlCommand comm=new SqlCommand(sql,myconn);
            SqlDataReader rs=comm.ExecuteReader();
            int index = 5;
            while(rs.Read())
            {
                myds.Tables[0].Columns.Add(rs[2].ToString());
                myds.Tables[0].Rows[0][index] = rs[0].ToString();
                myds.Tables[0].Rows[1][index] = double.Parse(rs[0].ToString()) * double.Parse(rs[1].ToString());
                index++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkdata();
        }
        
    }
}