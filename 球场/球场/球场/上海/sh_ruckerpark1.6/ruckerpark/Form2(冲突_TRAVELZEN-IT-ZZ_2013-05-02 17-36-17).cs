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
            double hour = Math.Truncate((DateTime.Now - begdatetime).TotalHours);
            double _min = Math.Ceiling(min/10);

            commstr = "select isnull(sum(xf_point),0) from card_consumption where cardid='" + textBox1.Text + "'and enddatetime>='"+DateTime.Now.Date+"' group by cardid";
            mycomm.CommandText = commstr;
            int pay_already = Convert.ToInt16(mycomm.ExecuteScalar());

            //初始化价格
            double money, min_money,hy_min_money,max_money;
            double deposit = 30;//押金
            int key_hour = 17;
            double xf_money = 0;
            max_money = 18;//会员每天最高消费
            //if (check_holiday())
            //{
            //    money = 1;
            //    min_money = 15;
            //    hy_min_money = 7;//hy会员
            //    max_money = 20;
            //}
            //else
            //{
            //    min_money = 12;
            //    hy_min_money = 6;
            //    money = 1;
            //    max_money = 15;
            //}

            //------------20120331-------------//
            //if (DateTime.Now.Hour < key_hour)
            //{
            //    min_money = 12;
            //    xf_money=Math.Round(low_pay_count(Math.Truncate((DateTime.Now - begdatetime).TotalMinutes)));
            //}
            //else
            //{
            //    min_money = 14;
            //    if (begdatetime.Hour >= 17)
            //    {
            //        xf_money = high_pay_count(Math.Truncate((DateTime.Now - begdatetime).TotalMinutes));
            //        if (xf_money < min_money) xf_money = min_money;
            //    }
            //    else
            //    {
            //        DateTime key_time=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,key_hour,0,0);
            //        xf_money=Math.Round(low_pay_count(Math.Truncate((key_time - begdatetime).TotalMinutes))+Math.Truncate((DateTime.Now - key_time).TotalMinutes));
            //    }
            //}
            //------------20120331-------------//


            //------------20130429-------------//
            min_money = 20;
            max_money = 30;
            xf_money = Math.Truncate((DateTime.Now - begdatetime).TotalMinutes) / 15 * 2.5;
            //------------20130429-------------//

            string filed = "xf_money";

            if (this.ismember(textBox1.Text))
            {
                filed = "xf_point";
                //xf_money = Math.Ceiling(min / 30) * hy_min_money/2;//会员半小时计费
                //if (xf_money < hy_min_money) xf_money = hy_min_money;
                //if (xf_money > max_money) xf_money = max_money;
                xf_money = (xf_money + pay_already) > max_money ? max_money - pay_already : xf_money;
            }
            else {
                if (xf_money < min_money) xf_money = min_money;
            }

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
                    if (xf_money > max_money) xf_money = max_money;
                    double integral = xf_money*1;//计算消费积分

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
                    }else {
                        mes = mes + "卡内还剩："+point+"元\n\n";
                    }
                    mes = mes + "\n\n当前积分：" + integral.ToString();
                    if (ball_number != "") mes = mes +"应归还" + ball_number + "号球";
                }else{
                    double smoney = deposit - xf_money;
                    if (smoney > 1)
                    {
                        mes = "本次消费" + xf_money.ToString() + "元\n\n应找零" + smoney.ToString() + "元";
                    }else {
                        mes = "本次消费" + xf_money.ToString() + "元\n\n应收款" + Math.Abs(smoney).ToString() + "元";
                    }
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
            if (min > 120 && min < 180) xf_money = Math.Round(Math.Round((min - 120) / 10) * diff_money,1) + min_money;
            if (min > 180) xf_money = max_money;
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

        //------------20120331分隔时间前计费---------------//
        private double low_pay_count(double minutes)
        {
            double money,billing_point;
            money = 1.5;
            billing_point = 15;
            return minutes / billing_point * money;
        }

        //------------20120331分隔时间后计费---------------//
        private double high_pay_count(double minutes)
        {
            double money,billing_point;
            money = 1.75;
            billing_point = 15;
            return minutes / billing_point * money;
        }


    }

    //double money, min_money,hy_min_money,max_money;
    public class caclCost
    {
        public double money { get; set; }//消费金额
        public double min_money { get; set; }//非会员最低消费
        public double hy_min_money { get; set; }//会员最低消费
        public double max_money { get; set; }//非会员最高消费
        public double hy_max_money { get; set; }//会员最高消费
    }


    //20130502开始重构
    public class TariffInfo//关于资费信息的类
    {
        public double min_money { get; set; }
        public double max_money { get; set; }
        public double cph { get; set; }
        public double discount { get; set; }

        public TariffInfo()
        {
            getTariffInfo(false);
        }

        public TariffInfo(bool tof)
        {
            getTariffInfo(tof);
        }

        private void getTariffInfo(bool tof)
        {
            if (tof)
            {
                min_money = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["hy_min_money"]);
                max_money = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["hy_max_money"]);
                cph = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["hy_cph"]);
                discount = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["hy_discount"]);
            }
            else
            {
                min_money = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["min_money"]);
                max_money = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["max_money"]);
                cph = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["cph"]);
                discount = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["discount"]);
            }
        }
    }

    public abstract class Customer
    {
        public string CardNo { get; set; }

        public Customer(string CardNo)
        {
            this.CardNo = CardNo;
        }

        protected abstract void kk();

        protected abstract void jk();

        protected abstract void borrowBall();

        public bool ismember()//判断是否是会员
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select count(id) from vipcard where cardid='" + CardNo + "'";
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
    }
}