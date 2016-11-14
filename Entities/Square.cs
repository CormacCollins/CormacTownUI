using System;
using SwinGameSDK;

namespace MyGame
{
	public class Square : GameEntity, IGameObject
	{
        public Square()
        { }
			
		public Square (float x, float y, GeneList geneList) : base(x, y, geneList)
		{
            
		}
		
		public override void SetUpChildEnt ()
		{
			this.Size = 10;
		}

		public override void Draw()
		{
			SwinGame.FillRectangle(this.Color, this.X - (this.Size / 2), this.Y - (this.Size / 2), this.Size, this.Size);
			//If a mutation is in geneList then show the tag
			if (this.IsMutation)
			{
				DrawMutationTag();
			}
		}
		
		public void DrawMutationTag(){
			SwinGame.FillRectangle(Color.White, this.X - ((this.Size/2)/2) , this.Y -  ((this.Size/2)/2) , this.Size/2, this.Size/2);
		}

        public override void Move()
		{
            if (this.AnimationStatus == Animation.none)
            {
                UpdateSpeed();  
            }
            else
            {
                this.Animate();
            }
           
        }

    }
				
}

