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
    public partial class adduser : Form
    {
        public adduser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select count(1) from users where cardid='"+textBox1.Text+"'";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myconn.Open();
            if (mycomm.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("卡号已存在", "警告", MessageBoxButtons.OK);
            }
            else
            {
                commstr = "insert into users(cardid,name,password,rights) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "'," + comboBox1.SelectedItem.ToString() + ")";
                mycomm.CommandText = commstr;
                if (mycomm.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("添加成功", "提醒", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("添加失败", "警告", MessageBoxButtons.OK);
                }
            }
            myconn.Close();
        }
    }
}