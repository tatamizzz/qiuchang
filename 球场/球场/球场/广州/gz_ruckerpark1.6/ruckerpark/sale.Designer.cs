namespace ruckerpark
{
    partial class sale
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sale_submit = new System.Windows.Forms.Button();
            this.cardid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.shop_name = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sale_info = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ticket = new System.Windows.Forms.Label();
            this.newuser = new System.Windows.Forms.Label();
            this.usercz = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.end_date = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.sale_content = new System.Windows.Forms.Label();
            this.sale_pro = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.numeric = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.barter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sale_submit
            // 
            this.sale_submit.Location = new System.Drawing.Point(292, 615);
            this.sale_submit.Name = "sale_submit";
            this.sale_submit.Size = new System.Drawing.Size(45, 23);
            this.sale_submit.TabIndex = 17;
            this.sale_submit.Text = "销售";
            this.sale_submit.UseVisualStyleBackColor = true;
            this.sale_submit.Click += new System.EventHandler(this.sale_submit_Click);
            // 
            // cardid
            // 
            this.cardid.Location = new System.Drawing.Point(142, 617);
            this.cardid.Name = "cardid";
            this.cardid.Size = new System.Drawing.Size(144, 21);
            this.cardid.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(83, 622);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "会员卡号";
            // 
            // shop_name
            // 
            this.shop_name.AutoSize = true;
            this.shop_name.Location = new System.Drawing.Point(37, 420);
            this.shop_name.Name = "shop_name";
            this.shop_name.Size = new System.Drawing.Size(0, 12);
            this.shop_name.TabIndex = 36;
            this.shop_name.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(217, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 37;
            this.label2.Text = "饮料销售情况";
            // 
            // sale_info
            // 
            this.sale_info.AutoSize = true;
            this.sale_info.Location = new System.Drawing.Point(37, 204);
            this.sale_info.Name = "sale_info";
            this.sale_info.Size = new System.Drawing.Size(0, 12);
            this.sale_info.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(217, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 39;
            this.label3.Text = "门票销售清单";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "门票销售：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(195, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "新会员办理：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(342, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 42;
            this.label6.Text = "会员充值：";
            // 
            // ticket
            // 
            this.ticket.AutoSize = true;
            this.ticket.ForeColor = System.Drawing.Color.Red;
            this.ticket.Location = new System.Drawing.Point(117, 77);
            this.ticket.Name = "ticket";
            this.ticket.Size = new System.Drawing.Size(41, 12);
            this.ticket.TabIndex = 43;
            this.ticket.Text = "label7";
            // 
            // newuser
            // 
            this.newuser.AutoSize = true;
            this.newuser.ForeColor = System.Drawing.Color.Red;
            this.newuser.Location = new System.Drawing.Point(278, 77);
            this.newuser.Name = "newuser";
            this.newuser.Size = new System.Drawing.Size(41, 12);
            this.newuser.TabIndex = 44;
            this.newuser.Text = "label7";
            // 
            // usercz
            // 
            this.usercz.AutoSize = true;
            this.usercz.ForeColor = System.Drawing.Color.Red;
            this.usercz.Location = new System.Drawing.Point(424, 77);
            this.usercz.Name = "usercz";
            this.usercz.Size = new System.Drawing.Size(41, 12);
            this.usercz.TabIndex = 45;
            this.usercz.Text = "label7";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(343, 615);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 23);
            this.button1.TabIndex = 46;
            this.button1.Text = "清除重选";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // start_date
            // 
            this.start_date.Location = new System.Drawing.Point(99, 671);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(88, 21);
            this.start_date.TabIndex = 47;
            // 
            // end_date
            // 
            this.end_date.Location = new System.Drawing.Point(225, 671);
            this.end_date.Name = "end_date";
            this.end_date.Size = new System.Drawing.Size(97, 21);
            this.end_date.TabIndex = 48;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(337, 669);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(43, 23);
            this.button2.TabIndex = 49;
            this.button2.Text = "查询";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(66, 676);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 50;
            this.label7.Text = "从";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(202, 675);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 51;
            this.label8.Text = "到";
            // 
            // sale_content
            // 
            this.sale_content.AutoSize = true;
            this.sale_content.Location = new System.Drawing.Point(36, 617);
            this.sale_content.Name = "sale_content";
            this.sale_content.Size = new System.Drawing.Size(0, 12);
            this.sale_content.TabIndex = 52;
            // 
            // sale_pro
            // 
            this.sale_pro.AutoSize = true;
            this.sale_pro.Location = new System.Drawing.Point(474, 717);
            this.sale_pro.Name = "sale_pro";
            this.sale_pro.Size = new System.Drawing.Size(0, 12);
            this.sale_pro.TabIndex = 53;
            this.sale_pro.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(72, 645);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(80, 20);
            this.comboBox1.TabIndex = 54;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(193, 644);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(62, 20);
            this.comboBox2.TabIndex = 55;
            // 
            // numeric
            // 
            this.numeric.Location = new System.Drawing.Point(300, 643);
            this.numeric.Name = "numeric";
            this.numeric.Size = new System.Drawing.Size(39, 21);
            this.numeric.TabIndex = 56;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 648);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 57;
            this.label9.Text = "把";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(158, 648);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 58;
            this.label10.Text = "换成";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(265, 648);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 59;
            this.label11.Text = "数量";
            // 
            // barter
            // 
            this.barter.Location = new System.Drawing.Point(345, 641);
            this.barter.Name = "barter";
            this.barter.Size = new System.Drawing.Size(75, 23);
            this.barter.TabIndex = 60;
            this.barter.Text = "确定";
            this.barter.UseVisualStyleBackColor = true;
            this.barter.Click += new System.EventHandler(this.barter_Click);
            // 
            // sale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(790, 769);
            this.Controls.Add(this.barter);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numeric);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.sale_pro);
            this.Controls.Add(this.sale_content);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.end_date);
            this.Controls.Add(this.start_date);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.usercz);
            this.Controls.Add(this.newuser);
            this.Controls.Add(this.ticket);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sale_info);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.shop_name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cardid);
            this.Controls.Add(this.sale_submit);
            this.Name = "sale";
            this.Text = "销售新版";
            this.Load += new System.EventHandler(this.sale_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sale_submit;
        private System.Windows.Forms.TextBox cardid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label shop_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label sale_info;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label ticket;
        private System.Windows.Forms.Label newuser;
        private System.Windows.Forms.Label usercz;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker start_date;
        private System.Windows.Forms.DateTimePicker end_date;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label sale_content;
        private System.Windows.Forms.Label sale_pro;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox numeric;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button barter;
    }
}