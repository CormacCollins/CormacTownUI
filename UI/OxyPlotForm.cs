using System;
using System.Windows.Forms;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace MyGame
{
	public class OxyPlotForm : Form
	{
		private OxyPlot.WindowsForms.PlotView plot1;
		
		 public OxyPlotForm()
        {
           	var model = new PlotModel { Title = "ScatterSeries" };
			var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
			var r = new Random(314);
			for (int i = 0; i < 100; i++)
			{
			    var x = r.NextDouble();
			    var y = r.NextDouble();
			    var size = r.Next(5, 15);
			    var colorValue = r.Next(100, 1000);
			    scatterSeries.Points.Add(new ScatterPoint(x, y, size, colorValue));
			}
			
			model.Series.Add(scatterSeries);
			model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
        }
		
		
		public void InitializeComponent ()
		{
            this.plot1 = new OxyPlot.WindowsForms.PlotView();
            this.SuspendLayout();
            // 
            // plot1
            // 
            this.plot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plot1.Location = new System.Drawing.Point(0, 0);
            this.plot1.Name = "plot1";
            this.plot1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plot1.Size = new System.Drawing.Size(484, 312);
            this.plot1.TabIndex = 0;
            this.plot1.Text = "plot1";
            this.plot1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plot1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plot1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 312);
            this.Controls.Add(this.plot1);
            this.Name = "Form1";
            this.Text = "Example 1 (WindowsForms)";
            this.ResumeLayout(false);
			
		}
	}
}

