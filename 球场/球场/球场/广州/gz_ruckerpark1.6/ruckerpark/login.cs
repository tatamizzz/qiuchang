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
    
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginsys();
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

        private void loginsys()
        {
            if (parametersf.checknum(textBox1.Text))
            {
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection myconn = new SqlConnection(connstr);
                string commstr = "select count(1) from users where cardid='" + parametersf.checkstr(textBox1.Text) + "'";
                SqlCommand mycomm = new SqlCommand(commstr, myconn);
                myconn.Open();
                if (mycomm.ExecuteScalar().ToString() != "1")
                {
                    MessageBox.Show("请使用员工卡登入", "警告", MessageBoxButtons.OK);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Focus();
                }
                else
                {
                    commstr = "select password from users where cardid='" + textBox1.Text + "'";
                    mycomm.CommandText = commstr;
                    if (textBox2.Text == mycomm.ExecuteScalar().ToString())
                    {
                        myconn.Close();
                        parametersf.username = textBox1.Text;
                        Form1 f1 = new Form1();
                        f1.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("卡号或者密码错误", "警告", MessageBoxButtons.OK);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox1.Focus();
                    }
                }
                myconn.Close();
            }
            else
            {
                MessageBox.Show("此卡号含有非法字符", "警告", MessageBoxButtons.OK);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginsys();
            }
        }
    }
}