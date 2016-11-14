using System;
using SwinGameSDK;

namespace MyGame
{
	public class Circle : GameEntity, IGameObject
	{
        public Circle()
        {
        }
			
		public Circle (float x, float y, GeneList geneList) : base( x, y, geneList)
		{
			SetUpChildEnt();
		}
		
		public override void SetUpChildEnt ()
		{
		}
		
		public override void Draw ()
		{			
			
			SwinGame.FillCircle(this.Color, this.X, this.Y, this.Size);
			if (this.IsMutation)
			{
				DrawMutationTag();
			}
		}
		public void DrawMutationTag(){
			SwinGame.FillRectangle(Color.White, this.X - ((this.Size/2)/2), this.Y - ((this.Size/2)/2) , this.Size/2, this.Size/2);
		}


        public override void Move()
		{
            if (this.AnimationStatus == Animation.none)
            {
                UpdateSpeed();
            }
            else {
                this.Animate();
            }
        }
		

	}
}

