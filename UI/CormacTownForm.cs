using System;
using System.Windows.Forms;

namespace MyGame
{
	public class CormacTownForm : Form
	{
		
		private PageType _type;

		public PageType ReturnType {
			get {
				return _type;
			}
			set {
				_type = value;
			}
		}
		
		public CormacTownForm ()
		{
		}
	}
}

