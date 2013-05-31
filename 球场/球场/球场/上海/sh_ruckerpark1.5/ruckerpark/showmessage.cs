using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ruckerpark
{
    public partial class showmessage : Form
    {
        public showmessage()
        {
            InitializeComponent();
        }
        int flag = 0;
        private void showmessage_Load(object sender, EventArgs e)
        {
            setlocation(4);
        }

        private void setlocation(int pl)
        {
            label1.Left = this.Width / 2 - label1.Width / 2;
            label2.Left = this.Width / 2 - label2.Width / 2;
            label3.Left = this.Width / 2 - label3.Width / 2;
            label4.Left = this.Width / 2 - label4.Width / 2;
            switch (pl)
            {
                case 1:
                    label1.Top =270/2;
                    break;
                case 2:
                    label1.Top = 270 / 2 - 10;
                    label2.Top = 270 / 2 + 10;
                    break;
                case 3:
                    label1.Top = 270 / 2 - 35;
                    label2.Top = 270 / 2;
                    label3.Top = 270 / 2 + 35;
                    break;
            }
        }

        public void settext(string s1,string s2,string s3,string s4)
        {
            label1.Text = s1;
            label2.Text = s2;
            label3.Text = s3;
            label4.Text = s4;
            if (s2 == "") setlocation(1);
            if (s3 == "") setlocation(2);
            if (s4 == "") setlocation(3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}