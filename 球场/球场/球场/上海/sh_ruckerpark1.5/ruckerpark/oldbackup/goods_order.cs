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
    public partial class goods_order : Form
    {
        public goods_order()
        {
            InitializeComponent();
        }

        DataTable goods_order_dt = new DataTable();
        SqlDataAdapter myda;
        double orderid = 0;

        private void goods_order_Load(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select goods_tiaoma as '条码',goods_huohao as '货号',goods_name as '商品名称',goods_spcification as '规格',goods_stock_now as '当前库存',goods_number_order as '订货量',reporter,orderid,goods_purchase_price from goods_order where id<0";
            SqlCommand mycomm = new SqlCommand(commstr,myconn);

            myda = new SqlDataAdapter(mycomm);
            myda.Fill(goods_order_dt);
            dataGridView1.DataSource = goods_order_dt;
            maskedTextBox1.Text = "1";
            textBox1.Select();
            orderid =Convert.ToDouble( DateTime.Now.ToString("yyyyMMddhhmmss"));
            dataGridView1.Columns["reporter"].Visible=false;
            dataGridView1.Columns["orderid"].Visible=false;
            
        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text != "")//条码不能为空
                {
                    string connstr = ConfigurationManager.AppSettings["connectionstring"];
                    SqlConnection myconn = new SqlConnection(connstr);
                    string commstr = "select goods_tiaoma,goods_info.goods_huohao,goods_name,goods_spcification,goods_stock from goods_info,goods_stock where goods_tiaoma='" + textBox1.Text + "' and goods_stock.goods_huohao=goods_info.goods_huohao";
                    SqlCommand mycomm = new SqlCommand(commstr, myconn);
                    DataTable mydt = new DataTable();
                    new SqlDataAdapter(mycomm).Fill(mydt);
                    if (mydt.Rows.Count == 1)
                    {
                        DataRow mydr = goods_order_dt.NewRow();
                        mydr[0] = mydt.Rows[0][0];
                        mydr[1] = mydt.Rows[0][1];
                        mydr[2] = mydt.Rows[0][2];
                        mydr[3] = mydt.Rows[0][3];
                        mydr[4] = mydt.Rows[0][4];
                        mydr[5] = maskedTextBox1.Text;
                        mydr[6] = parametersf.username;
                        mydr[7] = orderid;
                        mydr[8] = "0.0";
                        goods_order_dt.Rows.Add(mydr);
                    }
                    else
                    {
                        MessageBox.Show("条码错误！", "警告", MessageBoxButtons.OK);
                    }
                    textBox1.Text = "";
                    textBox1.Focus();
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder myscb = new SqlCommandBuilder(myda);
            if (myda.Update(goods_order_dt) == goods_order_dt.Rows.Count)
            {
                MessageBox.Show("订单提交成功", "提醒", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("订单提交失败", "警告", MessageBoxButtons.OK);
            }
        }
    }
}