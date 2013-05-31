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
    public partial class goods_order_check : Form
    {
        public goods_order_check()
        {
            InitializeComponent();
        }

        DataTable goods_order_check_dt=new DataTable();
        SqlDataAdapter myda;

        private void goods_order_check_Load(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select orderid from goods_order_list_state order by orderid desc";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            DataTable mydt = new DataTable();
            new SqlDataAdapter(mycomm).Fill(mydt);
            comboBox1.DataSource = mydt;
            comboBox1.DisplayMember = mydt.Columns[0].ToString();
            comboBox1.ValueMember = mydt.Columns[0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select goods_name as '商品名称',goods_spcification as '单位',goods_stock_now as '当前库存',goods_number_order as '订货量',goods_number_storage as '实收数量',orderid,goods_huohao,id from goods_order where orderid='" + comboBox1.SelectedValue + "'";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myda = new SqlDataAdapter(mycomm);
            goods_order_check_dt.Clear();
            myda.Fill(goods_order_check_dt);
            dataGridView1.DataSource = goods_order_check_dt;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder myscb = new SqlCommandBuilder(myda);
            if (myda.Update(goods_order_check_dt) == dataGridView1.Rows.Count)
            {
                MessageBox.Show("验货成功", "提醒", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("验货失败", "警告", MessageBoxButtons.OK);
            }
        }
    }
}