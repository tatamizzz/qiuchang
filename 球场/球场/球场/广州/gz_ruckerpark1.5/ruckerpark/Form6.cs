using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace ruckerpark
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        ParameterDiscreteValue pdv1 = new ParameterDiscreteValue();
        ParameterDiscreteValue pdv2 = new ParameterDiscreteValue();

        private void button1_Click(object sender, EventArgs e)
        {
            

            ParameterFields paramFields = new ParameterFields();
            ParameterField paramField1 = new ParameterField();
            paramField1.Name = "@starttime";
            
            pdv1.Value = dateTimePicker1.Value.Date;
            paramField1.CurrentValues.Add(pdv1);
            paramFields.Add(paramField1);

            ParameterField paramField2 = new ParameterField();
            paramField2.Name = "@endtime";
            
            pdv2.Value = new DateTime(dateTimePicker2.Value.Year, dateTimePicker2.Value.Month, dateTimePicker2.Value.Day,23,59,59);
            paramField2.CurrentValues.Add(pdv2);
            paramFields.Add(paramField2);

            
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        Form9 f9 = new Form9();
                        f9.ShowDialog();
                        break;
                    }
                case 1:
                    {
                        initdb();
                        break;
                    }

                case 2:
                    if (dateTimePicker1.Value==dateTimePicker2.Value)
                    {
                        CrystalReport5 CrystalReport = new CrystalReport5();
                        crystalReportViewer1.ParameterFieldInfo = paramFields;
                        crystalReportViewer1.ReportSource = CrystalReport;
                    }
                    else
                    {
                        CrystalReport1 CrystalReport = new CrystalReport1();
                        crystalReportViewer1.ParameterFieldInfo = paramFields;
                        crystalReportViewer1.ReportSource = CrystalReport;
                    }
                    break;
                case 3:
                    CrystalReport2 CrystalReport1 = new CrystalReport2();
                    crystalReportViewer1.ParameterFieldInfo = paramFields;
                    crystalReportViewer1.ReportSource = CrystalReport1;
                    break;
                case 4:
                    CrystalReport3 CrystalReport2 = new CrystalReport3();
                    crystalReportViewer1.ParameterFieldInfo = paramFields;
                    crystalReportViewer1.ReportSource = CrystalReport2;
                    break;
                case 5:
                    CrystalReport4 CrystalReport3 = new CrystalReport4();
                    crystalReportViewer1.ParameterFieldInfo = paramFields;
                    crystalReportViewer1.ReportSource = CrystalReport3;
                    break;
            }
        }

        private void initdb()
        {
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);
            string commstr = "truncate table total_table";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);
            myconn.Open();
            mycomm.ExecuteNonQuery();
            myconn.Close();
            DataSet myds = new DataSet();

            myds.Tables.Add();
            myds.Tables[0].Columns.Add("时间");
            myds.Tables[0].Columns.Add("会员消费");
            myds.Tables[0].Columns.Add("非会员消费");
            myds.Tables[0].Columns.Add("借球");
            myds.Tables[0].Columns.Add("会员卡充值");
            myds.Tables[0].Columns.Add("商品销售");
            myds.Tables.Add();
            myds.Tables.Add();
            myds.Tables.Add();
            myds.Tables.Add();
            myds.Tables.Add();
            if (dateTimePicker1.Value.Date == dateTimePicker2.Value.Date)
            {
                commstr = "select min(datepart(hh,begdatetime)) as '时间',sum(xf_point) as '会员消费',sum(xf_money) as '非会员消费' from card_consumption where begdatetime between '" + pdv1.Value + "' and '" + pdv2.Value + "' group by datepart(hh,begdatetime)";
            }
            else
            {
                commstr = "select convert(char(10),max(begdatetime),120) as '时间',sum(xf_point) as '会员消费',sum(xf_money) as '非会员消费' from card_consumption where begdatetime between '" + pdv1.Value + "' and '" + pdv2.Value + "' group by datepart(dd,begdatetime)";
            }
            mycomm.CommandText = commstr;
            new SqlDataAdapter(mycomm).Fill(myds.Tables[1]);
            if (dateTimePicker1.Value.Date == dateTimePicker2.Value.Date)
            {
                commstr = "select min(datepart(hh,adddatetime)) as '时间',sum(chongzhi) as '会员卡冲值' from vipcard_cz where adddatetime between '" + pdv1.Value + "' and '" + pdv2.Value + "' group by datepart(hh,adddatetime)";
            }
            else
            {
                commstr = "select convert(char(10),max(adddatetime),120) as '时间',sum(chongzhi) as '会员卡冲值' from vipcard_cz where adddatetime between '" + pdv1.Value + "' and '" + pdv2.Value + "' group by datepart(dd,adddatetime)";
            }
            mycomm.CommandText = commstr;
            new SqlDataAdapter(mycomm).Fill(myds.Tables[2]);
            if (dateTimePicker1.Value.Date == dateTimePicker2.Value.Date)
            {
                commstr = "select min(datepart(hh,adddatetime)) as '时间',sum(salemoney) as '会员卡销售' from vipcard_sale where adddatetime between '" + pdv1.Value + "' and '" + pdv2.Value + "' group by datepart(hh,adddatetime)";
            }
            else
            {
                commstr = "select convert(char(10),max(adddatetime),120) as '时间',sum(salemoney) as '会员卡销售' from vipcard_sale where adddatetime between '" + pdv1.Value + "' and '" + pdv2.Value + "' group by datepart(dd,adddatetime)";
            }
            mycomm.CommandText = commstr;
            new SqlDataAdapter(mycomm).Fill(myds.Tables[3]);
            if (dateTimePicker1.Value.Date == dateTimePicker2.Value.Date)
            {
                commstr = "select min(datepart(hh,begdatetime)) as '时间',count(*)*5 as '非会员借球金额' from card_consumption where ball_number is not null and xf_point is null and xf_money is not null and begdatetime between '" + pdv1.Value + "' and '" + pdv2.Value + "' group by datepart(hh,enddatetime)";
            }
            else
            {
                commstr = "select convert(char(10),max(begdatetime),120) as '时间',count(*)*5 as '非会员借球金额' from card_consumption where ball_number is not null and xf_point is null and xf_money is not null and begdatetime between '" + pdv1.Value + "' and '" + pdv2.Value + "' group by datepart(dd,enddatetime)";
            }
            mycomm.CommandText = commstr;
            new SqlDataAdapter(mycomm).Fill(myds.Tables[4]);
            if (dateTimePicker1.Value.Date == dateTimePicker2.Value.Date)
            {
                commstr = "select min(datepart(hh,goods_sale.adddate)) as '时间',sum(goods_amounts*goods_price) from goods_sale,goods_info where goods_info.goods_huohao=goods_sale.goods_huohao and goods_sale.adddate between '" + pdv1.Value + "' and '" + pdv2.Value + "' group by datepart(hh,goods_sale.adddate)";
            }
            else
            {
                commstr = "select min(datepart(dd,goods_sale.adddate)) as '时间',sum(goods_amounts*goods_price) from goods_sale,goods_info where goods_info.goods_huohao=goods_sale.goods_huohao and goods_sale.adddate between '" + pdv1.Value + "' and '" + pdv2.Value + "' group by datepart(dd,goods_sale.adddate)";
            }
            mycomm.CommandText = commstr;
            new SqlDataAdapter(mycomm).Fill(myds.Tables[5]);

            for (int i = 0; i < myds.Tables[1].Rows.Count; i++)
            {
                DataRow mydr = myds.Tables[0].NewRow();
                mydr[0] = myds.Tables[1].Rows[i][0];
                mydr[1] = myds.Tables[1].Rows[i][1];
                mydr[2] = myds.Tables[1].Rows[i][2];
                myds.Tables[0].Rows.Add(mydr);
            }

            for (int i = 0; i < myds.Tables[4].Rows.Count; i++)
            {
                bool flg = true;
                for (int j = 0; j < myds.Tables[0].Rows.Count; j++)
                {
                    if (myds.Tables[4].Rows[i][0].ToString() == myds.Tables[0].Rows[j][0].ToString())
                    {
                        if (myds.Tables[0].Rows[j][3].ToString() != "")
                        {
                            myds.Tables[0].Rows[j][3] = Convert.ToInt32(myds.Tables[0].Rows[j][3].ToString()) + Convert.ToInt32(myds.Tables[4].Rows[i][1].ToString());
                        }
                        else
                        {
                            myds.Tables[0].Rows[j][3] = myds.Tables[4].Rows[i][1].ToString();
                        }
                        flg = false;
                    }
                }
                if (flg)
                {
                    DataRow mydr = myds.Tables[0].NewRow();
                    mydr[0] = myds.Tables[4].Rows[i][0];
                    mydr[3] = myds.Tables[4].Rows[i][1];
                    myds.Tables[0].Rows.Add(mydr);
                }
            }

            for (int i = 0; i < myds.Tables[5].Rows.Count; i++)
            {
                bool flg = true;
                for (int j = 0; j < myds.Tables[0].Rows.Count; j++)
                {
                    if (myds.Tables[5].Rows[i][0].ToString() == myds.Tables[0].Rows[j][0].ToString())
                    {
                        if (myds.Tables[0].Rows[j][5].ToString() != "")
                        {
                            myds.Tables[0].Rows[j][5] = Convert.ToInt32(myds.Tables[0].Rows[j][5].ToString()) + Convert.ToInt32(myds.Tables[5].Rows[i][1].ToString());
                        }
                        else
                        {
                            myds.Tables[0].Rows[j][5] = myds.Tables[4].Rows[i][1].ToString();
                        }
                        flg = false;
                    }
                }
                if (flg)
                {
                    DataRow mydr = myds.Tables[0].NewRow();
                    mydr[0] = myds.Tables[5].Rows[i][0];
                    mydr[5] = myds.Tables[5].Rows[i][1];
                    myds.Tables[0].Rows.Add(mydr);
                }
            }

            for (int i = 0; i < myds.Tables[2].Rows.Count; i++)
            {
                bool flg = true;
                for (int j = 0; j < myds.Tables[0].Rows.Count; j++)
                {
                    if (myds.Tables[2].Rows[i][0].ToString() == myds.Tables[0].Rows[j][0].ToString())
                    {
                        myds.Tables[0].Rows[j][4] = myds.Tables[2].Rows[i][1];
                        flg = false;
                    }
                }
                if (flg)
                {
                    DataRow mydr = myds.Tables[0].NewRow();
                    mydr[0] = myds.Tables[2].Rows[i][0];
                    mydr[4] = myds.Tables[2].Rows[i][1];
                    myds.Tables[0].Rows.Add(mydr);
                }
            }


            for (int i = 0; i < myds.Tables[3].Rows.Count; i++)
            {
                bool flg = true;
                for (int j = 0; j < myds.Tables[0].Rows.Count; j++)
                {
                    if (myds.Tables[3].Rows[i][0].ToString() == myds.Tables[0].Rows[j][0].ToString())
                    {
                        if (myds.Tables[0].Rows[j][4].ToString() != "")
                        {
                            myds.Tables[0].Rows[j][4] = (Convert.ToInt32(myds.Tables[0].Rows[j][4].ToString()) + Convert.ToInt32(myds.Tables[3].Rows[i][1].ToString()));
                        }
                        else
                        {
                            myds.Tables[0].Rows[j][4] = myds.Tables[3].Rows[i][1];
                        }
                        flg = false;
                    }
                }
                if (flg)
                {
                    DataRow mydr = myds.Tables[0].NewRow();
                    mydr[0] = myds.Tables[3].Rows[i][0];
                    mydr[4] = myds.Tables[3].Rows[i][1];
                    myds.Tables[0].Rows.Add(mydr);
                }
            }

            for (int i = 0; i < myds.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < myds.Tables[0].Columns.Count; j++)
                {
                    if (myds.Tables[0].Rows[i][j].ToString() == "") myds.Tables[0].Rows[i][j] = 0;
                }
            }

            myconn.Open();
            for(int i=0;i<myds.Tables[0].Rows.Count;i++)
            {
                commstr = "insert into total_table(时间,会员消费,非会员消费,借球,会员卡充值,商品销售) values('" + myds.Tables[0].Rows[i][0] + "'," + myds.Tables[0].Rows[i][1] + "," + myds.Tables[0].Rows[i][2] + "," + myds.Tables[0].Rows[i][3] + "," + myds.Tables[0].Rows[i][4]+","+myds.Tables[0].Rows[i][5]+")";
                mycomm.CommandText=commstr;
                mycomm.ExecuteNonQuery();
            }
            myconn.Close();
            CrystalReport6 CrystalReport = new CrystalReport6();
            crystalReportViewer1.ReportSource = CrystalReport;
        }
    }
}