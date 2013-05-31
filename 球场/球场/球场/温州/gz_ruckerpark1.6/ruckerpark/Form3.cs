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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vipcardsale();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection myconn = new SqlConnection(connstr);
                string commstr = "select count(1) from vipcard where cardid='"+textBox1.Text+"'";
                SqlCommand mycomm = new SqlCommand(commstr, myconn);
                myconn.Open();
                if (Convert.ToInt16(mycomm.ExecuteScalar().ToString()) >= 1)
                {
                    MessageBox.Show("会员卡号已存在", "警告", MessageBoxButtons.OK);
                    textBox1.Text = "";
                }
                else
                {
                    textBox6.Focus();
                }
                myconn.Close();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select count(1) from vipcard where cardid='" + textBox1.Text + "'";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myconn.Open();
            if (Convert.ToInt16(mycomm.ExecuteScalar().ToString()) >= 1)
            {
                MessageBox.Show("会员卡号已存在", "警告", MessageBoxButtons.OK);
                textBox1.Text = "";
            }
            myconn.Close();
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                vipcardsale();
            }
        }

        private void vipcardsale()
        {
            if (textBox1.Text != "" && textBox6.Text != "")
            {
                double point = int.Parse(textBox6.Text.ToString());
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection myconn = new SqlConnection(connstr);
                double integral = 0;
                double integral_base = 200;
                int a = (int) point/200;
                if (a >= 1) integral = a * integral_base;

                string commstr = "insert into vipcard(cardid,m_name,m_age,m_qq,m_mobile,point,hoop_name,integral) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "'," + point + ",'"+hoopname.Text+"','"+integral+"')";
                SqlCommand mycomm = new SqlCommand(commstr, myconn);
                myconn.Open();
                if (mycomm.ExecuteNonQuery() == 2)
                {

                    MessageBox.Show("销售成功", "点卡销售", MessageBoxButtons.OK);
                    Close();

                }
                myconn.Close();
            }
            else
            {
                MessageBox.Show("卡号或者金额不能为空", "警告", MessageBoxButtons.OK);
            }
        }
    }
}