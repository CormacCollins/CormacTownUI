using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
	public class CormacTownButton : Button
	{
		public CormacTownButton () 
		{
			this.BackColor = Color.Black;
			this.Font = new Font("Broadway", 16);
			this.Width = 160;
			this.Height = 40;
			this.ForeColor = System.Drawing.Color.White;
		}
	}
	
	public class CormacTownLabel : Label
	{
		public CormacTownLabel (int fontSize) 
		{
			this.Font = new Font("Broadway", fontSize);
			this.Width = 350;
			this.Height = 40;
			this.ForeColor = System.Drawing.Color.White;
		}
	}
}

