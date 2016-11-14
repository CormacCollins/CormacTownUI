using System;

using System.Windows.Forms;
using System.Threading.Tasks;
using System.Timers;
using SwinGameSDK;
using System.Drawing;

namespace MyGame
{
	public class SelectionPage : CormacTownForm
	{
		private CormacTownLabel _label1;
		private CormacTownLabel _label2;
		private CormacTownLabel _ColorLabel1;
		private CormacTownLabel _ColorLabel2;
		private CormacTownLabel _ColorLabel3;
		//private CormacTownLabel _label4;
		//private CormacTownLabel _label5;
		private int _shapesXstartPos;
		private int _shapesYStartPos;
		private bool _fullShapeList;
		//private bool _checked;
		private GetUserData _userData;
		private int _AttractivenssValue;
		private int _FitnessValue;
		private int _ColorValue1;
		private int _ColorValue2;
		private int _ColorValue3;
		private ProgressBar _progressBar;


		
		public SelectionPage (GetUserData userData)
		{
				
			
			// --------------------- To transfer all user selections to populate the game -------------------------- //
            _userData = userData;		

			//Distance from Max for placing the shapes when the user selects them
			_shapesXstartPos = 400;
			_shapesYStartPos = 460;
			
			
			
			
			// ------ Identical Form type -------------- //
			//------- Populate with same stats ---------//			
			this.Size = new System.Drawing.Size (800, 600);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.Fixed3D;
			this.BackColor = System.Drawing.Color.White;
			
			//prepare progress bar - to be added when 'Start' button clicked
			_progressBar = new ProgressBar();
			_progressBar.Width = 400;
			_progressBar.Height = 40;
			_progressBar.Location = new System.Drawing.Point(this.Left+(this.Width/2)-  (_progressBar.Width/2), this.Top + (this.Height/2));
			_progressBar.Maximum = 30000;
			_progressBar.Step = 1;
			
			//Underlay//
			CormacTownLabel underlay = new CormacTownLabel(24);
			underlay.Width = this.Width;
			underlay.Height = 100;
			underlay.BackColor = System.Drawing.Color.Black;
			underlay.Location = new System.Drawing.Point(0, 0);
			
			
			// ------- Heading ----------- //
			CormacTownLabel titleLabel = new CormacTownLabel(24);
			titleLabel.Text = "Cormac Town";
			
			titleLabel.BackColor = System.Drawing.Color.Black;
			titleLabel.Location = new System.Drawing.Point((0 + this.Width/2) - (titleLabel.Width) + 200, 20);
			
			
			//------------ Instruction ----------- //
			CormacTownLabel instructionLabel = new CormacTownLabel(10);
			instructionLabel.Width = 500;
			instructionLabel.Height = 50;
			instructionLabel.BackColor = System.Drawing.Color.Black;
			instructionLabel.Width = this.Width;
			//instructionLabel.Font = new System.Drawing.Font("Calibri", 10);
			instructionLabel.Text = "Modify the type of your shape: Attractiveneess, Fitness & RGB color.\nThen select the entity type Triangle, Circle, Square.";
			instructionLabel.Location = new System.Drawing.Point(0, 75);
			
						
			// --------------- Start label ---------------------------- //
			CormacTownButton startButton = new CormacTownButton();
			startButton.Location = new System.Drawing.Point((this.Width - 200), (this.Height - 100));
			startButton.Text = "Start Game";
			startButton.Click += new EventHandler(OnStartClick);
				
			
			
			// -------------- Attractiveness ------------------- //	
			TrackBar trackBar1 = new TrackBar();
			trackBar1.BackColor =  System.Drawing.Color.FromArgb(0, 0, 0);
			trackBar1.Orientation = Orientation.Horizontal;
			trackBar1.SetRange(1, 100);
			trackBar1.Value = 50;
			_AttractivenssValue = trackBar1.Value;
			trackBar1.Location = new System.Drawing.Point(10, 160);
			trackBar1.Scroll += new EventHandler(trackBar1_Scroll);
			
			// -------------- Attractiveness Label-------------- //	
			_label1 = new CormacTownLabel(12); //Smaller font
			_label1.Height = 30;
			_label1.Width = 190;
			_label1.ForeColor = System.Drawing.Color.Black;
			_label1.Text = "Attractiveness: " + trackBar1.Value.ToString();
			_label1.Location = new System.Drawing.Point(120, 160);
			
			// -------------- Fitness ------------------- //
			
			TrackBar trackBar2 = new TrackBar();
			trackBar2.BackColor =  System.Drawing.Color.FromArgb(0, 0, 0);
			trackBar2.Orientation = Orientation.Horizontal;
			trackBar2.SetRange(1, 100);
			trackBar2.Value = 50;
			_FitnessValue = trackBar2.Value;
			trackBar2.Location = new System.Drawing.Point(10, 210);  			
			trackBar2.Scroll += new EventHandler(trackBar2_Scroll);		
			
			// -------------- Fitness Label --------------- //	
			
			_label2 = new CormacTownLabel(12); //Smaller font
			_label2.Height = 30;
			_label2.Width = 150;
			_label2.ForeColor = System.Drawing.Color.Black;
			_label2.Text = "Fitness: " + trackBar2.Value.ToString();
			_label2.Location = new System.Drawing.Point(120, 210);
			
			// -------------- Color ------------------- //		
			TrackBar trackBar3Color1 = new TrackBar();
			trackBar3Color1.BackColor = System.Drawing.Color.FromArgb(255, 4, 4);
			trackBar3Color1.Orientation = Orientation.Horizontal;
			trackBar3Color1.SetRange(1, 255);
			trackBar3Color1.Name = "Red";
			trackBar3Color1.Value = 35;
			trackBar3Color1.Width = 100;
			_ColorValue1 = trackBar3Color1.Value;
			trackBar3Color1.Location = new System.Drawing.Point(10, 260);				
			trackBar3Color1.Scroll += new EventHandler(trackBar3_Scroll);
			
			TrackBar trackBar3Color2 = new TrackBar();
			trackBar3Color2.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
			trackBar3Color2.Orientation = Orientation.Horizontal;
			trackBar3Color2.SetRange(1, 255);
			trackBar3Color2.Name = "Green";
			trackBar3Color2.Value = 15;
			trackBar3Color2.Width = 100;
			_ColorValue2 = trackBar3Color2.Value;
			trackBar3Color2.Location = new System.Drawing.Point(10, 310);				
			trackBar3Color2.Scroll += new EventHandler(trackBar3_Scroll);
			
			TrackBar trackBar3Color3 = new TrackBar();
			trackBar3Color3.BackColor = System.Drawing.Color.FromArgb(0, 0, 255);
			trackBar3Color3.Orientation = Orientation.Horizontal;
			trackBar3Color3.SetRange(1, 255);
			trackBar3Color3.Name = "Blue";
			trackBar3Color3.Value = 25;
			trackBar3Color3.Width = 100;
			_ColorValue3 = trackBar3Color3.Value;
			trackBar3Color3.Location = new System.Drawing.Point(10, 360);				
			trackBar3Color3.Scroll += new EventHandler(trackBar3_Scroll);
			
			
			// ---------------- RGB LABELS ------------ //
			//RGB value 1 //
			_ColorLabel1 = new CormacTownLabel(12); //Smaller font
			_ColorLabel1.Height = 30;
			_ColorLabel1.Width = 100;
			_ColorLabel1.ForeColor = System.Drawing.Color.Black;
			_ColorLabel1.Text = "Red: " + _ColorValue1.ToString();
			_ColorLabel1.Location = new System.Drawing.Point(120, 260);
			
			//RGB value 2//
			 _ColorLabel2 = new CormacTownLabel(12); //Smaller font
			_ColorLabel2.Height = 30;
			_ColorLabel2.Width = 120;
			_ColorLabel2.ForeColor = System.Drawing.Color.Black;
			_ColorLabel2.Text = "Green: " + _ColorValue2.ToString();
			_ColorLabel2.Location = new System.Drawing.Point(120, 310);
			
			
		    //RGB value 3//
			 _ColorLabel3 = new CormacTownLabel(12); //Smaller font
			_ColorLabel3.Height = 30;
			_ColorLabel3.Width = 100;
			_ColorLabel3.ForeColor = System.Drawing.Color.Black;
			_ColorLabel3.Text = "Blue: " + _ColorValue3.ToString();
			_ColorLabel3.Location = new System.Drawing.Point(120, 360);
			

			// ---------------- Triangle Button -------------------------- //
			CormacTownButton triangleEntityButton = new CormacTownButton();
			triangleEntityButton.Location = new System.Drawing.Point(15, this.Height - 100);
			triangleEntityButton.Text = "Triangle";	
			triangleEntityButton.Name = "Triangle";
			triangleEntityButton.Font = new System.Drawing.Font("Broadway", 10);
			triangleEntityButton.Width = 100;
			triangleEntityButton.Height = 40;
			triangleEntityButton.Click += new EventHandler (OnClickSHape);  //Event handler is the dleegate and been given particular implementaitotno of this method
			
			// ---------------- Circle Button -------------------------- //
			CormacTownButton circleEntityButton = new CormacTownButton();
			circleEntityButton.Location = new System.Drawing.Point(120, this.Height - 100);
			circleEntityButton.Text = "Circle";	
			circleEntityButton.Name = "Circle";
			circleEntityButton.Font = new System.Drawing.Font("Broadway", 10);
			circleEntityButton.Width = 100;
			circleEntityButton.Height = 40;
			circleEntityButton.Click += new EventHandler (OnClickSHape);  //Event handler is the dleegate and been given particular implementaitotno of this method
			
			// ---------------- Square Button -------------------------- //
			CormacTownButton squareEntityButton = new CormacTownButton();
			squareEntityButton.Location = new System.Drawing.Point(225, this.Height - 100);
			squareEntityButton.Text = "Square";	
			squareEntityButton.Name = "Square";
			squareEntityButton.Font = new System.Drawing.Font("Broadway", 10);
			squareEntityButton.Width = 100;
			squareEntityButton.Height = 40;
			squareEntityButton.Click += new EventHandler (OnClickSHape);  //Event handler is the dleegate and been given particular implementaitotno of this method

			
			
			
			this.Controls.Add(titleLabel);
			this.Controls.Add (_label1);
			this.Controls.Add (_label2);
			this.Controls.Add (_ColorLabel1);
			this.Controls.Add (_ColorLabel2);
			this.Controls.Add (_ColorLabel3);
			this.Controls.Add(startButton);
			
			this.Controls.Add (trackBar1);
			this.Controls.Add (trackBar2);
			this.Controls.Add (trackBar3Color1);
			this.Controls.Add (trackBar3Color2);
			this.Controls.Add (trackBar3Color3);
			
			
			this.Controls.Add (squareEntityButton);
			this.Controls.Add(circleEntityButton);
			this.Controls.Add(triangleEntityButton);
			this.Controls.Add(instructionLabel);
			this.Controls.Add(underlay);
			//this.ShowDialog ();
	
		}
		
