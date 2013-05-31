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
    public partial class goods_info_modify : Form
    {
        public goods_info_modify()
        {
            InitializeComponent();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (parametersf.isnullrecord("select count(1) from goods_info where goods_tiaoma='" + textBox4.Text + "'"))
                {
                    string connstr = ConfigurationManager.AppSettings["connectionstring"];
                    SqlConnection myconn = new SqlConnection(connstr);
                    string commstr = "select * from goods_info where goods_tiaoma='" + textBox4.Text + "'";
                    SqlCommand mycomm = new SqlCommand(commstr, myconn);
                    DataTable mydt = new DataTable();
                    new SqlDataAdapter(mycomm).Fill(mydt);
                    textBox1.Text = mydt.Rows[0][3].ToString();
                    textBox2.Text = mydt.Rows[0][2].ToString();
                    textBox3.Text = mydt.Rows[0][6].ToString();
                    textBox5.Text=mydt.Rows[0][5].ToString();
                    comboBox1.SelectedIndex = comboBox1.FindString(mydt.Rows[0][4].ToString());
                }
                else
                {
                    MessageBox.Show("此条码不存在", "警告", MessageBoxButtons.OK);
                }
            }
        }

        private void goods_info_modify_Load(object sender, EventArgs e)
        {
            textBox4.Select();
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select id,specification_name from goods_specification";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            DataSet myds = new DataSet();
            new SqlDataAdapter(mycomm).Fill(myds);
            comboBox1.DataSource = myds.Tables[0];
            comboBox1.DisplayMember = myds.Tables[0].Columns[1].ToString();
            comboBox1.ValueMember = myds.Tables[0].Columns[1].ToString();
            label8.Text = parametersf.username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "update goods_info set goods_tiaoma='"+textBox2.Text+"',goods_name='"+textBox1.Text+"', goods_spcification='"+comboBox1.SelectedValue+"',goods_price="+textBox3.Text+",modify_person='"+label8.Text+"',goods_spcification_big="+textBox5.Text+" where goods_tiaoma='" + textBox4.Text + "'";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myconn.Open();
            if (mycomm.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("商品信息修改成功", "提醒", MessageBoxButtons.OK);
                Close();
            }
            else
            {
                MessageBox.Show("商品信息修改失败", "警告", MessageBoxButtons.OK);
            }
            myconn.Close();
        }
    }
}