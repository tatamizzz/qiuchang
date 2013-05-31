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
            this.gethuohao();
        }

        private void gethuohao()
        {
            label8.Text = parametersf.username;
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select max(goods_huohao) as huohao from goods_info";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myconn.Open();
            SqlDataReader rs = mycomm.ExecuteReader();
            int huohao = 0;
            while (rs.Read())
            {
                huohao = rs[0].ToString() == "" ? 0 : int.Parse(rs[0].ToString());
            }
            label2.Text = (huohao + 1).ToString();
            myconn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "insert into goods_info(goods_huohao,goods_name,goods_price,reporter) values("+label2.Text+",'"+textBox1.Text+"','"+textBox3.Text+"','"+label8.Text+"')";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myconn.Open();
            if (mycomm.ExecuteNonQuery() == 2)
            {
                MessageBox.Show("商品信息添加成功", "提醒", MessageBoxButtons.OK);
                textBox1.Text = "";
                textBox3.Text = "";
                this.gethuohao();
            }
            myconn.Close();
        }
    }
}