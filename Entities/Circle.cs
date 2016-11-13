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
			SwinGame.DrawRectangle(Color.White, this.X - (this.Size/4) + this.Size, this.Y - (this.Size/4) + this.Size, this.Size/2, this.Size/2);
		}


        public override void Move(float deltaTime)
		{
            if (this.AnimationStatus == Animation.none)
            {
                UpdateSpeed();
            }
            else {
                this.Animate();
            }
        }
		
		public override void CheckCollisionScreen ()
		{
            Collision.LevelCollisionCircle(this);		
		}
		
		public override GameEntity CreateOffspring (GameEntity g)
		{			
			Circle newEntity = new Circle(this.X, this.Y,  this.EntityGenes.CombineGeneLists(g.EntityGenes));
			return (newEntity as GameEntity);
		}
	}
}

