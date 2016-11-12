using System;
using SwinGameSDK;
using System.Windows.Forms; 

namespace MyGame
{
    public class EntityMain
    {
		private bool quitGame;
		
        public EntityMain (EntityEnvironment gameData)
		{
			//Flag for when button in game is pressed
			quitGame = false;
			
			UpdateGame gameUpdate = new UpdateGame (gameData);


			var gameTimer = SwinGame.CreateTimer ();
			gameTimer.Start ();

			while (false == SwinGame.WindowCloseRequested () || quitGame)
			{
				gameData.DeltaTime = gameTimer.Ticks;
              

				SwinGame.ProcessEvents ();
				SwinGame.ClearScreen (Color.White);

				
				gameUpdate.DrawEntities (gameData);


				SwinGame.DrawFramerate (0, 0);
				SwinGame.RefreshScreen (60);
				gameTimer.Reset ();
				
//				if (gameData.GameState == UIButtonFlags.quit)
//				{
//					quitGame = true;
//				}
            }
        }
    }
}