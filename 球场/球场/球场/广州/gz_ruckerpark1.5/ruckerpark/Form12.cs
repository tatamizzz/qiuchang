using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace ruckerpark
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            this.current_model();
        }

        private void jz()
        {
            string person_name = name.Text;
            if (person_name != "")
            {
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection conn = new SqlConnection(connstr);

                string sql = "select count(1) from court where court_name='" + person_name + "' and court_end is null";
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                if (Convert.ToInt32(comm.ExecuteScalar()) == 0)
                {
                    MessageBox.Show("你输入的包场联系人不存在", "包场结算", MessageBoxButtons.OK);
                }else{
                    sql = "select court_start from court where court_name='" + person_name + "' and court_end is null";
                    comm.CommandText=sql;
                    DateTime start = Convert.ToDateTime(comm.ExecuteScalar().ToString());
                    DateTime end = DateTime.Now;
                    double total_min = Math.Truncate((end - start).TotalMinutes);
                    double total_hour = Math.Round(total_min / 60);

                    int now_hourmin = int.Parse(DateTime.Now.ToString("Hmm"));
                    int start_hourmin = int.Parse(start.ToString("Hmm"));

                    string key = this.current_money(); ;
                    double money = double.Parse(ConfigurationManager.AppSettings[key]);

                    double min_money = money / 60;
                    double xf_money = 0;

                    if (this.check_holiday())
                    {
                        xf_money = Math.Round(total_min * min_money, 1);
                    }
                    else
                    {
                        if (now_hourmin <= 1530 && start_hourmin <= 1530) xf_money = Math.Round(total_min * min_money, 1);
                        if (now_hourmin >= 1530 && now_hourmin >= 1530) xf_money = Math.Round(total_min * min_money, 1);
                        if (start_hourmin < 1530 && now_hourmin >= 1530)
                        {
                            DateTime tmp_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 00);
                            double mins = Math.Truncate((tmp_date - start).TotalMinutes);
                            double min_two = total_min - mins;
                            double moneys = double.Parse(ConfigurationManager.AppSettings["team_am"]);
                            double moneys_min = moneys / 60;

                            xf_money = Math.Round(mins * moneys_min, 1);
                            xf_money = xf_money + Math.Round(min_two * min_money);
                            xf_money = Math.Round(xf_money, 1);
                        }
                    }
                    string mes = "本次消费" + xf_money.ToString() + "元";
                    if (MessageBox.Show(mes, "结账卡提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        sql = "update court set court_end='" + end + "',xf_money='" + xf_money + "' where court_name='" + person_name + "'";
                        comm.CommandText = sql;
                        comm.ExecuteNonQuery();
                    }
                    parametersf.dgf1 = true;
                    parametersf.dgf2 = true;
                    this.Close();
                }
            }
            else {
                MessageBox.Show("请输入包场联系人", "包场结算", MessageBoxButtons.OK);
            }
        }

        private void current_model()
        {
            bool holiday = this.check_holiday();
            DateTime dt = DateTime.Now;
            int nowdate = int.Parse(DateTime.Now.ToString("Hmm"));
            string key = "";
            string text = "";
            if (holiday == true)
            {
                key = "team_holiday";
                text = "当前包场价格：";
            }
            if (nowdate >= 1530 && holiday == false)
            {
                key = "team_pm";
                text = "当前包场价格：";
            }

            if (nowdate < 1530 && holiday == false)
            {
                key = "team_am";
                text = "当前包场价格：";
            }
            string money = ConfigurationManager.AppSettings[key];
            text = money + "元/小时";
            title.Text = text;
        }

        private string current_money()
        {
            bool holiday = this.check_holiday();
            DateTime dt = DateTime.Now;
            int nowdate = int.Parse(DateTime.Now.ToString("Hmm"));
            string key = "";
            if (holiday == true) key = "team_holiday";
            if (nowdate >= 1530 && holiday == false) key = "team_pm";
            if (nowdate < 1530 && holiday == false) key = "team_am";
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.jz();
        }

        private void name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                jz();
            }
        }
    }
}
