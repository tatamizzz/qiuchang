﻿using System;
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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vipcardcz();
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
                vipcardcz();
            }
        }

        private void vipcardcz()
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection myconn = new SqlConnection(connstr);
                double point = Convert.ToDouble(textBox2.Text);
                double integral = 0;
                double integral_base = 0;//充值送积分
                int a = (int) point / 50;
                if (a >= 1) integral = a * integral_base;

                string commstr = "update vipcard set point=point+" + point + ",integral=integral+"+integral+" where cardid='" + textBox1.Text + "'";
                SqlCommand mycomm = new SqlCommand(commstr, myconn);
                myconn.Open();
                if (mycomm.ExecuteNonQuery() == 1)
                {
                    if (MessageBox.Show("续费成功", "点卡续费", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        commstr = "insert into vipcard_cz(cardid,chongzhi) values('" + textBox1.Text + "'," + textBox2.Text + ")";
                        mycomm.CommandText = commstr;
                        mycomm.ExecuteNonQuery();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("此点卡不存在", "点卡续费");
                }
            }
            else
            {
                MessageBox.Show("卡号或者金额不能为空", "警告", MessageBoxButtons.OK);
            }
        }
    }
}