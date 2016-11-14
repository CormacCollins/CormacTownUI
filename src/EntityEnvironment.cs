using System;
using System.Collections.Generic;
using SwinGameSDK;

namespace MyGame
{
	public class EntityEnvironment
	{
		/// <summary>
		/// Contains List of Entities and additional game data and functions
		/// </summary>

		private List<GameEntity> _listForStats;		
		private List<IGameObject> _gameEntities;    
        private int _entityID;
        private List<GameEntity> _toDelete;
		//This list to acumulate new entities that we will need to add after the main update loop
        private List<IGameObject> _newEntitiesToBeAdded;
        private GameEntity _selectedEntity;
		private UIButtonFlags _gameState;
		private bool _gameOver;

        // --- KEEP COUNT OF TYPES -- ///
        private int _squares;
        private int _circles;
        private int _triangles;
        

        public EntityEnvironment ()
		{
			//Set up game flags and lists
			_gameOver = false;
            _toDelete = new List<GameEntity>();
            _newEntitiesToBeAdded = new List<IGameObject>();
            
			//Start id's at 0
            _entityID = 0;
            _gameEntities = new List<IGameObject>();
			_gameState = UIButtonFlags.none;
			
			//Count for in game UI
            Squares = 0;
            Triangles = 0;
            Circles = 0;
        }

        public int GiveEntityID(){
            _entityID++;
            return _entityID;
        }
       
		//Add to count depending on Type when an Ent is added
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
        
		//Main 'Update' loop - re-factoring has almost left this redundant - which if continued would eventually be the case (too much coupling / shared purpose with GameEntity)
        public void UpdateEntities(){
			CheckIfDead();			
        }

		//Not Using//
        public void IsSelected(GameEntity g)
        {
            if (SwinGame.MouseClicked(MouseButton.LeftButton)){
                if (g.IsAt(SwinGame.MousePosition().X, SwinGame.MousePosition().Y))
                {
                    // ----------- Get GameEntity by type ------------------ //
					// create new entity with same stats to show in gmae UI //
					
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

		//Used in combinatino with 'Death()' function by GameEntity which flags then Ent when at 0 life
		//Once the animation is complete the IsDead flag will lead back it being deleted from the EntityEnvironment list
		public void CheckIfDead ()
		{			
			foreach (IGameObject g in _gameEntities)
			{
				if (((g as GameEntity).Life <= 0) && (!(g as GameEntity).IsDead))
				{
					(g as GameEntity).SpeedX = 0;
					(g as GameEntity).SpeedY = 0;		
					(g as GameEntity).AnimationStatus = Animation.death;
					(g as GameEntity).CanGiveBirth = false;
				}
				
				if ((g as GameEntity).IsDead)
				{					
					(g as GameEntity).X = -500 * ((g as GameEntity).ID + 1); //Moving them in seperate areas out of scope (so they don't mate or anything)
					(g as GameEntity).Y = -500 * ((g as GameEntity).ID + 1);
					if(!(_toDelete.Exists(x => (x as GameEntity).ID == (g as GameEntity).ID))){
						_toDelete.Add ((g as GameEntity));
					}
					(g as GameEntity).CanGiveBirth = false;
				}
			}
		}

		public List<IGameObject> GameEntities {
			get {return _gameEntities;}
		}

        public List<GameEntity> ToDelete{
            get{return _toDelete;}
            set{ _toDelete = value; }
        }

        public List<IGameObject> NewEntitiesToBeAdded{
            get {return _newEntitiesToBeAdded;}
            set {_newEntitiesToBeAdded = value;}
        }

        public int Squares{
            get{return _squares;}
            set {_squares = value;}
        }

        public int Circles{
            get{return _circles;}
            set{_circles = value;}
        }

        public int Triangles{
            get{ return _triangles;}
            set { _triangles = value;}
        }

        public GameEntity SelectedEntity{
            get{return _selectedEntity;}
            set{_selectedEntity = value;}
        }

		public bool GameOver{
			get{return _gameOver;}
			set{_gameOver = value;}
		}

		public List<GameEntity> ListForStats{
			get{return _listForStats;}
			set{_listForStats = value;}
		}

		public UIButtonFlags GameState{
			get{return _gameState;}
			set{_gameState = value;}
		}

        public void AddGameEntity (IGameObject g){ 
			(g as GameEntity).TimeTillMate();
            (g as GameEntity).ID = GiveEntityID();
            TypeCount(g.GetType());
            _gameEntities.Add (g);
		}
	}
}

