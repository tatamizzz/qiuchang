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
    public partial class sale : Form
    {
        TextBox[] mytext;
        CheckBox[] mycheck;
        DataTable mydt;
        public sale()
        {
            InitializeComponent();
        }

        private void sale_Load(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string sql = "select distinct a.id,a.goods_price,a.goods_name from goods_info as a,goods_stock as b where b.goods_huohao=a.goods_huohao and b.goods_stock>=1";
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand comm = new SqlCommand(sql, conn);
            mydt = new DataTable();
            new SqlDataAdapter(sql,conn).Fill(mydt);
            mytext = new TextBox[mydt.Rows.Count];
            mycheck = new CheckBox[mydt.Rows.Count];

            SqlDataReader rs = comm.ExecuteReader();
            int index = 0;
            int x = 24;
            int y = 263;
            while (rs.Read())
            {
                int pro_id = int.Parse(rs[0].ToString());
                if (index % 7 == 0 && index != 0)
                {
                    y = y + 55;
                    x = 24;
                }

                CheckBox check = new CheckBox();
                check.Text=rs[2] + "("+ rs[1].ToString() + "元)";
                check.Size = new System.Drawing.Size(90, 20);
                check.Tag = rs[0].ToString();
                check.Name= "tb"+index.ToString();
                check.Location = new System.Drawing.Point(x, y);
                mycheck[index] = check;
                this.Controls.Add(check);

                TextBox tb  = new TextBox();
                tb.Size = new System.Drawing.Size(90, 20);
                tb.Text = "0";
                tb.Name = "l" + index;
                tb.Location = new System.Drawing.Point(x, y + 20);
                mytext[index] = tb;
                this.Controls.Add(tb);
                x = x + 95;
                index++;
            }
            rs.Close();

            sql = "select a.goods_name from goods_info as a,goods_stock as b where b.goods_huohao=a.goods_huohao";
            comm = new SqlCommand(sql, conn);
            SqlDataAdapter myda1 = new SqlDataAdapter(comm);
            DataSet myds = new DataSet();
            myda1.Fill(myds);

            SqlDataAdapter myda2 = new SqlDataAdapter(comm);
            DataSet myds2 = new DataSet();
            myda2.Fill(myds2);

            comboBox1.DataSource = myds.Tables[0];
            comboBox1.DisplayMember = myds.Tables[0].Columns[0].ToString();
            comboBox2.DataSource = myds2.Tables[0];
            comboBox2.DisplayMember = myds2.Tables[0].Columns[0].ToString();
            this.countshop();
        }

        private void sale_submit_Click(object sender, EventArgs e)
        {
            double prices = 0;
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string sql;
            string num_day = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            string vipcardid = cardid.Text;

            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand comm=new SqlCommand("",conn);

            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                if (mycheck[i].Checked == false) continue;
                int index = this.findindex(mycheck[i].Tag.ToString());
                double num = 0;
                double price = 0;
                string name = this.findname(int.Parse(mycheck[i].Tag.ToString()));

                num = this.findnum(index);
                price = this.findprice(int.Parse(mycheck[i].Tag.ToString()));
                prices = prices + (num * price);
            }

            if (vipcardid != "")
            {
                
                sql = "select point from vipcard where cardid='" + vipcardid + "'";
                comm.CommandText = sql;
                double user_point = double.Parse(comm.ExecuteScalar().ToString());
                if (user_point > prices)
                {
                    if (MessageBox.Show("卡内余额为:" + user_point + "元是否从卡里扣除" + prices.ToString() + "元", "系统提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (prices > user_point)
                        {
                            double diff = Math.Round(prices - user_point);
                            sql = "update vipcard set point=0 where cardid='" + vipcardid + "'";
                            comm.CommandText = sql;
                            comm.ExecuteNonQuery();
                            this.add_vipcardshop(prices - diff);
                            MessageBox.Show("还应收" + diff.ToString() + "元");
                        }
                        else
                        {
                            sql = "update vipcard set point=point-" + prices.ToString() + " where cardid='" + vipcardid + "'";
                            comm.CommandText = sql;
                            comm.ExecuteNonQuery();
                            double diff = Math.Round((user_point - prices), 2);
                            this.add_vipcardshop(prices);
                            MessageBox.Show("卡内余额为:" + diff.ToString() + "元");
                        }
                    }
                    else
                    {
                        MessageBox.Show("应收款" + prices.ToString() + "元", "系统提醒");
                    }
                    double integral = prices * 0;
                    sql = "update vipcard set shop_integral=shop_integral+" + integral.ToString() + " where cardid='" + vipcardid + "'";
                    comm.CommandText = sql;
                    comm.ExecuteNonQuery();
                    this.shop();
                }else{
                    MessageBox.Show("无法消费，卡内余额为"+user_point.ToString()+"，实际消费金额"+prices.ToString(),"警告");
                }
            }
            else
            {
                MessageBox.Show("应收款" + prices.ToString() + "元", "系统提醒");
                this.shop();
            }
            this.countshop();
            this.clearshop();
            cardid.Text = "";
        }

        private void clearshop()
        {
            sale_pro.Text = "";
            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                mytext[i].Text ="0";
                mycheck[i].Checked = false;
            }
        }

        private void countshop()
        {
            DateTime date = DateTime.Now;
            string numday = date.ToString("yyyyMMdd");
            string sql = "select sum(num) as total,[name] from sale where num_day='" + numday + "' group by [name];";
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader rs = comm.ExecuteReader();
            int index = 0;
            string text="";
            while (rs.Read())
            {
                if (index % 5 == 0 && index!=0)
                {
                    text = text +" " + rs[1].ToString() + "：" + rs[0].ToString()+"瓶 \n\n";
                }
                else {
                    text = text + " " + rs[1].ToString() + "：" + rs[0].ToString() + "瓶 ";
                }
                index++;
            }
            rs.Close();
            sale_info.Text = text;
            this.cticket();
            this.cusercz();
            this.cnewuser();
        }

        private void cticket()
        {
            string date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            string start = date + " 00:00:00";
            string end = date + " 23:59:59";

            string sql = "select sum(xf_point) as point,sum(xf_money) as money from card_consumption where begdatetime>='" +start + "' and enddatetime<='"+end+"'";
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader rs = comm.ExecuteReader();
            while (rs.Read())
            {
                double point = double.Parse(rs[0].ToString() ==""? "0" :rs[0].ToString());
                double money = double.Parse(rs[1].ToString() == "" ? "0" : rs[1].ToString());
                double total = point + money;
                ticket.Text = total.ToString() + "元";
            }
        }

        private void cnewuser()
        {
            string date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            string start = date + " 00:00:00";
            string end = date + " 23:59:59";

            string sql = "select sum(salemoney) as total from vipcard_sale where adddatetime>='" + start + "' and adddatetime<='" + end + "'";
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader rs = comm.ExecuteReader();
            while (rs.Read())
            {
               newuser.Text = rs[0].ToString() + "元";
            }
        }

        private void cusercz()
        {
            string date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            string start = date + " 00:00:00";
            string end = date + " 23:59:59";

            string sql = "select sum(chongzhi) as total from vipcard_cz where adddatetime>='" + start + "' and adddatetime<='" + end + "'";
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader rs = comm.ExecuteReader();
            while (rs.Read())
            {
               usercz.Text = rs[0].ToString() + "元";
            }
        }

        private void shop()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string sql;
            DateTime date = DateTime.Now;
            string num_day = date.ToString("yyyyMMdd");
            string vipcardid = cardid.Text;
            DateTime sale_date = DateTime.Now;

            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand comm = new SqlCommand("", conn);
            string query = "";

            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                if (mycheck[i].Checked == false) continue;
                int index = i;
                double num = 0;
                double price = 0;
                string name = this.findname(int.Parse(mycheck[i].Tag.ToString()));
              
                num = this.findnum(index);
                price = this.findprice(int.Parse(mycheck[i].Tag.ToString()));
                sql = "select id from sale where name='" + name + "' and num_day='" + num_day + "'";
                comm.CommandText = sql;
                string id = "";
                try
                {
                    id = comm.ExecuteScalar().ToString();
                }
                catch {
                    id = "";
                }
                if (id == "")
                {
                    sql = "insert into sale(num,name,price,num_day)values('" + num.ToString() + "','" + name + "','" + price.ToString() + "','" + num_day + "')";
                }
                else {
                    sql = "update sale set num=num+"+num.ToString()+" where id='"+id+"'";
                }
                comm.CommandText = sql;
                comm.ExecuteNonQuery();

                sql = "update goods_stock set goods_stock=goods_stock-" + num.ToString() + " where goods_huohao=(select goods_huohao from goods_info where id='" + mycheck[i].Tag.ToString() + "')";
                comm.CommandText = sql;
                comm.ExecuteNonQuery();              
            }
        }

        private void add_vipcardshop(double shop_total)
        {
            DateTime date =DateTime.Now;
            string numday = date.ToString("yyyyMMdd");
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string id = "";
            string query = "";
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            string sql = "select id from vipcard_shop where numday = "+numday;
            SqlCommand comm = new SqlCommand(sql, conn);
            try
            {
                id = comm.ExecuteScalar().ToString();
            }
            catch {
                id = "";
            }


            if (id == "")
            {
                query = "insert into vipcard_shop(numday,shop_count,shop_total)values("+numday+",1,"+shop_total.ToString()+")";
            }
            else {
                query = "update vipcard_shop set shop_count=shop_count+1,shop_total=shop_total+" + shop_total.ToString() + " where id="+id;
            }
            comm.CommandText = query;
            comm.ExecuteNonQuery();
        }

        private double findnum(int index)
        {
            return double.Parse(mytext[index].Text.ToString());
        }

        private string findname(int pro_id)
        {
            string sql = "select goods_name from goods_info where id='" + pro_id.ToString() + "'";
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            return comm.ExecuteScalar().ToString();
        }

        private double findprice(int pro_id)
        {
            string sql = "select goods_price from goods_info where id='"+pro_id.ToString()+"'";
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            return double.Parse(comm.ExecuteScalar().ToString());
        }

        private int findindex(string pro_id)
        {
            int index = 0;
            for (int i = 0; i < mydt.Rows.Count; i++)
            {
                if (mydt.Rows[i][0].ToString() == pro_id)
                {
                    index=i;
                    break;
                }
            }
            return index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.clearshop();
        }

        private void barter_Click(object sender, EventArgs e)
        {
            string barter_name = comboBox1.Text;
            string barter = comboBox2.Text;
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            string num=numeric.Text;
            string sql = "";
            string mess="";
            string stock = "";
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand comm = new SqlCommand("", conn);

            sql = "select goods_stock from goods_stock where goods_huohao=(select goods_huohao from goods_info where goods_name='" + barter + "')";
            comm.CommandText = sql;
            conn.Open();
            stock = comm.ExecuteScalar().ToString();
            if(int.Parse(stock)>int.Parse(num))
            {
               
                sql = "update goods_stock set goods_stock=goods_stock+" + num + " where goods_huohao=(select goods_huohao from goods_info where goods_name='" + barter_name + "')";
                comm.CommandText = sql;
                comm.ExecuteNonQuery();

                sql = "update sale set num=num+"+num+" where name='"+barter_name+"'";
                comm.CommandText = sql;
                comm.ExecuteNonQuery();

                sql = "update goods_stock set goods_stock=goods_stock-" + num + " where goods_huohao=(select goods_huohao from goods_info where goods_name='" + barter + "')";
                comm.CommandText = sql;
                comm.ExecuteNonQuery();

                sql = "update sale set num=num-" + num + " where name='" + barter + "'";
                comm.CommandText = sql;
                comm.ExecuteNonQuery();

                sql = "select goods_price from goods_info where goods_name='" + barter_name + "'";
                comm.CommandText=sql;
                double total_one = double.Parse(num) * double.Parse(comm.ExecuteScalar().ToString());

                sql="select goods_price from goods_info where goods_name='"+barter+"'";
                comm.CommandText=sql;
                double total_two=double.Parse(num) * double.Parse(comm.ExecuteScalar().ToString());
                mess = "把" + barter_name + "换成" + barter + "数量:" + num + "成功";
                if (total_two - total_one > 0)
                {
                    mess = mess + "\n\n应补" + (total_two - total_one).ToString() + "元";
                }
                else {
                    mess = mess + "\n\n应退" + (Math.Abs(total_two - total_one)).ToString() + "元";
                }

                MessageBox.Show(mess, "提示");
                this.countshop();
            }
            else {
                mess = "不好意思，兑换失败。" + barter + "库存不足" + num;
                MessageBox.Show(mess,"警告");
            }

        }
    }
}
