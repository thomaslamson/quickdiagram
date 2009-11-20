using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu muMain;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private FloatControlLib.FloatBar fbMain;
		private FloatControlLib.DockControl dcMain;
		private System.Windows.Forms.Splitter spMain;
		private System.Windows.Forms.ImageList imgsToolbar;
		private System.Windows.Forms.ImageList imgsToolbox;
		private System.Windows.Forms.ToolBarButton tbbNew;
		private System.Windows.Forms.ToolBarButton tbbOpen;
		private System.Windows.Forms.ToolBarButton tbbSave;
		private System.Windows.Forms.StatusBarPanel sbpStatus;
		private System.Windows.Forms.StatusBarPanel sbpPosition;
		private System.Windows.Forms.ToolBar tbMain;
		private System.Windows.Forms.StatusBar sbMain;
		private System.Windows.Forms.ToolBarButton tbSep1;
		private System.Windows.Forms.ToolBarButton tbbUndo;
		private System.Windows.Forms.ToolBarButton tbbRedo;
		private System.Windows.Forms.ToolBarButton tbSep2;
		private System.Windows.Forms.ToolBarButton tbbCut;
		private System.Windows.Forms.ToolBarButton tbbCopy;
		private System.Windows.Forms.ToolBarButton tbbPaste;
		private System.Windows.Forms.ToolBarButton tbbDelete;
		private System.Windows.Forms.ToolBarButton tbSep3;
		private System.Windows.Forms.SaveFileDialog saveFileDlg;
		private System.Windows.Forms.OpenFileDialog openFileDlg;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem menuItem18;
        private MenuItem menuItem19;
        private MenuItem menuItem20;

		private EEDomain.ReadFromXml readXml;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			ResetStatusBarSize();

			m_catalog			= new CatalogForm();
			m_catalog.TopLevel	= false;

			m_recognition			= new RecognitionForm();
			m_recognition.TopLevel	= false;

			fbMain.AddFloatWindow(m_catalog, imgsToolbox.Images[0], "Catalog", 160, 100, false);

			dcMain.AddDockWindow(m_recognition, imgsToolbox.Images[2], "Recognition", 100);
			dcMain.Width = 160;
			spMain.Enabled	= true;

			m_floatBarWidth = fbMain.Width;
			m_dockBarWidth	= dcMain.Width;

			m_catalog.LoadTemplates(Environment.CurrentDirectory + "\\Templates");
			m_catalog.StencilSelected	+= new StencilSelectedEvent(m_catalog_StencilSelected);
			m_catalog.TemplateSelected	+= new TemplateSelectedEvent(frmStencil_TemplateSelected);

			m_recognition.RecognitionResultSelected += new RecognitionResultSelectedEvent(m_recognition_RecognitionResultSelected);

			menuItem2_Click(null, null);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.muMain = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.tbMain = new System.Windows.Forms.ToolBar();
            this.tbbNew = new System.Windows.Forms.ToolBarButton();
            this.tbbOpen = new System.Windows.Forms.ToolBarButton();
            this.tbbSave = new System.Windows.Forms.ToolBarButton();
            this.tbSep1 = new System.Windows.Forms.ToolBarButton();
            this.tbbCut = new System.Windows.Forms.ToolBarButton();
            this.tbbCopy = new System.Windows.Forms.ToolBarButton();
            this.tbbPaste = new System.Windows.Forms.ToolBarButton();
            this.tbbDelete = new System.Windows.Forms.ToolBarButton();
            this.tbSep2 = new System.Windows.Forms.ToolBarButton();
            this.tbbRedo = new System.Windows.Forms.ToolBarButton();
            this.tbbUndo = new System.Windows.Forms.ToolBarButton();
            this.tbSep3 = new System.Windows.Forms.ToolBarButton();
            this.imgsToolbar = new System.Windows.Forms.ImageList(this.components);
            this.sbMain = new System.Windows.Forms.StatusBar();
            this.sbpStatus = new System.Windows.Forms.StatusBarPanel();
            this.sbpPosition = new System.Windows.Forms.StatusBarPanel();
            this.spMain = new System.Windows.Forms.Splitter();
            this.imgsToolbox = new System.Windows.Forms.ImageList(this.components);
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.dcMain = new FloatControlLib.DockControl();
            this.fbMain = new FloatControlLib.FloatBar();
            ((System.ComponentModel.ISupportInitialize)(this.sbpStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // muMain
            // 
            this.muMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem10,
            this.menuItem11,
            this.menuItem16});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5,
            this.menuItem6,
            this.menuItem7,
            this.menuItem8,
            this.menuItem9});
            this.menuItem1.Text = "&File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "&New Diagram";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "&Open Diagram";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "&Close Diagram";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 3;
            this.menuItem5.Text = "-";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 4;
            this.menuItem6.Text = "&Save Diagram";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 5;
            this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem19,
            this.menuItem20});
            this.menuItem7.Text = "Save Diagram &as";
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 0;
            this.menuItem19.Text = "EE Diagram";
            this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 1;
            this.menuItem20.Text = "Others ...";
            this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 6;
            this.menuItem8.Text = "-";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 7;
            this.menuItem9.Text = "E&xit";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 1;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem12,
            this.menuItem13});
            this.menuItem10.Text = "Input";
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 0;
            this.menuItem12.Text = "Diagram";
            this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 1;
            this.menuItem13.Text = "Value";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 2;
            this.menuItem11.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem14,
            this.menuItem15});
            this.menuItem11.Text = "Simulation";
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 0;
            this.menuItem14.Text = "List Equation";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 1;
            this.menuItem15.Text = "Solve Unknown";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 3;
            this.menuItem16.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem17,
            this.menuItem18});
            this.menuItem16.Text = "Generation";
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 0;
            this.menuItem17.Text = "Display Pspice Code";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 1;
            this.menuItem18.Text = "Save Pspice Code";
            // 
            // tbMain
            // 
            this.tbMain.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tbMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbbNew,
            this.tbbOpen,
            this.tbbSave,
            this.tbSep1,
            this.tbbCut,
            this.tbbCopy,
            this.tbbPaste,
            this.tbbDelete,
            this.tbSep2,
            this.tbbRedo,
            this.tbbUndo,
            this.tbSep3});
            this.tbMain.ButtonSize = new System.Drawing.Size(16, 16);
            this.tbMain.DropDownArrows = true;
            this.tbMain.ImageList = this.imgsToolbar;
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.ShowToolTips = true;
            this.tbMain.Size = new System.Drawing.Size(806, 28);
            this.tbMain.TabIndex = 0;
            this.tbMain.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbMain_ButtonClick);
            // 
            // tbbNew
            // 
            this.tbbNew.ImageIndex = 0;
            this.tbbNew.Name = "tbbNew";
            // 
            // tbbOpen
            // 
            this.tbbOpen.ImageIndex = 1;
            this.tbbOpen.Name = "tbbOpen";
            // 
            // tbbSave
            // 
            this.tbbSave.ImageIndex = 2;
            this.tbbSave.Name = "tbbSave";
            // 
            // tbSep1
            // 
            this.tbSep1.Name = "tbSep1";
            this.tbSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbCut
            // 
            this.tbbCut.ImageIndex = 5;
            this.tbbCut.Name = "tbbCut";
            // 
            // tbbCopy
            // 
            this.tbbCopy.ImageIndex = 6;
            this.tbbCopy.Name = "tbbCopy";
            // 
            // tbbPaste
            // 
            this.tbbPaste.ImageIndex = 7;
            this.tbbPaste.Name = "tbbPaste";
            // 
            // tbbDelete
            // 
            this.tbbDelete.ImageIndex = 8;
            this.tbbDelete.Name = "tbbDelete";
            // 
            // tbSep2
            // 
            this.tbSep2.Name = "tbSep2";
            this.tbSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbRedo
            // 
            this.tbbRedo.ImageIndex = 4;
            this.tbbRedo.Name = "tbbRedo";
            // 
            // tbbUndo
            // 
            this.tbbUndo.ImageIndex = 3;
            this.tbbUndo.Name = "tbbUndo";
            // 
            // tbSep3
            // 
            this.tbSep3.Name = "tbSep3";
            this.tbSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // imgsToolbar
            // 
            this.imgsToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgsToolbar.ImageStream")));
            this.imgsToolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.imgsToolbar.Images.SetKeyName(0, "");
            this.imgsToolbar.Images.SetKeyName(1, "");
            this.imgsToolbar.Images.SetKeyName(2, "");
            this.imgsToolbar.Images.SetKeyName(3, "");
            this.imgsToolbar.Images.SetKeyName(4, "");
            this.imgsToolbar.Images.SetKeyName(5, "");
            this.imgsToolbar.Images.SetKeyName(6, "");
            this.imgsToolbar.Images.SetKeyName(7, "");
            this.imgsToolbar.Images.SetKeyName(8, "");
            // 
            // sbMain
            // 
            this.sbMain.Location = new System.Drawing.Point(0, 372);
            this.sbMain.Name = "sbMain";
            this.sbMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sbpStatus,
            this.sbpPosition});
            this.sbMain.ShowPanels = true;
            this.sbMain.Size = new System.Drawing.Size(806, 23);
            this.sbMain.TabIndex = 1;
            this.sbMain.SizeChanged += new System.EventHandler(this.sbMain_SizeChanged);
            // 
            // sbpStatus
            // 
            this.sbpStatus.Name = "sbpStatus";
            // 
            // sbpPosition
            // 
            this.sbpPosition.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.sbpPosition.Name = "sbpPosition";
            // 
            // spMain
            // 
            this.spMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.spMain.Location = new System.Drawing.Point(222, 28);
            this.spMain.Name = "spMain";
            this.spMain.Size = new System.Drawing.Size(4, 344);
            this.spMain.TabIndex = 4;
            this.spMain.TabStop = false;
            // 
            // imgsToolbox
            // 
            this.imgsToolbox.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgsToolbox.ImageStream")));
            this.imgsToolbox.TransparentColor = System.Drawing.Color.Transparent;
            this.imgsToolbox.Images.SetKeyName(0, "");
            this.imgsToolbox.Images.SetKeyName(1, "");
            this.imgsToolbox.Images.SetKeyName(2, "");
            // 
            // saveFileDlg
            // 
            this.saveFileDlg.DefaultExt = "xml";
            this.saveFileDlg.Filter = "Diagram(*.xml)|*.xml|All Files(*.*)|*.*";
            // 
            // openFileDlg
            // 
            this.openFileDlg.DefaultExt = "xml";
            this.openFileDlg.Filter = "Diagram(*.xml)|*.xml|All Files(*.*)|*.*";
            // 
            // dcMain
            // 
            this.dcMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.dcMain.Location = new System.Drawing.Point(30, 28);
            this.dcMain.Name = "dcMain";
            this.dcMain.Padding = new System.Windows.Forms.Padding(2);
            this.dcMain.Size = new System.Drawing.Size(192, 344);
            this.dcMain.TabIndex = 3;
            this.dcMain.DockItemFloating += new FloatControlLib.DockItemFloatingEvent(this.dcMain_DockItemFloating);
            this.dcMain.DockItemClosing += new FloatControlLib.DockItemClosingEvent(this.dcMain_DockItemClosing);
            // 
            // fbMain
            // 
            this.fbMain.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.fbMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.fbMain.Location = new System.Drawing.Point(0, 28);
            this.fbMain.Name = "fbMain";
            this.fbMain.Padding = new System.Windows.Forms.Padding(2, 2, 3, 2);
            this.fbMain.Size = new System.Drawing.Size(30, 344);
            this.fbMain.TabIndex = 2;
            this.fbMain.FloatItemClosing += new FloatControlLib.FloatItemClosingEvent(this.fbMain_FloatItemClosing);
            this.fbMain.FloatWindowHiding += new FloatControlLib.FloatWindowHidingEvent(this.fbMain_FloatWindowHiding);
            this.fbMain.FloatItemDocking += new FloatControlLib.FloatItemDockingEvent(this.fbMain_FloatItemDocking);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(806, 395);
            this.Controls.Add(this.spMain);
            this.Controls.Add(this.dcMain);
            this.Controls.Add(this.fbMain);
            this.Controls.Add(this.sbMain);
            this.Controls.Add(this.tbMain);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.muMain;
            this.Name = "MainForm";
            this.Text = "QuickDiagram";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sbpStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbpPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private CatalogForm		m_catalog;
		private RecognitionForm	m_recognition;

		private int	m_floatBarWidth;
		private int m_dockBarWidth;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void ResetStatusBarSize()
		{
			if (sbMain.Size.Width > sbpPosition.Width + sbpStatus.MinWidth)
			{
				sbpStatus.Width = sbMain.Size.Width - sbpPosition.Width;
			}
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void sbMain_SizeChanged(object sender, System.EventArgs e)
		{
			ResetStatusBarSize();
		}

		private void fbMain_FloatItemClosing(System.Windows.Forms.Control floatControl, System.Drawing.Image floatIcon, string floatCaption, ref bool bCanceled)
		{
			if (floatControl.Equals(m_catalog))
			{
				this.Activate();
				bCanceled = true;
			}

			if (floatControl is StencilForm)
			{
				floatControl.Dispose();
			}
		}

		private void fbMain_FloatItemDocking(System.Windows.Forms.Control floatControl, System.Drawing.Image floatIcon, string floatCaption, ref bool bCanceled)
		{
			if (floatControl.Equals(m_catalog))
			{
				m_catalog.HideOnDeactivated = true;
			}

			dcMain.AddDockWindow(floatControl, floatIcon, floatCaption, 100);
			spMain.Enabled	= true;

			if (dcMain.Width == 0)
			{
				dcMain.Width = m_dockBarWidth;
			}

			if (fbMain.FloatWindowCount == 1)
			{
				fbMain.Width = 0;
			}
		}

		private void dcMain_DockItemFloating(System.Windows.Forms.Control dockControl, System.Drawing.Image dockIcon, string dockCaption, ref bool bCanceled)
		{
			bool hideCaption = true;

			if (dockControl.Equals(m_catalog))
			{
				m_catalog.HideOnDeactivated = false;
				hideCaption = false;
			}

			if (dockControl.Equals(m_recognition))
			{
				hideCaption = false;
			}

			fbMain.AddFloatWindow(dockControl, dockIcon, dockCaption, dcMain.Width, 100, hideCaption);

			if (fbMain.Width == 0)
			{
				fbMain.Width = m_floatBarWidth;
			}

			if (dcMain.DockWindowCount == 1)
			{
				m_dockBarWidth	= dcMain.Width;
				dcMain.Width	= 0;
				spMain.Enabled	= false;
			}
		}

		private void fbMain_FloatWindowHiding(System.Windows.Forms.Control floatControl)
		{
			if (floatControl.Equals(m_catalog))
			{
				m_catalog.HideFloatSelectionForm();
			}
		}

		private void dcMain_DockItemClosing(System.Windows.Forms.Control dockControl, System.Drawing.Image dockIcon, string dockCaption, ref bool bCanceled)
		{
			if (dockControl.Equals(m_catalog))
			{
				dcMain_DockItemFloating(dockControl, dockIcon, dockCaption, ref bCanceled);
			}

			if (dockControl.Equals(m_recognition))
			{
				dcMain_DockItemFloating(dockControl, dockIcon, dockCaption, ref bCanceled);
			}

			if (dockControl is StencilForm)
			{
				dockControl.Dispose();
			}

			if (dcMain.DockWindowCount == 1)
			{
				m_dockBarWidth	= dcMain.Width;
				dcMain.Width	= 0;
				spMain.Enabled	= false;
			}
		}

		private void m_catalog_StencilSelected(Stencil selectedStencil, string caption)
		{
			FloatControlLib.FloatItem	item;
			StencilForm					frmStencil;

			frmStencil			= new StencilForm();
			frmStencil.TopLevel = false;

			frmStencil.InitializeStencil(selectedStencil);
			frmStencil.TemplateSelected +=new TemplateSelectedEvent(frmStencil_TemplateSelected);

			item = fbMain.AddFloatWindow(frmStencil, imgsToolbox.Images[1], caption, 160, 100, true);

			if (fbMain.Width == 0)
			{
				fbMain.Width = m_floatBarWidth;
			}

			fbMain.AnimateShowFloatWindow(item);
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			DrawingForm	frmDrawing;

			frmDrawing				= new DrawingForm(Environment.CurrentDirectory + "\\Templates");
			frmDrawing.Owner		= this;
			frmDrawing.MdiParent	= this;
			frmDrawing.WindowState	= FormWindowState.Maximized;
			frmDrawing.Visible		= true;
			frmDrawing.status		= new UserStatus();
			frmDrawing.MouseMoveOnDrawingArea	+= new MouseMoveOnDrawingAreaEvent(frmDrawing_MouseMoveOnDrawingArea);
			frmDrawing.RecognitionResultChanged	+= new RecognitionResultChangedEvent(frmDrawing_RecognitionResultChanged);
			frmDrawing.RecognitionResultStart	+= new RecognitionResultEvent(frmDrawing_RecognitionResultStart);
			frmDrawing.RecognitionResultEnd		+= new RecognitionResultEvent(frmDrawing_RecognitionResultEnd);
			frmDrawing.DrawingModeChanged		+= new DrawingModeChangedEvent(frmDrawing_DrawingModeChanged);
		}

		private void frmDrawing_MouseMoveOnDrawingArea(int x, int y)
		{
			sbpPosition.Text = x.ToString() + " : " + y.ToString();
		}

		private void frmStencil_TemplateSelected(GOMLib.GOM_Template template)
		{
			DrawingForm	frmDrawing;

			if (this.ActiveMdiChild != null)
			{
				frmDrawing = (DrawingForm)this.ActiveMdiChild;

				if (frmDrawing.status.Action == UserActions.Sketching)
				{
					frmDrawing.FinishCurrentSketchObject();
				}

				frmDrawing.status.Action	= UserActions.InsertObject;
				frmDrawing.status.Template	= template;
			}
		}

		private void tbMain_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (e.Button.Equals(tbbNew))
			{
				menuItem2_Click(null, null);
			}
			if (e.Button.Equals(tbbOpen))
			{
				OnOpenDiagram();
			}
			if (e.Button.Equals(tbbSave))
			{
				OnSaveDiagram();
			}
			if (e.Button.Equals(tbbCut))
			{
				if (this.ActiveMdiChild != null)
				{
					DrawingForm	frmDrawing = (DrawingForm)this.ActiveMdiChild;

					frmDrawing.InvodeCommand(EditCommands.Cut);
				}
			}
			if (e.Button.Equals(tbbCopy))
			{
				if (this.ActiveMdiChild != null)
				{
					DrawingForm	frmDrawing = (DrawingForm)this.ActiveMdiChild;

					frmDrawing.InvodeCommand(EditCommands.Copy);
				}
			}
			if (e.Button.Equals(tbbPaste))
			{
				if (this.ActiveMdiChild != null)
				{
					DrawingForm	frmDrawing = (DrawingForm)this.ActiveMdiChild;

					frmDrawing.InvodeCommand(EditCommands.Paste);
				}
			}
			if (e.Button.Equals(tbbDelete))
			{
				if (this.ActiveMdiChild != null)
				{
					DrawingForm	frmDrawing = (DrawingForm)this.ActiveMdiChild;

					frmDrawing.InvodeCommand(EditCommands.Delete);
				}
			}
			if (e.Button.Equals(tbbUndo))
			{
				if (this.ActiveMdiChild != null)
				{
					DrawingForm	frmDrawing = (DrawingForm)this.ActiveMdiChild;

					frmDrawing.InvodeCommand(EditCommands.Undo);
				}
			}
			if (e.Button.Equals(tbbRedo))
			{
				if (this.ActiveMdiChild != null)
				{
					DrawingForm	frmDrawing = (DrawingForm)this.ActiveMdiChild;

					frmDrawing.InvodeCommand(EditCommands.Redo);
				}
			}
		}

		private void frmDrawing_RecognitionResultChanged(string resultXML)
		{
			UpdateRecognitionResult(resultXML);
		}

		private void frmDrawing_RecognitionResultStart()
		{
			((DrawingForm)this.ActiveMdiChild).ClearRecognitionResult();
			m_recognition.ClearRecognitionResult();
		}

		private void frmDrawing_RecognitionResultEnd()
		{
		}

		private void UpdateRecognitionResult(string resultXML)
		{
			System.Xml.XmlDocument	doc;
			System.Xml.XmlNode		resultNode;
			string					id;
			float					rotation;
			float					similarity;
			float					left, top, right, bottom;
			TemplatePack			template;

			m_recognition.BeginUpdateRecognitionResult();

			doc	= new System.Xml.XmlDocument();

			try
			{
				doc.LoadXml(resultXML);

				if (doc.DocumentElement.Name == "rec_result")
				{
					rotation= 0;
					similarity = 0;
					left	= 0;
					right	= 0;
					top		= 0;
					bottom	= 0;

					for (int i = 0; i < doc.DocumentElement.ChildNodes.Count; i++)
					{
						if (doc.DocumentElement.ChildNodes[i].Name == "result")
						{
							resultNode = doc.DocumentElement.ChildNodes[i];
							id = "";

							for (int j = 0; j < resultNode.ChildNodes.Count; j++)
							{
								if (resultNode.ChildNodes[j].Name == "id")
								{
									id = resultNode.ChildNodes[j].InnerText;
								}
								if (resultNode.ChildNodes[j].Name == "location")
								{
									for (int k = 0; k < resultNode.ChildNodes[j].ChildNodes.Count; k++)
									{
										if (resultNode.ChildNodes[j].ChildNodes[k].Name == "left")
										{
											left = float.Parse(resultNode.ChildNodes[j].ChildNodes[k].InnerText);
										}
										if (resultNode.ChildNodes[j].ChildNodes[k].Name == "top")
										{
											top = float.Parse(resultNode.ChildNodes[j].ChildNodes[k].InnerText);
										}
										if (resultNode.ChildNodes[j].ChildNodes[k].Name == "right")
										{
											right = float.Parse(resultNode.ChildNodes[j].ChildNodes[k].InnerText);
										}
										if (resultNode.ChildNodes[j].ChildNodes[k].Name == "bottom")
										{
											bottom = float.Parse(resultNode.ChildNodes[j].ChildNodes[k].InnerText);
										}
									}
								}
								if (resultNode.ChildNodes[j].Name == "orientation")
								{
									rotation = float.Parse(resultNode.ChildNodes[j].InnerText);
								}
								if (resultNode.ChildNodes[j].Name == "similarity")
								{
									similarity = float.Parse(resultNode.ChildNodes[j].InnerText);
								}
							}

							if (id.Length > 0)
							{
								template			= m_catalog.GetTemplateByID(id);

								if (template != null)
								{
									template.rect		= new RectangleF(Math.Min(left, right), Math.Min(top, bottom), Math.Abs(right - left), Math.Abs(bottom - top));
									template.rotation	= rotation;
									template.similarity = similarity;

									m_recognition.AddRecognitionResult(template);
									((DrawingForm)this.ActiveMdiChild).AddRecognitionResult(template);

									if (i > 10) break;

									if ( m_recognition.GetRecognitionCount() > 50 )
									{
										((DrawingForm)this.ActiveMdiChild).StopRecognition();
										break;
									}

								}
								else
								{
									System.Windows.Forms.MessageBox.Show("Unknown template id: " + id);
								}
							}
						}
					}
				}
			}
			catch
			{
				System.Windows.Forms.MessageBox.Show("Output from primitive recognition module is incorrect");
			}

			m_recognition.EndUpdateRecognitionResult();
			Application.DoEvents();
		}

		private void m_recognition_RecognitionResultSelected(TemplatePack template)
		{
			DrawingForm	frmDrawing;

			if (this.ActiveMdiChild != null)
			{
				frmDrawing = (DrawingForm)this.ActiveMdiChild;
				frmDrawing.ReplaceCurrentSketching(template);
			}
		}

		private void OnSaveDiagram()
		{
			DrawingForm	frmDrawing = (DrawingForm)this.ActiveMdiChild;
			try
			{
				frmDrawing.SaveDiagramToFile();
			}
			catch( Exception ex )
			{
				MessageBox.Show( this, ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		private void OnSaveDiagramAs(string type)
		{
			if ( saveFileDlg.ShowDialog(this) == DialogResult.OK )
			{
				DrawingForm	frmDrawing = (DrawingForm)this.ActiveMdiChild;
				try
				{
                    frmDrawing.SaveDiagramToFile(saveFileDlg.FileName, type);
				}
				catch( Exception ex )
				{
					MessageBox.Show( this, ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
		}

		private void OnOpenDiagram()
		{
			if ( openFileDlg.ShowDialog(this) == DialogResult.OK )
			{
				try
				{
					DrawingForm	frmDrawing = (DrawingForm)this.ActiveMdiChild;
					frmDrawing.LoadDiagramFromFile(openFileDlg.FileName);
					frmDrawing.Invalidate();
				}
				catch( Exception ex )
				{
					MessageBox.Show( this, ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			OnSaveDiagram();
		}


		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			OnOpenDiagram();
		}

		private void frmDrawing_DrawingModeChanged(DrawingMode drawingMode)
		{
			if ( drawingMode == DrawingMode.Editing )
			{
				sbpStatus.Text = "Editing";
			}
			else
			{
				sbpStatus.Text = "Sketching";
			}
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			
			if ( openFileDlg.ShowDialog(this) == DialogResult.OK )
			{
				try
				{
					readXml = new EEDomain.ReadFromXml(openFileDlg.FileName,false);
				}
				catch( Exception ex )
				{
					MessageBox.Show( this, ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
			
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			OutputForm form = new OutputForm();
			form.Text = "Generate";
			form.B3Action(readXml);
			form.Show();
		}

        //ADD 10/10/2009
		private void menuItem14_Click(object sender, System.EventArgs e)
		{
            DrawingForm frmDrawing = (DrawingForm)this.ActiveMdiChild;
			OutputForm form = new OutputForm();
            frmDrawing.DiagramConvertXml();
			form.Text = "Equation";
            if (readXml != null)
            {
                form.B1Action(readXml);
            }
            else 
            { 
                form.B1Action(frmDrawing.readXmls);
            }
			form.Show();
		}

		private void menuItem15_Click(object sender, System.EventArgs e)
		{
            DrawingForm frmDrawing = (DrawingForm)this.ActiveMdiChild;
            frmDrawing.DiagramConvertXml();
			OutputForm form = new OutputForm();
			form.Text = "Calculate";
            if (readXml != null)
            {
                form.B2Action(readXml);
            }
            else 
            { 
                form.B2Action(frmDrawing.readXmls);
            }
			form.Show();
		}

		public void menuItem13_Click(object sender, System.EventArgs e)
		{
			//InputForm inputForm = new InputForm(readXml);
            DrawingForm frmDrawing = (DrawingForm)this.ActiveMdiChild;
            InputForm inputForm = new InputForm(frmDrawing.readXmls);
			inputForm.Show();
		
		}

        private void menuItem19_Click(object sender, EventArgs e)
        {
            OnSaveDiagramAs("ee");
        }

        private void menuItem20_Click(object sender, EventArgs e)
        {
            OnSaveDiagramAs("others");
        }
        //end
	}

	public enum UserActions {Editing, Moving, Controlling, InsertObject, Selecting, Sketching, Linking, MovingKeyPoint}

	public class UserStatus
	{
		public UserStatus()
		{
			Action		= UserActions.Editing;
			Template	= null;
		}

		public UserActions			Action;
		public GOMLib.GOM_Template	Template;
	}
}
