namespace ruckerpark
{
    partial class integral_manage
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.integral = new System.Windows.Forms.TextBox();
            this.product_name = new System.Windows.Forms.TextBox();
            this.product_num = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.integral_pro = new System.Windows.Forms.Label();
            this.s_name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13F);
            this.label1.Location = new System.Drawing.Point(128, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "所需积分";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(95, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "兑换物品库存";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(127, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "兑换物品";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 13F);
            this.label5.Location = new System.Drawing.Point(152, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "积分兑换管理";
            // 
            // integral
            // 
            this.integral.Location = new System.Drawing.Point(213, 88);
            this.integral.Name = "integral";
            this.integral.Size = new System.Drawing.Size(100, 21);
            this.integral.TabIndex = 1;
            // 
            // product_name
            // 
            this.product_name.Location = new System.Drawing.Point(213, 121);
            this.product_name.Name = "product_name";
            this.product_name.Size = new System.Drawing.Size(100, 21);
            this.product_name.TabIndex = 2;
            // 
            // product_num
            // 
            this.product_num.Location = new System.Drawing.Point(213, 152);
            this.product_num.Name = "product_num";
            this.product_num.Size = new System.Drawing.Size(100, 21);
            this.product_num.TabIndex = 3;
            this.product_num.KeyDown += new System.Windows.Forms.KeyEventHandler(this.product_num_KeyDown);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F);
            this.button1.Location = new System.Drawing.Point(155, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(127, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "积分兑换物品";
            // 
            // integral_pro
            // 
            this.integral_pro.AutoSize = true;
            this.integral_pro.Location = new System.Drawing.Point(106, 259);
            this.integral_pro.Name = "integral_pro";
            this.integral_pro.Size = new System.Drawing.Size(0, 12);
            this.integral_pro.TabIndex = 11;
            // 
            // s_name
            // 
            this.s_name.Location = new System.Drawing.Point(175, 42);
            this.s_name.Name = "s_name";
            this.s_name.Size = new System.Drawing.Size(100, 21);
            this.s_name.TabIndex = 12;
            this.s_name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.s_name_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(62, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "要查找的物品名称:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(281, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(46, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "查找";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // integral_manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 671);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.s_name);
            this.Controls.Add(this.integral_pro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.product_num);
            this.Controls.Add(this.product_name);
            this.Controls.Add(this.integral);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "integral_manage";
            this.Text = "积分兑换管理";
            this.Load += new System.EventHandler(this.integral_manage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox integral;
        private System.Windows.Forms.TextBox product_name;
        private System.Windows.Forms.TextBox product_num;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label integral_pro;
        private System.Windows.Forms.TextBox s_name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
    }
}