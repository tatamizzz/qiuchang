using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace ruckerpark
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            zmryyetj mryyetj1 = new zmryyetj();

            ParameterFields paramFields = new ParameterFields();
            ParameterField paramField = new ParameterField();
            paramField.Name="@starttime";
            ParameterDiscreteValue rangeval = new ParameterDiscreteValue();
            rangeval.Value = "2008-07-25";//dateTimePicker2.Value.ToString("yyyy-mm-dd");
            paramField.CurrentValues.Add(rangeval);
            paramFields.Add(paramField);
            crystalReportViewer1.ParameterFieldInfo = paramFields;
            //mryyetj1.SetParameterValue(0, "2008-07-28 00:00:00");
            //mryyetj1.SetParameterValue("endtime", dateTimePicker2.Value);
            crystalReportViewer1.ReportSource = mryyetj1;
        }
    }
}