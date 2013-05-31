using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

namespace ruckerpark
{
    public partial class saledrink : Form
    {
        public saledrink()
        {
            InitializeComponent();
        }

        DataSet myds = new DataSet();
        SqlDataAdapter myda;
        ArrayList pricelist = new ArrayList();

        private void saledrink_Load(object sender, EventArgs e)
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "select goods_huohao as '货号',goods_tiaoma as '条码',goods_amounts as '数量' from goods_sale where id<0";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);


            myda = new SqlDataAdapter(mycomm);
            myda.Fill(myds);
            myds.Tables[0].Columns.Add("名称");
            dataGridView1.DataSource = myds.Tables[0];
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            DataGridViewButtonColumn dgvbc = new DataGridViewButtonColumn();
            dgvbc.Text = "删除";
            dgvbc.Name = "删除";
            dgvbc.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(dgvbc);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double totalmoney = 0.0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                totalmoney +=Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()) *Convert.ToDouble(pricelist[i]);
            }
            parametersf.showmessage("应收金额："+totalmoney.ToString());
            SqlCommandBuilder myscb = new SqlCommandBuilder(myda);
            myda.Update(myds.Tables[0]);
            myds.Tables[0].Clear();
            dataGridView1.Refresh();
            textBox2.Focus();
            pricelist.Clear();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text != "")//条码不能为空
                {
                    string connstr = ConfigurationManager.AppSettings["connectionstring"];
                    SqlConnection myconn = new SqlConnection(connstr);
                    string commstr = "select goods_huohao,goods_name,goods_price from goods_info where goods_tiaoma='" + textBox2.Text + "'";
                    SqlCommand mycomm = new SqlCommand(commstr, myconn);
                    DataTable mydt = new DataTable();
                    new SqlDataAdapter(mycomm).Fill(mydt);
                    if (mydt.Rows.Count != 0)//条码不能错误
                    {
                        DataRow mydr = myds.Tables[0].NewRow();
                        mydr[0] = mydt.Rows[0][0];
                        mydr[3] = mydt.Rows[0][1];
                        mydr[1] = textBox2.Text;
                        mydr[2] = comboBox2.SelectedItem.ToString();
                        myds.Tables[0].Rows.Add(mydr);
                        pricelist.Add(mydt.Rows[0][2]);
                    }
                    else
                    {
                        MessageBox.Show("条码错误！", "警告", MessageBoxButtons.OK);
                    }
                        textBox2.Text = "";
                        comboBox2.SelectedIndex = 0;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "删除")
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
                pricelist.RemoveAt(e.RowIndex);
            }
        }


    }
}