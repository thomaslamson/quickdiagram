using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuickDiagramUI
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class OutputForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.OpenFileDialog openFileDialog2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public OutputForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(8, 9);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(280, 391);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "richTextBox1";
			this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
			// 
			// OutputForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
			this.ClientSize = new System.Drawing.Size(296, 406);
			this.Controls.Add(this.richTextBox1);
			this.Name = "OutputForm";
			this.Text = "OutputForm";
			this.Load += new System.EventHandler(this.Form2_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Form2_Load(object sender, System.EventArgs e)
		{
			
		}
		public void B1Action(EEDomain.ReadFromXml r)
		{
			
			//EEDomain.ReadFromXml r = new EEDomain.ReadFromXml(openFileDialog2.FileName);
			EEDomain.ListEquation l = new EEDomain.ListEquation(r);
			richTextBox1.Text = (l.Output());
				
		}
		public void B2Action(EEDomain.ReadFromXml r)
		{
			
			//EEDomain.ReadFromXml r = new EEDomain.ReadFromXml(openFileDialog2.FileName);
			EEDomain.CalValue c = new EEDomain.CalValue(r);
			richTextBox1.Text = (c.Output());
				
		}
		public void B3Action(EEDomain.ReadFromXml r)
		{
			
			//EEDomain.ReadFromXml r = new EEDomain.ReadFromXml(openFileDialog2.FileName);
			EEDomain.GenCode g = new EEDomain.GenCode(r);
			richTextBox1.Text = (g.Output());
				
		}
		public void B4Action(EEDomain.ReadFromXml r)
		{			
			ArrayList dList = r.getDeviceList();
			IEnumerator dEnum = dList.GetEnumerator();
			while(dEnum.MoveNext())
			{
				if(((EEDomain.Device)dEnum.Current).GetType().Equals("EEDomain.VsourceDC"))
				{
					//((EEDomain.VsourceDC)dEnum.Current).SetVoltage();
				}
				if(((EEDomain.Device)dEnum.Current).GetType().Equals("EEDomain.Resistor"))
				{
					//((EEDomain.Resistor)dEnum.Current).SetResistance();
				}
			}
				
		}
		private void richTextBox1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
