using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class ShowInfoBox
    {
        private List<string> _outputString;
        private List<float> _outputData;
		private Color _boxColor;
        private List<InGameLabel> _labels;
		private UIButtonFlags _gameState;

        private GameEntity _entityDisplayed;
        private float _entityDisplayedX;
        private float _entityDisplayedY;
        //positions
        private float _x;
        private float _y;
        private float _width;
        private float _height;

		public Color BoxColor
		{
			get
			{
				return _boxColor;
			}
			set
			{
				_boxColor = value;
			}
		}

		public UIButtonFlags GameState
		{
			get
			{
				return _gameState;
			}
			set
			{
				_gameState = value;
			}
		}

        public float Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        public float EntityDisplayedX
        {
            get
            {
                return _entityDisplayedX;
            }

            set
            {
                _entityDisplayedX = value;
            }
        }

        public float EntityDisplayedY
        {
            get
            {
                return _entityDisplayedY;
            }

            set
            {
                _entityDisplayedY = value;
            }
        }

		public ShowInfoBox(float x, float y, float width, float height)
        {
			_gameState = UIButtonFlags.none;
            _labels = new List<InGameLabel>();

            _x = x;
            _y = y;
            Width = width;
            _height = height;
            _outputData = new List<float>();
            _outputString = new List<string>();
            //Default clr
            _boxColor = SwinGame.RGBAColor(123, 104, 238, 50); //50 opacity?
            EntityDisplayedX = 0;
            EntityDisplayedY = 0;
        }

        public void AddInformation(string labelName, string envInfo)
        {
            foreach (InGameLabel l in _labels)
            {
                if (l.Text.Contains(labelName))
                {
                    l.DataOutput = envInfo;
                }
            }
        }
        

        public void SetEntity(GameEntity g, float x, float y)
        {
            if(g == null){
                return;
            }
            _entityDisplayed = g;
            _entityDisplayed.X = _width/2 + _x;
            _entityDisplayed.Y = _entityDisplayedY;
        }

        public void Draw ()
		{
			SwinGame.FillRectangle (_boxColor, _x, _y, (int)Width, (int)_height);
			foreach (InGameLabel l in _labels)
			{                
				l.DrawLabel ();
            
				if (l.IsAt (SwinGame.MousePosition().X , SwinGame.MousePosition().Y) && SwinGame.MouseClicked(MouseButton.LeftButton))
				{
					_gameState = l.ButtonFlag;
					ButtonCase(l);
				}
            }

            if (_entityDisplayed != null)
            {
                _entityDisplayed.Draw();
                //_entityDisplayed.DrawOutline();
            }
			
			
        }

        public void AddLabel(InGameLabel l)
        {
            l.X = l.X + _x;
			l.Y = l.Y + _y;
            _labels.Add(l);
        }
		
		public void ButtonCase (InGameLabel l)
		{
			switch (l.ButtonFlag)
			{
				case(UIButtonFlags.resume): l.ButtonFlag = UIButtonFlags.pause;
					l.Text = "PAUSE";
					break;
				case(UIButtonFlags.pause): l.ButtonFlag = UIButtonFlags.resume;
					l.Text = "RESUME";				
					break;
				case(UIButtonFlags.quit): _gameState = UIButtonFlags.quit;
						break;
				case(UIButtonFlags.finish): _gameState = UIButtonFlags.finish;
				break;
				default: break;	
			}			
		}
    }



}