		private void trackBar1_Scroll(object sender, System.EventArgs e)
	    {
	        // Display the trackbar value in the text box.
			_label1.Text = "Attractiveness: " + (sender as TrackBar).Value;
			_AttractivenssValue = (sender as TrackBar).Value;
	    }
		
		private void trackBar2_Scroll(object sender, System.EventArgs e)
	    {
	        // Display the trackbar value in the text box.
			_label2.Text = "Fitness: " + (sender as TrackBar).Value;
			_FitnessValue = (sender as TrackBar).Value;
	    }
		private void trackBar3_Scroll(object sender, System.EventArgs e)
	    {
	        // Display the trackbar value in the text box.
			
			
			
			string s = (sender as TrackBar).Name;
			switch(s){
			case ("Red"): _ColorValue1  =  (sender as TrackBar).Value;
									  _ColorLabel1.Text = "Red: " + (sender as TrackBar).Value;
				break;
			case ("Green"): _ColorValue2 =  (sender as TrackBar).Value;
				                      _ColorLabel2.Text = "Green: " + (sender as TrackBar).Value;
				break;
			case ("Blue"): _ColorValue3 =  (sender as TrackBar).Value;
				                      _ColorLabel3.Text = "Blue: " + (sender as TrackBar).Value;
				break;
			default: break;				
			}
			
	    }
		
