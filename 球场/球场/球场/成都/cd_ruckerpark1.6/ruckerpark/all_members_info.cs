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
    public partial class all_members_info : Form
    {
        public all_members_info()
        {
            InitializeComponent();
        }

        private void all_members_info_Load(object sender, EventArgs e)
        {
            string uid, dbname, pwd;
            uid = ConfigurationManager.AppSettings["uid"];
            dbname = ConfigurationManager.AppSettings["database"];
            pwd = ConfigurationManager.AppSettings["pwd"];

            CrystalReport71.Load("CrystalReport8.rpt");
            CrystalReport71.SetDatabaseLogon(uid,pwd,".",dbname);
            crystalReportViewer1.ReportSource = CrystalReport71;
        }

    }
}