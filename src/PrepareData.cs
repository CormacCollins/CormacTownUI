using System;
using SwinGameSDK;

namespace MyGame
{
	public class PrepareData
	{
		EntityEnvironment _gameData;		
		
		public void GetData (GetUserData userSelection){
			//Ready to be populated with UI input
			_gameData = new EntityEnvironment();
			UnpackUserData getData = new UnpackUserData();
			
			//Load up entity environment with all the data requested by user
			getData.Unpack(userSelection, _gameData);
			
			//Gets sizing from windows forms UI
			SwinGame.OpenGraphicsWindow("GameMain", userSelection.SwinWindowSizeX, userSelection.SwinWindowSizeY); 
		}

		public EntityEnvironment GameData{
			get{return _gameData;}
		}
	}
}

