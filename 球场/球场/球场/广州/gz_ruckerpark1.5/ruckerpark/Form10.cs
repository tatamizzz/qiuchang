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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
         
            this.current_model();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = this.checkdata();
            if (message != "")
            {
                msg.Text = message;
            }
            else
            {
                court();
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                court();
            }
            
        }

        private void court()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            string court_start = DateTime.Now.ToString();
            string sql = "insert into court(court_name,court_number,court_per_num,court_tel,court_start)values('" + court_name.Text + "','" + court_number.Text + "','" + court_per_num.Text + "','" + tel.Text + "','" + court_start + "')";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            if (comm.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("包场成功", "提醒");
            }
            else
            {
                msg.Text = sql;
            }
            parametersf.dgf1 = true;
            parametersf.dgf2 = true;
            this.Close();
        }

        private string checkdata()
        {
            if (court_name.Text == "" || court_number.Text == "" || court_per_num.Text == "" || tel.Text == "") return "包场失败，包场信息没有填写完整";
            if (!parametersf.checknum(tel.Text) || tel.Text.Length != 11) return "包场失败，联系电话失误";
            return "";
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

        private void tel_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                court();
            }
        }

        private void tel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                court();
            }
        }
    }
}
