using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SwinGameSDK;

namespace MyGame
{
    class Smiley : GameEntity, IGameObject
    {
        private Bitmap _b;

		public Smiley (){
		}
		
		
        public Smiley(float x, float y, GeneList geneList) : base(x, y, geneList)
		{      
			SetUpChildEnt();
        }
		
		
		
		public override void SetUpChildEnt ()
		{
			_b = SwinGame.LoadBitmap("smiley.png"); 
            this.Size = 20;
		}

        public override void Draw()
        {            
            SwinGame.DrawBitmap(_b, this.X, this.Y);     
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

        public override void CheckCollisionScreen()
        {
            Collision.LevelCollisionCircle(this);
        }

        public override GameEntity CreateOffspring(GameEntity g)
        {
            Smiley newEntity = new Smiley(this.X, this.Y, this.EntityGenes.CombineGeneLists(g.EntityGenes));
            newEntity.X = this.X;
            newEntity.Y = this.Y;
            return (newEntity as GameEntity);
        }
    }
}
