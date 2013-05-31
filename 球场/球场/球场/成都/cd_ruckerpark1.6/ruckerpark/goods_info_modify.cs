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

        private void goods_info_modify_Load(object sender, EventArgs e)
        {

            showgoodsname();
            
            label8.Text = parametersf.username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "update goods_info set goods_name='"+textBox1.Text+"',goods_price="+textBox3.Text+",modify_person='"+label8.Text+"' where goods_name='" + comboBox2.Text + "'";
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

        private void showgoodsname()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select goods_name from goods_info";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            DataSet myds = new DataSet();
            new SqlDataAdapter(mycomm).Fill(myds);
            comboBox2.DataSource = myds.Tables[0];
            comboBox2.DisplayMember = myds.Tables[0].Columns[0].ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (parametersf.isnullrecord("select count(1) from goods_info where goods_name='" + comboBox2.Text + "'"))
            {
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection myconn = new SqlConnection(connstr);
                string commstr = "select * from goods_info where goods_name='" + comboBox2.Text + "'";
                SqlCommand mycomm = new SqlCommand(commstr, myconn);
                DataTable mydt = new DataTable();
                new SqlDataAdapter(mycomm).Fill(mydt);
                textBox1.Text = mydt.Rows[0][3].ToString();
                textBox3.Text = mydt.Rows[0][6].ToString();
            }
            else
            {
                MessageBox.Show("此条码不存在", "警告", MessageBoxButtons.OK);
            }
        }
    }
}