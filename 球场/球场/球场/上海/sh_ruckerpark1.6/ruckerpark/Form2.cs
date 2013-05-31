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

        private void clearAllText()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer(textBox1.Text);
            customer.kk();
            clearAllText();
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
                    clearAllText();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Customer customer = new Customer(textBox1.Text);
                customer.kk();
                clearAllText();
            }
        }

        

        //private double account(double min, double min_money, double max_money, double diff_money)
        //{
        //    double xf_money = min_money;
        //    if (min > 120 && min < 180) xf_money = Math.Round(Math.Round((min - 120) / 10) * diff_money,1) + min_money;
        //    if (min > 180) xf_money = max_money;
        //    if (xf_money > max_money) xf_money = max_money;
        //    return xf_money;
        //}

        //private double min_account(double min, double money)
        //{
        //    return Math.Round(Math.Round((min / 10)) * money,1);
        //}

        //private string current_money()
        //{
        //    bool holiday = this.check_holiday();
        //    DateTime dt = DateTime.Now;
        //    int nowdate = int.Parse(DateTime.Now.ToString("Hmm"));
        //    string key = "";
        //    if (holiday == true) key = "holiday";
        //    if (nowdate >= 1730 && holiday == false) key = "pm";
        //    if (nowdate < 1730 && holiday == false) key = "am";
        //    return key;
        //}

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
        //private double low_pay_count(double minutes)
        //{
        //    double money,billing_point;
        //    money = 1.5;
        //    billing_point = 15;
        //    return minutes / billing_point * money;
        //}

        //------------20120331分隔时间后计费---------------//
        //private double high_pay_count(double minutes)
        //{
        //    double money,billing_point;
        //    money = 1.75;
        //    billing_point = 15;
        //    return minutes / billing_point * money;
        //}


    }

    
    //20130502开始重构
    public class TariffInfo//关于资费信息的类
    {
        public List<double> min_money { get; set; }
        public List<double> max_money { get; set; }
        public List<double> cph { get; set; }
        public double discount { get; set; }
        public bool hasTopMoney { get; set; }
        public List<int> key_hour { get; set; }
        public double deposit { get; set; }

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
                min_money = new List<double>(Array.ConvertAll<string, double>(System.Configuration.ConfigurationManager.AppSettings["hy_min_money"].ToString().Split('|'), delegate(string n) { return Convert.ToDouble(n); }));
                max_money = new List<double>(Array.ConvertAll<string, double>(System.Configuration.ConfigurationManager.AppSettings["hy_max_money"].ToString().Split('|'), delegate(string n) { return Convert.ToDouble(n); }));
                cph = new List<double>(Array.ConvertAll<string, double>(System.Configuration.ConfigurationManager.AppSettings["hy_cph"].ToString().Split('|'), delegate(string n) { return Convert.ToDouble(n); }));
                discount = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["hy_discount"]);
            }
            else
            {
                min_money = new List<double>(Array.ConvertAll<string, double>(System.Configuration.ConfigurationManager.AppSettings["min_money"].ToString().Split('|'), delegate(string n) { return Convert.ToDouble(n); }));
                max_money = new List<double>(Array.ConvertAll<string, double>(System.Configuration.ConfigurationManager.AppSettings["max_money"].ToString().Split('|'), delegate(string n) { return Convert.ToDouble(n); }));
                cph = new List<double>(Array.ConvertAll<string, double>(System.Configuration.ConfigurationManager.AppSettings["cph"].ToString().Split('|'), delegate(string n) { return Convert.ToDouble(n); }));
                discount = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["discount"]);
            }
            hasTopMoney = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["hasTopMoney"]);
            key_hour = new List<int>(Array.ConvertAll<string, Int32>(System.Configuration.ConfigurationManager.AppSettings["key_hour"].ToString().Split('|'), delegate(string n) { try { return Convert.ToInt32(n); } catch (Exception e) { return -1; } }));
            deposit = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["deposit"]);
        }
    }

    public class Customer//客户类
    {
        static DateTime start_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
        static DateTime end_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        DBHelp db = new DBHelp();

        public string CardNo { get; set; }

        public string BallNo { get; set; }

        public DateTime begdatetime { get; set; }

        public DateTime enddatetime { get; set; }

        public Customer(string CardNo)
        {
            this.CardNo = CardNo;
            this.BallNo = "";
            checkRecord();
        }

        private void checkRecord()
        {
            string commstr = "select begdatetime from card_consumption where cardid='" + CardNo + "' and begdatetime>='" + start_date.ToString() + "' and enddatetime is null";
            object bdt=db.ExecuteScalar(commstr);
            if (bdt != null || bdt != "")
            {
                begdatetime = Convert.ToDateTime(bdt);
            }
        }

        public void kk()
        {

            if (parametersf.checknum(CardNo))
            {
                if (CardNo != "")
                {
                    string commstr = string.Format("select count(1) from card_consumption where cardid='{0}' and begdatetime>='{1}' and enddatetime is null", CardNo, start_date.ToString());
                    if (Convert.ToInt32(db.ExecuteScalar(commstr)) == 1)
                    {
                        jz();
                    }
                    else
                    {
                        if (MessageBox.Show("是否要开卡？", "开卡提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            kktj();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("卡号不能为空或卡号位数不对", "警告", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("此卡号含有非法字符", "警告", MessageBoxButtons.OK);
            }
        }

        private void kktj()
        {
            if (1 == 1)
            {
                if (ismember())
                {
                    string commstr = string.Format("select point from vipcard where cardid='{0}'",CardNo);
                    string point = db.ExecuteScalar(commstr).ToString();
                    string str;
                    if (double.Parse(point) <= 0)
                    {
                        MessageBox.Show("开卡失败——卡内余额等于零或已经为负数，请去充值", "警告", MessageBoxButtons.OK);
                    }
                    else
                    {
                        str = "\n\n卡内还剩余额" + point + "元";
                        if (double.Parse(point) <= 5) str = "\n\n卡内余额低于5元，剩余" + point + "元请充值";
                        commstr = string.Format("insert into card_consumption(cardid,begdatetime) values('{0}','{1}')", CardNo,DateTime.Now);
                        if (BallNo != "") commstr = string.Format("insert into card_consumption(cardid,begdatetime,ball_number) values('{0}','{1}',{2})", CardNo, DateTime.Now, BallNo);
                        db.InsertUpdateDel(commstr);
                        str = "开卡成功" + str;
                        MessageBox.Show(str, "刷卡消费", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    string commstr = string.Format("insert into card_consumption(cardid,begdatetime) values('{0}','{1}')", CardNo, DateTime.Now);
                    if (BallNo != "") commstr = string.Format("insert into card_consumption(cardid,begdatetime,ball_number) values('{0}','{1}',{2})", CardNo, DateTime.Now, BallNo);
                    db.InsertUpdateDel(commstr);
                    MessageBox.Show("开卡成功", "刷卡消费", MessageBoxButtons.OK);
                }
                parametersf.dgf1 = true;
                parametersf.dgf2 = true;
            }
            else
            {
                MessageBox.Show("卡号位数不足", "警告", MessageBoxButtons.OK);
            }

            
        }

        private void jz()//结账
        {
            TariffInfo ti = new TariffInfo(ismember());
            //TariffInfo highti = new TariffInfo(ismember());

            string commstr = "select begdatetime from card_consumption where cardid='" + CardNo + "' and begdatetime>='" + start_date.ToString() + "' and enddatetime is null";

            begdatetime = Convert.ToDateTime(db.ExecuteScalar(commstr));
            enddatetime = DateTime.Now;
            double xf_money = 0.0;

            commstr = "select isnull(sum(xf_point),0) from card_consumption where cardid='" + CardNo + "'and enddatetime>='" + DateTime.Now.Date + "' group by cardid";
            int pay_already = Convert.ToInt16(db.ExecuteScalar(commstr));


            CashSuper cs;

            if (ti.key_hour.IndexOf(-1)<0)
            {
                cs = new CashTimeSharing(this, ti);
                xf_money = cs.Cacl();
            }
            else
            {
                cs = new CashNormal(begdatetime, enddatetime, ti.cph[0],ti.max_money[0],ti.min_money[0]);
                xf_money = cs.Cacl();
            }

            //if (DateTime.Now.Hour < ti.key_hour[0])
            //{
            //    CashTimeSharing cts = new CashTimeSharing(this, ti);
            //    xf_money = Math.Round(cts.Cacl());
            //}
            //else
            //{
                
                
            //    if (!ismember())
            //    {
            //        highti.cph = 8;
            //        highti.min_money = 16;
            //    }
            //    if (begdatetime.Hour >= 17)
            //    {
            //        CashTimeSharing cts = new CashTimeSharing(this, highti);
            //        xf_money = Math.Round(cts.Cacl());
            //    }
            //    else
            //    {
                   
            //        CashTimeSharing cts = new CashTimeSharing(this,ti,highti);
            //        xf_money = Math.Round(cts.Cacl());
            //    }
            //}


            
            string filed = "xf_money";

            if (ismember())
            {
                filed = "xf_point";
                xf_money = (xf_money + pay_already) > cs.maxMoney ? cs.maxMoney - pay_already : xf_money;
            }
            else
            {
                if (xf_money < cs.minMoney) xf_money = cs.minMoney;
            }

            commstr = "select top 1 ball_number from card_consumption where cardid='" + CardNo + "' order by enddatetime desc";
            string ball_number = db.ExecuteScalar(commstr).ToString();//所借球号

            string mes = "本次消费" + xf_money.ToString() + "元";
            if (ball_number != "") mes = mes + "\n\n" + "应归还" + ball_number + "号球";
            mes = mes + "\n\n是否要结账？";
            if (MessageBox.Show(mes, "结账卡提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                string sql = "update card_consumption set enddatetime='" + DateTime.Now + "'," + filed + "='" + xf_money + "' where cardid='" + CardNo + "' and " + filed + " is null";
                db.InsertUpdateDel(sql);
                if (filed == "xf_point")
                {
                    if (xf_money > cs.maxMoney) xf_money = cs.maxMoney;
                    double integral = xf_money * 1;//计算消费积分

                    sql = "update vipcard set point=point-" + xf_money + ",xf_integral=xf_integral+" + integral + " where cardid='" + CardNo + "'";
                    db.InsertUpdateDel(sql);

                    sql = "select point,integral,xf_integral,shop_integral from vipcard where cardid='" + CardNo + "'";
                    SqlDataReader rs = db.ExecuteSqlDataReader(sql);
                    rs.Read();
                    string point = rs[0].ToString();
                    integral = double.Parse(rs[1].ToString()) + double.Parse(rs[2].ToString()) + double.Parse(rs[3].ToString());
                    double user_point = double.Parse(point);
                    mes = "本次消费" + xf_money.ToString() + "元\n\n";
                    if (user_point < 0)
                    {
                        mes = mes + "应收款" + Math.Abs(user_point).ToString() + "元\n\n";
                        sql = "update vipcard set point=0 where cardid='" + CardNo + "'";
                        db.InsertUpdateDel(sql);
                    }
                    else
                    {
                        mes = mes + "卡内还剩：" + point + "元\n\n";
                    }
                    rs.Close();
                    mes = mes + "\n\n当前积分：" + integral.ToString();
                    if (ball_number != "") mes = mes + "应归还" + ball_number + "号球";
                }
                else
                {
                    double smoney = ti.deposit - xf_money;
                    if (smoney > 1)
                    {
                        mes = "本次消费" + xf_money.ToString() + "元\n\n应找零" + smoney.ToString() + "元";
                    }
                    else
                    {
                        mes = "本次消费" + xf_money.ToString() + "元\n\n应收款" + Math.Abs(smoney).ToString() + "元";
                    }
                    if (ball_number != "") mes = mes + "\n\n" + "应归还" + ball_number + "号球";
                }
                MessageBox.Show(mes, "刷卡消费", MessageBoxButtons.OK);
            }
            parametersf.dgf1 = true;
            parametersf.dgf2 = true;
        }


        public bool ismember()//判断是否是会员
        {
            string commstr = "select count(id) from vipcard where cardid='" + CardNo + "'";
            if (Convert.ToInt32(db.ExecuteScalar(commstr)) == 1)
            {
                return true;
            }
            return false;
        }

        
    }

    public abstract class CashSuper
    {
        public double TotalMoney { get; set; }
        public double minMoney { get; set; }
        public double maxMoney { get; set; }


        public abstract double Cacl();
    }

    public class CashNormal : CashSuper//正常计费
    {
        DateTime begdatetime, enddatetime;
        double cph;

        public CashNormal(DateTime begdatetime, DateTime enddatetime, double cph)
        {
            this.begdatetime = begdatetime;
            this.enddatetime = enddatetime;
            this.cph = cph;
        }

        public CashNormal(DateTime begdatetime, DateTime enddatetime, double cph,double maxMoney,double minMoney)
        {
            this.begdatetime = begdatetime;
            this.enddatetime = enddatetime;
            this.cph = cph;
            this.maxMoney = maxMoney;
            this.minMoney = minMoney;
        }

        public override double Cacl()
        {
            double totalMinutes = (enddatetime - begdatetime).TotalMinutes;
            totalMinutes = totalMinutes > 2 ? totalMinutes : 0;
            if (totalMinutes != 0.0)
            {
                TotalMoney = Math.Round((totalMinutes / 15) * (cph / 4) + 0.5, 0, MidpointRounding.AwayFromZero);
            }
            return TotalMoney;
        }
    }

    public class CashTimeSharing : CashSuper//分时计费
    {
        Customer customer;
        TariffInfo ti;

        public CashTimeSharing(Customer customer, TariffInfo ti)
        {
            this.customer = customer;
            this.ti = ti;
        }

        public override double Cacl()
        {
            CashNormal cn;

            for (int i = 0; i < ti.key_hour.Count; i++)
            {
                if (customer.begdatetime.Hour < ti.key_hour[i])
                {
                    DateTime key_time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ti.key_hour[i], 0, 0);
                    if (customer.enddatetime.Hour <= ti.key_hour[i])
                    {
                        cn = new CashNormal(customer.begdatetime, customer.enddatetime, ti.cph[i]);
                        TotalMoney += cn.Cacl();
                        maxMoney = i >= ti.max_money.Count ? ti.max_money[ti.max_money.Count - 1] : ti.max_money[i];
                        minMoney = i >= ti.min_money.Count ? ti.min_money[ti.min_money.Count - 1] : ti.min_money[i];
                    }
                    else
                    {
                        cn = new CashNormal(customer.begdatetime, key_time, ti.cph[i]);
                        TotalMoney += cn.Cacl();

                        for (int j = 1; j < ti.key_hour.Count; j++)
                        {
                            if (customer.enddatetime.Hour <= ti.key_hour[j])
                            {
                                cn = new CashNormal(key_time, customer.enddatetime, ti.cph[j]);
                                TotalMoney += cn.Cacl();
                                maxMoney=j >= ti.max_money.Count?ti.max_money[ti.max_money.Count - 1]:ti.max_money[j];
                                minMoney = j >= ti.min_money.Count ? ti.min_money[ti.min_money.Count - 1] : ti.min_money[j];
                                break;
                            }
                            else
                            {
                                DateTime next_key_time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ti.key_hour[j], 0, 0);
                                cn = new CashNormal(key_time, next_key_time, ti.cph[j + 1]);
                                TotalMoney += cn.Cacl();
                                key_time = next_key_time;
                            }
                        }

                        if (customer.enddatetime.Hour > ti.key_hour[ti.key_hour.Count - 1])
                        {
                            maxMoney = ti.max_money[ti.max_money.Count - 1];
                            minMoney = ti.min_money[ti.min_money.Count - 1];
                            DateTime next_key_time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ti.key_hour[ti.key_hour.Count - 1], 0, 0);
                            cn = new CashNormal(next_key_time,customer.enddatetime, ti.cph[ti.cph.Count-1]);
                            TotalMoney += cn.Cacl();
                        }
                    }
                    break;
                }
            }

            if (customer.begdatetime.Hour > ti.key_hour[ti.key_hour.Count - 1])
            {
                maxMoney = ti.max_money[ti.max_money.Count - 1];
                minMoney = ti.min_money[ti.min_money.Count - 1];
                cn = new CashNormal(customer.begdatetime, customer.enddatetime, ti.cph[ti.cph.Count - 1]);
                TotalMoney += cn.Cacl();
            }
            return TotalMoney;
        }
    }
}