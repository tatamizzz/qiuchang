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
            string commstr = "select goods_huohao as '货号',goods_name as '商品名称',goods_spcification as '规格',goods_stock_now as '当前库存',goods_number_order as '订货量',reporter,orderid,goods_purchase_price as '进货价',goods_num as '箱数' from goods_order where id<0";
            SqlCommand mycomm = new SqlCommand(commstr,myconn);

            myda = new SqlDataAdapter(mycomm);
            myda.Fill(goods_order_dt);
            dataGridView1.DataSource = goods_order_dt;
            orderid =Convert.ToDouble( DateTime.Now.ToString("yyyyMMddhhmmss"));
            dataGridView1.Columns["reporter"].Visible=false;
            dataGridView1.Columns["orderid"].Visible=false;

            commstr = "select goods_name from goods_info";
            SqlCommand mycomm1 = new SqlCommand(commstr, myconn);
            SqlDataAdapter myda1 = new SqlDataAdapter(mycomm1);
            DataSet myds = new DataSet();
            myda1.Fill(myds);
            comboBox1.DataSource = myds.Tables[0];
            comboBox1.DisplayMember = myds.Tables[0].Columns[0].ToString();
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

        private void addgoodsorder()
        {
            
                if (comboBox1.Text != "")//条码不能为空
                {
                    string connstr = ConfigurationManager.AppSettings["connectionstring"];
                    SqlConnection myconn = new SqlConnection(connstr);

                    string commstr = "select goods_info.goods_huohao,goods_name,goods_spcification,goods_stock.goods_stock from goods_info,goods_stock where goods_name='" + comboBox1.Text + "' and goods_stock.goods_huohao=goods_info.goods_huohao";
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
                        mydr[4] = int.Parse(order_number.Text) * int.Parse(nums.SelectedItem.ToString());
                        mydr[5] = parametersf.username;
                        mydr[6] = orderid;
                        mydr[7] = "0.0";
                        mydr[8] = int.Parse(nums.SelectedItem.ToString());
                        goods_order_dt.Rows.Add(mydr);
                    }
                    else
                    {
                        MessageBox.Show("条码错误！", "警告", MessageBoxButtons.OK);
                    }
                }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            addgoodsorder();
        }
    }
}