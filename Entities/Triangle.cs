using System;
using SwinGameSDK;

namespace MyGame
{
	public class Triangle : GameEntity, IGameObject
	{
		//More accessible than Point2D
		private float _pointAx;
		private float _pointAy;
		private float _pointBx;
		private float _pointBy;
		private float _pointCx;
		private float _pointCy;

        public Triangle()
        { }
		
		public Triangle (float x, float y, GeneList geneList) : base(x, y, geneList)
		{
            SetUpChildEnt();
		}
		
		public override void SetUpChildEnt ()
		{
			this.Size = 20;
            //Populate triangle outer points of the middle x - y coodinates
            _pointAx = X - this.Size;
            _pointBx = X + this.Size;
            _pointCx = X;
            _pointAy = Y + this.Size;
            _pointBy = Y + this.Size;
            _pointCy = Y - this.Size;


           // this.EntityGenes = geneList;
		}

		public float PointAx
		{
			get{return _pointAx;}
			set{_pointAx = value;}
		}

		public float PointAy
		{
			get{return _pointAy;}
			set{_pointAy = value;}
		}

		public float PointBx
		{
			get{return _pointBx;}
			set{_pointBx = value;}
		}

		public float PointBy
		{
			get{return _pointBy;}
			set{_pointBy = value;}
		}

		public float PointCx
		{
			get{return _pointCx;}
			set{_pointCx = value;}
		}

		public float PointCy
		{
			get{return _pointCy;}
			set{_pointCy = value;}
		}

        public override void Draw()
		{
            CalculatePoints();
			SwinGame.FillTriangle(this.Color, this.PointAx, this.PointAy, this.PointBx, this.PointBy, this.PointCx, this.PointCy);
        }

        public override void DrawOutline()
        {
            CalculatePoints();
            //Color randColor = this.RandomizeColor();
            SwinGame.DrawTriangle(Color.Black, this.PointAx - 1, this.PointAy + 1, this.PointBx + 1, this.PointBy + 1, this.PointCx, this.PointCy - 1);
            SwinGame.DrawTriangle(Color.Black, this.PointAx - 2, this.PointAy + 2, this.PointBx + 2, this.PointBy + 2, this.PointCx, this.PointCy - 2);
        }

        private void CalculatePoints()
        {
            _pointAx = this.X - this.Size;
            _pointBx = this.X + this.Size;
            _pointCx = this.X;
            _pointAy = this.Y + this.Size;
            _pointBy = this.Y + this.Size;
            _pointCy = this.Y - this.Size;
        }
		
		public override void Move(float deltaTime)
		{
            if (this.AnimationStatus == Animation.none)
            {
                UpdateSpeed();
                CalculatePoints();
            }
            else
            {
                this.Animate();
            }
            
		}
		
		
		
		public override void CheckCollisionScreen ()
		{
            Collision.LevelCollisionTriangle(this);
		}
		
		public override GameEntity CreateOffspring (GameEntity g)
		{			
			Triangle newEntity = new Triangle(this.X, this.Y,  this.EntityGenes.CombineGeneLists(g.EntityGenes));
			newEntity.X = this.X;
			newEntity.Y = this.Y;
			return (newEntity as GameEntity);
		}
	}
}

