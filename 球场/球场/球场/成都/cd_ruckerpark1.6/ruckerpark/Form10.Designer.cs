namespace ruckerpark
{
    partial class Form10
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.court_number = new System.Windows.Forms.TextBox();
            this.tel = new System.Windows.Forms.TextBox();
            this.court_name = new System.Windows.Forms.TextBox();
            this.court_per_num = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.msg = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.s = new System.Windows.Forms.Label();
            this.Hours = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "包场人姓名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(36, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "包场地号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(36, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "包场人数:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(36, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "联系电话:";
            // 
            // court_number
            // 
            this.court_number.Location = new System.Drawing.Point(122, 99);
            this.court_number.Name = "court_number";
            this.court_number.Size = new System.Drawing.Size(131, 21);
            this.court_number.TabIndex = 3;
            // 
            // tel
            // 
            this.tel.Location = new System.Drawing.Point(122, 173);
            this.tel.Name = "tel";
            this.tel.Size = new System.Drawing.Size(131, 21);
            this.tel.TabIndex = 5;
            this.tel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tel_KeyDown);
            // 
            // court_name
            // 
            this.court_name.Location = new System.Drawing.Point(122, 28);
            this.court_name.Name = "court_name";
            this.court_name.Size = new System.Drawing.Size(131, 21);
            this.court_name.TabIndex = 1;
            // 
            // court_per_num
            // 
            this.court_per_num.Location = new System.Drawing.Point(122, 138);
            this.court_per_num.Name = "court_per_num";
            this.court_per_num.Size = new System.Drawing.Size(131, 21);
            this.court_per_num.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(83, 237);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "开始包场";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown);
            // 
            // msg
            // 
            this.msg.AutoSize = true;
            this.msg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.msg.ForeColor = System.Drawing.Color.Red;
            this.msg.Location = new System.Drawing.Point(64, 229);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(0, 14);
            this.msg.TabIndex = 10;
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("宋体", 12F);
            this.title.ForeColor = System.Drawing.Color.Red;
            this.title.Location = new System.Drawing.Point(110, 28);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(0, 16);
            this.title.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(37, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "包场价格：";
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(122, 66);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(46, 21);
            this.price.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(174, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "/小时";
            // 
            // s
            // 
            this.s.AutoSize = true;
            this.s.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.s.Location = new System.Drawing.Point(38, 205);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(88, 16);
            this.s.TabIndex = 15;
            this.s.Text = "包多少时：";
            // 
            // Hours
            // 
            this.Hours.Location = new System.Drawing.Point(122, 200);
            this.Hours.Name = "Hours";
            this.Hours.Size = new System.Drawing.Size(59, 21);
            this.Hours.TabIndex = 6;
            // 
            // Form10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 281);
            this.Controls.Add(this.Hours);
            this.Controls.Add(this.s);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.price);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.title);
            this.Controls.Add(this.msg);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.court_per_num);
            this.Controls.Add(this.court_name);
            this.Controls.Add(this.tel);
            this.Controls.Add(this.court_number);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form10";
            this.Text = "开始包场";
            this.Load += new System.EventHandler(this.Form10_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox court_number;
        private System.Windows.Forms.TextBox tel;
        private System.Windows.Forms.TextBox court_name;
        private System.Windows.Forms.TextBox court_per_num;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox price;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label s;
        private System.Windows.Forms.TextBox Hours;
    }
}