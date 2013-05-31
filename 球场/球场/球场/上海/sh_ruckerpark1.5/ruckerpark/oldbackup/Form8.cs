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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            label8.Text = parametersf.username;
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select id,specification_name from goods_specification";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            DataSet myds = new DataSet();
            new SqlDataAdapter(mycomm).Fill(myds);
            comboBox1.DataSource = myds.Tables[0];
            comboBox1.DisplayMember = myds.Tables[0].Columns[1].ToString();
            comboBox1.ValueMember = myds.Tables[0].Columns[1].ToString();

            commstr = "select max(goods_huohao) from goods_info";
            mycomm.CommandText = commstr;
            myconn.Open();
            label2.Text = (Convert.ToInt32(mycomm.ExecuteScalar()) + 1).ToString();
            myconn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_price,reporter) values("+label2.Text+",'"+textBox2.Text+"','"+textBox1.Text+"','"+comboBox1.SelectedValue+"',"+textBox3.Text+",'"+label8.Text+"')";
            MessageBox.Show(commstr);
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myconn.Open();
            if (mycomm.ExecuteNonQuery() == 2)
            {
                MessageBox.Show("商品信息添加成功", "提醒", MessageBoxButtons.OK);
                Close();
            }
            myconn.Close();
        }
    }
}