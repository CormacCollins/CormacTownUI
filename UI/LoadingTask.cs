using System;
using System.Threading.Tasks;
using MyGame;

namespace MyGame
{
	public class LoadingTask
	{
		GetUserData _userData;
		
		public LoadingTask (GetUserData userdata)
		{
			_userData = userdata;
		}
		
		
		public async Task<EntityEnvironment> LoadGame ()
		{			
			 return await Task.Run(() => OpenSwinGameWindow(_userData));
		}
				
		private static async Task<EntityEnvironment> OpenSwinGameWindow(GetUserData userData)
		{
			PrepareData prepData = new PrepareData();
			prepData.GetData(userData);
			return prepData.GameData;
		}
	}
}

