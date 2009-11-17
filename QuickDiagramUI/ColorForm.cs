using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for ColorForm.
	/// </summary>
	public class ColorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel plLeftTool;
		private System.Windows.Forms.Panel plDrawTool;
		private System.Windows.Forms.PictureBox pbDrawTool;
		private System.Windows.Forms.Panel plSpacer;
		private System.Windows.Forms.Panel plFillTool;
		private System.Windows.Forms.PictureBox pbFillTool;
		private System.Windows.Forms.Panel plSplit;
		private FloatControlLib.TabControl tabDraw;
		private System.Windows.Forms.Panel plDrawBack;
		private System.Windows.Forms.Panel plDraw;
		private System.Windows.Forms.Panel plFillBack;
		private System.Windows.Forms.Panel plFill;
		private FloatControlLib.TabControl tabFill;
		private System.Windows.Forms.Panel plDrawSplit;
		private System.Windows.Forms.Panel plFillSplit;
		private System.Windows.Forms.Panel plSolidLine;
		private System.Windows.Forms.Panel plDotLine;
		private System.Windows.Forms.Panel plDashLine;
		private System.Windows.Forms.Panel plDashDotLine;
		private System.Windows.Forms.Panel plDashDotDotLine;
		private System.Windows.Forms.GroupBox gbDraw;
		private System.Windows.Forms.GroupBox gbFill;
		private System.Windows.Forms.Panel plHoriz;
		private System.Windows.Forms.Panel plVert;
		private System.Windows.Forms.Panel plCross;
		private System.Windows.Forms.Panel plFDiag;
		private System.Windows.Forms.Panel plBDiag;
		private System.Windows.Forms.Panel plCDiag;
		private System.Windows.Forms.Panel plSolid;
		private System.Windows.Forms.RadioButton rbpDashDotDot;
		private System.Windows.Forms.RadioButton rbpDashDot;
		private System.Windows.Forms.RadioButton rbpDash;
		private System.Windows.Forms.RadioButton rbpDot;
		private System.Windows.Forms.RadioButton rbpSolid;
		private System.Windows.Forms.RadioButton rbbSolid;
		private System.Windows.Forms.RadioButton rbbCDiag;
		private System.Windows.Forms.RadioButton rbbBDiag;
		private System.Windows.Forms.RadioButton rbbFDiag;
		private System.Windows.Forms.RadioButton rbbCross;
		private System.Windows.Forms.RadioButton rbbVert;
		private System.Windows.Forms.RadioButton rbbHoriz;
		private FloatControlLib.ColorControl drawingColor;
		private FloatControlLib.ColorControl fillingColor;
		private System.Windows.Forms.GroupBox gbWidth;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown edtWidth;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ColorForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			bUnderSettingStyle = false;

			selectedPen		= null;
			selectedBrush	= null;

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

			bSolid	= new SolidBrush(System.Drawing.Color.Black);
			bHoriz	= new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Horizontal, System.Drawing.Color.Black, System.Drawing.Color.Transparent);
			bVert	= new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Vertical, System.Drawing.Color.Black, System.Drawing.Color.Transparent);
			bCross	= new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Cross, System.Drawing.Color.Black, System.Drawing.Color.Transparent);
			bFDiag	= new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal, System.Drawing.Color.Black, System.Drawing.Color.Transparent);
			bBDiag	= new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal, System.Drawing.Color.Black, System.Drawing.Color.Transparent);
			bCDiag	= new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DiagonalCross, System.Drawing.Color.Black, System.Drawing.Color.Transparent);

			rbbSolid.Tag	= bSolid;
			rbbHoriz.Tag	= bHoriz;
			rbbVert.Tag		= bVert;
			rbbCross.Tag	= bCross;
			rbbFDiag.Tag	= bFDiag;
			rbbBDiag.Tag	= bBDiag;
			rbbCDiag.Tag	= bCDiag;
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

		private GOMLib.GOM_Object_Primitive m_selectedObject;

		private System.Drawing.Pen		pSolid;
		private System.Drawing.Pen		pDot;
		private System.Drawing.Pen		pDash;
		private System.Drawing.Pen		pDashDot;
		private System.Drawing.Pen		pDashDotDot;
		private System.Drawing.Pen		selectedPen;

		private System.Drawing.Brush	bSolid;
		private System.Drawing.Brush	bHoriz;
		private System.Drawing.Brush	bVert;
		private System.Drawing.Brush	bCross;
		private System.Drawing.Brush	bFDiag;
		private System.Drawing.Brush	bBDiag;
		private System.Drawing.Brush	bCDiag;
		private System.Drawing.Brush	selectedBrush;

		private bool bUnderSettingStyle;

		public event DrawingStyleChangedEvent DrawingStyleChanged;
		public event FillingStyleChangedEvent FillingStyleChanged;

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorForm));
            this.plLeftTool = new System.Windows.Forms.Panel();
            this.plFillTool = new System.Windows.Forms.Panel();
            this.pbFillTool = new System.Windows.Forms.PictureBox();
            this.plSpacer = new System.Windows.Forms.Panel();
            this.plDrawTool = new System.Windows.Forms.Panel();
            this.pbDrawTool = new System.Windows.Forms.PictureBox();
            this.plSplit = new System.Windows.Forms.Panel();
            this.plDrawBack = new System.Windows.Forms.Panel();
            this.plDrawSplit = new System.Windows.Forms.Panel();
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
            this.tabDraw = new FloatControlLib.TabControl();
            this.plFillBack = new System.Windows.Forms.Panel();
            this.plFillSplit = new System.Windows.Forms.Panel();
            this.plFill = new System.Windows.Forms.Panel();
            this.fillingColor = new FloatControlLib.ColorControl();
            this.gbFill = new System.Windows.Forms.GroupBox();
            this.plSolid = new System.Windows.Forms.Panel();
            this.rbbSolid = new System.Windows.Forms.RadioButton();
            this.plCDiag = new System.Windows.Forms.Panel();
            this.plBDiag = new System.Windows.Forms.Panel();
            this.plFDiag = new System.Windows.Forms.Panel();
            this.plCross = new System.Windows.Forms.Panel();
            this.plVert = new System.Windows.Forms.Panel();
            this.plHoriz = new System.Windows.Forms.Panel();
            this.rbbCDiag = new System.Windows.Forms.RadioButton();
            this.rbbBDiag = new System.Windows.Forms.RadioButton();
            this.rbbFDiag = new System.Windows.Forms.RadioButton();
            this.rbbCross = new System.Windows.Forms.RadioButton();
            this.rbbVert = new System.Windows.Forms.RadioButton();
            this.rbbHoriz = new System.Windows.Forms.RadioButton();
            this.tabFill = new FloatControlLib.TabControl();
            this.plLeftTool.SuspendLayout();
            this.plFillTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFillTool)).BeginInit();
            this.plDrawTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawTool)).BeginInit();
            this.plDrawBack.SuspendLayout();
            this.plDraw.SuspendLayout();
            this.gbWidth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtWidth)).BeginInit();
            this.gbDraw.SuspendLayout();
            this.plFillBack.SuspendLayout();
            this.plFill.SuspendLayout();
            this.gbFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // plLeftTool
            // 
            this.plLeftTool.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plLeftTool.Controls.Add(this.plFillTool);
            this.plLeftTool.Controls.Add(this.plSpacer);
            this.plLeftTool.Controls.Add(this.plDrawTool);
            this.plLeftTool.Dock = System.Windows.Forms.DockStyle.Left;
            this.plLeftTool.Location = new System.Drawing.Point(0, 0);
            this.plLeftTool.Name = "plLeftTool";
            this.plLeftTool.Padding = new System.Windows.Forms.Padding(2);
            this.plLeftTool.Size = new System.Drawing.Size(24, 443);
            this.plLeftTool.TabIndex = 0;
            // 
            // plFillTool
            // 
            this.plFillTool.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plFillTool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plFillTool.Controls.Add(this.pbFillTool);
            this.plFillTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.plFillTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.plFillTool.Location = new System.Drawing.Point(2, 84);
            this.plFillTool.Name = "plFillTool";
            this.plFillTool.Padding = new System.Windows.Forms.Padding(2);
            this.plFillTool.Size = new System.Drawing.Size(20, 78);
            this.plFillTool.TabIndex = 2;
            this.plFillTool.Paint += new System.Windows.Forms.PaintEventHandler(this.plFillTool_Paint);
            this.plFillTool.Click += new System.EventHandler(this.plFillTool_Click);
            // 
            // pbFillTool
            // 
            this.pbFillTool.BackColor = System.Drawing.Color.Transparent;
            this.pbFillTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbFillTool.Image = ((System.Drawing.Image)(resources.GetObject("pbFillTool.Image")));
            this.pbFillTool.Location = new System.Drawing.Point(2, 2);
            this.pbFillTool.Name = "pbFillTool";
            this.pbFillTool.Size = new System.Drawing.Size(14, 16);
            this.pbFillTool.TabIndex = 0;
            this.pbFillTool.TabStop = false;
            // 
            // plSpacer
            // 
            this.plSpacer.Dock = System.Windows.Forms.DockStyle.Top;
            this.plSpacer.Location = new System.Drawing.Point(2, 80);
            this.plSpacer.Name = "plSpacer";
            this.plSpacer.Size = new System.Drawing.Size(20, 4);
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
            this.plDrawTool.Size = new System.Drawing.Size(20, 78);
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
            this.pbDrawTool.Size = new System.Drawing.Size(14, 16);
            this.pbDrawTool.TabIndex = 0;
            this.pbDrawTool.TabStop = false;
            // 
            // plSplit
            // 
            this.plSplit.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.plSplit.Dock = System.Windows.Forms.DockStyle.Left;
            this.plSplit.Location = new System.Drawing.Point(24, 0);
            this.plSplit.Name = "plSplit";
            this.plSplit.Size = new System.Drawing.Size(1, 443);
            this.plSplit.TabIndex = 1;
            // 
            // plDrawBack
            // 
            this.plDrawBack.BackColor = System.Drawing.SystemColors.Control;
            this.plDrawBack.Controls.Add(this.plDrawSplit);
            this.plDrawBack.Controls.Add(this.plDraw);
            this.plDrawBack.Controls.Add(this.tabDraw);
            this.plDrawBack.Location = new System.Drawing.Point(25, 0);
            this.plDrawBack.Name = "plDrawBack";
            this.plDrawBack.Size = new System.Drawing.Size(206, 400);
            this.plDrawBack.TabIndex = 2;
            this.plDrawBack.Visible = false;
            // 
            // plDrawSplit
            // 
            this.plDrawSplit.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.plDrawSplit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plDrawSplit.Location = new System.Drawing.Point(0, 375);
            this.plDrawSplit.Name = "plDrawSplit";
            this.plDrawSplit.Size = new System.Drawing.Size(206, 1);
            this.plDrawSplit.TabIndex = 2;
            // 
            // plDraw
            // 
            this.plDraw.Controls.Add(this.gbWidth);
            this.plDraw.Controls.Add(this.drawingColor);
            this.plDraw.Controls.Add(this.gbDraw);
            this.plDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plDraw.Location = new System.Drawing.Point(0, 0);
            this.plDraw.Name = "plDraw";
            this.plDraw.Padding = new System.Windows.Forms.Padding(3);
            this.plDraw.Size = new System.Drawing.Size(206, 376);
            this.plDraw.TabIndex = 1;
            // 
            // gbWidth
            // 
            this.gbWidth.Controls.Add(this.label1);
            this.gbWidth.Controls.Add(this.edtWidth);
            this.gbWidth.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbWidth.Location = new System.Drawing.Point(3, 144);
            this.gbWidth.Name = "gbWidth";
            this.gbWidth.Size = new System.Drawing.Size(200, 48);
            this.gbWidth.TabIndex = 8;
            this.gbWidth.TabStop = false;
            this.gbWidth.Text = "Line Width";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Width:";
            // 
            // edtWidth
            // 
            this.edtWidth.Location = new System.Drawing.Point(104, 20);
            this.edtWidth.Name = "edtWidth";
            this.edtWidth.Size = new System.Drawing.Size(88, 21);
            this.edtWidth.TabIndex = 0;
            this.edtWidth.ValueChanged += new System.EventHandler(this.edtWidth_ValueChanged);
            // 
            // drawingColor
            // 
            this.drawingColor.Color = System.Drawing.Color.White;
            this.drawingColor.Location = new System.Drawing.Point(8, 200);
            this.drawingColor.Name = "drawingColor";
            this.drawingColor.Size = new System.Drawing.Size(188, 168);
            this.drawingColor.TabIndex = 7;
            this.drawingColor.Load += new System.EventHandler(this.drawingColor_Load);
            this.drawingColor.ColorSelected += new FloatControlLib.ColorSelectedEvent(this.drawingColor_ColorSelected);
            // 
            // gbDraw
            // 
            this.gbDraw.Controls.Add(this.rbpDashDot);
            this.gbDraw.Controls.Add(this.rbpDashDotDot);
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
            this.gbDraw.Size = new System.Drawing.Size(200, 141);
            this.gbDraw.TabIndex = 6;
            this.gbDraw.TabStop = false;
            this.gbDraw.Text = "Drawing Style";
            // 
            // rbpDashDotDot
            // 
            this.rbpDashDotDot.Location = new System.Drawing.Point(8, 120);
            this.rbpDashDotDot.Name = "rbpDashDotDot";
            this.rbpDashDotDot.Size = new System.Drawing.Size(88, 16);
            this.rbpDashDotDot.TabIndex = 9;
            this.rbpDashDotDot.Text = "DashDotDot";
            this.rbpDashDotDot.CheckedChanged += new System.EventHandler(this.rbpSolid_CheckedChanged);
            // 
            // rbpDashDot
            // 
            this.rbpDashDot.Location = new System.Drawing.Point(8, 96);
            this.rbpDashDot.Name = "rbpDashDot";
            this.rbpDashDot.Size = new System.Drawing.Size(88, 16);
            this.rbpDashDot.TabIndex = 8;
            this.rbpDashDot.Text = "DashDot";
            this.rbpDashDot.CheckedChanged += new System.EventHandler(this.rbpSolid_CheckedChanged);
            // 
            // rbpDash
            // 
            this.rbpDash.Location = new System.Drawing.Point(8, 72);
            this.rbpDash.Name = "rbpDash";
            this.rbpDash.Size = new System.Drawing.Size(88, 16);
            this.rbpDash.TabIndex = 7;
            this.rbpDash.Text = "Dash";
            this.rbpDash.CheckedChanged += new System.EventHandler(this.rbpSolid_CheckedChanged);
            // 
            // rbpDot
            // 
            this.rbpDot.Location = new System.Drawing.Point(8, 48);
            this.rbpDot.Name = "rbpDot";
            this.rbpDot.Size = new System.Drawing.Size(88, 16);
            this.rbpDot.TabIndex = 6;
            this.rbpDot.Text = "Dot";
            this.rbpDot.CheckedChanged += new System.EventHandler(this.rbpSolid_CheckedChanged);
            // 
            // rbpSolid
            // 
            this.rbpSolid.Location = new System.Drawing.Point(8, 24);
            this.rbpSolid.Name = "rbpSolid";
            this.rbpSolid.Size = new System.Drawing.Size(88, 16);
            this.rbpSolid.TabIndex = 5;
            this.rbpSolid.Text = "Solid";
            this.rbpSolid.CheckedChanged += new System.EventHandler(this.rbpSolid_CheckedChanged);
            // 
            // plDashDotDotLine
            // 
            this.plDashDotDotLine.BackColor = System.Drawing.Color.White;
            this.plDashDotDotLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDashDotDotLine.Location = new System.Drawing.Point(104, 118);
            this.plDashDotDotLine.Name = "plDashDotDotLine";
            this.plDashDotDotLine.Size = new System.Drawing.Size(88, 16);
            this.plDashDotDotLine.TabIndex = 4;
            this.plDashDotDotLine.Paint += new System.Windows.Forms.PaintEventHandler(this.plDashDotDotLine_Paint);
            // 
            // plDashDotLine
            // 
            this.plDashDotLine.BackColor = System.Drawing.Color.White;
            this.plDashDotLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDashDotLine.Location = new System.Drawing.Point(104, 94);
            this.plDashDotLine.Name = "plDashDotLine";
            this.plDashDotLine.Size = new System.Drawing.Size(88, 16);
            this.plDashDotLine.TabIndex = 3;
            this.plDashDotLine.Paint += new System.Windows.Forms.PaintEventHandler(this.plDashDotLine_Paint);
            // 
            // plDashLine
            // 
            this.plDashLine.BackColor = System.Drawing.Color.White;
            this.plDashLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDashLine.Location = new System.Drawing.Point(104, 70);
            this.plDashLine.Name = "plDashLine";
            this.plDashLine.Size = new System.Drawing.Size(88, 16);
            this.plDashLine.TabIndex = 2;
            this.plDashLine.Paint += new System.Windows.Forms.PaintEventHandler(this.plDashLine_Paint);
            // 
            // plDotLine
            // 
            this.plDotLine.BackColor = System.Drawing.Color.White;
            this.plDotLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDotLine.Location = new System.Drawing.Point(104, 46);
            this.plDotLine.Name = "plDotLine";
            this.plDotLine.Size = new System.Drawing.Size(88, 16);
            this.plDotLine.TabIndex = 1;
            this.plDotLine.Paint += new System.Windows.Forms.PaintEventHandler(this.plDotLine_Paint);
            // 
            // plSolidLine
            // 
            this.plSolidLine.BackColor = System.Drawing.Color.White;
            this.plSolidLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plSolidLine.Location = new System.Drawing.Point(104, 22);
            this.plSolidLine.Name = "plSolidLine";
            this.plSolidLine.Size = new System.Drawing.Size(88, 16);
            this.plSolidLine.TabIndex = 0;
            this.plSolidLine.Paint += new System.Windows.Forms.PaintEventHandler(this.plSolidLine_Paint);
            // 
            // tabDraw
            // 
            this.tabDraw.BackColor = System.Drawing.SystemColors.Control;
            this.tabDraw.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabDraw.Location = new System.Drawing.Point(0, 376);
            this.tabDraw.Name = "tabDraw";
            this.tabDraw.Padding = new System.Windows.Forms.Padding(2);
            this.tabDraw.SelectedTab = null;
            this.tabDraw.Size = new System.Drawing.Size(206, 24);
            this.tabDraw.TabIndex = 0;
            this.tabDraw.SelectionChange += new FloatControlLib.SelectionChangeEvent(this.tabDraw_SelectionChange);
            // 
            // plFillBack
            // 
            this.plFillBack.BackColor = System.Drawing.SystemColors.Control;
            this.plFillBack.Controls.Add(this.plFillSplit);
            this.plFillBack.Controls.Add(this.plFill);
            this.plFillBack.Controls.Add(this.tabFill);
            this.plFillBack.Location = new System.Drawing.Point(248, 0);
            this.plFillBack.Name = "plFillBack";
            this.plFillBack.Size = new System.Drawing.Size(206, 400);
            this.plFillBack.TabIndex = 3;
            this.plFillBack.Visible = false;
            // 
            // plFillSplit
            // 
            this.plFillSplit.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.plFillSplit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plFillSplit.Location = new System.Drawing.Point(0, 375);
            this.plFillSplit.Name = "plFillSplit";
            this.plFillSplit.Size = new System.Drawing.Size(206, 1);
            this.plFillSplit.TabIndex = 2;
            // 
            // plFill
            // 
            this.plFill.Controls.Add(this.fillingColor);
            this.plFill.Controls.Add(this.gbFill);
            this.plFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFill.Location = new System.Drawing.Point(0, 0);
            this.plFill.Name = "plFill";
            this.plFill.Padding = new System.Windows.Forms.Padding(3);
            this.plFill.Size = new System.Drawing.Size(206, 376);
            this.plFill.TabIndex = 1;
            // 
            // fillingColor
            // 
            this.fillingColor.Color = System.Drawing.Color.White;
            this.fillingColor.Location = new System.Drawing.Point(8, 200);
            this.fillingColor.Name = "fillingColor";
            this.fillingColor.Size = new System.Drawing.Size(188, 168);
            this.fillingColor.TabIndex = 8;
            this.fillingColor.ColorSelected += new FloatControlLib.ColorSelectedEvent(this.fillingColor_ColorSelected);
            // 
            // gbFill
            // 
            this.gbFill.Controls.Add(this.plSolid);
            this.gbFill.Controls.Add(this.rbbSolid);
            this.gbFill.Controls.Add(this.plCDiag);
            this.gbFill.Controls.Add(this.plBDiag);
            this.gbFill.Controls.Add(this.plFDiag);
            this.gbFill.Controls.Add(this.plCross);
            this.gbFill.Controls.Add(this.plVert);
            this.gbFill.Controls.Add(this.plHoriz);
            this.gbFill.Controls.Add(this.rbbCDiag);
            this.gbFill.Controls.Add(this.rbbBDiag);
            this.gbFill.Controls.Add(this.rbbFDiag);
            this.gbFill.Controls.Add(this.rbbCross);
            this.gbFill.Controls.Add(this.rbbVert);
            this.gbFill.Controls.Add(this.rbbHoriz);
            this.gbFill.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFill.Location = new System.Drawing.Point(3, 3);
            this.gbFill.Name = "gbFill";
            this.gbFill.Size = new System.Drawing.Size(200, 192);
            this.gbFill.TabIndex = 0;
            this.gbFill.TabStop = false;
            this.gbFill.Text = "Filling Style";
            // 
            // plSolid
            // 
            this.plSolid.BackColor = System.Drawing.Color.White;
            this.plSolid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plSolid.Location = new System.Drawing.Point(96, 24);
            this.plSolid.Name = "plSolid";
            this.plSolid.Size = new System.Drawing.Size(96, 16);
            this.plSolid.TabIndex = 15;
            this.plSolid.Paint += new System.Windows.Forms.PaintEventHandler(this.plSolid_Paint);
            // 
            // rbbSolid
            // 
            this.rbbSolid.Location = new System.Drawing.Point(8, 24);
            this.rbbSolid.Name = "rbbSolid";
            this.rbbSolid.Size = new System.Drawing.Size(88, 16);
            this.rbbSolid.TabIndex = 14;
            this.rbbSolid.Text = "Solid";
            this.rbbSolid.CheckedChanged += new System.EventHandler(this.rbbSolid_CheckedChanged);
            // 
            // plCDiag
            // 
            this.plCDiag.BackColor = System.Drawing.Color.White;
            this.plCDiag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plCDiag.Location = new System.Drawing.Point(96, 168);
            this.plCDiag.Name = "plCDiag";
            this.plCDiag.Size = new System.Drawing.Size(96, 16);
            this.plCDiag.TabIndex = 13;
            this.plCDiag.Paint += new System.Windows.Forms.PaintEventHandler(this.plCDiag_Paint);
            // 
            // plBDiag
            // 
            this.plBDiag.BackColor = System.Drawing.Color.White;
            this.plBDiag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plBDiag.Location = new System.Drawing.Point(96, 144);
            this.plBDiag.Name = "plBDiag";
            this.plBDiag.Size = new System.Drawing.Size(96, 16);
            this.plBDiag.TabIndex = 12;
            this.plBDiag.Paint += new System.Windows.Forms.PaintEventHandler(this.plBDiag_Paint);
            // 
            // plFDiag
            // 
            this.plFDiag.BackColor = System.Drawing.Color.White;
            this.plFDiag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plFDiag.Location = new System.Drawing.Point(96, 120);
            this.plFDiag.Name = "plFDiag";
            this.plFDiag.Size = new System.Drawing.Size(96, 16);
            this.plFDiag.TabIndex = 11;
            this.plFDiag.Paint += new System.Windows.Forms.PaintEventHandler(this.plFDiag_Paint);
            // 
            // plCross
            // 
            this.plCross.BackColor = System.Drawing.Color.White;
            this.plCross.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plCross.Location = new System.Drawing.Point(96, 96);
            this.plCross.Name = "plCross";
            this.plCross.Size = new System.Drawing.Size(96, 16);
            this.plCross.TabIndex = 10;
            this.plCross.Paint += new System.Windows.Forms.PaintEventHandler(this.plCross_Paint);
            // 
            // plVert
            // 
            this.plVert.BackColor = System.Drawing.Color.White;
            this.plVert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plVert.Location = new System.Drawing.Point(96, 72);
            this.plVert.Name = "plVert";
            this.plVert.Size = new System.Drawing.Size(96, 16);
            this.plVert.TabIndex = 9;
            this.plVert.Paint += new System.Windows.Forms.PaintEventHandler(this.plVert_Paint);
            // 
            // plHoriz
            // 
            this.plHoriz.BackColor = System.Drawing.Color.White;
            this.plHoriz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plHoriz.Location = new System.Drawing.Point(96, 48);
            this.plHoriz.Name = "plHoriz";
            this.plHoriz.Size = new System.Drawing.Size(96, 16);
            this.plHoriz.TabIndex = 8;
            this.plHoriz.Paint += new System.Windows.Forms.PaintEventHandler(this.plHoriz_Paint);
            // 
            // rbbCDiag
            // 
            this.rbbCDiag.Location = new System.Drawing.Point(8, 168);
            this.rbbCDiag.Name = "rbbCDiag";
            this.rbbCDiag.Size = new System.Drawing.Size(88, 16);
            this.rbbCDiag.TabIndex = 7;
            this.rbbCDiag.Text = "C-Diagonal";
            this.rbbCDiag.CheckedChanged += new System.EventHandler(this.rbbSolid_CheckedChanged);
            // 
            // rbbBDiag
            // 
            this.rbbBDiag.Location = new System.Drawing.Point(8, 144);
            this.rbbBDiag.Name = "rbbBDiag";
            this.rbbBDiag.Size = new System.Drawing.Size(88, 16);
            this.rbbBDiag.TabIndex = 6;
            this.rbbBDiag.Text = "B-Diagonal";
            this.rbbBDiag.CheckedChanged += new System.EventHandler(this.rbbSolid_CheckedChanged);
            // 
            // rbbFDiag
            // 
            this.rbbFDiag.Location = new System.Drawing.Point(8, 120);
            this.rbbFDiag.Name = "rbbFDiag";
            this.rbbFDiag.Size = new System.Drawing.Size(88, 16);
            this.rbbFDiag.TabIndex = 5;
            this.rbbFDiag.Text = "F-Diagonal";
            this.rbbFDiag.CheckedChanged += new System.EventHandler(this.rbbSolid_CheckedChanged);
            // 
            // rbbCross
            // 
            this.rbbCross.Location = new System.Drawing.Point(8, 96);
            this.rbbCross.Name = "rbbCross";
            this.rbbCross.Size = new System.Drawing.Size(88, 16);
            this.rbbCross.TabIndex = 4;
            this.rbbCross.Text = "Cross";
            this.rbbCross.CheckedChanged += new System.EventHandler(this.rbbSolid_CheckedChanged);
            // 
            // rbbVert
            // 
            this.rbbVert.Location = new System.Drawing.Point(8, 72);
            this.rbbVert.Name = "rbbVert";
            this.rbbVert.Size = new System.Drawing.Size(88, 16);
            this.rbbVert.TabIndex = 3;
            this.rbbVert.Text = "Vertical";
            this.rbbVert.CheckedChanged += new System.EventHandler(this.rbbSolid_CheckedChanged);
            // 
            // rbbHoriz
            // 
            this.rbbHoriz.Location = new System.Drawing.Point(8, 48);
            this.rbbHoriz.Name = "rbbHoriz";
            this.rbbHoriz.Size = new System.Drawing.Size(88, 16);
            this.rbbHoriz.TabIndex = 2;
            this.rbbHoriz.Text = "Horizontal";
            this.rbbHoriz.CheckedChanged += new System.EventHandler(this.rbbSolid_CheckedChanged);
            // 
            // tabFill
            // 
            this.tabFill.BackColor = System.Drawing.SystemColors.Control;
            this.tabFill.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabFill.Location = new System.Drawing.Point(0, 376);
            this.tabFill.Name = "tabFill";
            this.tabFill.Padding = new System.Windows.Forms.Padding(2);
            this.tabFill.SelectedTab = null;
            this.tabFill.Size = new System.Drawing.Size(206, 24);
            this.tabFill.TabIndex = 0;
            this.tabFill.SelectionChange += new FloatControlLib.SelectionChangeEvent(this.tabFill_SelectionChange);
            // 
            // ColorForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(632, 443);
            this.Controls.Add(this.plFillBack);
            this.Controls.Add(this.plDrawBack);
            this.Controls.Add(this.plSplit);
            this.Controls.Add(this.plLeftTool);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ColorForm";
            this.Text = "Color and Style";
            this.plLeftTool.ResumeLayout(false);
            this.plFillTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFillTool)).EndInit();
            this.plDrawTool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDrawTool)).EndInit();
            this.plDrawBack.ResumeLayout(false);
            this.plDraw.ResumeLayout(false);
            this.gbWidth.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.edtWidth)).EndInit();
            this.gbDraw.ResumeLayout(false);
            this.plFillBack.ResumeLayout(false);
            this.plFill.ResumeLayout(false);
            this.gbFill.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public GOMLib.GOM_Object_Primitive SelectedObject
		{
			get
			{
				return m_selectedObject;
			}
			set
			{
				if ((m_selectedObject == null) || (!m_selectedObject.Equals(value)))
				{
					m_selectedObject = value;
					ResetSelectedObject();
				}
			}
		}

		private void plDrawTool_Click(object sender, System.EventArgs e)
		{
			plFillTool.BackColor	= Color.FromName("ControlLightLight");
			plDrawTool.BackColor	= Color.FromName("Control");
			plFillBack.Visible		= false;
			plDrawBack.Visible		= true;
			plFillBack.Dock			= DockStyle.None;
			plDrawBack.Dock			= DockStyle.Fill;
		}

		private void plFillTool_Click(object sender, System.EventArgs e)
		{
			plDrawTool.BackColor = Color.FromName("ControlLightLight");
			plFillTool.BackColor = Color.FromName("Control");
			plDrawBack.Visible		= false;
			plFillBack.Visible		= true;
			plDrawBack.Dock			= DockStyle.None;
			plFillBack.Dock			= DockStyle.Fill;
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

		private void plFillTool_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			System.Drawing.Drawing2D.Matrix	orgTrans;

			orgTrans = e.Graphics.Transform;
			e.Graphics.TranslateTransform(plFillTool.Width, pbFillTool.Height);
			e.Graphics.RotateTransform(90);
			e.Graphics.DrawString("Filling", this.Font, System.Drawing.Brushes.Black, 2, (plFillTool.Width - this.Font.Height) / 2);
			
			e.Graphics.Transform = orgTrans;
		}

		private void ResetSelectedObject()
		{
			while (tabDraw.TabCount > 0)
			{
				tabDraw.RemoveTab(0);
			}

			while (tabFill.TabCount > 0)
			{
				tabFill.RemoveTab(0);
			}

			if (m_selectedObject != null)
			{
				for (int i = 0; i < m_selectedObject.rgDrawingStyles.Count; i++)
				{
					tabDraw.AddTab(m_selectedObject.rgDrawingStyles[i].id, pbDrawTool.Image, 60, m_selectedObject.rgDrawingStyles[i]);
				}

				for (int i = 0; i < m_selectedObject.rgFillingStyles.Count; i++)
				{
					tabFill.AddTab(m_selectedObject.rgFillingStyles[i].id, pbFillTool.Image, 60, m_selectedObject.rgFillingStyles[i]);
				}

				if (plDrawTool.BackColor.Name.Equals("Control"))
				{
					plDrawTool_Click(null, null);
				}
				else
				{
					plFillTool_Click(null, null);
				}

				this.PerformLayout();
			}
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

		private void plSolid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.FillRectangle(bSolid, 0, 0, plSolid.Width, plSolid.Height);
		}

		private void plHoriz_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.FillRectangle(bHoriz, 0, 0, plHoriz.Width, plHoriz.Height);
		}

		private void plVert_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.FillRectangle(bVert, 0, 0, plVert.Width, plVert.Height);
		}

		private void plCross_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.FillRectangle(bCross, 0, 0, plCross.Width, plCross.Height);
		}

		private void plFDiag_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.FillRectangle(bFDiag, 0, 0, plFDiag.Width, plFDiag.Height);
		}

		private void plBDiag_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.FillRectangle(bBDiag, 0, 0, plBDiag.Width, plBDiag.Height);
		}

		private void plCDiag_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.FillRectangle(bCDiag, 0, 0, plCDiag.Width, plCDiag.Height);
		}

		private void SetDrawingStyle(GOMLib.GOM_Style_Drawing style)
		{
			bUnderSettingStyle = true;

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

		private void SetFillingStyle(GOMLib.GOM_Style_Filling style)
		{
			bUnderSettingStyle = true;

			if (style.fillingStyle is SolidBrush)
			{
				rbbSolid.Checked = true;
				fillingColor.Color = ((SolidBrush)style.fillingStyle).Color;
			}
			else if (style.fillingStyle is System.Drawing.Drawing2D.HatchBrush)
			{
				switch (((System.Drawing.Drawing2D.HatchBrush)style.fillingStyle).HatchStyle)
				{
					case System.Drawing.Drawing2D.HatchStyle.Horizontal:
					{
						rbbHoriz.Checked = true;
						break;
					}
					case System.Drawing.Drawing2D.HatchStyle.Vertical:
					{
						rbbVert.Checked = true;
						break;
					}
					case System.Drawing.Drawing2D.HatchStyle.Cross:
					{
						rbbCross.Checked = true;
						break;
					}
					case System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal:
					{
						rbbBDiag.Checked = true;
						break;
					}
					case System.Drawing.Drawing2D.HatchStyle.ForwardDiagonal:
					{
						rbbFDiag.Checked = true;
						break;
					}
					case System.Drawing.Drawing2D.HatchStyle.DiagonalCross:
					{
						rbbCDiag.Checked = true;
						break;
					}
				}
				fillingColor.Color = ((System.Drawing.Drawing2D.HatchBrush)style.fillingStyle).ForegroundColor;
			}

			bUnderSettingStyle = false;
		}

		private void tabDraw_SelectionChange(FloatControlLib.TabItem selectedItem)
		{
			SetDrawingStyle((GOMLib.GOM_Style_Drawing)selectedItem.Tag);
		}

		private void tabFill_SelectionChange(FloatControlLib.TabItem selectedItem)
		{
			SetFillingStyle((GOMLib.GOM_Style_Filling)selectedItem.Tag);
		}

		private void UpdateDrawingStyle()
		{
			GOMLib.GOM_Style_Drawing	style;

			if (!bUnderSettingStyle)
			{
				if (tabDraw.SelectedTab != null)
				{
					if (tabDraw.SelectedTab.Tag != null)
					{
						if (selectedPen != null)
						{
							style = (GOMLib.GOM_Style_Drawing)tabDraw.SelectedTab.Tag;
							style.drawingStyle = (Pen)selectedPen.Clone();
							style.drawingStyle.Color = drawingColor.Color;
							style.drawingStyle.Width = (float)System.Math.Max(1, edtWidth.Value);

							if (DrawingStyleChanged != null)
							{
								DrawingStyleChanged(m_selectedObject, style);
							}
						}
					}
				}
			}
		}

		private void UpdateFillingStyle()
		{
			GOMLib.GOM_Style_Filling	style;

			if (!bUnderSettingStyle)
			{
				if (tabFill.SelectedTab != null)
				{
					if (tabFill.SelectedTab.Tag != null)
					{
						if (selectedBrush != null)
						{
							style = (GOMLib.GOM_Style_Filling)tabFill.SelectedTab.Tag;
							if (selectedBrush is SolidBrush)
							{
								style.fillingStyle = new SolidBrush(fillingColor.Color);
							}
							else if (selectedBrush is System.Drawing.Drawing2D.HatchBrush)
							{
								style.fillingStyle = new System.Drawing.Drawing2D.HatchBrush(((System.Drawing.Drawing2D.HatchBrush)selectedBrush).HatchStyle, fillingColor.Color, System.Drawing.Color.Transparent);
							}

							if (FillingStyleChanged != null)
							{
								FillingStyleChanged(m_selectedObject, style);
							}
						}
					}
				}
			}
		}

		private void rbpSolid_CheckedChanged(object sender, System.EventArgs e)
		{
			selectedPen = (Pen)((RadioButton)sender).Tag;
			UpdateDrawingStyle();
		}

		private void rbbSolid_CheckedChanged(object sender, System.EventArgs e)
		{
			selectedBrush = (Brush)((RadioButton)sender).Tag;
			UpdateFillingStyle();
		}

		private void drawingColor_ColorSelected(System.Drawing.Color color)
		{
			UpdateDrawingStyle();
		}

		private void fillingColor_ColorSelected(System.Drawing.Color color)
		{
			UpdateFillingStyle();
		}

		private void edtWidth_ValueChanged(object sender, System.EventArgs e)
		{
			UpdateDrawingStyle();
		}

        private void drawingColor_Load(object sender, EventArgs e)
        {

        }
	}

	public delegate void DrawingStyleChangedEvent(GOMLib.GOM_Object_Primitive primitive, GOMLib.GOM_Style_Drawing style);
	public delegate void FillingStyleChangedEvent(GOMLib.GOM_Object_Primitive primitive, GOMLib.GOM_Style_Filling style);
}
