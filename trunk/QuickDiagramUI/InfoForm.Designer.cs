namespace QuickDiagramUI
{
    partial class InfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
            this.plDrawTool = new System.Windows.Forms.Panel();
            this.pbDrawTool = new System.Windows.Forms.PictureBox();
            this.plLeftTool = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ee_name = new System.Windows.Forms.TextBox();
            this.ee_value = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.y_value = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.x_value = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.plDrawTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawTool)).BeginInit();
            this.plLeftTool.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // plDrawTool
            // 
            this.plDrawTool.BackColor = System.Drawing.SystemColors.Control;
            this.plDrawTool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDrawTool.Controls.Add(this.pbDrawTool);
            this.plDrawTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.plDrawTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.plDrawTool.Location = new System.Drawing.Point(2, 2);
            this.plDrawTool.Name = "plDrawTool";
            this.plDrawTool.Padding = new System.Windows.Forms.Padding(2);
            this.plDrawTool.Size = new System.Drawing.Size(26, 98);
            this.plDrawTool.TabIndex = 0;
            this.plDrawTool.Paint += new System.Windows.Forms.PaintEventHandler(this.plDrawTool_Paint_1);
            // 
            // pbDrawTool
            // 
            this.pbDrawTool.BackColor = System.Drawing.Color.Transparent;
            this.pbDrawTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbDrawTool.Image = ((System.Drawing.Image)(resources.GetObject("pbDrawTool.Image")));
            this.pbDrawTool.Location = new System.Drawing.Point(2, 2);
            this.pbDrawTool.Name = "pbDrawTool";
            this.pbDrawTool.Size = new System.Drawing.Size(20, 20);
            this.pbDrawTool.TabIndex = 0;
            this.pbDrawTool.TabStop = false;
            // 
            // plLeftTool
            // 
            this.plLeftTool.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plLeftTool.Controls.Add(this.plDrawTool);
            this.plLeftTool.Dock = System.Windows.Forms.DockStyle.Left;
            this.plLeftTool.Location = new System.Drawing.Point(0, 0);
            this.plLeftTool.Name = "plLeftTool";
            this.plLeftTool.Padding = new System.Windows.Forms.Padding(2);
            this.plLeftTool.Size = new System.Drawing.Size(30, 248);
            this.plLeftTool.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ee_name);
            this.groupBox1.Controls.Add(this.ee_value);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(47, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(115, 88);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Info.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Name";
            // 
            // ee_name
            // 
            this.ee_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ee_name.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ee_name.Location = new System.Drawing.Point(46, 52);
            this.ee_name.Name = "ee_name";
            this.ee_name.Size = new System.Drawing.Size(59, 21);
            this.ee_name.TabIndex = 2;
            // 
            // ee_value
            // 
            this.ee_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ee_value.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ee_value.Location = new System.Drawing.Point(46, 25);
            this.ee_value.Name = "ee_value";
            this.ee_value.Size = new System.Drawing.Size(59, 21);
            this.ee_value.TabIndex = 1;
            this.ee_value.TextChanged += new System.EventHandler(this.ee_value_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Value";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.y_value);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.x_value);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(47, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(115, 85);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Location";
            // 
            // y_value
            // 
            this.y_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.y_value.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.y_value.Location = new System.Drawing.Point(46, 48);
            this.y_value.Name = "y_value";
            this.y_value.Size = new System.Drawing.Size(59, 21);
            this.y_value.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y";
            // 
            // x_value
            // 
            this.x_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.x_value.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.x_value.Location = new System.Drawing.Point(46, 21);
            this.x_value.Name = "x_value";
            this.x_value.Size = new System.Drawing.Size(59, 21);
            this.x_value.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "X";
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(66, 204);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(70, 21);
            this.OK.TabIndex = 6;
            this.OK.Text = "OK";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // InfoForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(174, 248);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.plLeftTool);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "InfoForm";
            this.Text = "Detail Information";
            this.plDrawTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawTool)).EndInit();
            this.plLeftTool.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plDrawTool;
        private System.Windows.Forms.PictureBox pbDrawTool;
        private System.Windows.Forms.Panel plLeftTool;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox ee_value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox y_value;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox x_value;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ee_name;

    }
}