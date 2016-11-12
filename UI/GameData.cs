using System;

namespace MyGame
{
	public struct GameData
	{
		Tuple<string, int> trait1;
		Tuple<string, int> trait2;
		int[] _colorRgbArray;
		string shapeType;
		
		public GameData(Tuple<string, int> t1, Tuple<string, int> t2, int[] intArr, string s){
			trait1 = t1;
			trait2 = t2;
			_colorRgbArray = intArr;		
			shapeType = s;
		}

		public Tuple<string, int> Trait1{
			get{return trait1;}
			set{trait1 = value;}
		}

		public Tuple<string, int> Trait2{
			get{return trait2;}
			set{trait2 = value;}
		}

		public int[] ColorRgbArray{
			get{return _colorRgbArray;}
		}

		public string ShapeType{
			get{return shapeType;}
			set{shapeType = value;}
		}

	}
}