		private void OnClickSHape (object sender, EventArgs e)
		{	
			
			if (_fullShapeList) {
				return;
			}
			
			Button sendingButton = (Button)sender;
			
			System.Drawing.Image shapeImage = System.Drawing.Image.FromFile (@"C:/Users/corma/Documents/Code/C\OOP/DemoForms/DemoForms/square.png");
			string s = sendingButton.Text;
			switch (s) {
			case ("Square"):
				shapeImage = System.Drawing.Image.FromFile (@"C:\Users\corma\Desktop\CormacTownJoint\Resources\images\square.png");
				break;
			case ("Circle"):
				shapeImage = System.Drawing.Image.FromFile (@"C:\Users\corma\Desktop\CormacTownJoint\Resources\images\circle.png");
				break;
			case ("Triangle"):
				shapeImage = System.Drawing.Image.FromFile (@"C:\Users\corma\Desktop\CormacTownJoint\Resources\images\triangle.png");
				break;
			default: shapeImage = null;
				break;
			}
			
			
			
			PictureBox pictureBox1 = new PictureBox ();	
			pictureBox1.Width = shapeImage.Width;
			pictureBox1.Height = shapeImage.Height;
			pictureBox1.Location = new System.Drawing.Point (this.Width - _shapesXstartPos, this.Height - _shapesYStartPos);
			pictureBox1.Image = shapeImage;
			this.Controls.Add (pictureBox1);
			
			_shapesXstartPos -= (shapeImage.Width + 5);
			
			//If x range gets close to edge of screen we reset it and move the Y value down
			if (_shapesXstartPos < 50) {
				_shapesXstartPos = 400; 
				_shapesYStartPos -= shapeImage.Height + 10;
			}
			
			
			if(_shapesYStartPos < 150)
			{
					_fullShapeList = true;	
			}
			
			_userData.AddDataEntity(s, _AttractivenssValue, _FitnessValue, new int[] {_ColorValue1, _ColorValue2, _ColorValue3});
			
		}
		
