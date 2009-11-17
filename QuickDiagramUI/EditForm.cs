using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for EditForm.
	/// </summary>
	public class EditForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.PictureBox pictureBox6;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.PictureBox pictureBox7;
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox8;
		private System.Windows.Forms.Panel panel11;
		private System.Windows.Forms.Panel panel12;
		private System.Windows.Forms.Panel panel13;
		private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox9;
        private Panel plLeftTool;
        private Panel plDrawTool;
        private PictureBox pbDrawTool;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EditForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			m_rgPanels = new ArrayList();
			m_rgPanels.Add(panel1);
			m_rgPanels.Add(panel2);
			m_rgPanels.Add(panel4);
			m_rgPanels.Add(panel5);
			m_rgPanels.Add(panel7);
			m_rgPanels.Add(panel8);
			m_rgPanels.Add(panel9);
			m_rgPanels.Add(panel10);
			m_rgPanels.Add(panel13);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private System.Collections.ArrayList	m_rgPanels;

		public event CommandSelectedEvent CommandSelected;

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.plLeftTool = new System.Windows.Forms.Panel();
            this.pbDrawTool = new System.Windows.Forms.PictureBox();
            this.plDrawTool = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.plLeftTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawTool)).BeginInit();
            this.plDrawTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(32, 27);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.panel1.Size = new System.Drawing.Size(131, 26);
            this.panel1.TabIndex = 0;
            this.panel1.Tag = "0";
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(31, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 14);
            this.label1.TabIndex = 1;
            this.label1.Tag = "0";
            this.label1.Text = "Group";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 20);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "0";
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(32, 53);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.panel2.Size = new System.Drawing.Size(131, 26);
            this.panel2.TabIndex = 1;
            this.panel2.Tag = "1";
            this.panel2.Click += new System.EventHandler(this.panel1_Click);
            this.panel2.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(31, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 14);
            this.label2.TabIndex = 1;
            this.label2.Tag = "1";
            this.label2.Text = "Ungroup";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.label1_Click);
            this.label2.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(7, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 20);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "1";
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox2.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(32, 79);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(131, 1);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.pictureBox3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(32, 80);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.panel4.Size = new System.Drawing.Size(131, 26);
            this.panel4.TabIndex = 3;
            this.panel4.Tag = "2";
            this.panel4.Click += new System.EventHandler(this.panel1_Click);
            this.panel4.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(31, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 1;
            this.label3.Tag = "2";
            this.label3.Text = "Bing to front";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Click += new System.EventHandler(this.label1_Click);
            this.label3.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(7, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 20);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Tag = "2";
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox3.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.pictureBox4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(32, 106);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.panel5.Size = new System.Drawing.Size(131, 25);
            this.panel5.TabIndex = 4;
            this.panel5.Tag = "3";
            this.panel5.Click += new System.EventHandler(this.panel1_Click);
            this.panel5.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(31, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 14);
            this.label4.TabIndex = 1;
            this.label4.Tag = "3";
            this.label4.Text = "Send to back";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Click += new System.EventHandler(this.label1_Click);
            this.label4.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(7, 3);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(24, 19);
            this.pictureBox4.TabIndex = 0;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Tag = "3";
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox4.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(32, 131);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(131, 1);
            this.panel6.TabIndex = 5;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.pictureBox5);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(32, 132);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.panel7.Size = new System.Drawing.Size(131, 26);
            this.panel7.TabIndex = 6;
            this.panel7.Tag = "4";
            this.panel7.Click += new System.EventHandler(this.panel1_Click);
            this.panel7.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(31, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 14);
            this.label5.TabIndex = 1;
            this.label5.Tag = "4";
            this.label5.Text = "Cut";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Click += new System.EventHandler(this.label1_Click);
            this.label5.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(7, 3);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(24, 20);
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Tag = "4";
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox5.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.pictureBox6);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(32, 158);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.panel8.Size = new System.Drawing.Size(131, 26);
            this.panel8.TabIndex = 7;
            this.panel8.Tag = "5";
            this.panel8.Click += new System.EventHandler(this.panel1_Click);
            this.panel8.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Location = new System.Drawing.Point(31, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 14);
            this.label6.TabIndex = 1;
            this.label6.Tag = "5";
            this.label6.Text = "Copy";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Click += new System.EventHandler(this.label1_Click);
            this.label6.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(7, 3);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(24, 20);
            this.pictureBox6.TabIndex = 0;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Tag = "5";
            this.pictureBox6.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox6.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label7);
            this.panel9.Controls.Add(this.pictureBox7);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(32, 184);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.panel9.Size = new System.Drawing.Size(131, 26);
            this.panel9.TabIndex = 8;
            this.panel9.Tag = "6";
            this.panel9.Click += new System.EventHandler(this.panel1_Click);
            this.panel9.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(31, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 14);
            this.label7.TabIndex = 1;
            this.label7.Tag = "6";
            this.label7.Text = "Paste";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Click += new System.EventHandler(this.label1_Click);
            this.label7.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox7.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(7, 3);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(24, 20);
            this.pictureBox7.TabIndex = 0;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.Tag = "6";
            this.pictureBox7.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox7.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.label8);
            this.panel10.Controls.Add(this.pictureBox8);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(32, 210);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.panel10.Size = new System.Drawing.Size(131, 26);
            this.panel10.TabIndex = 9;
            this.panel10.Tag = "7";
            this.panel10.Paint += new System.Windows.Forms.PaintEventHandler(this.panel10_Paint);
            this.panel10.Click += new System.EventHandler(this.panel1_Click);
            this.panel10.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Location = new System.Drawing.Point(31, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 14);
            this.label8.TabIndex = 1;
            this.label8.Tag = "7";
            this.label8.Text = "Delete";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Click += new System.EventHandler(this.label1_Click);
            this.label8.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(7, 3);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(24, 20);
            this.pictureBox8.TabIndex = 0;
            this.pictureBox8.TabStop = false;
            this.pictureBox8.Tag = "7";
            this.pictureBox8.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox8.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel11.Location = new System.Drawing.Point(31, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1, 347);
            this.panel11.TabIndex = 11;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(32, 26);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(131, 1);
            this.panel12.TabIndex = 12;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.label9);
            this.panel13.Controls.Add(this.pictureBox9);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(32, 0);
            this.panel13.Name = "panel13";
            this.panel13.Padding = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.panel13.Size = new System.Drawing.Size(131, 26);
            this.panel13.TabIndex = 13;
            this.panel13.Tag = "8";
            this.panel13.Click += new System.EventHandler(this.panel1_Click);
            this.panel13.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Left;
            this.label9.Location = new System.Drawing.Point(31, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 14);
            this.label9.TabIndex = 1;
            this.label9.Tag = "8";
            this.label9.Text = "Sketch";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Click += new System.EventHandler(this.label1_Click);
            this.label9.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox9.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(7, 3);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(24, 20);
            this.pictureBox9.TabIndex = 0;
            this.pictureBox9.TabStop = false;
            this.pictureBox9.Tag = "8";
            this.pictureBox9.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox9.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // plLeftTool
            // 
            this.plLeftTool.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plLeftTool.Controls.Add(this.plDrawTool);
            this.plLeftTool.Dock = System.Windows.Forms.DockStyle.Left;
            this.plLeftTool.Location = new System.Drawing.Point(0, 0);
            this.plLeftTool.Name = "plLeftTool";
            this.plLeftTool.Padding = new System.Windows.Forms.Padding(2);
            this.plLeftTool.Size = new System.Drawing.Size(31, 347);
            this.plLeftTool.TabIndex = 10;
            // 
            // pbDrawTool
            // 
            this.pbDrawTool.BackColor = System.Drawing.Color.Transparent;
            this.pbDrawTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbDrawTool.Image = ((System.Drawing.Image)(resources.GetObject("pbDrawTool.Image")));
            this.pbDrawTool.Location = new System.Drawing.Point(2, 2);
            this.pbDrawTool.Name = "pbDrawTool";
            this.pbDrawTool.Size = new System.Drawing.Size(21, 17);
            this.pbDrawTool.TabIndex = 0;
            this.pbDrawTool.TabStop = false;
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
            this.plDrawTool.Size = new System.Drawing.Size(27, 84);
            this.plDrawTool.TabIndex = 0;
            this.plDrawTool.Paint += new System.Windows.Forms.PaintEventHandler(this.plDrawTool_Paint);
            // 
            // EditForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(163, 347);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.plLeftTool);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.plLeftTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawTool)).EndInit();
            this.plDrawTool.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void ResetPanels(int idx)
		{
			for (int i = 0; i < m_rgPanels.Count; i++)
			{
				((Panel)m_rgPanels[i]).BackColor	= System.Drawing.SystemColors.Control;
			}

			if ((0 <= idx) && (idx < m_rgPanels.Count))
			{
				((Panel)m_rgPanels[idx]).BackColor	= System.Drawing.SystemColors.ControlLightLight;
			}
		}

		private void FireEvent(int idx)
		{
			if (CommandSelected != null)
			{
				switch (idx)
				{
					case 0:
					{
						CommandSelected(EditCommands.Group);
						break;
					}
					case 1:
					{
						CommandSelected(EditCommands.Ungroup);
						break;
					}
					case 2:
					{
						CommandSelected(EditCommands.BringToFront);
						break;
					}
					case 3:
					{
						CommandSelected(EditCommands.SendToBack);
						break;
					}
					case 4:
					{
						CommandSelected(EditCommands.Cut);
						break;
					}
					case 5:
					{
						CommandSelected(EditCommands.Copy);
						break;
					}
					case 6:
					{
						CommandSelected(EditCommands.Paste);
						break;
					}
					case 7:
					{
						CommandSelected(EditCommands.Delete);
						break;
					}
					case 8:
					{
						CommandSelected(EditCommands.Sketch);
						break;
					}
				}
			}
		}

		private void panel1_MouseEnter(object sender, System.EventArgs e)
		{
			ResetPanels(int.Parse(((Panel)sender).Tag.ToString()));
		}

		private void pictureBox1_MouseEnter(object sender, System.EventArgs e)
		{
			ResetPanels(int.Parse(((PictureBox)sender).Tag.ToString()));
		}

		private void label1_MouseEnter(object sender, System.EventArgs e)
		{
			ResetPanels(int.Parse(((Label)sender).Tag.ToString()));
		}

		private void plDrawTool_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		    System.Drawing.Drawing2D.Matrix	orgTrans;

			orgTrans = e.Graphics.Transform;
			e.Graphics.TranslateTransform(plDrawTool.Width, pbDrawTool.Height);
			e.Graphics.RotateTransform(90);
			e.Graphics.DrawString("Editing", this.Font, System.Drawing.Brushes.Black, 2, (plDrawTool.Width - this.Font.Height) / 2);
			
			e.Graphics.Transform = orgTrans;
		}

		private void panel1_Click(object sender, System.EventArgs e)
		{
			FireEvent(int.Parse(((Panel)sender).Tag.ToString()));
		}

		private void label1_Click(object sender, System.EventArgs e)
		{
			FireEvent(int.Parse(((Label)sender).Tag.ToString()));
		}

		private void pictureBox1_Click(object sender, System.EventArgs e)
		{
			FireEvent(int.Parse(((PictureBox)sender).Tag.ToString()));
		}

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
	}

	public enum EditCommands {Group, Ungroup, BringToFront, SendToBack, Cut, Copy, Paste, Delete, Sketch, Undo, Redo}
	public delegate void CommandSelectedEvent(EditCommands command);
}
