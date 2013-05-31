using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ruckerpark
{
    public partial class showmsg : Form
    {
        public showmsg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void showmsg_Load(object sender, EventArgs e)
        {

        }

        public void settext(string txt)
        {
            label1.Text=txt;
            setlocation();
        }

        private void setlocation()
        {
            label1.Left = this.Width / 2-label1.Width/2;
        }

    }
}