using System;

namespace MyGame
{
	public class GenerateRandomMovement
	{
		private float _randomY;
		private float _randomX;
		private float _randomSpeedX;
		private float _randomSpeedY;
		
		//Using Current time random generatino for x-direction, y-direction and speed
		public GenerateRandomMovement ()
		{
			DateTime d = DateTime.Now;	
			int time = d.Millisecond;
			Random rand = new Random (time);
			Random newRand = new Random (time);		
			Random newRand2 = new Random (time);
			Random newRand3 = new Random (time);
			float speedX = (float)(newRand2.Next (2, 4)*0.1);		
			float speedY = (float)(newRand3.Next (2, 4)*0.1);	
			//Random direction y
			int yRand = newRand.Next (1, 20);
			if (yRand < 10)
			{
				//Stay pos
			}
			else if (yRand > 10)
			{
				yRand = yRand * -1;
			}
			
			
			int xRand = newRand2.Next (1, 20);
			if (xRand > 10)
			{
				//Do nothing
			}
			else if (xRand < 10)
			{
				xRand = xRand * -1;
			}
			
			float y_Direction = ((yRand));			
			//Random direction x
			float x_Direction =  ((xRand));
			
			_randomX = x_Direction/200;
			_randomY = y_Direction/200;
			//_randomSpeedX = speedX/2;
			//_randomSpeedY = speedY/2;
		}

		public float RandomY{
			get{return _randomY;}
		}

		public float RandomX{
			get{return _randomX;}
		}

		public float RandomSpeedX{
			get{ return _randomSpeedX;}
		}

		public float RandomSpeedY{
			get{
				return _randomSpeedY;
			}
		}
	}
}

