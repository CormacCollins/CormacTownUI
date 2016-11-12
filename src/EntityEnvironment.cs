using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
	public class EntityEnvironment
	{
		/// <summary>
		/// Map contain information for game data also contains list of entities
		/// Map can contain many rooms - to be implemented
		/// </summary>


             // !!!! THIS CLASS NEEDS TO BE RE-THOUGHT - IS NOW COUPLED TO GAME ENTITIY INSTEAD OF IGameOBJECT
		private List<GameEntity> _listForStats;		
		private List<IGameObject> _gameEntities;
        private DateTime _matingTime;
        private MateTimer _timer;     
        private int _entityID;
        private List<GameEntity> _toDelete;
        private float _deltaTime;
        private List<IGameObject> _newEntitiesToBeAdded;
        private GameEntity _selectedEntity;
		private UIButtonFlags _gameState;
		

        // --- KEEP COUNT OF TYPES -- ///
        private int _squares;
        private int _circles;
        private int _triangles;
        
       /// public event EventHandler TimerChangeLife;
        public DateTime _lifeTimer;
        // public delegate void LifeDecayDelegate();
        //Need to add entityFavourabilityIndex (Structs with 3 x floats for weighting towrds genetic sway)

        public EntityEnvironment ()
		{
            _toDelete = new List<GameEntity>();
            _newEntitiesToBeAdded = new List<IGameObject>();
            
            _entityID = 0;
            _gameEntities = new List<IGameObject>();
            _lifeTimer = SetTimer(20000000);
            _matingTime = SetTimer(50000000); //5 secs
            _timer = new MateTimer(this, 4000); //4000 for 4 secs between each udpate event
			_gameState = UIButtonFlags.none;
			
            Squares = 0;
            Triangles = 0;
            Circles = 0;
        }

        public int GiveEntityID()
        {
            _entityID++;
            return _entityID;
        }
       

        public void TypeCount(Type t)
        {
            if (t == typeof(Circle)){
                Circles++;
            }
            else if (t == typeof(Triangle)){
                Triangles++;
            }
            else{
                Squares++;
            } 
        }
        
        public void UpdateEntities()
        {
            CheckTime();
            UpdateMatingTime();                
        }

        public void IsSelected(GameEntity g)
        {
            if (SwinGame.MouseClicked(MouseButton.LeftButton)){
                if (g.IsAt(SwinGame.MousePosition().X, SwinGame.MousePosition().Y))
                {
                    // ------------ Dramatically slows dfown game --------------- //

                    g.IsSelected = true;
                    Type entType = g.GetType();
                    object ent = Activator.CreateInstance(entType);
                    (ent as GameEntity).X = 0;
                    (ent as GameEntity).Y = 0;
                    (ent as GameEntity).Color = g.Color;
                    (ent as GameEntity).ID = g.ID;
					(ent as GameEntity).Life = g.Life;
                    (ent as GameEntity).EntityGenes = g.EntityGenes;
                    (ent as GameEntity).Size = g.Size;
                    (ent as GameEntity).IsSelected = true;

                   _selectedEntity = (GameEntity)ent;
                }
            }
        }

        private void UpdateMatingTime()
        {
            foreach (IGameObject g in _gameEntities)
            {
                (g as GameEntity).CheckMatingTime();
            }
        }

        public DateTime SetTimer(int i)
        {
            TimeSpan time = new TimeSpan(i); //2 seconds
            DateTime newTime = DateTime.Now;
            var timeDiff = newTime + time;
            return timeDiff;
        }

        public void CheckTime()
        {
            DateTime newTime = DateTime.Now;
            var diff = newTime.Subtract(_lifeTimer);
            if (diff.Seconds > 0)
            {
                TakeLife();
                _lifeTimer = SetTimer(20000000);
            }
        }


        public void TakeLife ()
		{
			for (int i = 0; i < _gameEntities.Count; i++)
			{
				GameEntity ent = (_gameEntities [i] as GameEntity);
				ent.DecreaseLife ();
				if ((ent.Life == 0) && (ent.AnimationStatus != Animation.death))
				{
					ent.AnimationStatus = Animation.death;
					ent.TimeTillMate();
					ent.SpeedX = 0;
					ent.SpeedY = 0;
				}
				IsAlive(ent);				               
            }
        }
		
		public void IsAlive (GameEntity ent)
		{
			if (ent.IsDead)
                {                    
                    if (ent != null)
                    {
						KillEnt(ent);
                    }
                } 
		}
		
		public void KillEnt (GameEntity ent)
		{
			 //Stop and hide the shape as it may take time to go out of scope
            ent.X = -500*(ent.ID + 1); //Moving them in seperate areas out of scope (so they don't mate or anything)
            ent.Y = -500*(ent.ID + 1);
            ent.SpeedX = 0;
            ent.SpeedY = 0;
            //need to get a reference to the entity to remove it outside of our render loop
            _toDelete.Add(ent);
		}

		public List<IGameObject> GameEntities {
			get {
				return _gameEntities;
			}
		}

        public List<GameEntity> ToDelete
        {
            get{return _toDelete;}
            set{ _toDelete = value; }
        }

        public float DeltaTime
        {
            get{return _deltaTime;}
            set{_deltaTime = value;}
        }

        public List<IGameObject> NewEntitiesToBeAdded
        {
            get {return _newEntitiesToBeAdded;}
            set {_newEntitiesToBeAdded = value;}
        }

        public int Squares
        {
            get{return _squares;}
            set {_squares = value;}
        }

        public int Circles
        {
            get{return _circles;}
            set{_circles = value;}
        }

        public int Triangles
        {
            get{ return _triangles;}
            set { _triangles = value;}
        }

        public GameEntity SelectedEntity
        {
            get{return _selectedEntity;}
            set{_selectedEntity = value;}
        }


		public List<GameEntity> ListForStats
		{
			get
			{
				return _listForStats;
			}
			set
			{
				_listForStats = value;
			}
		}

		public UIButtonFlags GameState
		{
			get
			{
				return _gameState;
			}
			set
			{
				_gameState = value;
			}
		}

        public void AddGameEntity (IGameObject g)
		{ 
            (g as GameEntity).AddTimeCheck(_matingTime);
            (g as GameEntity).ID = GiveEntityID();
            TypeCount(g.GetType());
            _gameEntities.Add (g);
		}

        public void MatingTimeUpdate(TimeSpan s) {
            _matingTime += s;
        }

        public void ProvideStatistics()
        {
           
        }
	}
}

