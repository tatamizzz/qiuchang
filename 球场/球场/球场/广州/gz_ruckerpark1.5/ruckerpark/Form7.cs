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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ball_borrow();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ball_borrow();
            }
        }

        private void ball_borrow()
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection myconn = new SqlConnection(connstr);
                string commstr = "update card_consumption set ball_number=" + textBox2.Text + " where cardid='" + textBox1.Text + "' and enddatetime is null";
                SqlCommand mycomm = new SqlCommand(commstr, myconn);
                myconn.Open();
                if (mycomm.ExecuteNonQuery() == 1)
                {

                    if (MessageBox.Show("借球成功", "借球", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        myconn.Close();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("此卡尚未开卡", "警告", MessageBoxButtons.OK);
                }
                myconn.Close();
            }
            else
            {
                MessageBox.Show("卡号或者球号不能为空", "警告", MessageBoxButtons.OK);
            }
            parametersf.dgf1 = true;
            parametersf.dgf2 = true;
        }
    }
}