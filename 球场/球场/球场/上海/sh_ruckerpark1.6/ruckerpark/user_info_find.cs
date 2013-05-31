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
    public partial class user_info_find : Form
    {
        public user_info_find()
        {
            InitializeComponent();
        }

        private void user_info_find_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            label15.Text ="";
            label3.Text = "";
            label5.Text ="";
            label7.Text = "";
            label9.Text = "";
            label11.Text = "";
            hoopname.Text = "";
            if (e.KeyCode == Keys.Enter)
            {
                int_pro.Text = "";
                int_pro.Visible = false;
                integral_b.Visible = false;

                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection myconn = new SqlConnection(connstr);
                myconn.Open();
                
                string commstr = "select count(1) from vipcard where cardid='" + textBox1.Text + "'";
                SqlCommand mycomm = new SqlCommand(commstr, myconn);
                if (mycomm.ExecuteScalar().ToString() != "1")
                {
                    MessageBox.Show("此号码非会员号码或者尚未销售", "警告", MessageBoxButtons.OK);
                }
                else
                {
                    commstr = "select * from vipcard where cardid='" + textBox1.Text + "'";
                    mycomm.CommandText = commstr;
                    SqlDataReader rs = mycomm.ExecuteReader();
                    rs.Read();
                    label15.Text = rs[2].ToString();
                    label3.Text = rs[3].ToString();
                    label5.Text = rs[4].ToString();
                    label7.Text = rs[5].ToString();
                    label9.Text = rs[6].ToString();
                    hoopname.Text = rs[8].ToString();
                    integral.Text = rs[9].ToString() + "+" + rs[10].ToString()+"+" + rs[11].ToString();
                    int user_integral = int.Parse(rs[9].ToString()) + int.Parse(rs[10].ToString()) + int.Parse(rs[11].ToString());
                    rs.Close();
                    commstr = "select count(1) from card_consumption where cardid='" + textBox1.Text + "'";
                    mycomm.CommandText = commstr;
                    label11.Text = mycomm.ExecuteScalar().ToString();

                    commstr = "select sum(xf_point) from card_consumption where cardid='" + textBox1.Text + "'";
                    mycomm.CommandText = commstr;
                    label13.Text = mycomm.ExecuteScalar().ToString();

                    commstr = "select id,integral,product_name,product_num from integral order by integral";
                    mycomm.CommandText = commstr;
                    rs = mycomm.ExecuteReader();
                    string text = "";
                    int index=0;
                    int i = 1;
                    int x=150;
                    int y = 500;
                    while (rs.Read())
                    {
                        string str_integral = rs[1].ToString();
                        if (user_integral >= int.Parse(rs[1].ToString())) {
                            
                            if (i % 3 == 0 && i != 0)
                            {
                                text = text + rs[2].ToString().Trim() + "(库存" + rs[3].ToString() + "),需要" + rs[1].ToString()+"\n\n";
                            }
                            else {
                                text = text + rs[2].ToString().Trim() + "(库存" + rs[3].ToString() + "),需要" + rs[1].ToString() + "  ";
                            }

                            if (index % 4 == 0 && index != 0)
                            {
                                y = y + 35;
                                x = 150;
                            }

                            Button bt = new Button();
                            bt.Size = new System.Drawing.Size(80,20);
                            bt.Name = "bt" + index.ToString();
                            bt.Tag=rs[0]+"|"+textBox1.Text;
                            bt.Text="兑换"+rs[2].ToString();
                            bt.Location=new System.Drawing.Point(x, y);
                            this.Controls.Add(bt);
                            bt.Click += new System.EventHandler(btn_click);
                            x=x+85;
                            index++;
                            i++;
                        }
                    }
                    
                    if (text != "")
                    {
                        int_pro.Text = "可以兑换的物品有：\n\n"+text;
                        int_pro.Visible = true;
                    }
                }
                myconn.Close();
                textBox1.Text = "";
            }
        }

        private void btn_click(object sender, EventArgs e)
        {
                Button bt = (Button)sender;//将触发此事件的对象转换为该Button对象
                string[] str = bt.Tag.ToString().Split('|');
                string id = str[0];
                string cardid = str[1];

                string sql = "select integral,product_num from integral where id='" + id + "'";
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection conn = new SqlConnection(connstr);
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader rs = comm.ExecuteReader();
                rs.Read();
                int integrals = int.Parse(rs[0].ToString());
                int number = int.Parse(rs[1].ToString());
                rs.Close();
                if (number == 0)
                {
                    MessageBox.Show("该物品没有了", "系统提醒");
                }
                else
                {

                    sql = "select xf_integral,integral,shop_integral from vipcard where cardid='" + cardid + "'";
                    comm.CommandText = sql;
                    rs = comm.ExecuteReader();
                    rs.Read();
                    int xf_integral = int.Parse(rs[0].ToString());
                    int cz_integral = int.Parse(rs[1].ToString());
                    int shop_integral = int.Parse(rs[2].ToString());
                    int cz_diff = 0;
                    int shop_diff = 0;
                    rs.Close();

                    int diff = xf_integral - integrals;
                    string filed="";
                    if (diff > 0)
                    {
                        filed = "xf_integral=xf_integral-" + integrals.ToString();
                    }
                    else {
                        filed = "xf_integral=0";
                        diff = Math.Abs(diff);
                        cz_diff = cz_integral - diff;
                        if (cz_diff > 0)
                        {
                            filed =  "xf_integral=0 ,integral=integral-" + diff.ToString();
                        }
                        else {
                            shop_diff = shop_integral - cz_diff;
                            cz_diff = Math.Abs(cz_diff);
                            if (shop_diff > 0)
                            {
                                filed = "integral=0,xf_integral=0,shop_integral=shop_integral-"+cz_diff.ToString();
                            }
                            else {
                                filed = "integral=0,xf_integral=0,shop_integral=0";
                            }
                        }
                    }
                    
                    sql = "update integral set product_num=product_num-1 where id='" + id + "'";
                    comm.CommandText = sql;
                    comm.ExecuteNonQuery();

                    string query = "update vipcard set "+filed+" where cardid='" + cardid + "'";
                    comm.CommandText = query;
                    comm.ExecuteNonQuery();

                    MessageBox.Show("兑换奖成成功", "系统提示");
                    this.Close();
                }
                
        }

        private void integral_b_Click(object sender, EventArgs e)
        {
            string [] str = integral_b.Tag.ToString().Split('|');
            string id = str[0];
            string cardid = str[1];
            
            string sql = "select integral,product_num from integral where id='" + id + "'";
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand comm = new SqlCommand(sql,conn);
            conn.Open();
            SqlDataReader rs = comm.ExecuteReader();
            rs.Read();
            int integrals = int.Parse(rs[0].ToString());
            int number = int.Parse(rs[1].ToString());
            rs.Close();
            if (number == 0)
            {
                MessageBox.Show("该物品没有了", "系统提醒");
            }
            else
            {

                sql = "select xf_integral,integral,shop_integral from vipcard where cardid='" + cardid + "'";
                comm.CommandText = sql;
                rs = comm.ExecuteReader();
                rs.Read();
                int xf_integral = int.Parse(rs[0].ToString());
                int cz_integral = int.Parse(rs[1].ToString());
                int shop_integral = int.Parse(rs[2].ToString());
                rs.Close();

                int diff = xf_integral - integrals;
                sql = "update integral set product_num=product_num-1 where id='" + id + "'";
                comm.CommandText = sql;
                comm.ExecuteNonQuery();

                if (diff > 0)
                {
                    sql = "update vipcard set integral=integral-"+integrals.ToString()+" where cardid='"+cardid+"'";
                    comm.CommandText = sql;
                    comm.ExecuteNonQuery();
                }
                else {
                    diff = Math.Abs(diff);
                    diff = cz_integral - diff;
                    if (diff > 0)
                    {
                        sql = "update vipcard set xf_integral=0,integral=integral-" + diff.ToString() + " where cardid='" + cardid + "'";
                        comm.CommandText = sql;
                        comm.ExecuteNonQuery();
                    }
                    else {
                        diff = Math.Abs(diff);
                        sql = "update vipcard set xf_integral=0,integral=0,shop_integral=shop_integral-" + diff.ToString() + " where cardid='" + cardid + "'";
                        comm.CommandText = sql;
                        comm.ExecuteNonQuery();
                    }
                   
                }

                MessageBox.Show("兑换奖成功","系统提示");
            }
        }

    }
}