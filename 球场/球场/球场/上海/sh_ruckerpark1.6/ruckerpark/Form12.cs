using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace ruckerpark
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void jz()
        {
            string person_name = name.Text;
            if (person_name != "")
            {
                string connstr = ConfigurationManager.AppSettings["connectionstring"];
                SqlConnection conn = new SqlConnection(connstr);

                string sql = "select count(1) from court where court_name='" + person_name + "' and court_end is null";
                SqlCommand comm = new SqlCommand(sql, conn);
                conn.Open();
                if (Convert.ToInt32(comm.ExecuteScalar()) == 0)
                {
                    MessageBox.Show("你输入的包场联系人不存在", "包场结算", MessageBoxButtons.OK);
                }else{
                    sql = "select court_start,court_price,court_hour from court where court_name='" + person_name + "' and court_end is null";
                    comm.CommandText=sql;
                    SqlDataReader rs = comm.ExecuteReader();
                    rs.Read();
                    
                    DateTime start = Convert.ToDateTime(rs[0].ToString());
                    DateTime end = DateTime.Now;
                    double court_price = Convert.ToDouble(rs[1]);
                    double court_hour = Convert.ToDouble(rs[2]);
                    double xf_money = 0;
                    rs.Close();

                    //计算打了多久
                    double total_min = Math.Truncate((end - start).TotalMinutes);
                    int hour = (int)total_min / 60;
                    if (hour<1)
                    {
                        hour = 1;
                    }

                    //计算每10分钟的价格
                    double min_money = Math.Round(court_price / 2);
                    double mins =total_min - (hour * 60);
                    double court_min = Math.Ceiling(mins / 30);
                    xf_money = hour * court_price;
                    if (mins > 0)
                    { 
                        xf_money=xf_money+(court_min * min_money);
                    }
                    
                    string mes = "本次消费" + xf_money.ToString() + "元";
                                       
                    if (MessageBox.Show(mes, "结账卡提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        sql = "update court set court_end='" + end + "',xf_money='" + xf_money + "' where court_name='" + person_name + "'";
                        comm.CommandText = sql;
                        comm.ExecuteNonQuery();
                    }
                    parametersf.dgf1 = true;
                    parametersf.dgf2 = true;
                    this.Close();
                }
            }
            else {
                MessageBox.Show("请输入包场联系人", "包场结算", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.jz();
        }

        private void name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                jz();
            }
        }
    }
}
