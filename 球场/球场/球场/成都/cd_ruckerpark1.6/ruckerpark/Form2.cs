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
namespace ruckerpark
{
    public partial class Form2 : Form
    {
        static DateTime start_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
        static DateTime end_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        public Form2()
        {
            InitializeComponent();
        }

        private bool ismember(string cardid)//判断是否是会员
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select count(id) from vipcard where cardid='"+cardid+"'";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myconn.Open();
            if (Convert.ToInt32(mycomm.ExecuteScalar()) == 1)
            {
                myconn.Close();
                return true;
            }
            myconn.Close();
            return false;
        }

        private void jz()//结账
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);

            string commstr = "select begdatetime from card_consumption where cardid='"+textBox1.Text+"' and begdatetime>='"+start_date.ToString()+"' and enddatetime is null";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myconn.Open();

            DateTime begdatetime =Convert.ToDateTime(mycomm.ExecuteScalar().ToString());
            double min= Math.Truncate((DateTime.Now - begdatetime).TotalMinutes);

            string key = this.current_money();
            string [] moneys = ConfigurationManager.AppSettings[key].Split('|');
            double xf_money = 10;
            string filed = this.ismember(textBox1.Text) ? "xf_point" : "xf_money";
            if (filed == "xf_point") xf_money = 6;
 
            commstr = "select top 1 ball_number from card_consumption where cardid='" + textBox1.Text + "' order by enddatetime desc";
            mycomm.CommandText = commstr;
            string ball_number = mycomm.ExecuteScalar().ToString();//所借球号
            string mes = "本次消费" + xf_money.ToString() + "元";

            if (ball_number != "") mes = mes + "\n\n" + "应归还" + ball_number + "号球";
            mes = mes + "\n\n是否要结账？";
            if (MessageBox.Show(mes, "结账卡提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                
                string sql = "update card_consumption set enddatetime='" + DateTime.Now + "'," + filed + "='" + xf_money + "' where cardid='" + textBox1.Text + "' and "+filed+" is null";
                mycomm.CommandText = sql;
                mycomm.ExecuteNonQuery();
                if (filed == "xf_point")
                {
                    double integral = xf_money*10;
                    sql = "update vipcard set point=point-" + xf_money + ",xf_integral=xf_integral+"+integral+" where cardid='" + textBox1.Text + "'";
                    mycomm.CommandText = sql;
                    mycomm.ExecuteNonQuery();
                    sql = "select point,integral,xf_integral,shop_integral from vipcard where cardid='" + textBox1.Text + "'";
                    mycomm.CommandText = sql;
                    SqlDataReader rs = mycomm.ExecuteReader();
                    rs.Read();
                    string point = rs[0].ToString();
                    integral = double.Parse(rs[1].ToString())+double.Parse(rs[2].ToString())+double.Parse(rs[3].ToString());
                    double user_point = double.Parse(point);
                    mes = "本次消费"+xf_money.ToString()+"元\n\n";
                    if (user_point< 0)
                    {
                        rs.Close();
                        mes = mes + "应收款" + Math.Abs(user_point).ToString() + "元\n\n";
                        sql = "update vipcard set point=0 where cardid='" + textBox1.Text + "'";
                        mycomm.CommandText = sql;
                        mycomm.ExecuteNonQuery();
                    }
                    else {
                        mes = mes + "卡内还剩："+point+"元\n\n";
                    }
                    mes = mes + "\n\n当前积分：" + integral.ToString();
                    if (ball_number != "") mes = mes +"应归还" + ball_number + "号球";
                }else {
                     mes = "本次消费" + xf_money.ToString() + "元" ;
                     if (ball_number != "") mes = mes + "\n\n" + "应归还" + ball_number + "号球";     
                }
                MessageBox.Show(mes, "刷卡消费", MessageBoxButtons.OK);
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
            parametersf.dgf1 = true;
            parametersf.dgf2 = true;
        }
        //-------------------------------------------------------开卡提交------------------------------------------------------------//
        private void kktj()
        {
            if (1==1)
            {
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection myconn = new SqlConnection(connstr);
                if (ismember(textBox1.Text))
                {
                    string commstr = "select point from vipcard where cardid='" + textBox1.Text + "'";
                    SqlCommand mycomm = new SqlCommand(commstr, myconn);
                    myconn.Open();
                    string point = mycomm.ExecuteScalar().ToString();
                    string str;
                    if (double.Parse(point) <= 0)
                    {
                        MessageBox.Show("开卡失败——卡内余额等于零或已经为负数，请去充值", "警告", MessageBoxButtons.OK);
                    }
                    else
                    {
                        str = "\n\n卡内还剩余额" + point + "元";
                        if (double.Parse(point) <= 5) str = "\n\n卡内余额低于5元，剩余" + point + "元请充值";
                        commstr = "insert into card_consumption(cardid,begdatetime) values('" + textBox1.Text + "','" + DateTime.Now + "')";
                        if (textBox2.Text != "") commstr = "insert into card_consumption(cardid,begdatetime,ball_number) values('" + textBox1.Text + "','" + DateTime.Now + "'," + textBox2.Text + ")";
                        mycomm.CommandText = commstr;
                        mycomm.ExecuteNonQuery();
                        str = "开卡成功" + str;
                        MessageBox.Show(str, "刷卡消费", MessageBoxButtons.OK);
                    }
                    this.Submit(textBox1.Text.ToString());
                }else{
                    string commstr = "insert into card_consumption(cardid,begdatetime) values('" + textBox1.Text + "','" + DateTime.Now + "')";
                    if (textBox2.Text != "") commstr = "insert into card_consumption(cardid,begdatetime,ball_number) values('" + textBox1.Text + "','" + DateTime.Now + "'," + textBox2.Text + ")";
                    SqlCommand mycomm = new SqlCommand(commstr, myconn);
                    myconn.Open();
                    mycomm.ExecuteNonQuery();
                    MessageBox.Show("开卡成功", "刷卡消费", MessageBoxButtons.OK);
                }
                myconn.Close();
                parametersf.dgf1 = true;
                parametersf.dgf2 = true;
            }
            else
            {
                MessageBox.Show("卡号位数不足", "警告", MessageBoxButtons.OK);
            }
            
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }
        //-------------------------------------------------------开卡提交------------------------------------------------------------//


        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kk();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (parametersf.checknum(textBox1.Text))
                {
                    textBox2.Focus();
                }
                else
                {
                    MessageBox.Show("此卡号含有非法字符", "警告", MessageBoxButtons.OK);
                    textBox1.Text = "";
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                    kk();
            }
        }

        private void kk()
        {

            if (parametersf.checknum(textBox1.Text))
            {
                if (textBox1.Text != "")
                {
                    string connstr = ConfigurationManager.AppSettings["connectionstring"];
                    SqlConnection myconn = new SqlConnection(connstr);
                    string commstr = "select count(1) from card_consumption where cardid='" +textBox1.Text + "' and begdatetime>='"+start_date.ToString()+"' and enddatetime is null";
                    SqlCommand mycomm = new SqlCommand(commstr, myconn);
                    myconn.Open();
                    if (Convert.ToInt32(mycomm.ExecuteScalar()) == 1)
                    {
                        jz();
                    }
                    else
                    {
                        if (MessageBox.Show("是否要开卡？", "开卡提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            kktj();
                        }
                        else
                        {
                            textBox1.Text = "";
                            textBox2.Text = "";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("卡号不能为空或卡号位数不对", "警告", MessageBoxButtons.OK);
                    textBox1.Text = "";
                    textBox1.Focus();
                }
            }
            else
            {
                MessageBox.Show("此卡号含有非法字符", "警告", MessageBoxButtons.OK);
                textBox1.Text = "";
            }
        }

        private void Submit(string data)
        {
            string post_url = "http://peak.hoopchina.com/index.php?c=request&a=batting";
            string court = ConfigurationManager.AppSettings["court"];
            string post_data = "court="+court+"&data="+data;
            this.PostData(post_url, post_data);
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

        private double account(double min, double min_money, double max_money, double diff_money)
        {
            double xf_money = min_money;
            if (min < 120) xf_money = min_money;
            if (min >= 120) xf_money = max_money;
            if (xf_money > max_money) xf_money = max_money;
            return xf_money;
        }

        private double min_account(double min, double money)
        {
            return Math.Round(Math.Round((min / 10)) * money,1);
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
            string[] holiday_list = holiday.Split(',');
            foreach (string day in holiday_list)
            {
                int fday = int.Parse(day);
                if (fday == date) return true;
            }
            string week = Convert.ToDateTime(dt).DayOfWeek.ToString();
            if (week == "Sunday" || week == "Saturday") return true;
            return false;
        }
    }
}