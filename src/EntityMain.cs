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
			gameData.GameOver = false;
			
			
			UpdateGame gameUpdate = new UpdateGame (gameData);


			var gameTimer = SwinGame.CreateTimer ();
			gameTimer.Start ();
			
			//SwinGame.ToggleWindowBorder();
			

			while (false == SwinGame.WindowCloseRequested ())
			{
				gameData.DeltaTime = gameTimer.Ticks;
              	

				SwinGame.ProcessEvents ();
				SwinGame.ClearScreen (SwinGame.RGBAColor (224, 224, 224, 80));

				
				gameUpdate.DrawEntities (gameData);


				SwinGame.DrawFramerate (0, 0);
				SwinGame.RefreshScreen (60);
				gameTimer.Reset ();
				
				if (gameData.GameOver){
					break;
				}
            }
			
        }
    }
}