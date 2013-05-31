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
    public partial class integral_manage : Form
    {
        public integral_manage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.integral_save();
        }

        private void integral_save()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            string sql = "insert into integral(integral,product_name,product_num)values('" + integral.Text + "','" + product_name.Text + "','" + product_num.Text + "')";
            if (button1.Tag!=null) sql = "update integral set integral='"+integral.Text+"',product_name='"+product_name.Text+"',product_num='"+product_num.Text+"' where id='"+button1.Tag.ToString()+"'";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            comm.ExecuteNonQuery();
            integral.Text = "";
            product_name.Text = "";
            product_num.Text = "";
            this.integral_list();
            MessageBox.Show("保存成功", "系统提示");
        }

        private void product_num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                this.integral_save();
            }
        }

        private void integral_list()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            string sql = "select integral,product_name,product_num from integral order by integral;";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader rs = comm.ExecuteReader();
            string text = "";
            while (rs.Read())
            {
                text = text + "所需积分:" + rs[0].ToString() + " 兑换物品:" + rs[1].ToString().Trim() + "（库存:" + rs[2].ToString() + "）\n";
            }
            integral_pro.Text = text;
        }

        private void integral_manage_Load(object sender, EventArgs e)
        {
            this.integral_list();
        }

        private void select_name(string p_name)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection conn = new SqlConnection(connstr);
            string sql = "select top 1 integral,product_name,product_num,id from integral where product_name like '%"+p_name+"%' order by integral;";
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader rs = comm.ExecuteReader();
            while (rs.Read())
            {
                integral.Text = rs[0].ToString();
                product_name.Text = rs[1].ToString();
                product_num.Text = rs[2].ToString();
                button1.Tag = rs[3].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.select_name(s_name.Text.ToString());
        }

        private void s_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.select_name(s_name.Text.ToString());
            }
        }
    }
}
