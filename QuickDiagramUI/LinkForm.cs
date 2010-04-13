using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for LinkForm.
	/// </summary>
	public class LinkForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel plSplit;
		private System.Windows.Forms.Panel plLeftTool;
		private System.Windows.Forms.Panel plSpacer;
		private System.Windows.Forms.Panel plDrawTool;
		private System.Windows.Forms.PictureBox pbDrawTool;
		private System.Windows.Forms.Panel plDraw;
		private System.Windows.Forms.GroupBox gbWidth;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown edtWidth;
		private FloatControlLib.ColorControl drawingColor;
		private System.Windows.Forms.GroupBox gbDraw;
		private System.Windows.Forms.RadioButton rbpDashDotDot;
		private System.Windows.Forms.RadioButton rbpDashDot;
		private System.Windows.Forms.RadioButton rbpDash;
		private System.Windows.Forms.RadioButton rbpDot;
		private System.Windows.Forms.RadioButton rbpSolid;
		private System.Windows.Forms.Panel plDashDotDotLine;
		private System.Windows.Forms.Panel plDashDotLine;
		private System.Windows.Forms.Panel plDashLine;
		private System.Windows.Forms.Panel plDotLine;
		private System.Windows.Forms.Panel plSolidLine;
        private System.Windows.Forms.Panel plLink;
		private System.Windows.Forms.GroupBox gbLinkStyle;
		private System.Windows.Forms.RadioButton rbpLine;
		private System.Windows.Forms.RadioButton rbpPolyLine;
		private System.Windows.Forms.RadioButton rbpCurve;
		private System.Windows.Forms.RadioButton rbpNone;
		private System.Windows.Forms.RadioButton rbpCircle;
		private System.Windows.Forms.RadioButton rbpTriangle;
		private System.Windows.Forms.RadioButton rbpDiamond;
		private System.Windows.Forms.GroupBox gbTerminalStyle;
		private System.Windows.Forms.ComboBox cbPoints;
		private System.Windows.Forms.GroupBox gbMisc;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.PictureBox pbLine;
		private System.Windows.Forms.PictureBox pbPolyLine;
		private System.Windows.Forms.PictureBox pbCurve;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.PictureBox pictureBox5;
		private System.Windows.Forms.RadioButton rbpArrow;
        private Panel pStyle;
        private GroupBox cbbond;
        private Panel pOtherTool;
        private PictureBox pictureBox6;
        private Panel plLinkTool;
        private PictureBox pbLinkTool;
        private RadioButton bond_cbw;
        private RadioButton bond_cb3;
        private RadioButton bond_cb2;
        private RadioButton bond_cb1;
        private RadioButton bond_cbnone;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LinkForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			pSolid					= new Pen(System.Drawing.Color.Black, 1);
			pSolid.DashStyle		= System.Drawing.Drawing2D.DashStyle.Solid;
			pDot					= new Pen(System.Drawing.Color.Black, 1);
			pDot.DashStyle			= System.Drawing.Drawing2D.DashStyle.Dot;
			pDash					= new Pen(System.Drawing.Color.Black, 1);
			pDash.DashStyle			= System.Drawing.Drawing2D.DashStyle.Dash;
			pDashDot				= new Pen(System.Drawing.Color.Black, 1);
			pDashDot.DashStyle		= System.Drawing.Drawing2D.DashStyle.DashDot;
			pDashDotDot				= new Pen(System.Drawing.Color.Black, 1);
			pDashDotDot.DashStyle	= System.Drawing.Drawing2D.DashStyle.DashDotDot;

			rbpSolid.Tag		= pSolid;
			rbpDot.Tag			= pDot;
			rbpDash.Tag			= pDash;
			rbpDashDot.Tag		= pDashDot;
			rbpDashDotDot.Tag	= pDashDotDot;

			rbpNone.Tag		= GOMLib.GOM_Terminal_Style.None;
			rbpArrow.Tag	= GOMLib.GOM_Terminal_Style.Arrow;
			rbpCircle.Tag	= GOMLib.GOM_Terminal_Style.Circle;
			rbpTriangle.Tag	= GOMLib.GOM_Terminal_Style.Triangle;
			rbpDiamond.Tag	= GOMLib.GOM_Terminal_Style.Diamond;
			
			m_selectedLink			= null;
			selectedPen				= null;
			bUnderSettingStyle		= false;
			cbPoints.SelectedIndex	= 0;
			plDrawTool_Click(null, null);
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkForm));
            this.plSplit = new System.Windows.Forms.Panel();
            this.plLeftTool = new System.Windows.Forms.Panel();
            this.pOtherTool = new System.Windows.Forms.Panel();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.plLinkTool = new System.Windows.Forms.Panel();
            this.pbLinkTool = new System.Windows.Forms.PictureBox();
            this.plSpacer = new System.Windows.Forms.Panel();
            this.plDrawTool = new System.Windows.Forms.Panel();
            this.pbDrawTool = new System.Windows.Forms.PictureBox();
            this.plDraw = new System.Windows.Forms.Panel();
            this.gbWidth = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.edtWidth = new System.Windows.Forms.NumericUpDown();
            this.drawingColor = new FloatControlLib.ColorControl();
            this.gbDraw = new System.Windows.Forms.GroupBox();
            this.rbpDashDotDot = new System.Windows.Forms.RadioButton();
            this.rbpDashDot = new System.Windows.Forms.RadioButton();
            this.rbpDash = new System.Windows.Forms.RadioButton();
            this.rbpDot = new System.Windows.Forms.RadioButton();
            this.rbpSolid = new System.Windows.Forms.RadioButton();
            this.plDashDotDotLine = new System.Windows.Forms.Panel();
            this.plDashDotLine = new System.Windows.Forms.Panel();
            this.plDashLine = new System.Windows.Forms.Panel();
            this.plDotLine = new System.Windows.Forms.Panel();
            this.plSolidLine = new System.Windows.Forms.Panel();
            this.plLink = new System.Windows.Forms.Panel();
            this.gbMisc = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.gbTerminalStyle = new System.Windows.Forms.GroupBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbPoints = new System.Windows.Forms.ComboBox();
            this.rbpDiamond = new System.Windows.Forms.RadioButton();
            this.rbpTriangle = new System.Windows.Forms.RadioButton();
            this.rbpArrow = new System.Windows.Forms.RadioButton();
            this.rbpCircle = new System.Windows.Forms.RadioButton();
            this.rbpNone = new System.Windows.Forms.RadioButton();
            this.gbLinkStyle = new System.Windows.Forms.GroupBox();
            this.pbCurve = new System.Windows.Forms.PictureBox();
            this.pbPolyLine = new System.Windows.Forms.PictureBox();
            this.pbLine = new System.Windows.Forms.PictureBox();
            this.rbpCurve = new System.Windows.Forms.RadioButton();
            this.rbpPolyLine = new System.Windows.Forms.RadioButton();
            this.rbpLine = new System.Windows.Forms.RadioButton();
            this.pStyle = new System.Windows.Forms.Panel();
            this.cbbond = new System.Windows.Forms.GroupBox();
            this.bond_cbnone = new System.Windows.Forms.RadioButton();
            this.bond_cbw = new System.Windows.Forms.RadioButton();
            this.bond_cb3 = new System.Windows.Forms.RadioButton();
            this.bond_cb2 = new System.Windows.Forms.RadioButton();
            this.bond_cb1 = new System.Windows.Forms.RadioButton();
            this.plLeftTool.SuspendLayout();
            this.pOtherTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.plLinkTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLinkTool)).BeginInit();
            this.plDrawTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawTool)).BeginInit();
            this.plDraw.SuspendLayout();
            this.gbWidth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtWidth)).BeginInit();
            this.gbDraw.SuspendLayout();
            this.plLink.SuspendLayout();
            this.gbMisc.SuspendLayout();
            this.gbTerminalStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbLinkStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPolyLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLine)).BeginInit();
            this.pStyle.SuspendLayout();
            this.cbbond.SuspendLayout();
            this.SuspendLayout();
            // 
            // plSplit
            // 
            this.plSplit.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.plSplit.Dock = System.Windows.Forms.DockStyle.Left;
            this.plSplit.Location = new System.Drawing.Point(29, 0);
            this.plSplit.Name = "plSplit";
            this.plSplit.Size = new System.Drawing.Size(1, 425);
            this.plSplit.TabIndex = 3;
            // 
            // plLeftTool
            // 
            this.plLeftTool.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plLeftTool.Controls.Add(this.pOtherTool);
            this.plLeftTool.Controls.Add(this.plLinkTool);
            this.plLeftTool.Controls.Add(this.plSpacer);
            this.plLeftTool.Controls.Add(this.plDrawTool);
            this.plLeftTool.Dock = System.Windows.Forms.DockStyle.Left;
            this.plLeftTool.Location = new System.Drawing.Point(0, 0);
            this.plLeftTool.Name = "plLeftTool";
            this.plLeftTool.Padding = new System.Windows.Forms.Padding(2);
            this.plLeftTool.Size = new System.Drawing.Size(29, 425);
            this.plLeftTool.TabIndex = 2;
            // 
            // pOtherTool
            // 
            this.pOtherTool.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pOtherTool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pOtherTool.Controls.Add(this.pictureBox6);
            this.pOtherTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pOtherTool.Location = new System.Drawing.Point(2, 179);
            this.pOtherTool.Name = "pOtherTool";
            this.pOtherTool.Padding = new System.Windows.Forms.Padding(2);
            this.pOtherTool.Size = new System.Drawing.Size(25, 84);
            this.pOtherTool.TabIndex = 3;
            this.pOtherTool.Paint += new System.Windows.Forms.PaintEventHandler(this.pOtherTool_Paint);
            this.pOtherTool.Click += new System.EventHandler(this.pOtherTool_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(2, 2);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(19, 17);
            this.pictureBox6.TabIndex = 0;
            this.pictureBox6.TabStop = false;
            // 
            // plLinkTool
            // 
            this.plLinkTool.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plLinkTool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plLinkTool.Controls.Add(this.pbLinkTool);
            this.plLinkTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.plLinkTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.plLinkTool.Location = new System.Drawing.Point(2, 90);
            this.plLinkTool.Name = "plLinkTool";
            this.plLinkTool.Padding = new System.Windows.Forms.Padding(2);
            this.plLinkTool.Size = new System.Drawing.Size(25, 84);
            this.plLinkTool.TabIndex = 2;
            this.plLinkTool.Paint += new System.Windows.Forms.PaintEventHandler(this.plLinkTool_Paint);
            this.plLinkTool.Click += new System.EventHandler(this.plLinkTool_Click);
            // 
            // pbLinkTool
            // 
            this.pbLinkTool.BackColor = System.Drawing.Color.Transparent;
            this.pbLinkTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbLinkTool.Image = ((System.Drawing.Image)(resources.GetObject("pbLinkTool.Image")));
            this.pbLinkTool.Location = new System.Drawing.Point(2, 2);
            this.pbLinkTool.Name = "pbLinkTool";
            this.pbLinkTool.Size = new System.Drawing.Size(19, 17);
            this.pbLinkTool.TabIndex = 0;
            this.pbLinkTool.TabStop = false;
            this.pbLinkTool.Click += new System.EventHandler(this.plLinkTool_Click);
            // 
            // plSpacer
            // 
            this.plSpacer.Dock = System.Windows.Forms.DockStyle.Top;
            this.plSpacer.Location = new System.Drawing.Point(2, 86);
            this.plSpacer.Name = "plSpacer";
            this.plSpacer.Size = new System.Drawing.Size(25, 4);
            this.plSpacer.TabIndex = 1;
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
            this.plDrawTool.Size = new System.Drawing.Size(25, 84);
            this.plDrawTool.TabIndex = 0;
            this.plDrawTool.Paint += new System.Windows.Forms.PaintEventHandler(this.plDrawTool_Paint);
            this.plDrawTool.Click += new System.EventHandler(this.plDrawTool_Click);
            // 
            // pbDrawTool
            // 
            this.pbDrawTool.BackColor = System.Drawing.Color.Transparent;
            this.pbDrawTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbDrawTool.Image = ((System.Drawing.Image)(resources.GetObject("pbDrawTool.Image")));
            this.pbDrawTool.Location = new System.Drawing.Point(2, 2);
            this.pbDrawTool.Name = "pbDrawTool";
            this.pbDrawTool.Size = new System.Drawing.Size(19, 17);
            this.pbDrawTool.TabIndex = 0;
            this.pbDrawTool.TabStop = false;
            this.pbDrawTool.Click += new System.EventHandler(this.plDrawTool_Click);
            // 
            // plDraw
            // 
            this.plDraw.Controls.Add(this.gbWidth);
            this.plDraw.Controls.Add(this.drawingColor);
            this.plDraw.Controls.Add(this.gbDraw);
            this.plDraw.Location = new System.Drawing.Point(29, 0);
            this.plDraw.Name = "plDraw";
            this.plDraw.Padding = new System.Windows.Forms.Padding(3);
            this.plDraw.Size = new System.Drawing.Size(210, 405);
            this.plDraw.TabIndex = 6;
            // 
            // gbWidth
            // 
            this.gbWidth.Controls.Add(this.label1);
            this.gbWidth.Controls.Add(this.edtWidth);
            this.gbWidth.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbWidth.Location = new System.Drawing.Point(3, 155);
            this.gbWidth.Name = "gbWidth";
            this.gbWidth.Size = new System.Drawing.Size(204, 52);
            this.gbWidth.TabIndex = 8;
            this.gbWidth.TabStop = false;
            this.gbWidth.Text = "Line Width";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Width:";
            // 
            // edtWidth
            // 
            this.edtWidth.Location = new System.Drawing.Point(104, 24);
            this.edtWidth.Name = "edtWidth";
            this.edtWidth.Size = new System.Drawing.Size(88, 22);
            this.edtWidth.TabIndex = 0;
            this.edtWidth.ValueChanged += new System.EventHandler(this.edtWidth_ValueChanged);
            // 
            // drawingColor
            // 
            this.drawingColor.Color = System.Drawing.Color.White;
            this.drawingColor.Location = new System.Drawing.Point(10, 215);
            this.drawingColor.Name = "drawingColor";
            this.drawingColor.Size = new System.Drawing.Size(190, 181);
            this.drawingColor.TabIndex = 7;
            this.drawingColor.ColorSelected += new FloatControlLib.ColorSelectedEvent(this.drawingColor_ColorSelected);
            // 
            // gbDraw
            // 
            this.gbDraw.Controls.Add(this.rbpDashDotDot);
            this.gbDraw.Controls.Add(this.rbpDashDot);
            this.gbDraw.Controls.Add(this.rbpDash);
            this.gbDraw.Controls.Add(this.rbpDot);
            this.gbDraw.Controls.Add(this.rbpSolid);
            this.gbDraw.Controls.Add(this.plDashDotDotLine);
            this.gbDraw.Controls.Add(this.plDashDotLine);
            this.gbDraw.Controls.Add(this.plDashLine);
            this.gbDraw.Controls.Add(this.plDotLine);
            this.gbDraw.Controls.Add(this.plSolidLine);
            this.gbDraw.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDraw.Location = new System.Drawing.Point(3, 3);
            this.gbDraw.Name = "gbDraw";
            this.gbDraw.Size = new System.Drawing.Size(204, 152);
            this.gbDraw.TabIndex = 6;
            this.gbDraw.TabStop = false;
            this.gbDraw.Text = "Drawing Style";
            // 
            // rbpDashDotDot
            // 
            this.rbpDashDotDot.Location = new System.Drawing.Point(8, 119);
            this.rbpDashDotDot.Name = "rbpDashDotDot";
            this.rbpDashDotDot.Size = new System.Drawing.Size(94, 17);
            this.rbpDashDotDot.TabIndex = 9;
            this.rbpDashDotDot.Text = "DashDotDot";
            this.rbpDashDotDot.CheckedChanged += new System.EventHandler(this.rbpSolid_CheckedChanged);
            // 
            // rbpDashDot
            // 
            this.rbpDashDot.Location = new System.Drawing.Point(10, 95);
            this.rbpDashDot.Name = "rbpDashDot";
            this.rbpDashDot.Size = new System.Drawing.Size(94, 18);
            this.rbpDashDot.TabIndex = 8;
            this.rbpDashDot.Text = "DashDot";
            this.rbpDashDot.CheckedChanged += new System.EventHandler(this.rbpSolid_CheckedChanged);
            // 
            // rbpDash
            // 
            this.rbpDash.Location = new System.Drawing.Point(10, 72);
            this.rbpDash.Name = "rbpDash";
            this.rbpDash.Size = new System.Drawing.Size(94, 17);
            this.rbpDash.TabIndex = 7;
            this.rbpDash.Text = "Dash";
            this.rbpDash.CheckedChanged += new System.EventHandler(this.rbpSolid_CheckedChanged);
            // 
            // rbpDot
            // 
            this.rbpDot.Location = new System.Drawing.Point(10, 49);
            this.rbpDot.Name = "rbpDot";
            this.rbpDot.Size = new System.Drawing.Size(94, 17);
            this.rbpDot.TabIndex = 6;
            this.rbpDot.Text = "Dot";
            this.rbpDot.CheckedChanged += new System.EventHandler(this.rbpSolid_CheckedChanged);
            // 
            // rbpSolid
            // 
            this.rbpSolid.Location = new System.Drawing.Point(10, 26);
            this.rbpSolid.Name = "rbpSolid";
            this.rbpSolid.Size = new System.Drawing.Size(94, 17);
            this.rbpSolid.TabIndex = 5;
            this.rbpSolid.Text = "Solid";
            this.rbpSolid.CheckedChanged += new System.EventHandler(this.rbpSolid_CheckedChanged);
            // 
            // plDashDotDotLine
            // 
            this.plDashDotDotLine.BackColor = System.Drawing.Color.White;
            this.plDashDotDotLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDashDotDotLine.Location = new System.Drawing.Point(104, 120);
            this.plDashDotDotLine.Name = "plDashDotDotLine";
            this.plDashDotDotLine.Size = new System.Drawing.Size(88, 17);
            this.plDashDotDotLine.TabIndex = 4;
            this.plDashDotDotLine.Paint += new System.Windows.Forms.PaintEventHandler(this.plDashDotDotLine_Paint);
            // 
            // plDashDotLine
            // 
            this.plDashDotLine.BackColor = System.Drawing.Color.White;
            this.plDashDotLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDashDotLine.Location = new System.Drawing.Point(104, 96);
            this.plDashDotLine.Name = "plDashDotLine";
            this.plDashDotLine.Size = new System.Drawing.Size(88, 17);
            this.plDashDotLine.TabIndex = 3;
            this.plDashDotLine.Paint += new System.Windows.Forms.PaintEventHandler(this.plDashDotLine_Paint);
            // 
            // plDashLine
            // 
            this.plDashLine.BackColor = System.Drawing.Color.White;
            this.plDashLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDashLine.Location = new System.Drawing.Point(104, 72);
            this.plDashLine.Name = "plDashLine";
            this.plDashLine.Size = new System.Drawing.Size(88, 18);
            this.plDashLine.TabIndex = 2;
            this.plDashLine.Paint += new System.Windows.Forms.PaintEventHandler(this.plDashLine_Paint);
            // 
            // plDotLine
            // 
            this.plDotLine.BackColor = System.Drawing.Color.White;
            this.plDotLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDotLine.Location = new System.Drawing.Point(104, 48);
            this.plDotLine.Name = "plDotLine";
            this.plDotLine.Size = new System.Drawing.Size(88, 17);
            this.plDotLine.TabIndex = 1;
            this.plDotLine.Paint += new System.Windows.Forms.PaintEventHandler(this.plDotLine_Paint);
            // 
            // plSolidLine
            // 
            this.plSolidLine.BackColor = System.Drawing.Color.White;
            this.plSolidLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plSolidLine.Location = new System.Drawing.Point(104, 24);
            this.plSolidLine.Name = "plSolidLine";
            this.plSolidLine.Size = new System.Drawing.Size(88, 17);
            this.plSolidLine.TabIndex = 0;
            this.plSolidLine.Paint += new System.Windows.Forms.PaintEventHandler(this.plSolidLine_Paint);
            // 
            // plLink
            // 
            this.plLink.Controls.Add(this.gbMisc);
            this.plLink.Controls.Add(this.gbTerminalStyle);
            this.plLink.Controls.Add(this.gbLinkStyle);
            this.plLink.Location = new System.Drawing.Point(242, 0);
            this.plLink.Name = "plLink";
            this.plLink.Padding = new System.Windows.Forms.Padding(3);
            this.plLink.Size = new System.Drawing.Size(210, 405);
            this.plLink.TabIndex = 7;
            // 
            // gbMisc
            // 
            this.gbMisc.Controls.Add(this.button1);
            this.gbMisc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMisc.Location = new System.Drawing.Point(3, 336);
            this.gbMisc.Name = "gbMisc";
            this.gbMisc.Size = new System.Drawing.Size(204, 66);
            this.gbMisc.TabIndex = 2;
            this.gbMisc.TabStop = false;
            this.gbMisc.Text = "Misc";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Remove the link";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbTerminalStyle
            // 
            this.gbTerminalStyle.Controls.Add(this.pictureBox5);
            this.gbTerminalStyle.Controls.Add(this.pictureBox4);
            this.gbTerminalStyle.Controls.Add(this.pictureBox3);
            this.gbTerminalStyle.Controls.Add(this.pictureBox2);
            this.gbTerminalStyle.Controls.Add(this.pictureBox1);
            this.gbTerminalStyle.Controls.Add(this.cbPoints);
            this.gbTerminalStyle.Controls.Add(this.rbpDiamond);
            this.gbTerminalStyle.Controls.Add(this.rbpTriangle);
            this.gbTerminalStyle.Controls.Add(this.rbpArrow);
            this.gbTerminalStyle.Controls.Add(this.rbpCircle);
            this.gbTerminalStyle.Controls.Add(this.rbpNone);
            this.gbTerminalStyle.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTerminalStyle.Location = new System.Drawing.Point(3, 120);
            this.gbTerminalStyle.Name = "gbTerminalStyle";
            this.gbTerminalStyle.Size = new System.Drawing.Size(204, 216);
            this.gbTerminalStyle.TabIndex = 1;
            this.gbTerminalStyle.TabStop = false;
            this.gbTerminalStyle.Text = "Terminal Style";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(88, 183);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(102, 21);
            this.pictureBox5.TabIndex = 10;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(88, 153);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(102, 21);
            this.pictureBox4.TabIndex = 9;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(88, 122);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(102, 22);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(88, 91);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(102, 22);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(88, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(102, 22);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // cbPoints
            // 
            this.cbPoints.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPoints.Items.AddRange(new object[] {
            "Start-point",
            "End-point"});
            this.cbPoints.Location = new System.Drawing.Point(16, 26);
            this.cbPoints.Name = "cbPoints";
            this.cbPoints.Size = new System.Drawing.Size(176, 22);
            this.cbPoints.TabIndex = 5;
            this.cbPoints.SelectedIndexChanged += new System.EventHandler(this.cbPoints_SelectedIndexChanged);
            // 
            // rbpDiamond
            // 
            this.rbpDiamond.Location = new System.Drawing.Point(10, 183);
            this.rbpDiamond.Name = "rbpDiamond";
            this.rbpDiamond.Size = new System.Drawing.Size(86, 17);
            this.rbpDiamond.TabIndex = 4;
            this.rbpDiamond.Text = "Diamond";
            this.rbpDiamond.CheckedChanged += new System.EventHandler(this.rbpNone_CheckedChanged);
            // 
            // rbpTriangle
            // 
            this.rbpTriangle.Location = new System.Drawing.Point(10, 153);
            this.rbpTriangle.Name = "rbpTriangle";
            this.rbpTriangle.Size = new System.Drawing.Size(86, 17);
            this.rbpTriangle.TabIndex = 3;
            this.rbpTriangle.Text = "Triangle";
            this.rbpTriangle.CheckedChanged += new System.EventHandler(this.rbpNone_CheckedChanged);
            // 
            // rbpArrow
            // 
            this.rbpArrow.Location = new System.Drawing.Point(10, 123);
            this.rbpArrow.Name = "rbpArrow";
            this.rbpArrow.Size = new System.Drawing.Size(86, 17);
            this.rbpArrow.TabIndex = 2;
            this.rbpArrow.Text = "Arrow";
            this.rbpArrow.CheckedChanged += new System.EventHandler(this.rbpNone_CheckedChanged);
            // 
            // rbpCircle
            // 
            this.rbpCircle.Location = new System.Drawing.Point(10, 93);
            this.rbpCircle.Name = "rbpCircle";
            this.rbpCircle.Size = new System.Drawing.Size(86, 17);
            this.rbpCircle.TabIndex = 1;
            this.rbpCircle.Text = "Circle";
            this.rbpCircle.CheckedChanged += new System.EventHandler(this.rbpNone_CheckedChanged);
            // 
            // rbpNone
            // 
            this.rbpNone.Location = new System.Drawing.Point(10, 62);
            this.rbpNone.Name = "rbpNone";
            this.rbpNone.Size = new System.Drawing.Size(86, 18);
            this.rbpNone.TabIndex = 0;
            this.rbpNone.Text = "None";
            this.rbpNone.CheckedChanged += new System.EventHandler(this.rbpNone_CheckedChanged);
            // 
            // gbLinkStyle
            // 
            this.gbLinkStyle.Controls.Add(this.pbCurve);
            this.gbLinkStyle.Controls.Add(this.pbPolyLine);
            this.gbLinkStyle.Controls.Add(this.pbLine);
            this.gbLinkStyle.Controls.Add(this.rbpCurve);
            this.gbLinkStyle.Controls.Add(this.rbpPolyLine);
            this.gbLinkStyle.Controls.Add(this.rbpLine);
            this.gbLinkStyle.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbLinkStyle.Location = new System.Drawing.Point(3, 3);
            this.gbLinkStyle.Name = "gbLinkStyle";
            this.gbLinkStyle.Size = new System.Drawing.Size(204, 117);
            this.gbLinkStyle.TabIndex = 0;
            this.gbLinkStyle.TabStop = false;
            this.gbLinkStyle.Text = "Link Style";
            // 
            // pbCurve
            // 
            this.pbCurve.Image = ((System.Drawing.Image)(resources.GetObject("pbCurve.Image")));
            this.pbCurve.Location = new System.Drawing.Point(88, 88);
            this.pbCurve.Name = "pbCurve";
            this.pbCurve.Size = new System.Drawing.Size(102, 21);
            this.pbCurve.TabIndex = 5;
            this.pbCurve.TabStop = false;
            // 
            // pbPolyLine
            // 
            this.pbPolyLine.Image = ((System.Drawing.Image)(resources.GetObject("pbPolyLine.Image")));
            this.pbPolyLine.Location = new System.Drawing.Point(88, 57);
            this.pbPolyLine.Name = "pbPolyLine";
            this.pbPolyLine.Size = new System.Drawing.Size(102, 21);
            this.pbPolyLine.TabIndex = 4;
            this.pbPolyLine.TabStop = false;
            // 
            // pbLine
            // 
            this.pbLine.Image = ((System.Drawing.Image)(resources.GetObject("pbLine.Image")));
            this.pbLine.Location = new System.Drawing.Point(88, 26);
            this.pbLine.Name = "pbLine";
            this.pbLine.Size = new System.Drawing.Size(102, 21);
            this.pbLine.TabIndex = 3;
            this.pbLine.TabStop = false;
            // 
            // rbpCurve
            // 
            this.rbpCurve.Enabled = false;
            this.rbpCurve.Location = new System.Drawing.Point(10, 87);
            this.rbpCurve.Name = "rbpCurve";
            this.rbpCurve.Size = new System.Drawing.Size(86, 25);
            this.rbpCurve.TabIndex = 2;
            this.rbpCurve.Text = "Curve";
            // 
            // rbpPolyLine
            // 
            this.rbpPolyLine.Location = new System.Drawing.Point(10, 54);
            this.rbpPolyLine.Name = "rbpPolyLine";
            this.rbpPolyLine.Size = new System.Drawing.Size(86, 26);
            this.rbpPolyLine.TabIndex = 1;
            this.rbpPolyLine.Text = "PolyLine";
            // 
            // rbpLine
            // 
            this.rbpLine.Location = new System.Drawing.Point(10, 22);
            this.rbpLine.Name = "rbpLine";
            this.rbpLine.Size = new System.Drawing.Size(86, 25);
            this.rbpLine.TabIndex = 0;
            this.rbpLine.Text = "Line";
            // 
            // pStyle
            // 
            this.pStyle.Controls.Add(this.cbbond);
            this.pStyle.Location = new System.Drawing.Point(455, 0);
            this.pStyle.Name = "pStyle";
            this.pStyle.Padding = new System.Windows.Forms.Padding(3);
            this.pStyle.Size = new System.Drawing.Size(210, 405);
            this.pStyle.TabIndex = 8;
            // 
            // cbbond
            // 
            this.cbbond.Controls.Add(this.bond_cbnone);
            this.cbbond.Controls.Add(this.bond_cbw);
            this.cbbond.Controls.Add(this.bond_cb3);
            this.cbbond.Controls.Add(this.bond_cb2);
            this.cbbond.Controls.Add(this.bond_cb1);
            this.cbbond.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbond.Location = new System.Drawing.Point(3, 3);
            this.cbbond.Name = "cbbond";
            this.cbbond.Size = new System.Drawing.Size(204, 393);
            this.cbbond.TabIndex = 0;
            this.cbbond.TabStop = false;
            this.cbbond.Text = "Style";
            // 
            // bond_cbnone
            // 
            this.bond_cbnone.Location = new System.Drawing.Point(15, 151);
            this.bond_cbnone.Name = "bond_cbnone";
            this.bond_cbnone.Size = new System.Drawing.Size(155, 25);
            this.bond_cbnone.TabIndex = 7;
            this.bond_cbnone.Text = "None";
            // 
            // bond_cbw
            // 
            this.bond_cbw.Location = new System.Drawing.Point(15, 120);
            this.bond_cbw.Name = "bond_cbw";
            this.bond_cbw.Size = new System.Drawing.Size(155, 25);
            this.bond_cbw.TabIndex = 6;
            this.bond_cbw.Text = "Wedge Bond";
            this.bond_cbw.CheckedChanged += new System.EventHandler(this.bond_CheckedChanged);
            // 
            // bond_cb3
            // 
            this.bond_cb3.Location = new System.Drawing.Point(15, 86);
            this.bond_cb3.Name = "bond_cb3";
            this.bond_cb3.Size = new System.Drawing.Size(155, 25);
            this.bond_cb3.TabIndex = 5;
            this.bond_cb3.Text = "Triple Bond";
            this.bond_cb3.CheckedChanged += new System.EventHandler(this.bond_CheckedChanged);
            // 
            // bond_cb2
            // 
            this.bond_cb2.Location = new System.Drawing.Point(15, 53);
            this.bond_cb2.Name = "bond_cb2";
            this.bond_cb2.Size = new System.Drawing.Size(155, 26);
            this.bond_cb2.TabIndex = 4;
            this.bond_cb2.Text = "Double Bond";
            this.bond_cb2.CheckedChanged += new System.EventHandler(this.bond_CheckedChanged);
            // 
            // bond_cb1
            // 
            this.bond_cb1.Checked = true;
            this.bond_cb1.Location = new System.Drawing.Point(15, 21);
            this.bond_cb1.Name = "bond_cb1";
            this.bond_cb1.Size = new System.Drawing.Size(104, 25);
            this.bond_cb1.TabIndex = 3;
            this.bond_cb1.TabStop = true;
            this.bond_cb1.Text = "Single Bond";
            this.bond_cb1.CheckedChanged += new System.EventHandler(this.bond_CheckedChanged);
            // 
            // LinkForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(760, 425);
            this.Controls.Add(this.pStyle);
            this.Controls.Add(this.plLink);
            this.Controls.Add(this.plDraw);
            this.Controls.Add(this.plSplit);
            this.Controls.Add(this.plLeftTool);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "LinkForm";
            this.Text = "LinkForm";
            this.plLeftTool.ResumeLayout(false);
            this.pOtherTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.plLinkTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLinkTool)).EndInit();
            this.plDrawTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawTool)).EndInit();
            this.plDraw.ResumeLayout(false);
            this.gbWidth.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.edtWidth)).EndInit();
            this.gbDraw.ResumeLayout(false);
            this.plLink.ResumeLayout(false);
            this.gbMisc.ResumeLayout(false);
            this.gbTerminalStyle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbLinkStyle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCurve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPolyLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLine)).EndInit();
            this.pStyle.ResumeLayout(false);
            this.cbbond.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private System.Drawing.Pen		pSolid;
		private System.Drawing.Pen		pDot;
		private System.Drawing.Pen		pDash;
		private System.Drawing.Pen		pDashDot;
		private System.Drawing.Pen		pDashDotDot;
		private System.Drawing.Pen		selectedPen;

		private GOMLib.GOM_Link		m_selectedLink;
		private bool				bUnderSettingStyle;

		public event LinkStyleChangedEvent	LinkStyleChanged;
		public event RemoveLinkEvent		RemoveLink;

		public GOMLib.GOM_Link SelectedLink
		{
			get
			{
				return m_selectedLink;
			}
			set
			{
				m_selectedLink = value;
				if (m_selectedLink != null)
				{
					SetLinkStyle(m_selectedLink);
				}
			}
		}

		private void plDrawTool_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			System.Drawing.Drawing2D.Matrix	orgTrans;

			orgTrans = e.Graphics.Transform;
			e.Graphics.TranslateTransform(plDrawTool.Width, pbDrawTool.Height);
			e.Graphics.RotateTransform(90);
			e.Graphics.DrawString("Drawing", this.Font, System.Drawing.Brushes.Black, 2, (plDrawTool.Width - this.Font.Height) / 2);
			
			e.Graphics.Transform = orgTrans;
		}

		private void plLinkTool_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			System.Drawing.Drawing2D.Matrix	orgTrans;

			orgTrans = e.Graphics.Transform;
			e.Graphics.TranslateTransform(plDrawTool.Width, pbDrawTool.Height);
			e.Graphics.RotateTransform(90);
			e.Graphics.DrawString("Link", this.Font, System.Drawing.Brushes.Black, 2, (plDrawTool.Width - this.Font.Height) / 2);
			
			e.Graphics.Transform = orgTrans;
		}

        private void pOtherTool_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.Matrix orgTrans;

            orgTrans = e.Graphics.Transform;
            e.Graphics.TranslateTransform(plDrawTool.Width, pbDrawTool.Height);
            e.Graphics.RotateTransform(90);
            e.Graphics.DrawString("Style", this.Font, System.Drawing.Brushes.Black, 2, (plDrawTool.Width - this.Font.Height) / 2);

            e.Graphics.Transform = orgTrans;
        }

		private void plDrawTool_Click(object sender, System.EventArgs e)
		{
            plLinkTool.BackColor = Color.FromName("ControlLightLight");
            plDrawTool.BackColor = Color.FromName("Control");
            pOtherTool.BackColor = Color.FromName("ControlLightLight");
            plLink.Visible = false;
            plDraw.Visible = true;
            pStyle.Visible = false;
            plLink.Dock = DockStyle.None;
            plDraw.Dock = DockStyle.Fill;
            pStyle.Dock = DockStyle.None;
		}

		private void plLinkTool_Click(object sender, System.EventArgs e)
		{
            plLinkTool.BackColor = Color.FromName("Control");
            plDrawTool.BackColor = Color.FromName("ControlLightLight");
            pOtherTool.BackColor = Color.FromName("ControlLightLight");
            plLink.Visible = true;
            plDraw.Visible = false;
            pStyle.Visible = false;
            plLink.Dock = DockStyle.Fill;
            plDraw.Dock = DockStyle.None;
            pStyle.Dock = DockStyle.None;
		}

        private void pOtherTool_Click(object sender, System.EventArgs e)
        {
            plLinkTool.BackColor = Color.FromName("ControlLightLight");
            plDrawTool.BackColor = Color.FromName("ControlLightLight");
            pOtherTool.BackColor = Color.FromName("Control");
            plLink.Visible = false;
            plDraw.Visible = false;
            pStyle.Visible = true;
            plLink.Dock = DockStyle.None;
            plDraw.Dock = DockStyle.None;
            pStyle.Dock = DockStyle.Fill;
        }

		private void plSolidLine_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(pSolid, 5, plSolidLine.ClientSize.Height / 2, plSolidLine.Width - 5, plSolidLine.ClientSize.Height / 2);
		}

		private void plDotLine_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(pDot, 5, plDotLine.ClientSize.Height / 2, plDotLine.Width - 5, plDotLine.ClientSize.Height / 2);
		}

		private void plDashLine_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(pDash, 5, plDashLine.ClientSize.Height / 2, plDashLine.Width - 5, plDashLine.ClientSize.Height / 2);
		}

		private void plDashDotLine_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(pDashDot, 5, plDashDotLine.ClientSize.Height / 2, plDashDotLine.Width - 5, plDashDotLine.ClientSize.Height / 2);
		}

		private void plDashDotDotLine_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawLine(pDashDotDot, 5, plDashDotDotLine.ClientSize.Height / 2, plDashDotDotLine.Width - 5, plDashDotDotLine.ClientSize.Height / 2);
		}

		private void rbpSolid_CheckedChanged(object sender, System.EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				selectedPen = (Pen)((RadioButton)sender).Tag;
				UpdateDrawingStyle();
			}
		}

		private void drawingColor_ColorSelected(System.Drawing.Color color)
		{
			UpdateDrawingStyle();
		}

		private void edtWidth_ValueChanged(object sender, System.EventArgs e)
		{
			UpdateDrawingStyle();
		}

		private void UpdateDrawingStyle()
		{
			if (!bUnderSettingStyle)
			{
				if (m_selectedLink != null)
				{
					m_selectedLink.m_drawingStyle.drawingStyle = (Pen)selectedPen.Clone();
					m_selectedLink.m_drawingStyle.drawingStyle.Color = drawingColor.Color;
					m_selectedLink.m_drawingStyle.drawingStyle.Width = (float)System.Math.Max(1, edtWidth.Value);

					if (LinkStyleChanged != null)
					{
						LinkStyleChanged(m_selectedLink);
					}
				}
			}
		}

		private void SetLinkStyle(GOMLib.GOM_Link link)
		{
			SetDrawingStyle(link.m_drawingStyle);

			bUnderSettingStyle = true;
			cbPoints_SelectedIndexChanged(null, null);
			bUnderSettingStyle = false;
		}

		private void SetDrawingStyle(GOMLib.GOM_Style_Drawing style)
		{
			bUnderSettingStyle = true;
            bond_ini();
			switch (style.drawingStyle.DashStyle)
			{
				case System.Drawing.Drawing2D.DashStyle.Solid:
				{
					rbpSolid.Checked = true;
					break;
				}
				case System.Drawing.Drawing2D.DashStyle.Dot:
				{
					rbpDot.Checked = true;
					break;
				}
				case System.Drawing.Drawing2D.DashStyle.Dash:
				{
					rbpDash.Checked = true;
					break;
				}
				case System.Drawing.Drawing2D.DashStyle.DashDot:
				{
					rbpDashDot.Checked = true;
					break;
				}
				case System.Drawing.Drawing2D.DashStyle.DashDotDot:
				{
					rbpDashDotDot.Checked = true;
					break;
				}
			}
			drawingColor.Color = style.drawingStyle.Color;
			edtWidth.Value = (decimal)style.drawingStyle.Width;

			bUnderSettingStyle = false;
		}

		private void cbPoints_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (m_selectedLink != null)
			{
				GOMLib.GOM_Terminal_Style	style = GOMLib.GOM_Terminal_Style.None;

				switch (cbPoints.SelectedIndex)
				{
					case 0:
					{
						style = m_selectedLink.m_startStyle;
						break;
					}
					case 1:
					{
						style = m_selectedLink.m_endStyle;
						break;
					}
				}

				switch (style)
				{
					case GOMLib.GOM_Terminal_Style.None:
					{
						rbpNone.Checked = true;
						break;
					}
					case GOMLib.GOM_Terminal_Style.Arrow:
					{
						rbpArrow.Checked = true;
						break;
					}
					case GOMLib.GOM_Terminal_Style.Circle:
					{
						rbpCircle.Checked = true;
						break;
					}
					case GOMLib.GOM_Terminal_Style.Diamond:
					{
						rbpDiamond.Checked = true;
						break;
					}
					case GOMLib.GOM_Terminal_Style.Triangle:
					{
						rbpTriangle.Checked = true;
						break;
					}
				}
			}
		}

		private void rbpNone_CheckedChanged(object sender, System.EventArgs e)
		{
			if (!bUnderSettingStyle)
			{
				if (((RadioButton)sender).Checked)
				{
					if (m_selectedLink != null)
					{
						switch (cbPoints.SelectedIndex)
						{
							case 0:
							{
								m_selectedLink.m_startStyle = (GOMLib.GOM_Terminal_Style)((RadioButton)sender).Tag;
								if (LinkStyleChanged != null)
								{
									LinkStyleChanged(m_selectedLink);
								}
								break;
							}
							case 1:
							{
								m_selectedLink.m_endStyle = (GOMLib.GOM_Terminal_Style)((RadioButton)sender).Tag;
								if (LinkStyleChanged != null)
								{
									LinkStyleChanged(m_selectedLink);
								}
								break;
							}
						}
					}
				}
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (RemoveLink != null)
			{
				RemoveLink(m_selectedLink);
			}
		}

#region "Chester new link

    //private GOMLib.GOM_Templates lss = new GOMLib.GOM_Templates();
    ////Load Link from XML file
    //public void LoadLinkStyle(string path)
    //{
    //    System.IO.DirectoryInfo rgpath = new System.IO.DirectoryInfo(Environment.CurrentDirectory + "\\Links"); //Indicate the Link Path
    //    System.IO.FileInfo[] rgFiles = rgpath.GetFiles(); //Retrieve the Link files

    //    for (int i = 0; i < rgFiles.Length; i++)
    //    {
    //        try
    //        {
    //            LoadFromFile(rgFiles[i].FullName);
    //        }
    //        catch
    //        {
    //        }
    //    }
    //}

    //private void LoadFromFile(string fileName)
    //{
      
    //    GOMLib.GOM_Template template;
    //    //GOMLib.GOM_Object_Primitive primitive;
    //    template = GOMLib.GOM_Template.LoadFromFile(fileName);
    //    lss.Add(template); //add the item to lss
    //}

    //Get object by name
        //public GOMLib.GOM_Template GetLinkStyleByName(string name)
        //{
        //    for (int i = 0; i < lss.Count; i++)
        //    {
        //        if (lss[i].id == name)
        //            return lss[i];
        //    }
        //    return null;		

        //}

        private void bond_CheckedChanged(object sender, System.EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                switch (((RadioButton)sender).Text)
                {
                    case "Single Bond":
                        m_selectedLink.link_style = "single_bond";
                        m_selectedLink.link_value = 1;
                        break;
                    case "Double Bond":
                        m_selectedLink.link_style = "double_bond";
                        m_selectedLink.link_value = 2;
                        break;
                    case "Triple Bond":
                        m_selectedLink.link_style = "triple_bond";
                        m_selectedLink.link_value = 3;
                        break;
                    case "Wedge Bond":
                        m_selectedLink.link_style = "wedge_bond";
                        m_selectedLink.link_value = 1;
                        break;
                    case "None":
                        m_selectedLink.link_style = "None";
                        m_selectedLink.link_value = 1;
                        break;
                }
                LinkStyleChanged(m_selectedLink);
                UpdateDrawingStyle();
            }
        }

        private void bond_ini()
        {
            if (m_selectedLink != null)
            {
                switch (m_selectedLink.link_style)
                {
                    default:
                        bond_cbnone.Checked = true; break;
                    case "single_bond":
                        bond_cb1.Checked = true; break;
                    case "double_bond":
                        bond_cb2.Checked = true; break;
                    case "triple_bond":
                        bond_cb3.Checked = true; break;
                    case "wedge_bond":
                        bond_cbw.Checked = true; break;
                }
            }
        }
        
#endregion


	}

	public delegate void LinkStyleChangedEvent(GOMLib.GOM_Link link);
	public delegate void RemoveLinkEvent(GOMLib.GOM_Link link);
}
