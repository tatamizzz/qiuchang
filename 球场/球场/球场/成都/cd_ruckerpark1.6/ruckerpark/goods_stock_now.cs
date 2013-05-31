using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace ruckerpark
{
    public partial class goods_stock_now : Form
    {
        public goods_stock_now()
        {
            InitializeComponent();
        }

        private void goods_stock_now_Load(object sender, EventArgs e)
        {
            string uid, dbname, pwd;
            uid = ConfigurationManager.AppSettings["uid"];
            dbname = ConfigurationManager.AppSettings["database"];
            pwd = ConfigurationManager.AppSettings["pwd"];

            CrystalReport91.Load("CrystalReport9.rpt");
            CrystalReport91.SetDatabaseLogon(uid, pwd, ".", dbname);
            crystalReportViewer1.ReportSource = CrystalReport91;
        }
    }
}