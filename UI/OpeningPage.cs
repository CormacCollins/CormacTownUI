using System;

using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;


namespace MyGame
{
	public class OpeningPage : CormacTownForm
	{
        private GetUserData _userData;

		public OpeningPage (GetUserData userData)
		{
            _userData = userData;
			this.ReturnType = PageType.Open;
			// --------------- Create picture box and load from file ---------------- //
			PictureBox pictureBox1 = new PictureBox ();			
			pictureBox1.ImageLocation = @"C:\Users\corma\Desktop\CormacTownJoint\Resources\images\logo.png
";
			//Loaded first before window
			
			const int logoWidth = 307;
			this.Size = new Size (800, 600);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.Fixed3D;
			this.BackColor = Color.FromArgb(123, 104, 238);
			
			//Size up picture box
			pictureBox1.Size = new System.Drawing.Size(300, 200);
			pictureBox1.Location = new Point(this.Width/2 - (logoWidth/2), 70);

			
			// ------------------ Title label --------------------------------------- //
			CormacTownLabel titleLabel = new CormacTownLabel(24);
			titleLabel.Text = "Cormac Town";
			titleLabel.Location = new Point(this.Width/2 - (titleLabel.Width/3), 20);
			
			
			CormacTownButton button1 = new CormacTownButton ();
			button1.Text = "START";
			button1.Location = new Point (this.Width/2 - (button1.Width/2), this.Height - 200);			
			button1.Click += new EventHandler (OnClick);  //Event handler is the dleegate and been given particular implementaitotno of this method
			
			CormacTownButton button2 = new CormacTownButton();
			button2.Text = "STATISTICS";
			button2.Width = 200;
			button2.Location  = new Point (this.Width/2 - (button2.Width/2), this.Height - 150);	
			
			
			
			
			this.AcceptButton = button1;
			this.Controls.Add (button1);
			
			this.AcceptButton = button2;
			this.Controls.Add (button2);
			
			this.Controls.Add(titleLabel);
			this.Controls.Add(pictureBox1);			
			
			this.ShowDialog ();
	
		}
		
		public void OnClick(object sender, EventArgs e)
		{			
			//Create new page, hide the old one
			//Set new page to the window position of the old page.
			SelectionPage s = new SelectionPage(_userData);
			s.StartPosition = FormStartPosition.Manual;
			s.Left = this.Left;
			s.Top = this.Top;
			this.Visible = false;
			s.ShowDialog();
			this.Close();
		}
    }
}

