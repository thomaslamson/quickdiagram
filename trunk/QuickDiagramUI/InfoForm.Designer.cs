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
            this.pg1 = new System.Windows.Forms.PropertyGrid();
            this.OK = new System.Windows.Forms.Button();
            this.plDrawTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawTool)).BeginInit();
            this.plLeftTool.SuspendLayout();
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
            this.plLeftTool.Size = new System.Drawing.Size(30, 273);
            this.plLeftTool.TabIndex = 11;
            // 
            // pg1
            // 
            this.pg1.Location = new System.Drawing.Point(31, 5);
            this.pg1.Name = "pg1";
            this.pg1.Size = new System.Drawing.Size(213, 226);
            this.pg1.TabIndex = 12;
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(105, 243);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(70, 21);
            this.OK.TabIndex = 6;
            this.OK.Text = "OK";
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // InfoForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(242, 273);
            this.Controls.Add(this.pg1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.plLeftTool);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "InfoForm";
            this.Text = "Detail Information";
            this.plDrawTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawTool)).EndInit();
            this.plLeftTool.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plDrawTool;
        private System.Windows.Forms.PictureBox pbDrawTool;
        private System.Windows.Forms.Panel plLeftTool;
        private System.Windows.Forms.PropertyGrid pg1;
        private System.Windows.Forms.Button OK;

    }
}