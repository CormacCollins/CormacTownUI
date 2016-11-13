using System;
using SwinGameSDK;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MyGame
{

    public abstract class GameEntity 
    {
        //Event created for detecting 'mating'

		private Color _color;
		private GeneList _entityGenes;
		private float _x;
		private float _y;
		private float _speedX;
		private float _speedY;
		private GenerateRandomMovement _randGenerator;
		private bool _canGiveBirth;
		private DateTime _time;
        private int _life;
        private int _ID;
        private float _size;
        private Animation _animationStatus;
        private bool _isSelected;
		private bool _isDead;
		private bool _isMutation;

        public GameEntity() {
            //Parameterless Constructor for Reflection
        }

		public GameEntity(float x, float y, GeneList geneList) 
		{
			PrepareEntity(x,  y, geneList);
		}
		
		public void PrepareEntity (float x, float y, GeneList geneList)
		{
            //Want genes to be 'normal' or standard for the initial shape instantiation
            _life = 100;
			_isDead = false;
			_size = 10;
			_entityGenes = geneList;
			_isMutation = geneList.ContainsMutation;
			_canGiveBirth = false;
            AnimationStatus = Animation.none;

			this.TimeTillMate();
			//Eventually GeneList should populate some of these values as soon as it is instantiated
			_x = x;
			_y = y;
           IAmGene g = _entityGenes.ReturnGene("ColorGene");
			
			List<int> color = new List<int>();
            color = (g as ColorGene).GeneValue;
			_color = SwinGame.RGBColor(Convert.ToByte(color[0]), Convert.ToByte(color[1]), Convert.ToByte(color[2])); //(byte)_entityGenes.GetGeneByType(GeneEnum.ColorGene).GeneValue, 0);
			_speedX = 0;
			_speedY = 0;
			
			_randGenerator = new GenerateRandomMovement();
            _speedX = RandGenerator.RandomX;
            _speedY = RandGenerator.RandomY;
		}
		

		 //Explicit displosal of object - ready for garbage collector
        ~GameEntity()
        {
           
        }

       

        public void AddTimeCheck(DateTime t)
        {
            _time = t;
        }
        

        public GenerateRandomMovement RandGenerator
		{
			get{ return _randGenerator; }
			set{ _randGenerator = value; }
		}

        public int ID {
           get { return _ID; }
            set { _ID = value;   }
        }

		public bool IsMutation
		{
			get
			{
				return _isMutation;
			}
			set
			{
				_isMutation = value;
			}
		}

		public bool CanGiveBirth
		{
			get{return _canGiveBirth;}
			set{_canGiveBirth = value;}
		}

		public DateTime Time
		{
			get{return _time;}
			set{_time = value;}
		}

		public Color Color {
			get {return _color;}
			set {_color = value;}
		}

		public GeneList EntityGenes {
			get {return _entityGenes;}
			set {_entityGenes = value;}
		}
		public float X {
			get {return _x;}
			set {_x = value;}
		}

		public float Y {
			get {return _y;}
			set {_y = value;}
		}
		
		public float SpeedX{
			get{return _speedX;}
			set{_speedX = value;}
		}

		public float SpeedY{
			get{return _speedY;}
			set{_speedY = value;}
		}

        public int Life{
            get{return _life;}
            set{_life = value;}
        }

        public float Size
		{
			get
			{
				return _size;
			}

			set
			{
                _size = value;
            }
        }

		public bool IsDead
		{
			get
			{
				return _isDead;
			}
			set
			{
				_isDead = value;
			}
		}

        public Animation AnimationStatus
        {
            get
            {
                return _animationStatus;
            }

            set
            {
                _animationStatus = value;
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
            }
        }

        //All entities will need to check collection and state 'IsAt' location
        public void UpdateEntity(IGameObject g, EntityEnvironment e)
        {
            GameEntity foreignEntity = (g as GameEntity);
            
            g.Draw();
			
			//Collision Check (including mating logic)
            CollisionChecks(foreignEntity, e);
			
            //Move shape (normal random function) - will hand movement logic to mating  if animation occurs
            Move(e.DeltaTime);

        }



        //Compare against entity that is 'selected' in the entity environment
        public void IsTheSelectedEntity(GameEntity e) {
            IsSelected = e != null ? ID == e.ID : false;
        }

        public void CollisionChecks (GameEntity foreignEntity, EntityEnvironment e)
		{
            // --- COLLISSION LOGIC -- //
            //CHeck if the object is not itself and the Collision Event timer will be true if requested elapsed time has passed

            if (((_ID == foreignEntity.ID) == false) || (!foreignEntity.CanGiveBirth))
            {

                //Can now correctly check for the event of IGameObjects contacting eachother
                if (IsAt(foreignEntity.X, foreignEntity.Y))
                {
                    //Checking if can birth (only allowed so often) and also only if they are attracted to eachother
                    if ((CheckIfCanMate(foreignEntity)) && (CheckIfCanMate(this)))
                    {
                        //CanGiveBirth flag is set to depending on the timer following each birthing                        
                        GameEntity newEnt = GiveBirth(foreignEntity);

                        //No births post birth for any of the entities            
                        newEnt.CanGiveBirth = false;
                        newEnt.Size = 1;
                        newEnt.AnimationStatus = Animation.born;
                        newEnt.SpeedX = 0;
                        newEnt.SpeedY = 0;

                        e.NewEntitiesToBeAdded.Add(newEnt as IGameObject);

                        //Set up parent entities to not give birth again straight away
                        this.TimeTillMate();
                        _speedX = (_speedX * 0.2F) * -1;
                        _speedY = (_speedY * 0.2F) * -1;
                        _animationStatus = Animation.birthing;

                        foreignEntity.TimeTillMate();
                        foreignEntity.SpeedX = (foreignEntity.SpeedX * 0.2F) * -1;
                        foreignEntity.SpeedY = (foreignEntity.SpeedY * 0.2F) * -1;
                        foreignEntity.AnimationStatus = Animation.birthing;


                        foreignEntity.CanGiveBirth = false;
                        _canGiveBirth = false;                  }
                }

                //Use this checkto also check against ScreenBoundaries
                this.CheckCollisionScreen();
            }
        }
		
		
		public GameEntity GiveBirth (GameEntity foreignEntity)
		{
			GameEntity newEnt;
			if (foreignEntity.Life >= _life)
			{
				return newEnt = foreignEntity.CreateOffspring (this);
			}
			else
			{
				 return newEnt =  this.CreateOffspring(foreignEntity);
			}
		}
		
		public bool CheckIfCanMate (GameEntity g)
		{
            if (g.CanGiveBirth)
            {
                return IsAttractedToEntity(g);
            }
            return false; 
		}

        public void CheckMatingTime()
        {
            //Get Current Time and Compare against TimeCounter Created after each Birth
            DateTime newTime = DateTime.Now;
            var diff = newTime.Subtract(_time);
            if (diff.Milliseconds > 0)
            {
                _canGiveBirth = true;
            }
        }
		
		public void TimeTillMate ()
		{
            TimeSpan time = new TimeSpan(100000000); //10 seconds
            DateTime newTime = DateTime.Now;
            var timeDiff = newTime + time;
			_time = timeDiff;
		}
				
		public bool IsAt (float x, float y)
		{
			if ((_x > x - 10) && (_x < x + 10))
			{
				if ((_y > y - 10) && (_y < y + 10))
				{
					return true;
				}
			}
			
			return false;
		}

        public void DecreaseLife ()
		{
			if (_life <= 0){
				return;
			}
			
            IAmGene gene = this.EntityGenes.ReturnGene("PhysicalStrength");
            List<int> geneVal = gene.GeneValue;
            int g = geneVal[0];
            if (g == 0)
            {
                g = 100;
            }
            else if (g >= 50)
            {
                g = 100 - g;
            }
            else if (g < 50)
            {
                g = 100 - g;
            }
            //Make g a low number / fraction then make it slightly higher (magic numbers for good death speed
            g = (g / 15) * 2;

            _life -= g;
        }


		// ------------ Take the difference between the genes and multiplies it. If this new number is close to the 'Interested' Entites Gene value then....  
		// ------------ they will not mate - as this large difference has put the entity off. THis will work in the other direction. If the 'Interesed' ENtity is much lower than
		// ------------ the Entity is is looking at, the difference will be negative and it will most likely be attracted
		public bool IsAttractedToEntity(GameEntity g)
		{
			List<int> aList = this.EntityGenes.ReturnGene ("Attractiveness").GeneValue;
			int a = aList[0];
			List<int> bList = g.EntityGenes.ReturnGene ("Attractiveness").GeneValue;
			int b = bList[0];
			int geneDiff = a - b; 
			Random newRand = new Random ();
			
			if ((newRand.Next (5) * geneDiff) >= (a - (a * 0.15))) {
                _canGiveBirth = false;  //Need entities to not continually check attractiveness after initial
                g.CanGiveBirth = false;
                return false;                
			}
            // This entity wants to mate with the other	
            return true;
        }

        public void Animate ()
		{
			if (_animationStatus == Animation.birthing)
			{
				Birthing ();
			}

			if (_animationStatus == Animation.born)
			{
				Grow ();
				return;
			}
			
			if (_animationStatus == Animation.death)
			{
				DeathAnimation();
			}
        }

        public void Grow()
        {
            //assuming small size start
			_size += 0.05F;
            if (_size >= 10.0F)
            {
                _animationStatus = Animation.none;
                _speedY = _randGenerator.RandomY;
                _speedX = _randGenerator.RandomX;
            }
        }

        public void Birthing()
        {
            UpdateSpeed();
            if (_time < (DateTime.Now + new TimeSpan(1000000)))
            {
                _animationStatus = Animation.none;
                _speedX = _randGenerator.RandomX;
                _speedY = _randGenerator.RandomY;
            }         
             
        }


        public void UpdateSpeed()
        {
            this.X = (this.X) + ((this.SpeedX));// *(deltaTime/10));
            this.Y = (this.Y) + ((this.SpeedY));// * (deltaTime / 10));
        }
		
		public void DeathAnimation ()
		{
			
			//Update animation for 'death'
			if (_time.Second > (DateTime.Now).Second)
			{
				_isDead = true;
				_animationStatus = Animation.none;
			}
			else
			{
				if (_size > 10)
				{
					_size -= 0.05F;
				}
				DrawEyes ();
				//Lower color to black so it 'dies'
				EntityGenes.GetGenesList [0].GeneValue [0] = 0;
				EntityGenes.GetGenesList [0].GeneValue [1] = 0;
				EntityGenes.GetGenesList [0].GeneValue [2] = 0;
			}
		}
		
		public void DrawEyes ()
		{
			SwinGame.DrawLine(Color.White, X - 5, Y - 5, X - 2, Y);
			SwinGame.DrawLine(Color.White, X - 5, Y, X - 2, Y - 5);
			SwinGame.DrawLine(Color.White, X + 5, Y - 5, X + 2, Y );
			SwinGame.DrawLine(Color.White, X + 5, Y, X + 2, Y - 5);
		}
		
		public bool IsTooWhite()
		{
			foreach (int i in _entityGenes.GetGenesList[0].GeneValue)
			{
				if (i < 245){
					return true;
				}
			}
			return false;
		}

		public abstract void Draw ();
		public abstract void Move (float deltaTime);
		public abstract void CheckCollisionScreen(); //Could make 'OuterWall' objects to check against, that way don't need ScreenCHeck
		public abstract GameEntity CreateOffspring(GameEntity g);
		public abstract void SetUpChildEnt ();


    }
}

