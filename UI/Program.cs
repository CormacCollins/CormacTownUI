using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
	class MainClass
	{
		
		public static void Main ()
		{
		 	GetUserData userData = new GetUserData();
			OpeningPage n = new OpeningPage(userData);			
		}
	}
}
