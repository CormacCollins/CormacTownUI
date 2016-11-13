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
			this.Size = 15;
		}

		public override void Draw()
		{
			SwinGame.FillRectangle(this.Color, this.X - (this.Size / 2), this.Y - (this.Size / 2), this.Size, this.Size);
			if (this.IsMutation)
			{
				DrawMutationTag();
			}
		}
		
		public void DrawMutationTag(){
			SwinGame.DrawRectangle(Color.White, this.X + (this.Size/4), this.Y + (this.Size/4), this.Size/2, this.Size/2);
		}

        public override void Move(float deltaTime)
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
		
		public override void CheckCollisionScreen ()
		{
            Collision.LevelCollisionSquare(this);	
		}
		
		public override GameEntity CreateOffspring (GameEntity g)
		{	
			Square newEntity = new Square (this.X, this.Y, this.EntityGenes.CombineGeneLists (g.EntityGenes));
			return (newEntity as GameEntity);			
		}


    }
				
}

