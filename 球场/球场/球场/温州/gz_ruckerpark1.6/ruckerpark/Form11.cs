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
    public partial class Form11 : Form
    {
        static DataTable court = new DataTable();
        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            court_list.AllowUserToOrderColumns = false;
            string connstr = ConfigurationManager.AppSettings["connectionstring"];
            SqlConnection myconn = new SqlConnection(connstr);

            string commstr = "select court_name as '包场人',court_number as '场地号',court_per_num as '包场人数',court_tel as '联系电话',court_start as '开始包场时间',court_end from court order by court_start desc";
            SqlCommand mycomm = new SqlCommand(commstr, myconn);

            court.Columns.Add("行号");

            new SqlDataAdapter(mycomm).Fill(court);
            court_list.DataSource = court;
            court_list.Columns[6].Visible = false;
            court.Columns.Add("状态");
            //标题行居中未解决
            for (int i = 0; i < court.Rows.Count; i++)
            {
                if (court.Rows[i][7].ToString() != "")
                {
                   court.Rows[i][7] = "已结账";
                   court.Rows[i][0] = i + 1;
                   court_list.Rows[i].DefaultCellStyle.ForeColor = Color.SlateGray;
                   court_list.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    court.Rows[i][7] = "在场内";
                    court.Rows[i][0] = i + 1;
                    court_list.Rows[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
        }
    }
}
