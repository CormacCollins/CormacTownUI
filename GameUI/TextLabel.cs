using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class TextLabel : InGameLabel
    {
        private string _gameInfoText;

        public TextLabel(float relativeX, float relativeY, float width, float height, string textString, string gameInfo) : base(
                                                                       relativeX, relativeY, width, height, textString)
        {
            _gameInfoText = gameInfo;
        }

        //public override void DrawLabel()
        //{

         //   SwinGame.FillRectangle(this.Color, this.X, this.Y, this.Width, this.Height);
        //    SwinGame.DrawText(this.Text + _gameInfoText , this.TextColor, this.X + 5F, this.Y + (this.Height / 2));
       // }

        


    }
}
