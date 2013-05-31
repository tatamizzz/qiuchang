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
    public partial class fill_card : Form
    {
        public fill_card()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr="";
            if(textBox1.Text!="")
                commstr = "select cardid from vipcard where m_name='" + textBox1.Text + "'";
            if(textBox2.Text!="")
                commstr = "select cardid from vipcard where m_mobile='" + textBox2.Text + "'";
            if (parametersf.isnullrecord("select count(1)"+commstr.Substring(14)))
            {
                SqlCommand mycomm = new SqlCommand(commstr, myconn);
                myconn.Open();
                label4.Text = mycomm.ExecuteScalar().ToString();
                myconn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label4.Text != "")
            {
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection myconn = new SqlConnection(connstr);
                string commstr = "update vipcard set cardid='"+textBox3.Text+"' where cardid='"+label4.Text+"'";
                SqlCommand mycomm = new SqlCommand(commstr, myconn);
                myconn.Open();
                if (mycomm.ExecuteNonQuery() == 1)
                {
                    commstr = "update vipcard set point=point-10 where cardid='" + textBox3.Text + "'";
                    mycomm.CommandText = commstr;
                    if (mycomm.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("²¹¿¨³É¹¦","²¹¿¨", MessageBoxButtons.OK);
                    }
                }
                myconn.Close();
            }
        }
    }
}