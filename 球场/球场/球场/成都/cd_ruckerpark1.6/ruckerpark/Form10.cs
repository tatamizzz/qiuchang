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
            string sql = "insert into court(court_name,court_number,court_per_num,court_tel,court_start,court_price,court_hour)values('" + court_name.Text + "','" + court_number.Text + "','" + court_per_num.Text + "','" + tel.Text + "','" + court_start + "',"+price.Text+","+Hours.Text.ToString()+")";
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
            if (court_name.Text == "" || court_number.Text == "" || court_per_num.Text == "" || tel.Text == "" || price.Text == "") return "包场失败，包场信息没有填写完整";
            if (!parametersf.checknum(tel.Text) || tel.Text.Length != 11) return "包场失败，联系电话失误";
            if (!parametersf.checknum(Hours.Text)) return "包场失败，包场小时不是数字";
            return "";
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
