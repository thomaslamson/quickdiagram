using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for DrawingForm.
	/// </summary>
	public class DrawingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel plDraw;
		private FloatControlLib.SmartTagControl tagColor;
		private FloatControlLib.SmartTagControl tagEdit;
		private FloatControlLib.SmartTagControl tagLink;
        private FloatControlLib.FishEye tagFishEye;
        private FloatControlLib.SmartTagControl tagInfo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        
		public DrawingForm(string templatePath)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			m_LButtonPressed	= false;
			m_CtrlPressed		= false;
			m_bRedoing			= false;

			m_selectingRect	= new System.Drawing.Rectangle(0, 0, 0, 0);
			m_selectedPen	= new Pen(System.Drawing.Color.FromArgb(150, 0, 0, 255), 1);
			m_groupSelPen	= new Pen(System.Drawing.Color.FromArgb(255, 200, 200, 200), 1);
			m_selectingPen	= new Pen(System.Drawing.Color.FromArgb(100, 0, 0, 0), 1);
			m_groupSelPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			m_selectingPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

			m_frmColor					= new ColorForm();
			m_frmColor.TopLevel			= false;
			m_frmColor.FormBorderStyle	= FormBorderStyle.None;
			m_frmColor.Visible			= false;
			m_frmColor.DrawingStyleChanged	+= new DrawingStyleChangedEvent(m_frmColor_DrawingStyleChanged);
			m_frmColor.FillingStyleChanged	+= new FillingStyleChangedEvent(m_frmColor_FillingStyleChanged);

			m_frmEdit					= new EditForm();
			m_frmEdit.TopLevel			= false;
			m_frmEdit.FormBorderStyle	= FormBorderStyle.None;
			m_frmEdit.Visible			= false;
			m_frmEdit.CommandSelected	+= new CommandSelectedEvent(m_frmEdit_CommandSelected);

			m_frmLink					= new LinkForm();
			m_frmLink.TopLevel			= false;
			m_frmLink.FormBorderStyle	= FormBorderStyle.None;
			m_frmLink.Visible			= false;
			m_frmLink.LinkStyleChanged	+= new LinkStyleChangedEvent(m_frmLink_LinkStyleChanged);
			m_frmLink.RemoveLink		+= new RemoveLinkEvent(m_frmLink_RemoveLink);

            //xxx new add
            m_frmInfo = new InfoForm();
            m_frmInfo.TopLevel = false;
            m_frmInfo.FormBorderStyle = FormBorderStyle.None;
            m_frmInfo.Visible = false;
            m_frmInfo.DrawingInfoChanged += new DrawingInfoChanged(m_frmInfo_DrawingInfoChanged);
            tagInfo.TagControl = m_frmInfo;
           //end 

			tagColor.TagControl	= m_frmColor;
			tagEdit.TagControl	= m_frmEdit;
			tagLink.TagControl	= m_frmLink;

			m_clipBoard	= new GOMLib.GOM_Objects();
			m_selObjs	= new GOMLib.GOM_Objects();
			m_rgObjects	= new GOMLib.GOM_Objects();
			m_bitmap	= new Bitmap(plDraw.Width, plDraw.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

			m_preprocess = new PreprocessQueue();
			m_preprocess.m_bDebugging	= false;
			m_preprocess.UpdateRequired += new PreprocessOverEvent(m_preprocess_UpdateRequired);
			m_preprocess.StartPreprocessThread();

			m_recognition = new RecognitionQueue(templatePath);
			m_recognition.m_bDebugging		= false;
			m_recognition.RecognitionStart	+= new RecognitionEvent(m_recognition_RecognitionStart);
			m_recognition.RecognitionEnd	+= new RecognitionEvent(m_recognition_RecognitionEnd);
			m_recognition.RecognitionOver	+= new RecognitionOverEvent(m_recognition_RecognitionOver);
			m_recognition.StartRecognitionThread();

			m_rgRedoList= new ArrayList();
			m_rgLinks	= new GOMLib.GOM_Links();
			m_rgStroke	= null;
			m_sketch	= null;

			m_selectedPoint	= null;
			m_selectedLink	= null;
			m_selectedLinkKeyPoint = null;

			m_tracker = new MouseTrackerLib.CTrackerClass();

			Graphics.FromImage(m_bitmap).FillRectangle(System.Drawing.Brushes.White, 0, 0, plDraw.Width, plDraw.Height);
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

				m_preprocess.StopPreprocessThread();
				m_recognition.StopRecognitionThread();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingForm));
            this.plDraw = new System.Windows.Forms.Panel();
            this.tagFishEye = new FloatControlLib.FishEye();
            this.tagLink = new FloatControlLib.SmartTagControl();
            this.tagEdit = new FloatControlLib.SmartTagControl();
            this.tagColor = new FloatControlLib.SmartTagControl();
            this.tagInfo = new FloatControlLib.SmartTagControl();
            this.plDraw.SuspendLayout();
            this.SuspendLayout();
            // 
            // plDraw
            // 
            this.plDraw.BackColor = System.Drawing.Color.White;
            this.plDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plDraw.Controls.Add(this.tagInfo);
            this.plDraw.Controls.Add(this.tagFishEye);
            this.plDraw.Controls.Add(this.tagLink);
            this.plDraw.Controls.Add(this.tagEdit);
            this.plDraw.Controls.Add(this.tagColor);
            this.plDraw.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plDraw.Location = new System.Drawing.Point(10, 9);
            this.plDraw.Name = "plDraw";
            this.plDraw.Size = new System.Drawing.Size(960, 646);
            this.plDraw.TabIndex = 0;
            this.plDraw.DoubleClick += new System.EventHandler(this.plDraw_DoubleClick);
            this.plDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.plDraw_Paint);
            this.plDraw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plDraw_MouseMove);
            this.plDraw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.plDraw_MouseDown);
            this.plDraw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.plDraw_MouseUp);
            // 
            // tagFishEye
            // 
            this.tagFishEye.Caption = "";
            this.tagFishEye.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagFishEye.ImageHeight = 33;
            this.tagFishEye.LabelHeight = 18;
            this.tagFishEye.LabelWidth = 64;
            this.tagFishEye.Location = new System.Drawing.Point(80, 8);
            this.tagFishEye.Name = "tagFishEye";
            this.tagFishEye.SelectedIndex = -1;
            this.tagFishEye.Size = new System.Drawing.Size(19, 19);
            this.tagFishEye.TabIndex = 3;
            this.tagFishEye.TagIcon = ((System.Drawing.Image)(resources.GetObject("tagFishEye.TagIcon")));
            this.tagFishEye.Visible = false;
            this.tagFishEye.Load += new System.EventHandler(this.tagFishEye_Load);
            this.tagFishEye.FishEyeItemSelected += new FloatControlLib.FishEyeItemSelectedEvent(this.tagFishEye_FishEyeItemSelected);
            // 
            // tagLink
            // 
            this.tagLink.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagLink.Location = new System.Drawing.Point(56, 8);
            this.tagLink.Name = "tagLink";
            this.tagLink.Size = new System.Drawing.Size(19, 19);
            this.tagLink.TabIndex = 2;
            this.tagLink.TagControl = null;
            this.tagLink.TagIcon = ((System.Drawing.Image)(resources.GetObject("tagLink.TagIcon")));
            this.tagLink.TagWindowHeight = 405;
            this.tagLink.TagWindowWidth = 240;
            this.tagLink.Visible = false;
            // 
            // tagEdit
            // 
            this.tagEdit.BackColor = System.Drawing.SystemColors.Control;
            this.tagEdit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagEdit.Location = new System.Drawing.Point(32, 8);
            this.tagEdit.Name = "tagEdit";
            this.tagEdit.Size = new System.Drawing.Size(19, 19);
            this.tagEdit.TabIndex = 1;
            this.tagEdit.TagControl = null;
            this.tagEdit.TagIcon = ((System.Drawing.Image)(resources.GetObject("tagEdit.TagIcon")));
            this.tagEdit.TagWindowHeight = 240;
            this.tagEdit.TagWindowWidth = 150;
            this.tagEdit.Visible = false;
            // 
            // tagColor
            // 
            this.tagColor.BackColor = System.Drawing.SystemColors.Control;
            this.tagColor.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagColor.Location = new System.Drawing.Point(8, 9);
            this.tagColor.Name = "tagColor";
            this.tagColor.Size = new System.Drawing.Size(19, 19);
            this.tagColor.TabIndex = 0;
            this.tagColor.TagControl = null;
            this.tagColor.TagIcon = ((System.Drawing.Image)(resources.GetObject("tagColor.TagIcon")));
            this.tagColor.TagWindowHeight = 400;
            this.tagColor.TagWindowWidth = 230;
            this.tagColor.Visible = false;
            // 
            // tagInfo
            // 
            this.tagInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tagInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagInfo.Location = new System.Drawing.Point(105, 9);
            this.tagInfo.Name = "tagInfo";
            this.tagInfo.Size = new System.Drawing.Size(19, 19);
            this.tagInfo.TabIndex = 7;
            this.tagInfo.TagControl = null;
            this.tagInfo.TagIcon = ((System.Drawing.Image)(resources.GetObject("tagInfo.TagIcon")));
            this.tagInfo.TagWindowHeight = 240;
            this.tagInfo.TagWindowWidth = 180;
            this.tagInfo.Visible = false;
            // 
            // DrawingForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(8, 8);
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(500, 167);
            this.Controls.Add(this.plDraw);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DrawingForm";
            this.Text = "DrawingForm";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DrawingForm_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DrawingForm_KeyDown);
            this.plDraw.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private int		m_orgX, m_orgY;
		private string	m_editingMode = "default";
		private string	m_recognitionResult = "";
		private bool	m_CtrlPressed;
		private bool	m_LButtonPressed;
		private bool	m_bRedoing;

		private System.Drawing.Bitmap		m_bitmap;
		private System.Drawing.Pen			m_selectedPen;
		private System.Drawing.Pen			m_selectingPen;
		private System.Drawing.Pen			m_groupSelPen;
		private System.Drawing.Rectangle	m_selectingRect;

		private ColorForm		m_frmColor;
		private EditForm		m_frmEdit;
		private LinkForm		m_frmLink;
        private InfoForm        m_frmInfo;

		private PreprocessQueue		m_preprocess;
		private RecognitionQueue	m_recognition;

		private GOMLib.GOM_Objects			m_clipBoard;
		private GOMLib.GOM_Objects			m_rgObjects;
		private GOMLib.GOM_Objects			m_selObjs;
		private GOMLib.GOM_Point			m_selectedPoint;
		private GOMLib.GOM_Link				m_selectedLink;
		private GOMLib.GOM_Point			m_selectedLinkKeyPoint;
		private GOMLib.GOM_Object_Sketch	m_sketch;

		private MouseTrackerLib.ITracker	m_tracker;

		private System.Collections.ArrayList	m_rgStroke;
		private GOMLib.GOM_Links				m_rgLinks;
		private System.Collections.ArrayList	m_rgRedoList;

		private string						m_currentFileName = "QuickDiagram.xml";

        //xxx new add
        public EEDomain.ReadFromXml readXmls;
        //end add

		public event MouseMoveOnDrawingAreaEvent	MouseMoveOnDrawingArea;
		public event RecognitionResultChangedEvent	RecognitionResultChanged;
		public event RecognitionResultEvent			RecognitionResultStart;
		public event RecognitionResultEvent			RecognitionResultEnd;
		public event DrawingModeChangedEvent		DrawingModeChanged;


		public UserStatus	status;
		public string		RecognitionResult
		{
			get
			{
				string	ret;

				//System.Threading.Monitor.Enter(m_recognitionResult);
				ret = m_recognitionResult;
				//System.Threading.Monitor.Exit(m_recognitionResult);

				return ret;
			}

			set
			{
				if ( m_recognitionResult != null )
				{
					//System.Threading.Monitor.Enter(m_recognitionResult);
					m_recognitionResult = value;
					//System.Threading.Monitor.Exit(m_recognitionResult);
				}
			}
		}

		private void plDraw_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (MouseMoveOnDrawingArea != null)
			{
				MouseMoveOnDrawingArea(e.X, e.Y);
			}

			switch (status.Action)
			{
				case UserActions.Editing:
				{
					System.Windows.Forms.Cursor	cursor;

					cursor = Cursors.Arrow;

					for (int i = 0; i < m_selObjs.Count; i++)
					{
						GOMLib.GOM_Point pt;

						pt = m_selObjs[i].GetMovablePointAt(e.X, e.Y);

						if (pt != null)
						{
							if (pt.Controllable)
							{
								cursor = Cursors.Cross;
							}
							else
							{
								if ((pt.id.Equals(GOMLib.GOM_Special_Point_Name.SE_RESIZING_POINT)) ||
									(pt.id.Equals(GOMLib.GOM_Special_Point_Name.NW_RESIZING_POINT)) ||
									(pt.id.Equals(GOMLib.GOM_Special_Point_Name.SW_RESIZING_POINT)) ||
									(pt.id.Equals(GOMLib.GOM_Special_Point_Name.NE_RESIZING_POINT)))
								{
									cursor = Cursors.SizeAll;
								}
								if (pt.id.Equals(GOMLib.GOM_Special_Point_Name.ROTATION_POINT))
								{
									cursor = Cursors.NoMove2D;
								}
							}
							break;
						}
					}

					for (int i = 0; i < m_rgObjects.Count; i++)
					{
						GOMLib.GOM_Point pt;

						pt = m_rgObjects[i].GetConnectablePointAt(e.X, e.Y);

						if (pt!= null)
						{
							cursor = Cursors.Hand;
							break;
						}
					}

					plDraw.Cursor = cursor;
					break;
				}
				case UserActions.InsertObject:
				{
					plDraw.Cursor = Cursors.Cross;
					break;
				}
				case UserActions.Sketching:
				{
					System.Windows.Forms.Cursor	cursor = Cursors.Arrow;

					UpdateSketchTrack();

					for (int i = 0; i < m_rgObjects.Count; i++)
					{
						GOMLib.GOM_Point pt;

						pt = m_rgObjects[i].GetConnectablePointAt(e.X, e.Y);

						if (pt!= null)
						{
							cursor = Cursors.Hand;
							break;
						}
					}

					for (int i = 0; i < m_rgLinks.Count; i++)
					{
						if ( m_rgLinks[i].IsPointOnLink(e.X, e.Y, m_rgObjects) )
						{
							cursor = Cursors.Hand;
							break;
						}
					}


					plDraw.Cursor = cursor;

					break;
				}
				case UserActions.Linking:
				{
					System.Windows.Forms.Cursor	cursor = Cursors.Arrow;
					GOMLib.GOM_Interface_Graphic_Object touchedObj = null;
					GOMLib.GOM_Point touchedPoint = null;

					for (int i = 0; i < m_rgObjects.Count; i++)
					{
						GOMLib.GOM_Point pt;

						pt = m_rgObjects[i].GetConnectablePointAt(e.X, e.Y);

						if (pt!= null)
						{
							cursor = Cursors.Hand;
							if ( m_selObjs[0]!=m_rgObjects[i] || m_selectedPoint!=pt )
							{
								touchedObj = m_rgObjects[i];
								touchedPoint = pt;
							}
							break;
						}
					}

					plDraw.Cursor = cursor;

					m_orgX = e.X;
					m_orgY = e.Y;

					DrawObjectsOnCanvas();

					if ( touchedObj != null )
					{
						GOMLib.GOM_Interface_Graphic_Object graphicObject = touchedObj;
						System.Drawing.RectangleF		rc;
						rc = graphicObject.BoundingBox;
						Graphics	canvas;
						canvas = plDraw.CreateGraphics();
						canvas.TranslateTransform(graphicObject.xOffset, graphicObject.yOffset);
						canvas.TranslateTransform((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
						canvas.RotateTransform(graphicObject.rotation);
						canvas.TranslateTransform(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
						System.Drawing.Pen TouchedBoxPen = new System.Drawing.Pen(System.Drawing.Color.Red, 2);

						canvas.DrawRectangle(TouchedBoxPen, touchedPoint.x - 2, touchedPoint.y - 2, 4, 4);

						canvas.Dispose();
					}

					break;
				}
				case UserActions.Controlling:
				{
					if ((m_selectedPoint != null) && (m_selObjs.Count == 1))
					{
						PointF	pt = new PointF(e.X, e.Y);

						pt = m_selObjs[0].PointToObject(pt);

						for (int i = 0; i < m_selectedPoint.Constraints.Count; i++)
						{
							if (((m_selectedPoint.Constraints[i].EditingMode == null) && m_editingMode.Equals("default")) ||
								(m_selectedPoint.Constraints[i].EditingMode.Equals(m_editingMode)))
							{
								m_selectedPoint.x = pt.X;
								m_selectedPoint.y = pt.Y;
								m_selectedPoint.Constraints[i].ApplyConstraints();

								DrawObjectsOnCanvas();
							}
						}

						m_selObjs[0].CalculateBoundingBox();
					}
					break;
				}
				case UserActions.Moving:
				{
					for (int i = 0; i < m_selObjs.Count; i++)
					{
						m_selObjs[i].xOffset += e.X - m_orgX;
						m_selObjs[i].yOffset += e.Y - m_orgY;
					}

					m_orgX = e.X;
					m_orgY = e.Y;

					DrawObjectsOnCanvas();

					if ( m_selObjs.Count == 1 )
					{
						GOMLib.GOM_Interface_Graphic_Object graphicObject = m_selObjs[0];
						System.Drawing.RectangleF		rc;
						rc = graphicObject.BoundingBox;
						Graphics	canvas;
						canvas = plDraw.CreateGraphics();
						canvas.TranslateTransform(graphicObject.xOffset, graphicObject.yOffset);
						canvas.TranslateTransform((rc.Left + rc.Right) / 2, (rc.Top + rc.Bottom) / 2);
						canvas.RotateTransform(graphicObject.rotation);
						canvas.TranslateTransform(-(rc.Left + rc.Right) / 2, -(rc.Top + rc.Bottom) / 2);
						System.Drawing.Pen TouchedBoxPen = new System.Drawing.Pen(System.Drawing.Color.Red, 2);

						for ( int i=0; i<m_rgObjects.Count; i++ )
						{
							GOMLib.GOM_Object_LinkNode linkNode = m_rgObjects[i] as GOMLib.GOM_Object_LinkNode;
							if ( !(graphicObject is GOMLib.GOM_Object_LinkNode) && linkNode != null )
							{
								GOMLib.GOM_Links links = GetLinks(linkNode);
								if ( links.Count == 1 )
								{
									GOMLib.GOM_Point touchedPoint = graphicObject.GetConnectablePointAt((int)linkNode.xOffset, (int)linkNode.yOffset);
									if ( touchedPoint != null )
									{
										canvas.DrawRectangle(TouchedBoxPen, touchedPoint.x - 2, touchedPoint.y - 2, 4, 4);
									}
								}
							}
						}

						canvas.Dispose();
					}

					break;
				}
				case UserActions.MovingKeyPoint:
				{
					m_selectedLinkKeyPoint.x = e.X;
					m_selectedLinkKeyPoint.y = e.Y;

					DrawObjectsOnCanvas();
					break;
				}
				case UserActions.Selecting:
				{
					m_selectingRect.X		= Math.Min(m_orgX, e.X);
					m_selectingRect.Y		= Math.Min(m_orgY, e.Y);
					m_selectingRect.Width	= Math.Abs(m_orgX - e.X);
					m_selectingRect.Height	= Math.Abs(m_orgY - e.Y);

					DrawObjectsOnCanvas();
					break;
				}
			}
		}

		public void ReplaceCurrentSketching(TemplatePack template)
		{
			GOMLib.GOM_Object_Primitive	primitive;

			if (status.Action == UserActions.Sketching)
			{
				m_sketch = null;

				primitive = new GOMLib.GOM_Object_Primitive();
				primitive.InitializeFromTemplate(template.template);

				primitive.xOffset	= template.rect.Left;
				primitive.yOffset	= template.rect.Top;

				GOMLib.GOM_Default_Values.ScaleObject(primitive, 25, 25);
//				GOMLib.GOM_Default_Values.ScaleObject(primitive, template.rect.Width, template.rect.Height);
/*
				double rotation = (template.rotation / System.Math.PI) * 180;

				if (rotation < 0)
				{
					rotation += 360;
				}
				rotation = (float)System.Math.Round(rotation / 45) * 45;

				primitive.rotation	= (float)rotation;
*/
				m_rgObjects.Add(primitive);
                //save to somewhere
                //end

				m_sketch = new GOMLib.GOM_Object_Sketch();
				tagFishEye.ClearFishEyeItems();

				DrawObjectsOnCanvas();
			}
		}

		private System.Collections.ArrayList DecodePointTrackFromXML(string xmlTrack)
		{
			System.Xml.XmlDocument			doc;
			System.Collections.ArrayList	rgStroke;
			GOMLib.SketchPoint				pt;

			rgStroke = new ArrayList();

			try
			{
				doc = new System.Xml.XmlDocument();
				doc.LoadXml(xmlTrack);

				if (System.String.Compare(doc.DocumentElement.Name, "input", true) == 0)
				{
					for (int i = 0; i < doc.DocumentElement.ChildNodes.Count; i++)
					{
						pt = new GOMLib.SketchPoint();
						pt.x = int.Parse(doc.DocumentElement.ChildNodes[i].Attributes["x"].Value);
						pt.y = int.Parse(doc.DocumentElement.ChildNodes[i].Attributes["y"].Value);
						pt.time = int.Parse(doc.DocumentElement.ChildNodes[i].Attributes["t"].Value);
						rgStroke.Add(pt);
					}
				}
			}
			catch
			{
				System.Windows.Forms.MessageBox.Show("Invalid format in mouse tracking.");
			}

			return rgStroke;
		}


		/// <summary>
		/// 1. Draw a stroke
		/// 2. Whether 2 end-points of the stroke are on the objects' connectable points?
		///   a. Yes: The stroke is a link connecting with two objects.
		///   b. No:  Whether there is 1 end-point of the stroke is on the object's connectable point?
		///     (1) Yes: The stroke is a link whose one end-ponit is connecting an object.
		///              Is another end-point on a link's empty connectable point?
		///              (a) Yes: Merge to links into one link.
		///              (b) No:  The other end-point of the link is empty.
		///     (2) No:  Whether there is 1 end-point on the existing link?
		///		         (a) Yes: The stroke is a link.
		///		         (b) No:  Add the stroke into the current sketch object.
		/// </summary>
		public void FinishCurrentSketchStroke()
		{
			if (m_rgStroke != null)
			{
				System.Collections.ArrayList	rgStroke;

				m_tracker.StopTracker();

				rgStroke	= CalculateSegments(m_rgStroke);
				m_rgStroke	= null;

				// Prepare key points of link
				GOMLib.GOM_Points keyPoints = new GOMLib.GOM_Points();
				for( int i=1; i<(rgStroke.Count-1); i++)
				{
					GOMLib.GOM_Point keyPt = new GOMLib.GOM_Point();
					keyPt.x = ((GOMLib.SketchPoint)rgStroke[i]).x;
					keyPt.y = ((GOMLib.SketchPoint)rgStroke[i]).y;
					keyPoints.Add( keyPt );
				}

				if (rgStroke.Count >= 2)
				{
					GOMLib.SketchPoint	startPt, endPt;
					GOMLib.GOM_Point	pt, pt1, pt2;
					GOMLib.GOM_Interface_Graphic_Object	obj1, obj2;

					startPt	= (GOMLib.SketchPoint)rgStroke[0];
					endPt	= (GOMLib.SketchPoint)rgStroke[rgStroke.Count - 1];
					pt1		= null;
					pt2		= null;
					obj1	= null;
					obj2	= null;

					for (int i = 0; i < m_rgObjects.Count; i++)
					{
						if (pt1 == null)
						{
							pt = m_rgObjects[i].GetConnectablePointAt(startPt.x, startPt.y);

							if (pt != null)
							{
								pt1		= pt;
								obj1	= m_rgObjects[i];
							}
						}
						if (pt2 == null)
						{
							pt = m_rgObjects[i].GetConnectablePointAt(endPt.x, endPt.y);

							if (pt != null)
							{
								pt2		= pt;
								obj2	= m_rgObjects[i];
							}
						}
					}

					// 2. Whether 2 end-points of the stroke are on the objects' connectable points?
					if ((pt1 != null) && (pt2 != null) && (pt1 != pt2))
					{
						//   a. Yes: The stroke is a link connecting with two objects.
						GOMLib.GOM_Link				link;

						link = GOMLib.GOM_Default_Values.CreateLink(obj1, pt1, obj2, pt2, keyPoints);
						AddLinkWithMergemence(link);
						DrawObjectsOnCanvas();
						return;
					}
					else
					{
						//   b. No:  Whether there is 1 end-point of the stroke is on the object's connectable point?
						if ( pt1 != null || pt2 != null )
						{
							//     (1) Yes: The stroke is a link whose one end-ponit is connecting an object.
							//              Is another end-point on a link's empty connectable point?
							GOMLib.GOM_Object_LinkNode linkNode = null;
							if ( pt1 == null )
							{
								linkNode = GetLinkNode(startPt.x, startPt.y);
							}
							if ( pt2 == null )
							{
								linkNode = GetLinkNode(endPt.x, endPt.y);
							}

							if ( linkNode != null )
							{
								//              (a) Yes: Merge to links into one link.
								GOMLib.GOM_Link link = null;
								if ( pt1 == null )
								{
									link = GOMLib.GOM_Default_Values.CreateLink(linkNode, linkNode.LinkPoint, obj2, pt2, keyPoints);
								}
								else
								{
									link = GOMLib.GOM_Default_Values.CreateLink(obj1, pt1, linkNode, linkNode.LinkPoint, keyPoints);
								}
								AddLinkWithMergemence(link);
								DrawObjectsOnCanvas();
								return;
							}
							else
							{
								//              (b) No:  The other end-point of the link is empty.
								GOMLib.GOM_Link				link = null;
								GOMLib.GOM_Object_LinkNode	newlinkNode = null;
								if ( pt1 == null )
								{
									newlinkNode = new GOMLib.GOM_Object_LinkNode(startPt.x, startPt.y);
									link = GOMLib.GOM_Default_Values.CreateLink( newlinkNode, newlinkNode.LinkPoint, obj2, pt2, keyPoints);
								}
								else
								{
									newlinkNode = new GOMLib.GOM_Object_LinkNode(endPt.x, endPt.y);
									link = GOMLib.GOM_Default_Values.CreateLink( obj1, pt1, newlinkNode, newlinkNode.LinkPoint, keyPoints);
								}
								m_rgObjects.Add(newlinkNode);
								AddLinkWithMergemence(link);
								DrawObjectsOnCanvas();
								return;
							}

						}
						else
						{
							//     (2) No:  Whether there is 1 end-point on the existing link?
							if ( GetLinkByPoint(startPt.x,startPt.y)!=null
								|| GetLinkByPoint(endPt.x,endPt.y)!=null )
							{
								//		         (a) Yes: The stroke is a link.
								GOMLib.GOM_Link				link = null;
								GOMLib.GOM_Object_LinkNode	startLinkNode = null;
								GOMLib.GOM_Object_LinkNode	endLinkNode = null;
								startLinkNode = new GOMLib.GOM_Object_LinkNode(startPt.x, startPt.y);
								endLinkNode = new GOMLib.GOM_Object_LinkNode(endPt.x, endPt.y);
								link = GOMLib.GOM_Default_Values.CreateLink(startLinkNode, startLinkNode.LinkPoint, endLinkNode, endLinkNode.LinkPoint, keyPoints);
								m_rgObjects.Add(startLinkNode);
								m_rgObjects.Add(endLinkNode);
								AddLinkWithMergemence(link);
								DrawObjectsOnCanvas();
								return;
							}
							else
							{
								//		         (b) No:  Add the stroke into the current sketch object.
								// Do nothing.
							}
						}
					}
				}

				rgStroke = DecodePointTrackFromXML(m_tracker.GetTrack_No_Collinear(plDraw.Handle.ToInt64(), 0.5));

				if ((m_sketch != null) && (rgStroke.Count > 1))
				{
					m_sketch.rgSketchSet.Add(rgStroke);
					m_preprocess.PushSketchAndWait(m_sketch, rgStroke);

					DrawObjectsOnCanvas();
				}
			}
		}

		/// <summary>
		/// Gets LinkNode by the given point.
		/// </summary>
		/// <param name="x">X coordinate of the given point.</param>
		/// <param name="y">Y coordinate of the given point.</param>
		/// <returns>The LinkNode. If there is no LinkNode under the given point, return null.</returns>
		public GOMLib.GOM_Object_LinkNode GetLinkNode( int x, int y )
		{
			for ( int i=0; i<m_rgObjects.Count; i++ )
			{
				if ( m_rgObjects[i] is GOMLib.GOM_Object_LinkNode )
				{
					GOMLib.GOM_Object_LinkNode linkNode = (GOMLib.GOM_Object_LinkNode)m_rgObjects[i];
					if ( linkNode.GetConnectablePointAt(x, y) != null )
					{
						return linkNode;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Gets LinkNode by the given point.
		/// </summary>
		/// <param name="point">The given point.</param>
		/// <returns>The LinkNode. If there is no LinkNode under the given point, return null.</returns>
		public GOMLib.GOM_Object_LinkNode GetLinkNode(GOMLib.GOM_Point point)
		{
			for( int i=0; i<m_rgObjects.Count; i++ )
			{
				GOMLib.GOM_Object_LinkNode linkNode = m_rgObjects[i] as GOMLib.GOM_Object_LinkNode;
				if ( linkNode != null )
				{
					if ( linkNode.LinkPoint == point )
					{
						return linkNode;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the links connecting to the given LinkNode.
		/// </summary>
		/// <param name="linkNode">The given LinkNode.</param>
		/// <returns>The links collection which connects to the given LinkNode.</returns>
		public GOMLib.GOM_Links GetLinks(GOMLib.GOM_Object_LinkNode linkNode)
		{
			GOMLib.GOM_Links links = new GOMLib.GOM_Links();
			for( int i=0; i<m_rgLinks.Count; i++ )
			{
				if ( m_rgLinks[i].m_startPt == linkNode.LinkPoint || m_rgLinks[i].m_endPt == linkNode.LinkPoint )
				{
					links.Add(m_rgLinks[i]);
				}
			}
			return links;
		}

		/// <summary>
		/// Merge two links into one.
		/// </summary>
		/// <param name="link1">The first link.</param>
		/// <param name="link2">The second link.</param>
		/// <param name="linkNode">The LinkNode between these two given links.</param>
		/// <returns>The new link.</returns>
		public GOMLib.GOM_Link MergeLinks(GOMLib.GOM_Link link1, GOMLib.GOM_Link link2, GOMLib.GOM_Object_LinkNode linkNode)
		{
			GOMLib.GOM_Link				link;
			if ( link1.m_startPt == link2.m_endPt )
			{
				link = GOMLib.GOM_Default_Values.CreateLink(link2.m_endObj, link2.m_endPt, link1.m_startObj, link1.m_startPt);
			}
			else if ( link1.m_endPt == link2.m_startPt )
			{
				link = GOMLib.GOM_Default_Values.CreateLink(link1.m_startObj, link1.m_startPt, link2.m_endObj, link2.m_endPt);
			}
			else
			{
				GOMLib.GOM_Point pt1 = (link1.m_startPt==linkNode.LinkPoint) ? link1.m_endPt : link1.m_startPt;
				GOMLib.GOM_Point pt2 = (link2.m_startPt==linkNode.LinkPoint) ? link2.m_endPt : link2.m_startPt;
				GOMLib.GOM_Interface_Graphic_Object obj1 = (link1.m_startPt==linkNode.LinkPoint) ? link1.m_endObj : link1.m_startObj;
				GOMLib.GOM_Interface_Graphic_Object obj2 = (link2.m_startPt==linkNode.LinkPoint) ? link2.m_endObj : link2.m_startObj;
				link = GOMLib.GOM_Default_Values.CreateLink(obj1, pt1, obj2, pt2);
			}

			m_rgLinks.Remove(link1);
			m_rgLinks.Remove(link2);
			m_rgObjects.Remove(linkNode);
			m_rgLinks.Add(link);

			return link;
		}

		/// <summary>
		/// Add a link. When the link needs to be merged, the mergemence occurs.
		/// </summary>
		/// <param name="link"></param>
		public void AddLinkWithMergemence(GOMLib.GOM_Link link)
		{
			GOMLib.GOM_Object_LinkNode startLinkNode	= GetLinkNode(link.m_startPt);
			GOMLib.GOM_Object_LinkNode endLinkNode		= GetLinkNode(link.m_endPt);

			if ( startLinkNode == null && endLinkNode == null )
			{
				m_rgLinks.Add(link);
				return;
			}

			bool needAddLink = false;
			bool merged = false;
			if ( startLinkNode != null )
			{
				GOMLib.GOM_Links startLinks = GetLinks(startLinkNode);
				if ( startLinks.Count == 0 )
				{
					GOMLib.GOM_Link splittedLink = GetLinkByPoint(link.m_startPt.x, link.m_startPt.y);
					if ( splittedLink !=null )
					{
						SplitLink(splittedLink, startLinkNode);
					}
					needAddLink = true;
				}
				else if ( startLinks.Count == 1 )
				{
					MergeLinks(startLinks[0], link, startLinkNode);
					merged = true;
				}
				else
				{
					needAddLink = true;
				}
			}
			if ( endLinkNode != null && !merged )
			{
				GOMLib.GOM_Links endLinks = GetLinks(endLinkNode);
				if ( endLinks.Count == 0 )
				{
					GOMLib.GOM_Link splittedLink = GetLinkByPoint(link.m_endPt.x, link.m_endPt.y);
					if ( splittedLink !=null )
					{
						SplitLink(splittedLink, endLinkNode);
					}
					needAddLink = true;
				}
				else if ( endLinks.Count == 1 )
				{
					MergeLinks(endLinks[0], link, endLinkNode);
				}
				else
				{
					needAddLink = true;
				}
			}

			if ( needAddLink )
			{
				m_rgLinks.Add(link);
			}
		}

		/// <summary>
		/// Gets a link by the given point.
		/// </summary>
		/// <param name="x">X coordinate of the given point.</param>
		/// <param name="y">Y coordinate of the given point.</param>
		/// <returns>The link.</returns>
		public GOMLib.GOM_Link GetLinkByPoint(float x, float y)
		{
			for( int i=0; i<m_rgLinks.Count; i++ )
			{
				if ( m_rgLinks[i].IsPointOnLink(x, y, m_rgObjects) )
				{
					return m_rgLinks[i];
				}
			}
			return null;
		}

		/// <summary>
		/// Split the link into two links by a LinkNode.
		/// </summary>
		/// <param name="splittedLink">The link which will be splitted.</param>
		/// <param name="splitNode">The LinkNode.</param>
		public void SplitLink(GOMLib.GOM_Link splittedLink, GOMLib.GOM_Object_LinkNode splitNode)
		{
			GOMLib.GOM_Link firstPart = GOMLib.GOM_Default_Values.CreateLink(splittedLink.m_startObj, splittedLink.m_startPt, splitNode, splitNode.LinkPoint);
			GOMLib.GOM_Link secondPart = GOMLib.GOM_Default_Values.CreateLink(splitNode, splitNode.LinkPoint, splittedLink.m_endObj, splittedLink.m_endPt);

			m_rgLinks.Remove(splittedLink);
			m_rgLinks.Add(firstPart);
			m_rgLinks.Add(secondPart);
		}

		public void FinishCurrentSketchObject()
		{
			m_recognition.StopRecognize();

			status.Action = UserActions.Editing;
			if ( DrawingModeChanged != null )
			{
				DrawingModeChanged(DrawingMode.Editing);
			}

			FinishCurrentSketchStroke();

			if (m_sketch != null)
			{
				if (m_sketch.rgSketchSet.Count > 0)
				{
					m_selObjs.Clear();
					m_selObjs.Add(m_sketch);
					m_rgObjects.Add(m_sketch);
					m_sketch.CalculateBoundingBox();
				}
				m_sketch = null;
			}		
		}

		public void ClearRecognitionResult()
		{
			tagFishEye.ClearFishEyeItems();
		}

		public void AddRecognitionResult(TemplatePack template)
		{
			tagFishEye.AddFishEyeItem(template.template.id, template.bitmap, template);
			DrawObjectsOnCanvas();
		}

		private void plDraw_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			GOMLib.GOM_Interface_Graphic_Object	selectedObj;

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				m_LButtonPressed = true;
			}
			
			switch (e.Button)
			{
				case System.Windows.Forms.MouseButtons.Right:
				{
					if ( m_selectedLink!= null && m_selectedLink.IsPointOnLink(e.X, e.Y, m_rgObjects) )
					{
						m_selectedLink.AddKeyPoint(e.X, e.Y, m_rgObjects);
						DrawObjectsOnCanvas();
					}
					else
					{
						if (status.Action == UserActions.Sketching)
						{
							if (!m_LButtonPressed)
							{
								FinishCurrentSketchObject();
							}
						}
						else
						{
							ClearRecognitionResult();
							m_sketch = new GOMLib.GOM_Object_Sketch();
							status.Action = UserActions.Sketching;
							if ( DrawingModeChanged != null )
							{
								DrawingModeChanged(DrawingMode.Sketching);
							}

						}

						DrawObjectsOnCanvas();
					}
					break;
				}
				case System.Windows.Forms.MouseButtons.Left:
				{
					switch (status.Action)
					{
						case UserActions.Sketching:
						{
//							FinishCurrentSketchStroke();

							ClearRecognitionResult();
							DrawObjectsOnCanvas();

							m_orgX		= -1;
							m_orgY		= -1;
							m_rgStroke	= new ArrayList();
							m_tracker.StartTracker(true);

							break;
						}
						case UserActions.Editing:
						{
							GOMLib.GOM_Link	link;

							plDraw.Capture	= true;

							selectedObj				= null;
							m_orgX					= e.X;
							m_orgY					= e.Y;
							m_selectedLinkKeyPoint	= null;

							//Ckeck for link key point at the mouse position
							if ( m_selectedLink != null && m_selObjs.Count<=0 )
							{
								for( int i=0; i<m_selectedLink.m_keyPts.Count; i++)
								{
									if ( GOMLib.GOM_Default_Values.IsMouseOnPoint(m_selectedLink.m_keyPts[i].x, m_selectedLink.m_keyPts[i].y, e.X, e.Y ) )
									{
										m_selectedLinkKeyPoint = m_selectedLink.m_keyPts[i];
										status.Action = UserActions.MovingKeyPoint;
										if ( DrawingModeChanged != null )
										{
											DrawingModeChanged(DrawingMode.Editing);
										}

										return;
									}
								}
							}

							//Check for object at the mouse position
							for (int i = m_rgObjects.Count - 1; i >= 0; i--)
							{
								if (m_rgObjects[i].IsPointInObject(e.X, e.Y))
								{
									selectedObj = m_rgObjects[i];

									if (!m_selObjs.Contains(selectedObj))
									{
										if (!m_CtrlPressed)
										{
											m_selObjs.Clear();
										}
										m_selObjs.Add(selectedObj);
									}
									status.Action = UserActions.Moving;
									if ( DrawingModeChanged != null )
									{
										DrawingModeChanged(DrawingMode.Editing);
									}
									DrawObjectsOnCanvas();

									return;
								}
							}

							m_selectedLink = null;
							for (int i = 0; i < m_rgLinks.Count; i++)
							{
								link = (GOMLib.GOM_Link)m_rgLinks[i];

								if (link.IsPointOnLink(e.X, e.Y, m_rgObjects))
								{
									m_selObjs.Clear();
									m_selectedLink = link;
									DrawObjectsOnCanvas();

									return;
								}
							}
							//Check for controllable points
							for (int i = 0; i < m_selObjs.Count; i++)
							{
								m_selectedPoint = m_selObjs[i].GetMovablePointAt(e.X, e.Y);

								if (m_selectedPoint != null)
								{
									status.Action	= UserActions.Controlling;
									selectedObj		= m_selObjs[i];

									m_selObjs.Clear();
									m_selObjs.Add(selectedObj);
									DrawObjectsOnCanvas();

									return;
								}
							}
							//Check for connectable points
							for (int i = 0; i < m_rgObjects.Count; i++)
							{
								m_selectedPoint = m_rgObjects[i].GetConnectablePointAt(e.X, e.Y);

								if (m_selectedPoint != null)
								{
									status.Action	= UserActions.Linking;
									selectedObj		= m_rgObjects[i];

									m_selObjs.Clear();
									m_selObjs.Add(selectedObj);
									DrawObjectsOnCanvas();

									return;
								}
							}
							//A object is selected. Update the selection list
							if (selectedObj != null)
							{
								if (!m_selObjs.Contains(selectedObj))
								{
									if (!m_CtrlPressed)
									{
										m_selObjs.Clear();
									}
									m_selObjs.Add(selectedObj);
								}
								status.Action = UserActions.Moving;
								if ( DrawingModeChanged != null )
								{
									DrawingModeChanged(DrawingMode.Editing);
								}

							}
							else
							{
								if (!m_CtrlPressed)
								{
									m_selObjs.Clear();
								}

								m_orgX = e.X;
								m_orgY = e.Y;

								status.Action = UserActions.Selecting;
								if ( DrawingModeChanged != null )
								{
									DrawingModeChanged(DrawingMode.Editing);
								}

							}
							DrawObjectsOnCanvas();
							break;
						}
						case UserActions.InsertObject:
						{
							break;
						}
					}
					break;
				}
			}
		}

		private void DrawObjectsOnCanvas()
		{
			Graphics	canvas;
			Region		rgnSel;
			bool		bColorTagVisible	= false;
			bool		bEditTagVisible		= false;
			int			tagX = 0;
			int			tagY = 0;

			canvas = Graphics.FromImage(m_bitmap);
			canvas.FillRectangle(System.Drawing.Brushes.White, 0, 0, m_bitmap.Width, m_bitmap.Height);

			rgnSel = null;
            DiagramConvertXml();
			for (int i = 0; i < m_rgObjects.Count; i++)
			{
				if (m_selObjs.Contains(m_rgObjects[i]) && (status.Action != UserActions.Sketching))
				{
					m_rgObjects[i].DrawSelected(canvas, m_selectedPen, Pens.Blue, Pens.Green, Pens.Red);
					if (rgnSel == null)
					{
						rgnSel = m_rgObjects[i].BoundingRegion;
					}
					else
					{
						rgnSel.Union(m_rgObjects[i].BoundingRegion);
					}
				}
				else
				{
					m_rgObjects[i].Draw(canvas, true);
				}
			}

            //new add week10
            if (readXmls != null)
            {
                readXmls.Show_info(canvas, m_rgObjects);
            }
            //end

			for (int i = 0; i < m_rgLinks.Count; i++)
			{
				if ( m_rgLinks[i]==m_selectedLink && m_selObjs.Count==0 )
				{
					m_rgLinks[i].DrawSelected(canvas, m_rgObjects);
				}
				else
				{
					m_rgLinks[i].Draw(canvas, m_rgObjects);
				}
			}

			if (m_selectedLink != null)
			{
				PointF	pt1, pt2;

				pt1 = m_selectedLink.StartPointInCanvas(m_rgObjects);
				pt2 = m_selectedLink.EndPointInCanvas(m_rgObjects);

				tagLink.Left	= (int)((pt1.X + pt2.X) / 2);
				tagLink.Top		= (int)((pt1.Y + pt2.Y) / 2);
				tagLink.Visible = true;

				m_frmLink.SelectedLink = m_selectedLink;
             
/*
				PointF	pt;

				pt = m_selectedLink.m_endObj.PointToCanvas(new PointF(m_selectedLink.m_endPt.x, m_selectedLink.m_endPt.y));

				tagLink.Left	= (int)pt.X;
				tagLink.Top		= (int)pt.Y;
				tagLink.Visible = true;
*/				
			}
			else
			{
				tagLink.Visible = false;
			}

			if (status.Action != UserActions.Sketching)
			{
				tagFishEye.Visible = false;

				if (m_selObjs.Count == 1)
				{
					PointF		pt;
					RectangleF	rc;
					GOMLib.GOM_Interface_Graphic_Object obj;	

					obj = m_selObjs[0];

					rc = obj.BoundingBox;

					pt		= obj.PointToCanvas(new PointF(rc.Left, rc.Top));
					tagX	= (int)pt.X;
					tagY	= (int)pt.Y;

					pt		= obj.PointToCanvas(new PointF(rc.Right, rc.Bottom));
					if (((int)pt.Y < tagY) || (((int)pt.Y == tagY) && ((int)pt.X > tagX)))
					{
						tagX	= (int)pt.X;
						tagY	= (int)pt.Y;
					}

					pt		= obj.PointToCanvas(new PointF(rc.Right, rc.Top));
					if (((int)pt.Y < tagY) || (((int)pt.Y == tagY) && ((int)pt.X > tagX)))
					{
						tagX	= (int)pt.X;
						tagY	= (int)pt.Y;
					}

					pt		= obj.PointToCanvas(new PointF(rc.Left, rc.Bottom));
					if (((int)pt.Y < tagY) || (((int)pt.Y == tagY) && ((int)pt.X > tagX)))
					{
						tagX	= (int)pt.X;
						tagY	= (int)pt.Y;
					}

					tagX +=10;
					tagY -=10;

					bEditTagVisible = true;

					if (obj is GOMLib.GOM_Object_Primitive)
					{
						bColorTagVisible			= true;
						m_frmColor.SelectedObject	= (GOMLib.GOM_Object_Primitive)obj;
                        //new add at 10/10/2009
                        m_frmInfo.setXmls(readXmls);
                        m_frmInfo.SelectedObject = (GOMLib.GOM_Object_Primitive)obj;
                        //end
					}
				}
				else if (m_selObjs.Count > 1)
				{
					System.Drawing.Rectangle	rc;

					rc = Rectangle.Round(rgnSel.GetBounds(canvas));

					rc.X		-= 6;
					rc.Y		-= 6;
					rc.Width	+= 12;
					rc.Height	+= 12;

					tagX = rc.Right;
					tagY = rc.Top;

					bEditTagVisible = true;

					canvas.DrawRectangle(m_groupSelPen, rc);
				}
			}
			else
			{
				if (m_sketch != null)
				{
					m_sketch.CalculateBoundingBox();
					m_sketch.Draw(canvas, true);

					if ((!m_LButtonPressed) && (tagFishEye.ItemCount != 0))
					{
						tagFishEye.Left		= (int)m_sketch.BoundingBox.Right;
						tagFishEye.Top		= (int)m_sketch.BoundingBox.Top;
						tagFishEye.Visible	= true;
					}
					else
					{
						tagFishEye.Visible	= false;
					}
				}

				DrawCurrentStroke(canvas);
			}

			if (bColorTagVisible)
			{
				tagColor.Left		= tagX;
				tagColor.Top		= tagY;

				tagX += 19;
			}
			tagColor.Visible = bColorTagVisible;

			if (bEditTagVisible)
			{
				tagEdit.Left	= tagX;
				tagEdit.Top		= tagY;


                tagX += 19;
			}
			tagEdit.Visible = bEditTagVisible;

            //new add at 4/10/2009
            if (bColorTagVisible)
            {
                tagInfo.Left = tagX;
                tagInfo.Top = tagY;
            }
            tagInfo.Visible = bColorTagVisible;
            //end  

			canvas.DrawRectangle(m_selectingPen, m_selectingRect);

			if (status.Action == UserActions.Linking)
			{
				if ((m_selectedPoint != null) && (m_selObjs.Count == 1))
				{
					PointF		pt;
					GOMLib.GOM_Interface_Graphic_Object	obj;

					obj = m_selObjs[0];
					pt = obj.PointToCanvas(new PointF(m_selectedPoint.x, m_selectedPoint.y));

					canvas.DrawLine(m_selectingPen, pt.X, pt.Y, m_orgX, m_orgY);
				}
			}

			plDraw.CreateGraphics().DrawImage(m_bitmap, 0, 0);
		}

		private void plDraw_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawImage(m_bitmap, 0, 0);
		}

		private void plDraw_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				m_LButtonPressed = false;
			}
			
			switch (e.Button)
			{
				case System.Windows.Forms.MouseButtons.Left:
				{
					switch (status.Action)
					{
						case UserActions.Linking:
						{
							for (int i = 0; i < m_rgObjects.Count; i++)
							{
								GOMLib.GOM_Point pt;

								pt = m_rgObjects[i].GetConnectablePointAt(e.X, e.Y);

								if (pt!= null)
								{

									if ( m_selObjs[0]!=m_rgObjects[i] || m_selectedPoint!=pt )
									{
										GOMLib.GOM_Link				link;

										link = GOMLib.GOM_Default_Values.CreateLink( m_selObjs[0], m_selectedPoint, m_rgObjects[i], pt);
										AddLinkWithMergemence(link);
										m_selObjs.Clear();
										m_selectedLink = link;
										break;
									}

								}
							}

							status.Action	= UserActions.Editing;
							m_selectedPoint = null;
							DrawObjectsOnCanvas();

							break;
						}
						case UserActions.Sketching:
						{
							FinishCurrentSketchStroke();
							break;
						}
						case UserActions.Editing:
						{
							break;
						}
						case UserActions.InsertObject:
						{
							GOMLib.GOM_Object_Primitive	primitive;

							primitive = new GOMLib.GOM_Object_Primitive();
							primitive.InitializeFromTemplate(status.Template);
							primitive.xOffset = e.X;
							primitive.yOffset = e.Y;
							GOMLib.GOM_Default_Values.ScaleObject(primitive, 25, 25);
							m_rgObjects.Add(primitive);

							status.Action	= UserActions.Editing;
							status.Template	= null;

							m_selObjs.Clear();
							m_selObjs.Add(primitive);

							DrawObjectsOnCanvas();
							break;
						}
						case UserActions.Controlling:
						{
							plDraw.Capture	= false;

							status.Action	= UserActions.Editing;
							m_selectedPoint	= null;
							break;
						}
						case UserActions.Moving:
						{
							plDraw.Capture	= false;

							status.Action	= UserActions.Editing;

							if ( m_selObjs.Count == 1 )
							{
								GOMLib.GOM_Interface_Graphic_Object graphicObject = m_selObjs[0];

								bool touched = false;
								for ( int i=0; i<m_rgObjects.Count; i++ )
								{
									GOMLib.GOM_Object_LinkNode linkNode = m_rgObjects[i] as GOMLib.GOM_Object_LinkNode;
									if ( !(graphicObject is GOMLib.GOM_Object_LinkNode) && linkNode != null )
									{
										GOMLib.GOM_Links links = GetLinks(linkNode);
										if ( links.Count == 1 )
										{
											GOMLib.GOM_Point touchedPoint = graphicObject.GetConnectablePointAt((int)linkNode.xOffset, (int)linkNode.yOffset);
											if ( touchedPoint != null )
											{
												if ( links[0].m_startObj == linkNode )
												{
													links[0].m_startObj = graphicObject;
													links[0].m_startPt = touchedPoint;
												}
												else
												{
													links[0].m_endObj = graphicObject;
													links[0].m_endPt = touchedPoint;
												}
												m_rgObjects.Remove(linkNode);
												touched = true;
											}
										}
									}
								}

								if ( touched )
								{
									DrawObjectsOnCanvas();
								}
							}

							break;
						}
						case UserActions.MovingKeyPoint:
						{
							m_selectedLinkKeyPoint = null;
							status.Action	= UserActions.Editing;

							if ( m_selectedLink.m_keyPts != null )
							{
								ArrayList pts = new ArrayList();
								PointF ptf = m_selectedLink.StartPointInCanvas(m_rgObjects);
								GOMLib.SketchPoint spt = new GOMLib.SketchPoint();
								spt.x = (int)ptf.X;
								spt.y = (int)ptf.Y;
								pts.Add( spt );
								for( int i=0; i<m_selectedLink.m_keyPts.Count; i++ )
								{
									spt = new GOMLib.SketchPoint();
									spt.x = (int)m_selectedLink.m_keyPts[i].x;
									spt.y = (int)m_selectedLink.m_keyPts[i].y;
									pts.Add( spt );
								}
								ptf = m_selectedLink.EndPointInCanvas(m_rgObjects);
								spt = new GOMLib.SketchPoint();
								spt.x = (int)ptf.X;
								spt.y = (int)ptf.Y;
								pts.Add( spt );

								pts = CalculateSegments(pts);

								m_selectedLink.m_keyPts = new GOMLib.GOM_Points();
								for( int i=0; i<(pts.Count-2); i++ )
								{
									GOMLib.GOM_Point pt = new GOMLib.GOM_Point();
									pt.x = ((GOMLib.SketchPoint)pts[i+1]).x;
									pt.y = ((GOMLib.SketchPoint)pts[i+1]).y;
									m_selectedLink.m_keyPts.Add( pt );
								}
							}

							DrawObjectsOnCanvas();
							break;
						}
						case UserActions.Selecting:
						{
							System.Drawing.Region	rgn;
							System.Drawing.Region	rgnIntersect;
							System.Drawing.Graphics	canvas;

							rgn		= new Region(m_selectingRect);

							canvas	= plDraw.CreateGraphics();

							for (int i = 0; i < m_rgObjects.Count; i++)
							{
								rgnIntersect = rgn.Clone();
								rgnIntersect.Intersect(m_rgObjects[i].BoundingRegion);

								if (!rgnIntersect.IsEmpty(canvas))
								{
									m_selObjs.Add(m_rgObjects[i]);
								}
							}
							plDraw.Capture	= false;

							status.Action	= UserActions.Editing;

							m_selectingRect.X		= 0;
							m_selectingRect.Y		= 0;
							m_selectingRect.Width	= 0;
							m_selectingRect.Height	= 0;

							DrawObjectsOnCanvas();
							break;
						}
					}
					break;
				}
			}
		}

		private void m_frmColor_DrawingStyleChanged(GOMLib.GOM_Object_Primitive primitive, GOMLib.GOM_Style_Drawing style)
		{
			DrawObjectsOnCanvas();
		}

		private void m_frmColor_FillingStyleChanged(GOMLib.GOM_Object_Primitive primitive, GOMLib.GOM_Style_Filling style)
		{
			DrawObjectsOnCanvas();
		}

        // xxx new add
        private void m_frmInfo_DrawingInfoChanged()
        {
            DrawObjectsOnCanvas();
        }

        //Convert the current Diagram to ReadFromXml type
        public void DiagramConvertXml()
        {
            EEDomain.ReadFromXml bak_xml = readXmls;
            GOMLib.GOM_Diagram diagram = new GOMLib.GOM_Diagram(m_rgObjects, m_rgLinks);
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Xml.XmlTextWriter xmltextWriter = new System.Xml.XmlTextWriter(stringWriter);
            diagram.SaveToXML(xmltextWriter);
            string diagramString = stringWriter.ToString();
            readXmls = new EEDomain.ReadFromXml(diagramString, true);
            if (bak_xml != null)
            {
                readXmls.SynDeviceList(bak_xml);
            }
        }
        //end

		private void DrawingForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			m_CtrlPressed = e.Control;

			// Add shortcuts here.
			if ( e.Control )
			{
				switch(e.KeyCode)
				{
					case Keys.X:
						InvodeCommand(EditCommands.Cut);
						break;
					case Keys.C:
						InvodeCommand(EditCommands.Copy);
						break;
					case Keys.V:
						InvodeCommand(EditCommands.Paste);
						break;
					case Keys.Z:
						InvodeCommand(EditCommands.Undo);
						break;
					case Keys.Y:
						InvodeCommand(EditCommands.Redo);
						break;
				}
			}
			if ( e.KeyCode == Keys.Delete )
			{
				InvodeCommand(EditCommands.Delete);
			}
		}

		private void DrawingForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			m_CtrlPressed = e.Control;
		}

		public void InvodeCommand(EditCommands command)
		{
			m_frmEdit_CommandSelected(command);
		}

		private void m_frmEdit_CommandSelected(EditCommands command)
		{
			if (status.Action == UserActions.Editing)
			{
				switch (command)
				{
					case EditCommands.Group:
					{
						if (m_selObjs.Count > 1)
						{
							GOMLib.GOM_Interface_Graphic_Object	obj;

							obj = GOMLib.GOM_Object_Group.CreateGroupObject(m_selObjs);

							if (obj != null)
							{
								for (int i = 0; i < m_selObjs.Count; i++)
								{
									m_rgObjects.Remove(m_selObjs[i]);
								}

								m_rgObjects.Add(obj);

								m_selObjs.Clear();
								m_selObjs.Add(obj);

								DrawObjectsOnCanvas();
							}
						}
						break;
					}
					case EditCommands.Ungroup:
					{
						if (m_selObjs.Count == 1)
						{
							if (m_selObjs[0] is GOMLib.GOM_Object_Group)
							{
								GOMLib.GOM_Objects	rgObjs;

								rgObjs = new GOMLib.GOM_Objects();

								((GOMLib.GOM_Object_Group)m_selObjs[0]).DecomposeGroupObject(rgObjs);

								m_rgObjects.Remove(m_selObjs[0]);
								m_selObjs.Clear();

								for (int i = 0; i < rgObjs.Count; i++)
								{
									m_rgObjects.Add(rgObjs[i]);
									m_selObjs.Add(rgObjs[i]);
								}

								DrawObjectsOnCanvas();
							}
						}
						break;
					}
					case EditCommands.BringToFront:
					{
						if (m_selObjs.Count > 0)
						{
							for (int i = 0; i < m_selObjs.Count; i++)
							{
								m_rgObjects.Remove(m_selObjs[i]);
							}

							for (int i = 0; i < m_selObjs.Count; i++)
							{
								m_rgObjects.Add(m_selObjs[i]);
							}

							DrawObjectsOnCanvas();
						}
						break;
					}
					case EditCommands.SendToBack:
					{
						if (m_selObjs.Count > 0)
						{
							for (int i = 0; i < m_selObjs.Count; i++)
							{
								m_rgObjects.Remove(m_selObjs[i]);
							}

							for (int i = 0; i < m_selObjs.Count; i++)
							{
								m_rgObjects.Insert(m_selObjs[i], 0);
							}

							DrawObjectsOnCanvas();
						}
						break;
					}
					case EditCommands.Delete:
					{
						if (m_selObjs.Count > 0)
						{
							for (int i = 0; i < m_selObjs.Count; i++)
							{
								m_rgObjects.Remove(m_selObjs[i]);

								int j = 0;

								while (j < m_rgLinks.Count)
								{
									if (((GOMLib.GOM_Link)m_rgLinks[j]).LinkWith(m_selObjs[i]))
									{
										if (m_rgLinks[j].Equals(m_selectedLink))
										{
											m_selectedLink = null;
										}

										m_rgLinks.RemoveAt(j);
									}
									else
									{
										j++;
									}
								}
							}

							m_selObjs.Clear();
							DrawObjectsOnCanvas();
						}
						break;
					}
					case EditCommands.Cut:
					{
						if (m_selObjs.Count > 0)
						{
							m_clipBoard.Clear();

							for (int i = 0; i < m_selObjs.Count; i++)
							{
								m_rgObjects.Remove(m_selObjs[i]);
								m_clipBoard.Add(m_selObjs[i]);
							}

							m_selObjs.Clear();
							DrawObjectsOnCanvas();
						}
						break;
					}
					case EditCommands.Copy:
					{
						if (m_selObjs.Count > 0)
						{
							m_clipBoard.Clear();

							for (int i = 0; i < m_selObjs.Count; i++)
							{
								m_clipBoard.Add(m_selObjs[i]);
							}
						}
						break;
					}
					case EditCommands.Paste:
					{
						if (m_clipBoard.Count > 0)
						{
							float	minX, minY;

							m_selObjs.Clear();

							for (int i = 0; i < m_clipBoard.Count; i++)
							{
								m_selObjs.Add(m_clipBoard[i].Clone());
							}

							minX = m_selObjs[0].xOffset;
							minY = m_selObjs[0].yOffset;

							for (int i = 1; i < m_selObjs.Count; i++)
							{
								minX = Math.Min(minX, m_selObjs[i].xOffset);
								minY = Math.Min(minY, m_selObjs[i].yOffset);
							}

							minX = tagEdit.Left - minX;
							minY = tagEdit.Top - minY;

							for (int i = 0; i < m_selObjs.Count; i++)
							{
								m_selObjs[i].xOffset += minX;
								m_selObjs[i].yOffset += minY;

								m_rgObjects.Add(m_selObjs[i]);
							}

							DrawObjectsOnCanvas();
						}
						break;
					}
					case EditCommands.Sketch:
					{
						if (m_selObjs.Count == 1)
						{
							if (m_selObjs[0] is GOMLib.GOM_Object_Sketch)
							{
								m_sketch = (GOMLib.GOM_Object_Sketch)m_selObjs[0];

								m_rgObjects.Remove(m_sketch);
								m_selObjs.Clear();

								status.Action = UserActions.Sketching;
								if ( DrawingModeChanged != null )
								{
									DrawingModeChanged(DrawingMode.Sketching);
								}

								DrawObjectsOnCanvas();
							}
						}
						break;
					}
				}
			}

			if (status.Action == UserActions.Sketching)
			{
				switch (command)
				{
					case EditCommands.Undo:
					{
						if (m_sketch != null)
						{
							while (m_sketch.rgStrokeToSketch.Count > 0)
							{
								if ((int)(m_sketch.rgStrokeToSketch[m_sketch.rgStrokeToSketch.Count - 1]) == m_sketch.rgSketchSet.Count)
								{
									m_sketch.rgStrokeToSketch.RemoveAt(m_sketch.rgStrokeToSketch.Count - 1);
									m_sketch.rgDrawings.RemoveAt(m_sketch.rgDrawings.Count - 1);
								}
								else
								{
									break;
								}
							}

							if (m_sketch.rgSketchSet.Count > 0)
							{
								m_rgRedoList.Add(m_sketch.rgSketchSet[m_sketch.rgSketchSet.Count - 1]);
								m_sketch.rgSketchSet.RemoveAt(m_sketch.rgSketchSet.Count - 1);
							}
							
							ClearRecognitionResult();

							if (m_sketch.rgSketchSet.Count > 0)
							{
//								m_recognition.PushStrokeAndWait(m_sketch.ExportRecognitionResultToXML());
								m_recognition.PushStroke(m_sketch.ExportRecognitionResultToXML());
							}

							DrawObjectsOnCanvas();
						}
						break;
					}
					case EditCommands.Redo:
					{
						if (m_rgRedoList.Count > 0)
						{
							if (m_sketch != null)
							{
								ClearRecognitionResult();

								m_bRedoing = true;
								m_sketch.rgSketchSet.Add(m_rgRedoList[m_rgRedoList.Count - 1]);
								m_preprocess.PushSketchAndWait(m_sketch, (ArrayList)m_rgRedoList[m_rgRedoList.Count - 1]);
								m_rgRedoList.RemoveAt(m_rgRedoList.Count - 1);

								DrawObjectsOnCanvas();
							}						
						}
						break;
					}
				}
			}

			tagEdit.HideTagWindow();
		}

		private void DrawCurrentStroke(System.Drawing.Graphics canvas)
		{
			if (m_rgStroke != null)
			{
				GOMLib.SketchPoint pt1, pt2;

				for (int i = 0; i < m_rgStroke.Count - 1; i++)
				{
					pt1 = (GOMLib.SketchPoint)m_rgStroke[i];
					pt2 = (GOMLib.SketchPoint)m_rgStroke[i + 1];
					canvas.DrawLine(System.Drawing.Pens.Red, pt1.x, pt1.y, pt2.x, pt2.y);
				}
			}		
		}

		private void UpdateSketchTrack()
		{
			if ((m_rgStroke != null) && (m_LButtonPressed))
			{
				System.Drawing.Point pt;

				pt = plDraw.PointToClient(System.Windows.Forms.Cursor.Position);

				if ((pt.X == m_orgX) && (pt.Y == m_orgY))
				{
					return;
				}

				m_orgX = pt.X;
				m_orgY = pt.Y;

				GOMLib.SketchPoint	point = new GOMLib.SketchPoint();

				point.x = pt.X;
				point.y = pt.Y;
				point.time = Environment.TickCount;

				m_rgStroke.Add(point);
				DrawCurrentStroke(plDraw.CreateGraphics());
			}
		}

		private void m_preprocess_UpdateRequired()
		{
			if (m_preprocess.m_iGestureID == 0)
			{
				plDraw.BeginInvoke(new MethodInvoker(HandleStroke));
			}
			else
			{
				plDraw.BeginInvoke(new MethodInvoker(HandleGesture));		
			}
		}

		private void HandleStroke()
		{
			if (!m_bRedoing)
			{
				m_rgRedoList.Clear();
			}

			m_bRedoing = false;

			DrawObjectsOnCanvas();

//			m_recognition.PushStrokeAndWait(m_sketch.ExportRecognitionResultToXML());
			m_recognition.PushStroke(m_sketch.ExportRecognitionResultToXML());
		}

		private void HandleGesture()
		{
			if (m_sketch != null)
			{
				while (m_sketch.rgStrokeToSketch.Count > 0)
				{
					if ((int)(m_sketch.rgStrokeToSketch[m_sketch.rgStrokeToSketch.Count - 1]) == m_sketch.rgSketchSet.Count)
					{
						m_sketch.rgStrokeToSketch.RemoveAt(m_sketch.rgStrokeToSketch.Count - 1);
						m_sketch.rgDrawings.RemoveAt(m_sketch.rgDrawings.Count - 1);
					}
					else
					{
						break;
					}
				}

				if (m_sketch.rgSketchSet.Count > 0)
				{
					m_sketch.rgSketchSet.RemoveAt(m_sketch.rgSketchSet.Count - 1);
				}
			}

			switch (m_preprocess.m_iGestureID)
			{
				case 1:
				{
					this.InvodeCommand(EditCommands.Undo);
					break;
				}
				case 2:
				{
					this.InvodeCommand(EditCommands.Redo);
					break;
				}
			}

			DrawObjectsOnCanvas();
		}

		public void StopRecognition()
		{
			m_recognition.StopRecognize();
		}

		private void NotifyRecognitionResult()
		{
			if (RecognitionResultChanged != null)
			{
				RecognitionResultChanged(this.RecognitionResult);
			}
		}

		private void NotifyRecognitionStart()
		{
			if (RecognitionResultStart != null)
			{
				RecognitionResultStart();
			}
		}

		private void NotifyRecognitionEnd()
		{
			if (RecognitionResultEnd != null)
			{
				RecognitionResultEnd();
			}
		}

		private void m_recognition_RecognitionOver(string primitiveXML)
		{
			this.RecognitionResult = primitiveXML;
			plDraw.BeginInvoke(new MethodInvoker(NotifyRecognitionResult));
			Application.DoEvents();
		}

		private void m_recognition_RecognitionStart()
		{
			plDraw.BeginInvoke(new MethodInvoker(NotifyRecognitionStart));
			Application.DoEvents();
		}

		private void m_recognition_RecognitionEnd()
		{
			plDraw.BeginInvoke(new MethodInvoker(NotifyRecognitionEnd));
			Application.DoEvents();
		}

		private void m_frmLink_LinkStyleChanged(GOMLib.GOM_Link link)
		{
			DrawObjectsOnCanvas();
		}

		private void m_frmLink_RemoveLink(GOMLib.GOM_Link link)
		{
			m_rgLinks.Remove(link);
			tagLink.HideTagWindow();
			m_selectedLink = null;
			DrawObjectsOnCanvas();
		}

		private void tagFishEye_FishEyeItemSelected(FloatControlLib.FishEyeItem item)
		{
			ReplaceCurrentSketching((TemplatePack)item.UserData);
		}

		/// <summary>
		/// Save the whole diagram to a xml file
		/// </summary>
		public void SaveDiagramToFile()
		{
			SaveDiagramToFile(m_currentFileName, "others");
		}

		/// <summary>
		/// Save the whole diagram to a xml file.
		/// </summary>
		/// <param name="fileName">File name into which the whole diagram is saved.</param>
		public void SaveDiagramToFile( string fileName, string type)
		{
			System.Xml.XmlTextWriter writer;
			writer = new System.Xml.XmlTextWriter(fileName, System.Text.Encoding.UTF8);
            switch (type)
            { 
                case "ee":
                    readXmls.SaveVar(fileName);
                    break;
                case "others":
                    break;
                default: 
                    break;
            }
			SaveDiagramToXml(writer);
			writer.Close();
			m_currentFileName = fileName;
		}

        /// <summary>
        /// Save the GOM_Diagram type to a xml file
        /// </summary>
		public void SaveDiagramToXml( System.Xml.XmlTextWriter writer )
		{
			GOMLib.GOM_Diagram diagram = new GOMLib.GOM_Diagram(m_rgObjects,m_rgLinks);
			diagram.SaveToXML(writer);
		}

		public void LoadDiagramFromFile( string fileName )
		{
			System.Xml.XmlDocument	doc = new System.Xml.XmlDocument();
			doc.Load( fileName );
            LoadDiagramFromXml(doc.DocumentElement);

            //Syn EEDomain file - new add week11
            try 
            {
                readXmls.LoadVar(fileName);
                DrawObjectsOnCanvas();
            }
            catch { }
		}

		public void LoadDiagramFromXml( System.Xml.XmlNode node)
		{
			GOMLib.GOM_Diagram diagram = new GOMLib.GOM_Diagram();
			diagram.LoadFromXML(node, null);
			this.m_rgObjects = diagram.Objects;
			this.m_rgLinks = diagram.Links;
            DrawObjectsOnCanvas();
		}

		private void plDraw_DoubleClick(object sender, System.EventArgs e)
		{
			if (status.Action == UserActions.Sketching)
			{
				m_LButtonPressed = false;
			}
			if (status.Action == UserActions.Selecting)
			{
				m_selectingRect.X		= 0;
				m_selectingRect.Y		= 0;
				m_selectingRect.Width	= 0;
				m_selectingRect.Height	= 0;
			}
			plDraw_MouseDown(null,
				new System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.Right,0,0,0,0));
		}

		private ArrayList CalculateSegments(ArrayList pts)
		{
			float error=6;
			ArrayList ret = new ArrayList();
			int size = pts.Count;
			if (size <= 2)
			{
				return (ArrayList)pts.Clone();
			}

			int i0 = 0; 
			ret.Add(pts[0]);
			while ( i0 < size - 1 )
			{
				double a2 = 2*Math.PI;
				double a1 = -a2;
				int i1 = i0, last_i1 = pts.Count-1;
				GOMLib.SketchPoint pt0 = (GOMLib.SketchPoint)pts[i0];
				while (i1<size-1 && a1<=a2)
				{
					i1++;
					GOMLib.SketchPoint pt1 = (GOMLib.SketchPoint)pts[i1];
					double dx = pt1.x - pt0.x;
					double dy = pt1.y - pt0.y;
					double angle = Math.Atan2(dy, dx);

					if ( dx<0 || ( dx==0 && dy<0 ) ) 
					{
						if (angle<=0)
							angle += Math.PI;
						else
							angle -= Math.PI;
					}

					double d = Math.Sqrt(dx*dx +dy*dy);
					double da = Math.Asin(error/d);
					double newa1 = angle-da, newa2 = angle+da;
					if (angle>=a1 && angle <=a2)
						last_i1 = i1;
					if (newa1>a1)
						a1 = newa1; 
					if (newa2 < a2)
						a2 = newa2; 
				}
				ret.Add( pts[last_i1] );
				i0 = last_i1;
			}
			return ret;
		}

        private void tagFishEye_Load(object sender, EventArgs e)
        {

        }

	}

	public enum DrawingMode
	{
		Editing,
		Sketching
	}

	public delegate void MouseMoveOnDrawingAreaEvent(int x, int y);
	public delegate void DrawingModeChangedEvent(DrawingMode drawingMode);
	public delegate void PreprocessOverEvent();
	public delegate void RecognitionEvent();
	public delegate void RecognitionOverEvent(string primitiveXML);
	public delegate void RecognitionResultChangedEvent(string resultXML);
	public delegate void RecognitionResultEvent();

	public class RecognitionQueue
	{
		private System.Threading.ManualResetEvent	recoReadyEvent = new System.Threading.ManualResetEvent(false);
		private string[]							templatePaths = null;
		private Recognition.IPrimitiveRecognition[]	recognitions = null;
		private bool								cancelReco = false;

		public RecognitionQueue(string TemplatePath)
		{
			RecognitionOver	= null;
			m_debugPrimitive= null;
			m_bTerminating	= false;
			m_bDebugging	= false;
			m_TemplatePath	= TemplatePath;
			m_rgSketch		= new System.Collections.ArrayList();
			m_WorkerThread	= new System.Threading.Thread(new System.Threading.ThreadStart(RecognitionThread));
			m_Mutex			= new System.Threading.Mutex();
			m_QueueNotEmpty	= new System.Threading.ManualResetEvent(false);
//			m_recognition	= new Recognition.CPrimitiveRecognitionClass();
//			m_recognition.LoadTemplates(TemplatePath);

			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(TemplatePath);
			System.IO.DirectoryInfo[] subs = di.GetDirectories();

			templatePaths = new string[subs.Length];
			for( int i=0; i<subs.Length; i++)
			{
				templatePaths[i] = subs[i].FullName;
			}
			Array.Sort(templatePaths);
		}

		public void StopRecognize()
		{
			cancelReco = true;
		}

		public void PushStrokeAndWait(string sketchXML)
		{
			m_Mutex.WaitOne();

			HandleRecognitionResult(m_recognition.RecognizeObjects(sketchXML));

			m_Mutex.ReleaseMutex();
		}

		public void PushStroke(string sketchXML)
		{
			cancelReco = true;

			m_Mutex.WaitOne();

			m_rgSketch.Add(sketchXML);
			m_QueueNotEmpty.Set();

			m_Mutex.ReleaseMutex();
		}
		
		private void RecognitionThread()
		{
			string	sketchXML;

			recognitions = new Recognition.IPrimitiveRecognition[templatePaths.Length];
			for( int i=0; i<templatePaths.Length; i++)
			{
				recognitions[i] = new Recognition.CPrimitiveRecognitionClass();
				recognitions[i].LoadTemplates(templatePaths[i]);
			}

			recoReadyEvent.Set();

			while (!m_bTerminating)
			{
				m_QueueNotEmpty.WaitOne();

				cancelReco = false;

				sketchXML = "";

				m_Mutex.WaitOne();

				if (m_bTerminating)
				{
					m_Mutex.ReleaseMutex();
					return;
				}
				else if (m_rgSketch.Count > 0)
				{
					sketchXML = (string)m_rgSketch[0];

					m_rgSketch.RemoveAt(0);

					if (m_rgSketch.Count == 0)
					{
						m_QueueNotEmpty.Reset();
					}
				}

				m_Mutex.ReleaseMutex();

				try
				{
					if (sketchXML.Length > 0)
					{
						if( RecognitionStart != null )
						{
							RecognitionStart();
						}

						for(int i=0; i<recognitions.Length; i++)
						{
							HandleRecognitionResult(recognitions[i].RecognizeObjects(sketchXML));
							if (cancelReco )
							{
								break;
							}
						}

						if ( RecognitionEnd != null )
						{
							RecognitionEnd();
						}
					}
				}
				catch
				{
					System.Windows.Forms.MessageBox.Show("Exception occurs on primitive recognition.");
					m_recognition	= null;
					m_recognition	= new Recognition.CPrimitiveRecognitionClass();
					m_recognition.LoadTemplates(m_TemplatePath);
				}
			}
		}

		public void StartRecognitionThread()
		{
			m_Mutex.WaitOne();
			
			if (m_bDebugging)
			{
				if (m_debugPrimitive == null)
				{
					m_debugPrimitive = new System.IO.StreamWriter("primitive_debug_" + Environment.TickCount.ToString() + ".log", true);
				}
			}
			
			m_bTerminating = false;
			m_WorkerThread.Start();
			m_WorkerThread.Priority = System.Threading.ThreadPriority.Lowest;

			m_Mutex.ReleaseMutex();

			recoReadyEvent.WaitOne();
		}

		public void StopRecognitionThread()
		{
			cancelReco = true;

			m_Mutex.WaitOne();

			m_bTerminating = true;
			m_QueueNotEmpty.Set();

			m_Mutex.ReleaseMutex();

			m_WorkerThread.Join();

			if (m_debugPrimitive != null)
			{
				m_debugPrimitive.Close();
				m_debugPrimitive = null;
			}
		}

		private void HandleRecognitionResult(string result)
		{
			if (m_debugPrimitive != null)
			{
				m_debugPrimitive.WriteLine(result);
				m_debugPrimitive.Flush();
			}

			if (RecognitionOver != null)
			{
				RecognitionOver(result);
			}
		}

		public event RecognitionOverEvent			RecognitionOver = null;
		public event RecognitionEvent				RecognitionStart = null;
		public event RecognitionEvent				RecognitionEnd = null;

		public	bool								m_bDebugging;
		private bool								m_bTerminating;
		private string								m_TemplatePath;
		private Recognition.IPrimitiveRecognition	m_recognition;
		private System.IO.StreamWriter				m_debugPrimitive;
		private System.Collections.ArrayList		m_rgSketch;
		private System.Threading.Mutex				m_Mutex;
		private System.Threading.Thread				m_WorkerThread;
		private System.Threading.ManualResetEvent	m_QueueNotEmpty;
	}

	public class PreprocessQueue
	{
		public PreprocessQueue()
		{
			UpdateRequired	= null;
			m_curSketchObj	= null;
			m_bTerminating	= false;
			m_bDebugging	= false;

			m_debugSketch	= null;
			m_debugStroke	= null;
			m_rgSketchObj	= new ArrayList();
			m_rgSketch		= new ArrayList();

			m_preprocess	= new Preprocessing.CStrokeRecognitionClass();
			m_refine		= new StrokeRefineLib.CStrokeRefinementClass();

			m_WorkerThread	= new System.Threading.Thread(new System.Threading.ThreadStart(PreprocessThread));
			m_Mutex			= new System.Threading.Mutex();
			m_QueueNotEmpty	= new System.Threading.ManualResetEvent(false);
		}

		public void StartPreprocessThread()
		{
			m_Mutex.WaitOne();
			
			if (m_bDebugging)
			{
				if (m_debugSketch == null)
				{
					m_debugSketch = new System.IO.StreamWriter("sketch_debug_" + Environment.TickCount.ToString() + ".log", true);
				}
				if (m_debugStroke == null)
				{
					m_debugStroke = new System.IO.StreamWriter("stroke_debug_" + Environment.TickCount.ToString() + ".log", true);
				}
			}

			m_bTerminating = false;
			m_WorkerThread.Start();

			m_Mutex.ReleaseMutex();
		}

		public void StopPreprocessThread()
		{
			m_Mutex.WaitOne();

			m_bTerminating = true;
			m_QueueNotEmpty.Set();

			m_Mutex.ReleaseMutex();

			m_WorkerThread.Join();

			if (m_debugSketch != null)
			{
				m_debugSketch.Close();
				m_debugSketch = null;
			}

			if (m_debugStroke != null)
			{
				m_debugStroke.Close();
				m_debugStroke = null;
			}
			
		}

		public void PushSketchAndWait(GOMLib.GOM_Object_Sketch sketchObj, System.Collections.ArrayList sketch)
		{
			m_Mutex.WaitOne();

			m_curSketchObj		= sketchObj;
			HandlePreprocessResult(m_preprocess.RecognizeStroke(EncodeSketchToXML(sketch)));

			m_Mutex.ReleaseMutex();
		}

		public void PushSketch(GOMLib.GOM_Object_Sketch sketchObj, System.Collections.ArrayList sketch)
		{
			m_Mutex.WaitOne();

			m_rgSketchObj.Add(sketchObj);
			m_rgSketch.Add(sketch);

			m_QueueNotEmpty.Set();

			m_Mutex.ReleaseMutex();
		}

		private void PreprocessThread()
		{
			GOMLib.GOM_Object_Sketch		sketchObj;
			System.Collections.ArrayList	sketch;

			while (!m_bTerminating)
			{
				m_QueueNotEmpty.WaitOne();

				sketch		= null;
				sketchObj	= null;

				m_Mutex.WaitOne();

				if (m_bTerminating)
				{
					m_Mutex.ReleaseMutex();
					return;
				}
				else if ((m_rgSketchObj.Count > 0) && (m_rgSketch.Count > 0))
				{
					sketchObj	= (GOMLib.GOM_Object_Sketch)m_rgSketchObj[0];
					sketch		= (System.Collections.ArrayList)m_rgSketch[0];

					m_rgSketchObj.RemoveAt(0);
					m_rgSketch.RemoveAt(0);

					if ((m_rgSketchObj.Count == 0) || (m_rgSketch.Count == 0))
					{
						m_QueueNotEmpty.Reset();
					}
				}

				m_Mutex.ReleaseMutex();

				try
				{
					if ((sketch != null) && (sketchObj != null))
					{
						m_curSketchObj		= sketchObj;
						HandlePreprocessResult(m_preprocess.RecognizeStroke(EncodeSketchToXML(sketch)));
					}
				}
				catch
				{
					System.Windows.Forms.MessageBox.Show("Exception occurs on stroke recognition.");
					m_preprocess = null;
					m_preprocess = new Preprocessing.CStrokeRecognitionClass();
				}
			}
		}

		private void HandlePreprocessResult(string result)
		{
			if (m_curSketchObj != null)
			{
				DecodeXMLToStroke(result);
				ReloadStrokesFromXML(m_curSketchObj, m_refine.Refine_MergeVertex(m_curSketchObj.ExportRecognitionResultToXML(), 10));
			}
			m_curSketchObj = null;

			if (UpdateRequired != null)
			{
				UpdateRequired();
			}		
		}

		private void ReloadStrokesFromXML(GOMLib.GOM_Object_Sketch sketchObj, string strokeXML)
		{
			System.Xml.XmlDocument	doc;
			System.Xml.XmlNode		rootNode;
			System.Xml.XmlNode		strokesNode;
			System.Xml.XmlNode		candidateNode;

			doc = new System.Xml.XmlDocument();
			doc.LoadXml(strokeXML);

			rootNode = doc.DocumentElement;

			if (rootNode != null)
			{	
				//Look for <output>
				if (System.String.Compare(rootNode.Name, "output", true) == 0)
				{
					sketchObj.rgDrawings.Clear();
					
					for (int i = 0; i < rootNode.ChildNodes.Count; i++)
					{
						//Look for <strokes>
						if (System.String.Compare(rootNode.ChildNodes[i].Name, "strokes", true) == 0)
						{
							strokesNode = rootNode.ChildNodes[i];
							//Look for <candidate> with maximum similarity
							for (int j = 0; j < strokesNode.ChildNodes.Count; j++)
							{
								if (System.String.Compare(strokesNode.ChildNodes[j].Name, "candidate", true) == 0)
								{
									candidateNode = strokesNode.ChildNodes[j];

									for (int k = 0; k < candidateNode.ChildNodes.Count; k++)
									{
										if (System.String.Compare(candidateNode.ChildNodes[k].Name, "line", true) == 0)
										{
											sketchObj.rgDrawings.Add(DecodeXMLToLine(candidateNode.ChildNodes[k]));
										}
										if (System.String.Compare(candidateNode.ChildNodes[k].Name, "arc", true) == 0)
										{
											sketchObj.rgDrawings.Add(DecodeXMLToArc(candidateNode.ChildNodes[k]));
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private string EncodeSketchToXML(System.Collections.ArrayList sketch)
		{
			string	sketchXML;
			int		timeStamp;
			
			sketchXML = "<?xml version=\"1.0\"?>\n<!--QuickDiagram input:PreProcessing.-->\n";
			sketchXML += "<input ptnum=\"" + sketch.Count + "\">\n";

			GOMLib.SketchPoint pt, pt0;

			if (sketch.Count > 0)
			{
				pt0 = (GOMLib.SketchPoint)sketch[0];

				for (int i = 0; i < sketch.Count; i++)
				{				
					pt			= (GOMLib.SketchPoint)sketch[i];
					timeStamp	= pt.time - pt0.time;
					sketchXML	+= "<packet x=\"" + pt.x.ToString() + "\" y=\"" + pt.y.ToString() + "\" t=\"" + timeStamp.ToString() + "\"/>";
				}
			}

			sketchXML += "</input>";

			if (m_debugSketch != null)
			{
				m_debugSketch.WriteLine(sketchXML);
				m_debugSketch.Flush();
			}

			return sketchXML;
		}

		private void DecodeXMLToStroke(string strokeXML)
		{
			System.Xml.XmlDocument	doc;
			System.Xml.XmlNode		rootNode;
			System.Xml.XmlNode		strokesNode;
			System.Xml.XmlNode		candidateNode;

			float	curSimilarity;
			float	maxSimilarity		= 0;
			int		maxSimilarityIdx	= -1;

			if (m_debugStroke != null)
			{
				m_debugStroke.WriteLine(strokeXML);
				m_debugStroke.Flush();
			}

			doc = new System.Xml.XmlDocument();
			doc.LoadXml(strokeXML);

			rootNode = doc.DocumentElement;

			if (rootNode != null)
			{	
				//Look for <output>
				if (System.String.Compare(rootNode.Name, "output", true) == 0)
				{
					//Look for gesture
					m_iGestureID = 0;
					for (int i = 0; i < rootNode.ChildNodes.Count; i++)
					{
						if (System.String.Compare(rootNode.ChildNodes[i].Name, "gesture", true) == 0)
						{
							System.Xml.XmlNode	gestureNode = rootNode.ChildNodes[i];

							for (int j = 0; j < gestureNode.Attributes.Count; j++)
							{
								if (System.String.Compare(gestureNode.Attributes[j].Name, "id", true) == 0)
								{
									m_iGestureID = int.Parse(gestureNode.Attributes[j].Value);
								}
							}
						}
					}

					for (int i = 0; i < rootNode.ChildNodes.Count; i++)
					{
						//Look for <strokes>
						if (System.String.Compare(rootNode.ChildNodes[i].Name, "strokes", true) == 0)
						{
							strokesNode = rootNode.ChildNodes[i];
							//Look for <candidate> with maximum similarity
							for (int j = 0; j < strokesNode.ChildNodes.Count; j++)
							{
								if (System.String.Compare(strokesNode.ChildNodes[j].Name, "candidate", true) == 0)
								{
									candidateNode = strokesNode.ChildNodes[j];
									for (int k = 0; k < candidateNode.Attributes.Count; k++)
									{
										if (System.String.Compare(candidateNode.Attributes[k].Name, "similarity", true) == 0)
										{
											curSimilarity = float.Parse(candidateNode.Attributes[k].Value);
											if (curSimilarity > maxSimilarity)
											{
												maxSimilarity = curSimilarity;
												maxSimilarityIdx = j;
											}
										}
									}
								}
							}
							//Create stroke set from the candidate with maximum similarity
							if (maxSimilarityIdx != -1)
							{
								candidateNode = strokesNode.ChildNodes[maxSimilarityIdx];
								for (int j = 0; j < candidateNode.ChildNodes.Count; j++)
								{
									if (System.String.Compare(candidateNode.ChildNodes[j].Name, "line", true) == 0)
									{
										m_curSketchObj.rgDrawings.Add(DecodeXMLToLine(candidateNode.ChildNodes[j]));
										m_curSketchObj.rgStrokeToSketch.Add(m_curSketchObj.rgSketchSet.Count);
									}
									if (System.String.Compare(candidateNode.ChildNodes[j].Name, "arc", true) == 0)
									{
										m_curSketchObj.rgDrawings.Add(DecodeXMLToArc(candidateNode.ChildNodes[j]));
										m_curSketchObj.rgStrokeToSketch.Add(m_curSketchObj.rgSketchSet.Count);
									}
								}
							}
						}
					}
				}
			}
		}

		private GOMLib.GOM_Interface_Drawing DecodeXMLToLine(System.Xml.XmlNode node)
		{
			GOMLib.GOM_Point	startPt, endPt;

			startPt = null;
			endPt	= null;

			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, "startpt", true) == 0)
				{
					startPt = DecodeXMLToPoint(node.ChildNodes[i]);
				}
				if (System.String.Compare(node.ChildNodes[i].Name, "endpt", true) == 0)
				{
					endPt = DecodeXMLToPoint(node.ChildNodes[i]);
				}
			}

			return new GOMLib.GOM_Drawing_Line(startPt, endPt, null);
		}

		private GOMLib.GOM_Interface_Drawing DecodeXMLToArc(System.Xml.XmlNode node)
		{
			GOMLib.GOM_Point	startPt, endPt, ltPt, rbPt;
			float				rotateAngle = 0;

			startPt = null;
			endPt	= null;
			ltPt	= null;
			rbPt	= null;

			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				if (System.String.Compare(node.ChildNodes[i].Name, "startpt", true) == 0)
				{
					startPt = DecodeXMLToPoint(node.ChildNodes[i]);
				}
				if (System.String.Compare(node.ChildNodes[i].Name, "endpt", true) == 0)
				{
					endPt = DecodeXMLToPoint(node.ChildNodes[i]);
				}
				if (System.String.Compare(node.ChildNodes[i].Name, "ltpt", true) == 0)
				{
					ltPt = DecodeXMLToPoint(node.ChildNodes[i]);
				}
				if (System.String.Compare(node.ChildNodes[i].Name, "rbpt", true) == 0)
				{
					rbPt = DecodeXMLToPoint(node.ChildNodes[i]);
				}
			}

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, "rotate", true) == 0)
				{
					rotateAngle = float.Parse(node.Attributes[i].Value);
				}
			}
			
			if ((startPt.x == endPt.x) && (startPt.y == endPt.y))
			{
				return new GOMLib.GOM_Drawing_Arc(ltPt, rbPt, startPt, startPt, null, rotateAngle);
			}
			else
			{
				return new GOMLib.GOM_Drawing_Arc(ltPt, rbPt, startPt, endPt, null, rotateAngle);
			}
		}

		private GOMLib.GOM_Point DecodeXMLToPoint(System.Xml.XmlNode node)
		{
			GOMLib.GOM_Point	pt;

			pt = new GOMLib.GOM_Point();

			for (int i = 0; i < node.Attributes.Count; i++)
			{
				if (System.String.Compare(node.Attributes[i].Name, "x", true) == 0)
				{
					pt.x = float.Parse(node.Attributes[i].Value);
				}
				if (System.String.Compare(node.Attributes[i].Name, "y", true) == 0)
				{
					pt.y = float.Parse(node.Attributes[i].Value);
				}
			}

			return pt;
		}

		
		private bool m_bTerminating;
		public	bool m_bDebugging;
		public  int  m_iGestureID;

		private System.Threading.Thread				m_WorkerThread;
		private System.Threading.Mutex				m_Mutex;
		private System.Threading.ManualResetEvent	m_QueueNotEmpty;

		private GOMLib.GOM_Object_Sketch			m_curSketchObj;

		private Preprocessing.IStrokeRecognition	m_preprocess;
		private StrokeRefineLib.IStrokeRefinement	m_refine;

		private System.IO.StreamWriter			m_debugSketch;
		private System.IO.StreamWriter			m_debugStroke;
		private System.Collections.ArrayList	m_rgSketchObj;
		private System.Collections.ArrayList	m_rgSketch;

		public event PreprocessOverEvent		UpdateRequired;
	}
}
