using System;
using SwinGameSDK;
using System.Windows.Forms; 

namespace MyGame
{
    public class EntityMain
    {
		//Game Data 'put' into main from Windows Forms UI
        public EntityMain (EntityEnvironment gameData)
		{
			//Flag for when button in game is pressed
			gameData.GameOver = false;
			
			
			UpdateGame gameUpdate = new UpdateGame (gameData);


			while (false == SwinGame.WindowCloseRequested ())
			{
				SwinGame.ProcessEvents ();
				SwinGame.ClearScreen (SwinGame.RGBAColor (224, 224, 224, 80));
				
				gameUpdate.DrawEntities (gameData);


				SwinGame.DrawFramerate (0, 0);
				SwinGame.RefreshScreen (60);
				
				if (gameData.GameOver){
					break;
				}
            }			
        }
    }
}