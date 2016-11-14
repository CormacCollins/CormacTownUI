using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    static public class Collision
    {
		/// <summary>
		/// For checking entities against the screen outsides
		/// </summary>
		/// <param name="g">The green component.</param>
		static public void LevelCollision(GameEntity g)
        {
            if (((g.X + g.Size) > SwinGame.ScreenWidth() - 200) && (g.X + g.Size < (SwinGame.ScreenWidth() - 200) + 2)) {
                g.SpeedX = g.SpeedX * -1;
                g.X = g.X - 1;
            }

            if (((g.X) < 0) && (g.X > -2))
            {
                g.SpeedX = g.SpeedX * -1;
                g.X = g.X + 1;
            }

            if (((g.Y + g.Size) > SwinGame.ScreenHeight()) && (g.X + g.Size < SwinGame.ScreenHeight() + 2))
            {
                g.SpeedY = g.SpeedY * -1;
                g.Y = g.Y - 1;
            }
            if (((g.Y) < 0) && (g.Y > -2))
            {
                g.SpeedY = g.SpeedY * -1;
                g.Y = g.Y + 1;
            }
        }
    }
}
