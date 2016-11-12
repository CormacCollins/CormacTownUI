using System;
using SwinGameSDK;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{

    class InGameLabel
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;
        private string _text;
		private string _alternateText;
        private string _dataOutput;
        private Color _color;
        private Color _textColor;
		private UIButtonFlags _buttonFlag;
		private int _padding;
		

        public InGameLabel(float relativeX, float relativeY, float width, float height, string textString)
        {
			_padding = 0;
			_buttonFlag = UIButtonFlags.none;
            _x = relativeX;
            _y = relativeY;
            Width = width;
            Height = height;
            Color = Color.White;
            TextColor = Color.Black;
            Text = textString;
            //default text clr
        }

		public int Padding
		{
			get
			{
				return _padding;
			}
			set
			{
				_padding = value;
			}
		}

		public UIButtonFlags ButtonFlag
		{
			get
			{
				return _buttonFlag;
			}
			
			set
			{
				_buttonFlag = value;
			}
		}

		public string AlternateText
		{
			get
			{
				return _alternateText;
			}
			set
			{
				_alternateText = value;
			}
		}

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
		

        public string DataOutput
        {
            get { return _dataOutput; }
            set { _dataOutput = value; }
        }

        public virtual void DrawLabel()
        {
            SwinGame.FillRectangle(Color, X, Y, (int)Width, (int)Height);
            SwinGame.DrawText(Text + _dataOutput, TextColor, (X + 5F) + _padding, _y + (Height / 2));
        }

        public void ChangeTextColor(Color clr)
        {
            TextColor = clr;
        }

        public void SetLabelColorRGBA(byte r, byte g, byte b, byte alpha)
        {
            Color = SwinGame.RGBAColor(r, g, b, alpha);
        }

        public bool IsAt(float x, float y) {
            //Check X/Y Positions are withing this labels range
            if((X > x) || (x > (X + Width)))
            {
                return false;
            }

            if ((Y > y) || (y > (Y + Height))) {
                return false;
            }
            return true;
        }
		
		
		

    }
    
}
