using System;
using System.Collections.Generic;

namespace MyGame
{
	public class GetUserData
	{
		List<GameData> _userInputList;
		
		int _swinWindowSizeX;
		int _swinWindowSizeY;
		// ----------------------- NEED TO PASS IN EVENT WHICH WILL LOAD SWINGAME FROM FORM -------------------------------- //
		
		
		public GetUserData ()
		{			
			_userInputList = new List<GameData>();		
		}
		
		//Shape type, traits added with string reference
		public void AddDataEntity(string s, int a, int b, int[] rgbArray)
		{
			GameData geneTuple = new GameData(new Tuple<string, int> ("Attractiveness", a), 
				new Tuple<string, int>("Fitness", b), rgbArray, s);
			
			_userInputList.Add(geneTuple);
		}
		
		public int SwinWindowSizeX{
			get{return _swinWindowSizeX;}
			set{_swinWindowSizeX = value;}
		}

		public int SwinWindowSizeY{
			get{return _swinWindowSizeY;}
			set{_swinWindowSizeY = value;}
		}		
		
		public List<GameData> UserInputList{
			get{return _userInputList;}
			set{_userInputList = value;}
		}
		
		
	}
}

