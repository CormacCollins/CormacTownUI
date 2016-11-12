using System;
using SwinGameSDK;

namespace MyGame
{
	public class PrepareData
	{
		EntityEnvironment _gameData;
		public PrepareData(){
			
		}
		
		
		public void GetData (GetUserData userSelection)
		{
			//Ready to be populated with UI input
			_gameData = new EntityEnvironment();
			UnpackUserData getData = new UnpackUserData();
			
			//Load up entity environment with all the data requested by user
			getData.Unpack(userSelection, _gameData);
			
			SwinGame.OpenGraphicsWindow("GameMain", userSelection.SwinWindowSizeX, userSelection.SwinWindowSizeY); //Needs to get sizing from UI

		}

		public EntityEnvironment GameData
		{
			get
			{
				return _gameData;
			}
		}
	}
}

