namespace E_shift_Trans
{
    partial class ParcelSwift
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpicup = new System.Windows.Forms.TextBox();
            this.txtlocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cready = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtExpress = new System.Windows.Forms.Button();
            this.txtSatndard = new System.Windows.Forms.Button();
            this.txtEconomy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(345, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pickup Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Delivery Location";
            // 
            // txtpicup
            // 
            this.txtpicup.Location = new System.Drawing.Point(237, 68);
            this.txtpicup.Name = "txtpicup";
            this.txtpicup.Size = new System.Drawing.Size(268, 20);
            this.txtpicup.TabIndex = 3;
            this.txtpicup.TextChanged += new System.EventHandler(this.txtpicup_TextChanged);
            // 
            // txtlocation
            // 
            this.txtlocation.AccessibleDescription = "dcf";
            this.txtlocation.AccessibleName = "we";
            this.txtlocation.Location = new System.Drawing.Point(237, 120);
            this.txtlocation.Name = "txtlocation";
            this.txtlocation.Size = new System.Drawing.Size(268, 20);
            this.txtlocation.TabIndex = 4;
            this.txtlocation.TextChanged += new System.EventHandler(this.txtlocation_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Redy for pickup";
            // 
            // cready
            // 
            this.cready.FormattingEnabled = true;
            this.cready.Items.AddRange(new object[] {
            "In 30 miniutes",
            "In 45 miniutes",
            "In 1 hours",
            "In 1 and half hours ",
            "In 2 hours",
            "In 3 hours"});
            this.cready.Location = new System.Drawing.Point(237, 178);
            this.cready.Name = "cready";
            this.cready.Size = new System.Drawing.Size(268, 21);
            this.cready.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(237, 337);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(262, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Countinue";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(234, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Delivery Speed";
            // 
            // txtExpress
            // 
            this.txtExpress.Location = new System.Drawing.Point(237, 230);
            this.txtExpress.Name = "txtExpress";
            this.txtExpress.Size = new System.Drawing.Size(75, 49);
            this.txtExpress.TabIndex = 9;
            this.txtExpress.Text = "button2";
            this.txtExpress.UseVisualStyleBackColor = true;
            this.txtExpress.Click += new System.EventHandler(this.txtExpress_Click);
            // 
            // txtSatndard
            // 
            this.txtSatndard.Location = new System.Drawing.Point(319, 230);
            this.txtSatndard.Name = "txtSatndard";
            this.txtSatndard.Size = new System.Drawing.Size(75, 49);
            this.txtSatndard.TabIndex = 10;
            this.txtSatndard.Text = "button3";
            this.txtSatndard.UseVisualStyleBackColor = true;
            this.txtSatndard.Click += new System.EventHandler(this.txtSatndard_Click);
            // 
            // txtEconomy
            // 
            this.txtEconomy.Location = new System.Drawing.Point(400, 230);
            this.txtEconomy.Name = "txtEconomy";
            this.txtEconomy.Size = new System.Drawing.Size(75, 49);
            this.txtEconomy.TabIndex = 11;
            this.txtEconomy.UseVisualStyleBackColor = true;
            this.txtEconomy.Click += new System.EventHandler(this.txtEconomy_Click);
            // 
            // ParcelSwift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtEconomy);
            this.Controls.Add(this.txtSatndard);
            this.Controls.Add(this.txtExpress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cready);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtlocation);
            this.Controls.Add(this.txtpicup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ParcelSwift";
            this.Text = "ParcelSwift";
            this.Load += new System.EventHandler(this.ParcelSwift_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtpicup;
        private System.Windows.Forms.TextBox txtlocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cready;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button txtExpress;
        private System.Windows.Forms.Button txtSatndard;
        private System.Windows.Forms.Button txtEconomy;
    }
}