		private void OnStartClick (object sender, EventArgs e)
		{			
			
			//Also need to match up swinWindow size with this windows size	
			_userData.SwinWindowSizeX = this.Width;
			_userData.SwinWindowSizeY = this.Height;
			
			this.Controls.Add (_progressBar);
			_progressBar.BringToFront ();
			
			PrepareData prepData = new PrepareData ();
			RunTimer (_progressBar);
			
			prepData.GetData (_userData);			
			
			this.Visible = false;

			EntityEnvironment environ = prepData.GameData;
			
			EntityMain newGame = new EntityMain (environ);
		}
		
		private void RunTimer(ProgressBar b)
		{
			TimeSpan time = new TimeSpan (50000000); //10 seconds
			DateTime newTime = DateTime.Now;
			var timeDiff = newTime + time;
			
			bool breakTimer = false;
			
			while (b.Value < 30000)
			{
				
//				DateTime currTime = DateTime.Now;
//				var diff = currTime.Subtract (timeDiff);
//				if (diff.TotalMilliseconds > 0)
//				{
//					//b.Value = 6000;
//					breakTimer = true;
//				}
				b.PerformStep();
			}
			//return breakTimer;
		}
			

		public void CloseForm ()
		{
			this.Visible = false;
			Close ();
		}
	}
}

