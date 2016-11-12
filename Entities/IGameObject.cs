using System;
using System.Collections.Generic;

namespace MyGame
{
	public interface IGameObject
	{		
		//Any object within game needs to CheckCollision, know where 'IsAt', and Draw.
		bool IsAt(float x, float y);
		void Draw();
		
		float X{
			get;
			set;
		}
		float Y{
			get;
			set;
		}
	}
	
}

