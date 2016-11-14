using System;

namespace MyGame
{
	public class GenerateRandomMovement
	{
		private float _randomY;
		private float _randomX;
		
		/// <summary>
		/// Created a random weighted X and Y value for random movement directions
		/// </summary>
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
			
			_randomX = x_Direction/50;
			_randomY = y_Direction/50;
		}

		public float RandomY{
			get{return _randomY;}
		}

		public float RandomX{
			get{return _randomX;}
		}

	}
}

