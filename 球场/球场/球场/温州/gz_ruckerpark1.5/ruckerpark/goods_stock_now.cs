using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            CrystalReport81.Load("CrystalReport8.rpt");
            CrystalReport81.SetDatabaseLogon("sa", "superpower", ".", "ruckerpark");
            crystalReportViewer1.ReportSource = CrystalReport81;
        }
    }
}