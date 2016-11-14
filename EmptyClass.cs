// --------------------------------------- ENTITY MAIN ------------------------------------------- //
using System;
using SwinGameSDK;
using System.Windows.Forms; 

namespace MyGame
{
    public class EntityMain
    {
		//Game Data 'put' into main from Windows Forms UI
        public EntityMain (EntityEnvironment gameData)
		{
			//Flag for when button in game is pressed
			gameData.GameOver = false;
			
			
			UpdateGame gameUpdate = new UpdateGame (gameData);


			while (false == SwinGame.WindowCloseRequested ())
			{
				SwinGame.ProcessEvents ();
				SwinGame.ClearScreen (SwinGame.RGBAColor (224, 224, 224, 80));
				
				gameUpdate.DrawEntities (gameData);


				SwinGame.DrawFramerate (0, 0);
				SwinGame.RefreshScreen (60);
				
				if (gameData.GameOver){
					break;
				}
            }			
        }
    }
}
// --------------------------------------- PREPARE DATA ------------------------------------------- //
using System;
using SwinGameSDK;

namespace MyGame
{
	public class PrepareData
	{
		EntityEnvironment _gameData;		
		
		public void GetData (GetUserData userSelection){
			//Ready to be populated with UI input
			_gameData = new EntityEnvironment();
			UnpackUserData getData = new UnpackUserData();
			
			//Load up entity environment with all the data requested by user
			getData.Unpack(userSelection, _gameData);
			
			//Gets sizing from windows forms UI
			SwinGame.OpenGraphicsWindow("GameMain", userSelection.SwinWindowSizeX, userSelection.SwinWindowSizeY); 
		}

		public EntityEnvironment GameData{
			get{return _gameData;}
		}
	}
}
// --------------------------------------- Statistics ------------------------------------------- //
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Statistics
    {
		private StatsStruct _circles;
		private StatsStruct _squares;
		private StatsStruct _triangles;		
		

        public Statistics() {
			_circles = new StatsStruct();
			_squares = new StatsStruct();
			_triangles = new StatsStruct();			
        }

		public StatsStruct Circles{
			get{return _circles;}
			set{_circles = value;}
		}

		public StatsStruct Squares{
			get{return _squares;}
			set{_squares = value;}
		}	
		
		public StatsStruct Trangles{
			get{return _triangles;}
			set{_triangles = value;}
		}	
		
		//Pre game statistics to compare against final ones
		public void AddPreGameStats (List<IGameObject> _ents)
		{
			//List for each type to obtain out of UI input -- //
			//Counting percentage of each type //
			//getting mutation number also//
			
			List<IGameObject> circles = new List<IGameObject> ();
			float circlePerc = 0;
			int circMutations = 0;
			
			List<IGameObject> squares = new List<IGameObject> ();
			float squaresPerc = 0;
			int squareMutations = 0;
			
			List<IGameObject> triangles = new List<IGameObject> ();
			float trianglesPerc = 0;
			int trianglesMutations = 0;
			
			foreach (IGameObject g in _ents)
			{
				if (g.GetType () == typeof(Circle))
				{
					circlePerc++;
					circles.Add (g);
					if ((g as GameEntity).IsMutation)
					{
						circMutations++;
					}
				}
				else if (g.GetType () == typeof(Square))
				{
					squaresPerc++;
					squares.Add (g);
					if ((g as GameEntity).IsMutation)
					{
						squareMutations++;
					}					
				}
				else
				{
					trianglesPerc++;
					triangles.Add (g);
					if ((g as GameEntity).IsMutation)
					{
						trianglesMutations++;
					}					
				}
			}	
			
			circlePerc = circlePerc/_ents.Count;
			squaresPerc = squaresPerc/_ents.Count;
			trianglesPerc = trianglesPerc/_ents.Count;
			
			
			PopulateStatsStructPre(circles, circlePerc, ref _circles, circMutations);
			PopulateStatsStructPre(squares, squaresPerc, ref  _squares, squareMutations);
			PopulateStatsStructPre(triangles, trianglesPerc, ref _triangles, trianglesMutations);			
		}
		
		
		//Post game stats 
		public void CalculateStats (List<IGameObject> _ents)
		{
			//List for each type to obtain out of UI input -- //
			//Counting percentage of each type //
			//getting mutation number also//
			List<IGameObject> circles = new List<IGameObject> ();
			float circlePerc = 0;
			int circMutations = 0;
			
			List<IGameObject> squares = new List<IGameObject> ();
			float squaresPerc = 0;
			int squareMutations = 0;
			
			List<IGameObject> triangles = new List<IGameObject> ();
			float trianglesPerc = 0;
			int trianglesMutations = 0;
			
			
			foreach (IGameObject g in _ents)
			{
				if (g.GetType () == typeof(Circle))
				{
					circlePerc++;
					circles.Add (g);
					if ((g as GameEntity).IsMutation)
					{
						circMutations++;
					}
				}
				else if (g.GetType () == typeof(Square))
				{
					squaresPerc++;
					squares.Add (g);
					if ((g as GameEntity).IsMutation)
					{
						squareMutations++;
					}					
				}
				else
				{
					trianglesPerc++;
					triangles.Add (g);
					if ((g as GameEntity).IsMutation)
					{
						trianglesMutations++;
					}					
				}
			}		
			

			circlePerc = circlePerc/_ents.Count;
			squaresPerc = squaresPerc/_ents.Count;
			trianglesPerc = trianglesPerc/_ents.Count;
			
			PopulateStatsStructPost(circles, circlePerc, ref _circles, circMutations);
			PopulateStatsStructPost(squares, squaresPerc, ref _squares, squareMutations);
			PopulateStatsStructPost(triangles, trianglesPerc, ref  _triangles, trianglesMutations);
			
		}
		
		//Gets data from the game EntityEnvironment and populates struct for displaying stats
		private void PopulateStatsStructPost (List<IGameObject> seperatedShapes, float percentage, ref StatsStruct s, int mutations)
		{
			if (seperatedShapes.Count () <= 0){
				return;
			}
						
			s.PopulationPercentagePost = percentage;


			float avgAttr = 0;
			float avgFitness = 0;
			
			
			//----- Calc Avg Traits ---- //
			foreach (IGameObject g in seperatedShapes)
			{
				//Attr gene
				avgAttr += (g as GameEntity).EntityGenes.GetGenesList[1].GeneValue[0];
				//Fitness gene
				avgFitness += (g as GameEntity).EntityGenes.GetGenesList[2].GeneValue[0];
			}
			
			s.AvgAttractivenssPost = avgAttr/seperatedShapes.Count();
			s.AvgFitnessPost = avgFitness/seperatedShapes.Count();
			
			s.Mutations = mutations;
		}
		
		//Populates struct with pre game info
		private void PopulateStatsStructPre (List<IGameObject> seperatedShapes, float percentage, ref StatsStruct s, int mutations)
		{
			if (seperatedShapes.Count () <= 0)
			{
				return;
			}
			
			s.PopulationPercentagePre = percentage;


			float avgAttr = 0;
			float avgFitness = 0;
			
			
			//----- Calc Avg Traits ---- //
			foreach (IGameObject g in seperatedShapes)
			{
				//Attr gene
				avgAttr += (g as GameEntity).EntityGenes.GetGenesList[1].GeneValue[0];
				//Fitness gene
				avgFitness += (g as GameEntity).EntityGenes.GetGenesList[2].GeneValue[0];
			}
			
			s.AvgAttractivenssPre = avgAttr/seperatedShapes.Count();
			s.AvgFitnessPre = avgFitness/seperatedShapes.Count();
			
			s.Mutations = mutations;
		}
    }
}

// --------------------------------------- StatsStruct ------------------------------------------- //
using System;

namespace MyGame
{
	public struct StatsStruct
	{
		string _type;
		float _populationPercentagePost;
		float _populationPercentagePre;
		bool _isTheDominant;
		float _avgFitnessPre;
		float _avgFitnessPost;
		float _avgAttractivenssPre;
		float _avgAttractivenssPost;
		int _mutations;	


		public float PopulationPercentagePre{
			get{return _populationPercentagePre;}
			set{_populationPercentagePre = value*100;}
		}

		public float AvgAttractivenssPost{
			get{return _avgAttractivenssPost; }
			set{ _avgAttractivenssPost = value;}
		}

		public float AvgAttractivenssPre{
			get{return _avgAttractivenssPre;}
			set{_avgAttractivenssPre = value;}
		}

		public float AvgFitnessPost{
			get{return _avgFitnessPost;}
			set{_avgFitnessPost = value;}
		}

		public float AvgFitnessPre{
			get{return _avgFitnessPre;}
			set{_avgFitnessPre = value;}
		}

		public string Type{
			get{return _type;}
			set{_type = value;}
		}

		public float PopulationPercentagePost{
			get{return _populationPercentagePost;}
			set{_populationPercentagePost = value*100;}
		}

		public bool IsTheDominant{
			get{return _isTheDominant;}
			set{_isTheDominant = value;}
		}
		
		public int Mutations{
			get{return _mutations;}
			set{_mutations = value;}
		}
	}
}

// --------------------------------------- UnpackUserData ------------------------------------------- //
using System;
using System.Collections.Generic;

namespace MyGame
{
	public class UnpackUserData
	{	
		Dictionary<string,Type> _typeLookUp;
		List<int> _xPos;
		List<int> _yPos;
		
		public UnpackUserData(){
			_typeLookUp = new Dictionary<string, Type>();
			_typeLookUp.Add("Square", typeof(Square));			
			_typeLookUp.Add("Triangle", typeof(Triangle));
			_typeLookUp.Add("Circle", typeof(Circle));
		
			_xPos = new List<int>();
			_yPos = new List<int>();
			FillX(_xPos);
			FillY(_yPos);
			
			//as well as randomly picking out values in the Unpack function
			//we are also 'shuffling' the values for a random spawn for each entity
			_yPos = RandomizeListValues(_yPos);
			_xPos = RandomizeListValues(_xPos);		
		}
		
		public void Unpack(GetUserData userData, EntityEnvironment e){
			List<GameData> data = userData.UserInputList;
			
			foreach (GameData g in data){
				
				object newEnt =  GetType(g.ShapeType);
				GeneList newList = new GeneList();
				ColorGene colGene = new ColorGene(g.ColorRgbArray[0], g.ColorRgbArray[1], g.ColorRgbArray[2]); 
				Attractiveness atrGene = new Attractiveness(g.Trait1.Item2);
				PhysicalStrength phyGene = new PhysicalStrength(g.Trait2.Item2);
				
				//Order is important - should really be re-factored in the geneList I know!
				newList.AddGene(colGene);
				newList.AddGene(atrGene);
				newList.AddGene(phyGene);
				
				//Add all the selected traits to the new entity + 'PrepareEntity' sets all the important flags which can't be set from reflection
				Random RandomX = new Random((int)DateTime.Now.Ticks);
				Random RandomY = new Random((int)(DateTime.Now.Ticks)*RandomX.Next());
				int x = GetAndRemove(_xPos,    RandomX.Next(1, _xPos.Count-1));
				int y = GetAndRemove(_yPos, RandomY.Next(1, _yPos.Count-1));
				(newEnt as GameEntity).PrepareEntity( x, y , newList);
				(newEnt as GameEntity).SetUpChildEnt();
				
				//Add entity to list
				e.AddGameEntity(newEnt as IGameObject); 
			}			
		}
		
		public Object GetType(string s)
		{
			return Activator.CreateInstance(_typeLookUp[s]);
		}
		
		public void FillX (List<int> s)
		{
			for (int i = 1; i < 29; i++)
			{
				s.Add(i*20);
			}			
		}
		public void FillY (List<int> s)
		{
			for (int i = 1; i < 28; i++)
			{
				s.Add(i*20);
			}			
		}

		public int GetAndRemove(List<int> list, int index){
			int val = list[index];
			list.RemoveAt(index);
			return val;
		}
				
		private List<int> RandomizeListValues(List<int> list)
		{
		     List<int> randList = new List<int>();
		
		     Random r = new Random();
		     int randomIndex = 0;
		     while (list.Count > 0)
		     {
		          randomIndex = r.Next(0, list.Count); //Choose a random object in the list
		          randList.Add(list[randomIndex]); //add it to the new, random list
		          list.RemoveAt(randomIndex); //remove to avoid duplicates
		     }
		
		     return randList; //return the new random list
}
					
	}
}
// --------------------------------------- EntityEnvironment ------------------------------------------- //
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


// --------------------------------------- GeneList ------------------------------------------- //
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyGame
{
	public class GeneList
	{
		/// <summary>
		/// Holds the genetic Values and informatino for an individual Entity
		/// can use the function 'CombineGeneList'
		/// </summary>
		private List<IAmGene> _genes = new List<IAmGene>(); 
		private bool _containsMutation;
		
		public GeneList(bool mutated=false){
			_containsMutation = mutated;	
        }

		public bool ContainsMutation{
			get{return _containsMutation;}
			set{_containsMutation = value;}
		}

		public List<IAmGene> GetGenesList{
			get{return _genes;}
		}
		
		public void AddGene(IAmGene g){
			_genes.Add(g);
		}
		
		public bool ContainsGeneInstance (IAmGene g){
			return Object.ReferenceEquals (this, g);
		}
		
		//Should return a Gene from which you search i.e. Type = ColorGene it will return it from the geneList
		public IAmGene ReturnGene (string t){
			foreach (IAmGene g in _genes)
			{
				if (g.Name == t){
					return  g;
				}
			}
			return null;
		}
		
		public GeneList CombineGeneLists (GeneList list){
			GeneList newList = new GeneList ();
			
			// ------------------ Hard coding used during a bug ------------------------- //
			
			IAmGene newgene = list.GetGenesList [0];
			IAmGene newgene2 = list.GetGenesList [1];
			IAmGene newgene3 = list.GetGenesList [2];

			IAmGene currentListGene = _genes [0];
			IAmGene currentListGene2 = _genes [1];
			IAmGene currentListGene3 = _genes [2];
			
			//TO be added genes
			IAmGene n1 = newgene.CombineGenes (currentListGene);
			IAmGene n2 = newgene2.CombineGenes (currentListGene2);
			IAmGene n3 = newgene3.CombineGenes (currentListGene3);
			
			if ((n1.IsMutated) || (n2.IsMutated) || (n3.IsMutated)){
				newList.ContainsMutation = true;
			}
			
            newList.AddGene(n1);
			newList.AddGene(n2);
            newList.AddGene(n3);

            return newList;			
		}
	}
}
// --------------------------------------- IAmGene ------------------------------------------- //
using System;
using System.Collections.Generic;

namespace MyGame
{
	public interface IAmGene
	{
		//Important for Universal combinatino of gene types
		List<int> GeneValue{
			get;
		}
		
		bool IsMutated{get;set;}
		
		string Name{
			get;
		}
		
		IAmGene CombineGenes(IAmGene g);			
	}
}

// --------------------------------------- PhysicalStrength ------------------------------------------- //
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PhysicalStrength : IAmGene
    {
		/// <summary>
		/// Gene used in dominance for an Entity also determines speed of death
		/// </summary>
		
		private List<int> _geneValue;
        private string _name;
		private bool _isMutated;
		
		public PhysicalStrength (int value, bool mutated=false)
		{
			_isMutated = mutated;
            _name = "PhysicalStrength";
            _geneValue = new List<int>();
            _geneValue.Add(value);
		}

		public bool IsMutated{
			get{return _isMutated;}
			set{_isMutated = value;}
		}

        public List<int> GeneValue{
			get{return _geneValue;}
			set{_geneValue = value;}
		}

        public string Name{
            get { return _name; }
            set { _name = value; }
        }

        public IAmGene CombineGenes (IAmGene g){
			bool didMutate = false;
			int gVal = CouldMutate (g.GeneValue [0], _geneValue [0], ref didMutate);
			if (gVal > 100){
				gVal = 100;
			}
            //Apply CouldMutate Fuction to potentially mutate - else slightly change value from parents for child
            PhysicalStrength atr = new PhysicalStrength(gVal, didMutate);
            return (atr as IAmGene);
        }

		public int CouldMutate (int a, int b, ref bool didMutate){
			//Get difference between strengths - make sure it is pos
			//ALso taking note of the biggest and smallest value
			int biggest;
			int smallest;
			int diff = a - b;
			if (diff < 0)
			{
				biggest = b;
				smallest = a;
				diff *= -1;
			}
			else if (diff > 0)
			{
				biggest = a;
				smallest = b;
			}
			else //If zero
			{
				diff = 1;
				biggest = a;
				smallest = a;
			}


			Random newRand = new Random ();
			//if random event (1/20) we apply a random multiplier to a portion of the 'Biggest' and add it to the bigger - This is a mutation
			int randNumber = newRand.Next (10);
			if (randNumber == 5)
			{
				didMutate = true;
				Random newRand2 = new Random ();
				int biggestPortion = biggest / 10;
				return (newRand2.Next (5) * biggestPortion) + biggest;
			}
            //Else we add a small portion to the smaller value - (eveolution is slow!)
         	else
			{
				//Random 1 out of 3 options should there be no mutation
				// 1st option gives a mild increase in geneValue
				// 2nd = no change - unlucky
				// 3rd = a chance that, if the difference is large the gene may get a bigger jump up
				//  --> e.g. if a diff of 50 - we would get a jump in 5
				switch (diff % 3)
				{
				case 0: return smallest + 1;
				case 1:  return smallest;
				case 2: return smallest + (int)(diff * 0.1);
				default: break;
				};
				return smallest;
            }
        }       
    }
}
// --------------------------------------- Attractiveness ------------------------------------------- //
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
	/// <summary>
	/// Detirmes Entity prefeence towards mating with eachother
	/// </summary>
    public class Attractiveness : IAmGene
    {
		private List<int> _geneValue;
		private string _name;
		private bool _isMutated;
		
		public Attractiveness(int value, bool mutated=false){			
			_isMutated = mutated;
			_name = "Attractiveness";
			_geneValue = new List<int>();
			_geneValue.Add(value);
		}

		public bool IsMutated{
			get{return _isMutated;}
			set{_isMutated = value;}
		}

		public List<int> GeneValue{
			get{return _geneValue;}
			set{_geneValue = value;}
		}		
		
		public string Name{
			get{return _name;}
			set{_name = value;}
		}
		
		//Need to code so small variance occurs with change of mutation
		public IAmGene CombineGenes(IAmGene g){
			bool didMutate = false;
			int gVal = CouldMutate (g.GeneValue [0], _geneValue [0], didMutate);
			if (gVal > 100){
				gVal = 100;
			}
            //Apply CouldMutate Fuction to potentially mutate - else slightly change value from parents for child
			Attractiveness atr = new Attractiveness(gVal, didMutate);
			return (atr as IAmGene);
		}


		public int CouldMutate (int a, int b, bool didMutate)
		{	
			//Get difference between strengths - make sure it is pos
			//ALso taking note of the biggest and smallest value
			int biggest;
			int smallest;
			int diff = a - b;
			
			if (diff < 0)
			{
				biggest = b;
				smallest = a;
			}
			else if (diff > 0)
			{
				biggest = a;
				smallest = b;
			}
			else //If zero
			{
				diff = 1; //Is really zero, but needs to be one for later calculation
				biggest = a;
				smallest = a;
			}

			Random newRand = new Random ();
			//if random event (1/20) we apply a random multiplier to a portion of the 'Biggest' and add it to the bigger - This is a mutation
			int randNumber = newRand.Next (10);
			
			// ------------------- 1 in 15 change of RANDOM MUTATION ---------------------- //
			
			if (randNumber == 5)
			{
				didMutate = true;
				Random newRand2 = new Random ();
				int biggestPortion = biggest / 10;
				return (newRand2.Next (5) * biggestPortion) + biggest;				
			}
			// ------------------- OR A GENERALIZED SMALLER INCREASE BELOW ---------------- //
            else
			{
				//Random 1 out of 3 options should there be no mutation
				// 1st option gives a mild increase in geneValue
				// 2nd = no change - unlucky
				// 3rd = a chance that, if the difference is large the gene may get a bigger jump up
				//  --> e.g. if a diff of 50 - we would get a jump in 5
				switch (diff % 3)
				{
				case 0: return smallest + 1;
				case 1:  return smallest;
				case 2: return smallest + (int)(diff * 0.1);
				default: break;
				};
				return smallest;
            }
        }        
    }
}
// --------------------------------------- ColorGene ------------------------------------------- //
using System;
using System.Collections;
using System.Collections.Generic;

namespace MyGame
{
	/// <summary>
	/// Contains RGB values for entity color that combine when a new entity is created
	/// Future iterations would seperate this from genetics, not necessary and causes the issue with extra weight in 'GeneValue'
	/// </summary>
	public class ColorGene : IAmGene
	{
		//3 values for each RGB value
		private List<int> _geneValue = new List<int>();
		private string _name;
		private GeneEnum _type;
		private bool _isMutated;

		public ColorGene (int r = 0, int g = 0, int b = 0, bool mutated = false)
		{
			_isMutated = mutated;
			_name = "ColorGene";
			if (r > 255){
				r = 0;
			}
			if (g > 255){
				g = 0;
			}
			if (r > 255){
				b = 0;
			}
			//Instantiate with specified color for RGB
			_geneValue.Add(r);
			_geneValue.Add(g);
			_geneValue.Add(b);
			_type = GeneEnum.ColorGene;
		}

		public bool IsMutated{
			get{return _isMutated;}
			set{_isMutated = value;}
		}

		public string Name{
			get{return _name;}
		}

		public List<int> GeneValue {
			get {return _geneValue;}
			set {_geneValue = value;}
		}

		public GeneEnum Type{
			get{return _type;}
			set{_type = value;}
		}		
		
		//For growing a join after mating
		public IAmGene CombineGenes(IAmGene g){
			ColorGene b = g as ColorGene;

            ColorGene newGene = new ColorGene (AddValue(_geneValue[0] ,b.GeneValue[0]), AddValue(_geneValue[1], b.GeneValue[1]), AddValue(_geneValue[2], b.GeneValue[2]));
			return newGene as IAmGene;
		}

        //If colors have peaked at 255 then these colors will remain
        public int AddValue(int a, int b)
        {
            int c;
           if (a + b > 255)
            {
               c = 0;
                return c;
            }
			c = a + b;
            return c;
        }
	}
}
// --------------------------------------- ColorEnum ------------------------------------------- //

// ---------- Meant for removing gene search by string ----- -//
//NOT IMPLEMENTED //

using System;

namespace MyGame
{
	public enum GeneEnum
	{
		ColorGene,
		SizeGene,
		ShapeGenes
	}
}

// --------------------------------------- Animation ------------------------------------------- //
using System;
namespace MyGame
{
	public enum Animation
	{
		none, birthing, born, death
	}
}
// --------------------------------------- Generate random movement ------------------------------------------- //
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

// --------------------------------------- Collision ------------------------------------------- //
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace MyGame
{
    static public class Collision
    {
		/// <summary>
		/// For checking entities against the screen outsides
		/// </summary>
		/// <param name="g">The green component.</param>
		static public void LevelCollision(GameEntity g)
        {
            if (((g.X + g.Size) > SwinGame.ScreenWidth() - 200) && (g.X + g.Size < (SwinGame.ScreenWidth() - 200) + 2)) {
                g.SpeedX = g.SpeedX * -1;
                g.X = g.X - 1;
            }

            if (((g.X) < 0) && (g.X > -2))
            {
                g.SpeedX = g.SpeedX * -1;
                g.X = g.X + 1;
            }

            if (((g.Y + g.Size) > SwinGame.ScreenHeight()) && (g.X + g.Size < SwinGame.ScreenHeight() + 2))
            {
                g.SpeedY = g.SpeedY * -1;
                g.Y = g.Y - 1;
            }
            if (((g.Y) < 0) && (g.Y > -2))
            {
                g.SpeedY = g.SpeedY * -1;
                g.Y = g.Y + 1;
            }
        }
    }
}

// --------------------------------------- IGameObject ------------------------------------------- //
using System;
using System.Collections.Generic;

namespace MyGame
{
	public interface IGameObject
	{		
		/// <summary>
		/// Originally planned to be implemented so there could be objects other than Entities that were Collissionable
		/// Needs to factored out in next iteration
		/// </summary>
		/// <returns><c>true</c> if this instance is at the specified x y; otherwise, <c>false</c>.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		bool IsAt(float x, float y);
		void Draw();
		
		float X{
			get;
			set;
		}
		float Y{
			get;
			set;
		}
	}
	
}

// --------------------------------------- GameEntity ------------------------------------------- //

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
        private float _life;
        private int _ID;
        private float _size;
        private Animation _animationStatus;
        private bool _isSelected;
		private bool _isDead;
		private bool _isMutation;
		
		//MapSize = 800/25 = 32 && 600/25 = 24		
		private int _mapCellPosX;
		private int _mapCellPosY;

		
		//WHen creating ents from reflection we need an empty Constructor
		//In addtion we have the prepare entity function that can be called alongside reflection
        public GameEntity() {
            //Parameterless Constructor for Reflection
        }

		public GameEntity(float x, float y, GeneList geneList) 
		{			
			PrepareEntity(x,  y, geneList);
		}
		
		public void PrepareEntity (float x, float y, GeneList geneList)
		{
            
			//Position X and how many places forward in intervals of 50
			_mapCellPosX = (int)(x/25);
			_mapCellPosY = (int)(y/25);
			
			//Set all traits normal for instantiation
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
            //Is called when entities moved outside the map and are out of scope
			//Garbage collector removes them to free up space
        }

        public GenerateRandomMovement RandGenerator{
			get{ return _randGenerator; }
			set{ _randGenerator = value; }
		}

		public int MapCellPosY{
			get{return _mapCellPosY;}
			set{_mapCellPosY = value;}
		}

		public int MapCellPosX{
			get{return _mapCellPosX;}
			set{_mapCellPosX = value;}
		}

        public int ID {
           get { return _ID;}
            set { _ID = value;}
        }

		public bool IsMutation{
			get{return _isMutation;}
			set{_isMutation = value;}
		}

		public bool CanGiveBirth{
			get{return _canGiveBirth;}
			set{_canGiveBirth = value;}
		}

		public DateTime Time{
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

        public float Life{
            get{return _life;}
            set{_life = value;}
        }

        public float Size{
			get{return _size;}
			set{_size = value;}
        }

		public bool IsDead{
			get{return _isDead;}
			set{_isDead = value;}
		}

        public Animation AnimationStatus{
            get{return _animationStatus;}
            set{_animationStatus = value;}
        }

        public bool IsSelected{
            get{return _isSelected;}
            set{_isSelected = value;}
        }
		
		public void UpdateCellPosition (){
			_mapCellPosX = (int)(_x/25);
			_mapCellPosY = (int)(_y/25);
		}
		
        //All entities will need to check collection and state 'IsAt' location
        public void UpdateEntity (EntityEnvironment e){
			//Slowly decay Entity
			DecreaseLife();
			
			//Move shape (normal random function) - will hand movement logic to mating  if animation occurs
			Move ();
			
			//For Collision partition
			UpdateCellPosition ();
			
			foreach (IGameObject g in e.GameEntities){		
				if (!this.CanGiveBirth)	{
					break;
				}
				
				//Check for itself
				if (_ID == (g as GameEntity).ID)
				{
					continue;
				}
				
				//Check for in the same screen partition to go any further
				if ((_mapCellPosX == (g as GameEntity).MapCellPosX) && (_mapCellPosY == (g as GameEntity).MapCellPosY))
				{
					//Collision Check (including mating logic)
					CollisionChecks (g as GameEntity, e);
				}				
			}
            
			
			//Check the mating timer 
			CheckMatingTime ();
			
            Draw();  
			
			//Use this checkto also check against ScreenBoundaries
            CheckCollisionScreen();

        }



        //Compare against entity that is 'selected' in the entity environment
        public void IsTheSelectedEntity(GameEntity e) {
            IsSelected = e != null ? ID == e.ID : false;
        }

        public void CollisionChecks (GameEntity foreignEntity, EntityEnvironment e)
		{
            // --- COLLISSION LOGIC -- //
            //CHeck if the object is not itself and the Collision Event timer will be true if requested elapsed time has passed
			CheckMatingTime ();
			
            if ((foreignEntity.CanGiveBirth) && (_canGiveBirth))
            {

                //Can now correctly check for the event of IGameObjects contacting eachother
                if (IsAt(foreignEntity.X, foreignEntity.Y))
                {
                    //Checking if can birth (only allowed so often) and also only if they are attracted to eachother
                    if ((CheckIfCanMate(foreignEntity)) && (foreignEntity.CheckIfCanMate(this)))
                    {
                        //CanGiveBirth flag is set to depending on the timer following each birthing                        
                         GameEntity newEnt = GiveBirth(foreignEntity);

                        //No births post birth for any of the entities            
                        newEnt.CanGiveBirth = false;
						newEnt.TimeTillMate();
                        newEnt.Size = 1;
                        newEnt.AnimationStatus = Animation.born;
                        newEnt.SpeedX = 0;
                        newEnt.SpeedY = 0;


                        //Set up parent entities to not give birth again straight away
                        this.TimeTillMate();
                        _speedX = (_speedX * 0.4F) * -1;
                        _speedY = (_speedY * 0.4F) * -1;
                        _animationStatus = Animation.birthing;

                        foreignEntity.TimeTillMate();
                        foreignEntity.SpeedX = (foreignEntity.SpeedX * 0.4F) * -1;
                        foreignEntity.SpeedY = (foreignEntity.SpeedY * 0.4F) * -1;
                        foreignEntity.AnimationStatus = Animation.birthing;


                        foreignEntity.CanGiveBirth = false;
                        _canGiveBirth = false;    
						
						
                        e.NewEntitiesToBeAdded.Add(newEnt as IGameObject);
					}
                }                
            }
        }
		
		
		//Depending on dominant Fitness, is which parent will create it's own instance
		public GameEntity GiveBirth (GameEntity foreignEntity)
		{
			GameEntity newEnt;
			if (foreignEntity.EntityGenes.GetGenesList[2].GeneValue[0] > _entityGenes.GetGenesList[2].GeneValue[0]){
				return newEnt = foreignEntity.CreateOffspring (this);
			}
			else{
				 return newEnt =  this.CreateOffspring(foreignEntity);
			}
		}
		
		public bool CheckIfCanMate (GameEntity g){
            if (g.CanGiveBirth){
                return IsAttractedToEntity(g);
            }
            return false; 
		}
		
		// ----------- RETURN INSTANCE RELATING TO CHILD CLASS TYPE ------------------- //
		public GameEntity CreateOffspring (GameEntity g){			
			object newEntity = Activator.CreateInstance(g.GetType());
			(newEntity as GameEntity).PrepareEntity(this.X, this.Y,  this.EntityGenes.CombineGeneLists(g.EntityGenes));
			return (newEntity as GameEntity);
		}
			
        public void CheckMatingTime (){
			//Get Current Time and Compare against TimeCounter Created after each Birth
			DateTime newTime = DateTime.Now;
			var diff = _time.Second - newTime.Second;
			int min1 = _time.Minute;
			int min2 = newTime.Minute;			
			
			int isLater = newTime.CompareTo(_time);
			//Seconds can tick over to mins
			if (isLater > 0){
				_canGiveBirth = true;
			}			
        }
		
		public void TimeTillMate (){
			TimeSpan time = new TimeSpan (100000000); //10 seconds
			DateTime newTime = DateTime.Now;
			int mins = newTime.Minute;		
			var timeDiff = newTime + time;

			_time = timeDiff;
			
		}
				
		//Basic collision check (must be in same partition to reach this)
		public bool IsAt (float x, float y){
			if ((_x > x - 10) && (_x < x + 10))
			{
				if ((_y > y - 10) && (_y < y + 10))
				{
					return true;
				}
			}			
			return false;
		}

        public void DecreaseLife (){
			if (_life <= 0){
				return;
			}
			
			IAmGene gene = this.EntityGenes.ReturnGene ("PhysicalStrength");
			List<int> geneVal = gene.GeneValue;
			float g = geneVal [0];
			if (g == 0)
			{
				g = 1;
			}
			if (g == 100)
			{
				g = 99;
			}
			if (g < 50)
			{
				g += g * 0.20F;
			}
			g = 100 - g;
			g = g/1000;

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
				Death();
			}
        }

		//After birth they are small size, quickly grow them and once reach their animation is none and they become normal
        public void Grow()
        {
            //assuming small size start
			_size += 0.1F;
            if (_size >= 10.0F)
            {
                _animationStatus = Animation.none;
                _speedY = _randGenerator.RandomY;
                _speedX = _randGenerator.RandomX;
            }
        }

		//Speed is slow and moving away from birthing area until timespan reached
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
		
		//quick shrink in size before dieing
		public void Death ()
		{
			if (_size > 0){
				_size -= 0.1F;
			}
			else{                
				_isDead = true;
            }    
             
        }

		//Change movement on speed every loop
        public void UpdateSpeed()
        {
            this.X = (this.X) + ((this.SpeedX));// *(deltaTime/10));
            this.Y = (this.Y) + ((this.SpeedY));// * (deltaTime / 10));
        }
		
		//Use levelCollision code
		public void CheckCollisionScreen ()
		{
            Collision.LevelCollision(this);
		}

		//Must functions for different entity types
		public abstract void Draw ();
		public abstract void Move ();
		public abstract void SetUpChildEnt ();


    }
}

// --------------------------------------- Circle ------------------------------------------- //
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
		}
		
		public override void Draw (){			
			
			SwinGame.FillCircle(this.Color, this.X, this.Y, this.Size);
			//If a mutation is in geneList then show the tag
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


// --------------------------------------- Square ------------------------------------------- //
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


// --------------------------------------- Triangle ------------------------------------------- //

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
			this.Size = 15;
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
			if (this.IsMutation)
			{
				DrawMutationTag();
			}
        }
		
		public void DrawMutationTag(){
			SwinGame.FillRectangle(Color.White, this.X - (this.Size/2) + ((this.Size/2)/2) , this.Y, this.Size/2, this.Size/2);
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
		
		public override void Move()
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

	}
}

// --------------------------------------- UpdateGame ------------------------------------------- //
using System;
using System.Collections.Generic;
using SwinGameSDK;


namespace MyGame
{
	public class UpdateGame
	{
        private ShowInfoBox _gameInfo;
		private ShowInfoBox _statsInfo;
		private ShowInfoBox _pauseGame;
		private bool _gameFinished;
		private Statistics _stats;
       

        public UpdateGame (EntityEnvironment e)
		{
			_gameFinished = false;
			
			_stats = new Statistics ();
			_stats.AddPreGameStats(e.GameEntities);
			
			
			// ------ PAUSE UI ------- //
			_pauseGame = new ShowInfoBox( 0, 0, (float)SwinGame.ScreenWidth()-200, (float)SwinGame.ScreenHeight());
            InGameLabel newLabel1 = new InGameLabel((_pauseGame.Width/2)-100, 250F, 200, 30, "Cormac Town");
            newLabel1.ChangeTextColor(Color.DarkRed);
            newLabel1.SetLabelColorRGBA(102, 102, 200, 255); 
			newLabel1.Padding = 50;
			_pauseGame.AddLabel(newLabel1);
			
			
            // ------ SETUP LABELS ---------- //
            // ------ Create InfoBox -------- //
            // ------ Create and add labels --//

            ShowInfoBox gameInformation = new ShowInfoBox((float)(SwinGame.ScreenWidth() - 200), 0, 200F, (float)SwinGame.ScreenHeight());
            InGameLabel pauseLabel = new InGameLabel(0F, 20F, 200, 30, "Cormac Town");
            newLabel1.ChangeTextColor(Color.DarkRed);
            newLabel1.SetLabelColorRGBA(102, 102, 200, 255); 
            gameInformation.AddLabel(pauseLabel);


            InGameLabel newLabel2 = new InGameLabel(0F, 55F, 200, 30, "Shapes alive: ");
            newLabel2.ChangeTextColor(Color.White);
            newLabel2.SetLabelColorRGBA(220, 20, 60, 255);
            gameInformation.AddLabel(newLabel2);

            InGameLabel newLabel3 = new InGameLabel(0F, 85F, 200, 30, "Square Count: ");
            newLabel3.ChangeTextColor(Color.White);
            newLabel3.SetLabelColorRGBA(220, 20, 60, 255);
            gameInformation.AddLabel(newLabel3);

            InGameLabel newLabel4 = new InGameLabel(0F, 115F, 200, 30, "Circle Count: ");
            newLabel4.ChangeTextColor(Color.White);
            newLabel4.SetLabelColorRGBA(220, 20, 60, 255);
            gameInformation.AddLabel(newLabel4);

            InGameLabel newLabel5 = new InGameLabel(0F, 145F, 200, 30, "Triangle Count: ");
            newLabel5.ChangeTextColor(Color.White);
            newLabel5.SetLabelColorRGBA(220, 20, 60, 255);
            gameInformation.AddLabel(newLabel5);

            InGameLabel newLabel6 = new InGameLabel(0F, 200F, 200, 30, "Entity: ");
            newLabel6.ChangeTextColor(Color.White);
            newLabel6.SetLabelColorRGBA(220, 20, 60, 255);
            gameInformation.AddLabel(newLabel6);

            
            InGameLabel entLabel1 = new InGameLabel(25, 290, 150, 30, "Attractiveness: ");
            entLabel1.ChangeTextColor(Color.White);
            entLabel1.SetLabelColorRGBA(102, 102, 143, 255);
            gameInformation.AddLabel(entLabel1);

            InGameLabel entLabel2 = new InGameLabel(25, 330, 150, 30, "Fitness: ");
            entLabel2.ChangeTextColor(Color.White);
            entLabel2.SetLabelColorRGBA(102, 102, 143, 255);
            gameInformation.AddLabel(entLabel2);
			
			InGameLabel entLabel3 = new InGameLabel(25, 370, 150, 30, "Life: ");
            entLabel3.ChangeTextColor(Color.White);
            entLabel3.SetLabelColorRGBA(102, 102, 143, 255);
            gameInformation.AddLabel(entLabel3);
			
			//Entity selected draw position           
            gameInformation.EntityDisplayedY = 260;

			InGameLabel entLabel4 = new InGameLabel(25, 430, 150, 30, "PAUSE");
            entLabel4.ChangeTextColor(Color.White);
            entLabel4.SetLabelColorRGBA(0, 0, 0, 255);
            entLabel4.ButtonFlag = UIButtonFlags.pause;
			entLabel4.AlternateText = "RESUME";
			entLabel4.Padding = 50;
			gameInformation.AddLabel(entLabel4);
			
			InGameLabel entLabel5 = new InGameLabel(25, 470, 150, 30, "FINISH");
            entLabel5.ChangeTextColor(Color.White);
            entLabel5.SetLabelColorRGBA(0, 0, 0, 255);
			entLabel5.Padding = 50;
            entLabel5.ButtonFlag = UIButtonFlags.finish;
			gameInformation.AddLabel(entLabel5);
			
			InGameLabel entLabel6 = new InGameLabel(25, 510, 150, 30, "QUIT");
            entLabel6.ChangeTextColor(Color.White);
            entLabel6.SetLabelColorRGBA(0, 0, 0, 255);
			entLabel6.Padding = 50;
            entLabel6.ButtonFlag = UIButtonFlags.quit;
			gameInformation.AddLabel(entLabel6);
			
            //Add to field for rendering in Draw loop
            _gameInfo = gameInformation;
        }

		// --------------------------------------------------- MAIN UPDATE FUNCTION ---------------------------------------------------------- //
		// -------------------------------------------------------------------------------------------------------------------------------------//
		public void DrawEntities (EntityEnvironment e)
		{
			//Refresh gameData to be displayed
			_gameInfo.AddInformation ("Shapes alive", e.GameEntities.Count.ToString ());
			_gameInfo.AddInformation ("Square Count", e.Squares.ToString ());
			_gameInfo.AddInformation ("Circle Count", e.Circles.ToString ());
			_gameInfo.AddInformation ("Triangle Count", e.Triangles.ToString ());
			//Update the 'selected' entity display
			UpdateSelectedEntity (e);

			_gameInfo.SetEntity (e.SelectedEntity, 230F, _gameInfo.Width / 2);
			e.GameState = _gameInfo.GameState;
			
			// -------------------------------------------- Check for game finished ------------------------------------------------------- //
			CheckForGameEnd(e);
			
			// -------------------------------------------- Draw the current UI ------------------------------------------------------- //
			DrawGameInfo(e);
			
			//------------------------------------------------If paused game stops-------------------------------------------------------//
			if (!(e.GameState == UIButtonFlags.pause))
			{
				// --------------------- MAIN ENTITY LOOP ------------------------------------------------------------------------------ //
				foreach (IGameObject g in e.GameEntities)
				{	
					e.UpdateEntities ();
					e.IsSelected (g as GameEntity);
							
					(g as GameEntity).UpdateEntity(e);
				}
			}
			
			//----------------------------------------Remove any Entities the game intends to destroy-------------------------------------//
            if (e.ToDelete.Count > 0)
            {
                foreach (GameEntity g in e.ToDelete)
                {
					int idForRemove = g.ID;
					int index = e.GameEntities.FindIndex(x => (x as GameEntity).ID == idForRemove);
                    e.GameEntities.RemoveAt(index); //Can get quickler run time with refactoring the RemoveAll function - if time
                }
                e.ToDelete.Clear();
            }

            // ---------------------------------------------- Add new entities to map list ---------------------------------------------- //
			if (e.NewEntitiesToBeAdded.Count > 0)
            {
                //add new entities to our game entities
                foreach (IGameObject g in e.NewEntitiesToBeAdded)
                {
                    e.AddGameEntity(g);
                }
                //clear list as they are now added
                e.NewEntitiesToBeAdded.Clear();
            }        
		}
		
        //----------------------------------Gets information to populate the EntityGraphic on side------------------------------------//
        public void UpdateSelectedEntity(EntityEnvironment e)
        {
            if (e.SelectedEntity == null) {           
                return;
            }
            int attrTrait = e.SelectedEntity.EntityGenes.ReturnGene("Attractiveness").GeneValue[0];
            int fitnessTrait = e.SelectedEntity.EntityGenes.ReturnGene("PhysicalStrength").GeneValue[0];
			float life = e.SelectedEntity.Life;
            _gameInfo.AddInformation("Attractiveness", attrTrait.ToString());
            _gameInfo.AddInformation("Fitness", fitnessTrait.ToString());			
			_gameInfo.AddInformation("Life", life.ToString());

        }

		
        public void GameFinishDisplay(EntityEnvironment e)
        {
			e.GameState = UIButtonFlags.pause;
        }
		
		// --------------- FINISH GAME CONSTRAINTS ------------------ //
		public void CheckForGameEnd (EntityEnvironment e)
		{			
			if (e.GameState == UIButtonFlags.quit)
			{
				//Breaks the main loop
				e.GameOver = true;
			}
			
			if ((e.GameEntities.Count >= 65) || (e.GameEntities.Count == 0))
			{
				//Brings on Stats UI
				_gameFinished = true;	
			}
			
			if (e.GameState == UIButtonFlags.finish)
			{
				//Brings on stats UI
				_gameFinished = true;
			}
		}
		
		public void DrawGameInfo (EntityEnvironment e)
		{			
			if (e.GameState == UIButtonFlags.pause)
			{
				_pauseGame.Draw();
			}			
			
			if (!_gameFinished)
			{
				_gameInfo.Draw ();
			}
			
			if (_gameFinished)
			{
				FinalizeGame (e);	
			}
			// ------------ DRAW GAME UI INFO / OR FINAL STATS DATA ------------- //
			
			if (_gameFinished)
			{
				_statsInfo.Draw ();
			}
			
		}
		
		// ------------------------------------------------------------ POPULATE STATS DATA FOR FINAL UI ------------------------------------------------------------------------- //
		public void FinalizeGame (EntityEnvironment e)
		{
			GameFinishDisplay (e);
			_gameFinished = true;
			//Prepare final stats to present
			_stats.CalculateStats ((e.GameEntities));
			

			//Infobox for game stats
			_statsInfo = new ShowInfoBox(0,  0, (SwinGame.ScreenWidth()), (SwinGame.ScreenHeight()));
			_statsInfo.BoxColor =  SwinGame.RGBAColor(123, 104, 238, 255);
			
			
			// ------------------------ CIRCLE -------------------------- //
			
			InGameLabel statLabel1 = new InGameLabel(0F, 15F, 800, 30, "Shape: Circle");
            statLabel1.ChangeTextColor(Color.White);
			statLabel1.Padding = 0;
            statLabel1.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel1);
			
			InGameLabel statLabel2 = new InGameLabel(0F, 45F, 400, 30, "Starting percentage: " + _stats.Circles.PopulationPercentagePre.ToString("0.00\\%"));
            statLabel2.ChangeTextColor(Color.White);
            statLabel2.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel2);
			
			InGameLabel statLabel3 = new InGameLabel(0F, 75F, 400, 30, "Average Starting fitness: " + _stats.Circles.AvgFitnessPre.ToString());
            statLabel3.ChangeTextColor(Color.White);
            statLabel3.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel3);
			
			InGameLabel statLabel4 = new InGameLabel(0F, 105F, 400, 30, "Average Starting attractiveness: " + _stats.Circles.AvgAttractivenssPre.ToString());
            statLabel4.ChangeTextColor(Color.White);
            statLabel4.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel4);
			
			InGameLabel statLabelmutation = new InGameLabel(0, 135, 800, 30, "Mutations " + _stats.Circles.Mutations.ToString());
            statLabelmutation.ChangeTextColor(Color.White);
            statLabelmutation.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabelmutation);
			
			InGameLabel statLabel5 = new InGameLabel(400, 45F, 400, 30, "Finishing percentage: " + _stats.Circles.PopulationPercentagePost.ToString("0.##\\%"));
            statLabel5.ChangeTextColor(Color.White);
            statLabel5.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel5);
			
			InGameLabel statLabel6 = new InGameLabel(400, 75F, 400, 30, "Average Finishing fitness: " + _stats.Circles.AvgFitnessPost.ToString());
            statLabel6.ChangeTextColor(Color.White);
            statLabel6.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel6);
			
			InGameLabel statLabel7 = new InGameLabel(400, 105F, 400, 30, "Average Finishing attractiveness: " + _stats.Circles.AvgAttractivenssPost.ToString());
            statLabel7.ChangeTextColor(Color.White);
            statLabel7.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel7);
			
			
			// ---------------------------- SQUARE -------------------------------- //
			
			InGameLabel statLabel8 = new InGameLabel(0, 185F, 800, 30, "Shape: Square");
            statLabel8.ChangeTextColor(Color.White);
			statLabel8.Padding = 0;
            statLabel8.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel8);
			
			InGameLabel statLabel9 = new InGameLabel(0, 215F, 400, 30, "Starting percentage: " + _stats.Squares.PopulationPercentagePre.ToString("0.##\\%"));
            statLabel9.ChangeTextColor(Color.White);
            statLabel9.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel9);
			
			InGameLabel statLabel10 = new InGameLabel(0, 245F, 400, 30, "Average Starting fitness: " + _stats.Squares.AvgFitnessPre.ToString());
            statLabel10.ChangeTextColor(Color.White);
            statLabel10.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel10);
			
			InGameLabel statLabel11 = new InGameLabel(0, 275F, 400, 30, "Average Starting attractiveness: " + _stats.Squares.AvgAttractivenssPre.ToString());
            statLabel11.ChangeTextColor(Color.White);
            statLabel11.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel11);
			
			InGameLabel statLabelmutation1 = new InGameLabel(0, 305, 800, 30, "Mutations " + _stats.Squares.Mutations.ToString());
            statLabelmutation1.ChangeTextColor(Color.White);
            statLabelmutation1.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabelmutation1);
			
			InGameLabel statLabel12 = new InGameLabel(400, 215F, 400, 30, "Finishing percentage: " + _stats.Squares.PopulationPercentagePost.ToString("0.##\\%"));
            statLabel12.ChangeTextColor(Color.White);
            statLabel12.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel12);
			
			InGameLabel statLabel13 = new InGameLabel(400, 245F, 400, 30, "Average Finishing fitness: " + _stats.Squares.AvgFitnessPost.ToString());
            statLabel13.ChangeTextColor(Color.White);
            statLabel13.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel13);
			
			InGameLabel statLabel14 = new InGameLabel(400, 275F, 400, 30, "Average Finishing attractiveness: " + _stats.Squares.AvgAttractivenssPost.ToString());
            statLabel14.ChangeTextColor(Color.White);
            statLabel14.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel14);
			
			// ---------------------------- Triangle -------------------------------- //
			
			InGameLabel statLabel16 = new InGameLabel(0, 355F, 800, 30, "Shape: Triangle");
            statLabel16.ChangeTextColor(Color.White);
			statLabel16.Padding = 0;
            statLabel16.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel16);
			
			InGameLabel statLabel17 = new InGameLabel(0, 385F, 400, 30, "Starting percentage: " + _stats.Trangles.PopulationPercentagePre.ToString("0.00\\%"));
            statLabel17.ChangeTextColor(Color.White);
            statLabel17.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel17);
			
			InGameLabel statLabel18 = new InGameLabel(0, 415F, 400, 30, "Average Starting fitness: " + _stats.Trangles.AvgFitnessPre.ToString());
            statLabel18.ChangeTextColor(Color.White);
            statLabel18.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel18);
			
			InGameLabel statLabel19 = new InGameLabel(0, 445F, 400, 30, "Average Starting attractiveness: " + _stats.Trangles.AvgAttractivenssPre.ToString());
            statLabel19.ChangeTextColor(Color.White);
            statLabel19.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel19);
			
			InGameLabel statLabelmutation2 = new InGameLabel(0, 475, 800, 30, "Mutations " + _stats.Trangles.Mutations.ToString());
            statLabelmutation2.ChangeTextColor(Color.White);
            statLabelmutation2.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabelmutation2);
			
			InGameLabel statLabel20 = new InGameLabel(400, 385F, 400, 30, "Finishing percentage: " + _stats.Trangles.PopulationPercentagePost.ToString("0.00\\%"));
            statLabel20.ChangeTextColor(Color.White);
            statLabel20.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel20);
			
			InGameLabel statLabel21 = new InGameLabel(400, 415F, 400, 30, "Average Finishing fitness: " + _stats.Trangles.AvgFitnessPost.ToString());
            statLabel21.ChangeTextColor(Color.White);
            statLabel21.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel21);
			
			InGameLabel statLabel22 = new InGameLabel(400, 445F, 400, 30, "Average Finishing attractiveness: " + _stats.Trangles.AvgAttractivenssPost.ToString());
            statLabel22.ChangeTextColor(Color.White);
            statLabel22.SetLabelColorRGBA(0, 0, 0, 255);
            _statsInfo.AddLabel(statLabel22);
			
			
		}
	}
}



// --------------------------------------- ShowInfoBox ------------------------------------------- //
using SwinGameSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class ShowInfoBox
    {
        private List<string> _outputString;
        private List<float> _outputData;
		private Color _boxColor;
        private List<InGameLabel> _labels;
		private UIButtonFlags _gameState;

        private GameEntity _entityDisplayed;
        private float _entityDisplayedX;
        private float _entityDisplayedY;
        //positions
        private float _x;
        private float _y;
        private float _width;
        private float _height;

		public Color BoxColor
		{
			get
			{
				return _boxColor;
			}
			set
			{
				_boxColor = value;
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

        public float Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        public float EntityDisplayedX
        {
            get
            {
                return _entityDisplayedX;
            }

            set
            {
                _entityDisplayedX = value;
            }
        }

        public float EntityDisplayedY
        {
            get
            {
                return _entityDisplayedY;
            }

            set
            {
                _entityDisplayedY = value;
            }
        }

		public ShowInfoBox(float x, float y, float width, float height)
        {
			_gameState = UIButtonFlags.none;
            _labels = new List<InGameLabel>();

            _x = x;
            _y = y;
            Width = width;
            _height = height;
            _outputData = new List<float>();
            _outputString = new List<string>();
            //Default clr
            _boxColor = SwinGame.RGBAColor(123, 104, 238, 50); //50 opacity?
            EntityDisplayedX = 0;
            EntityDisplayedY = 0;
        }

        public void AddInformation(string labelName, string envInfo)
        {
            foreach (InGameLabel l in _labels)
            {
                if (l.Text.Contains(labelName))
                {
                    l.DataOutput = envInfo;
                }
            }
        }
        

        public void SetEntity(GameEntity g, float x, float y)
        {
            if(g == null){
                return;
            }
            _entityDisplayed = g;
            _entityDisplayed.X = _width/2 + _x;
            _entityDisplayed.Y = _entityDisplayedY;
        }

        public void Draw ()
		{
			SwinGame.FillRectangle (_boxColor, _x, _y, (int)Width, (int)_height);
			foreach (InGameLabel l in _labels)
			{                
				l.DrawLabel ();
            
				if (l.IsAt (SwinGame.MousePosition().X , SwinGame.MousePosition().Y) && SwinGame.MouseClicked(MouseButton.LeftButton))
				{
					_gameState = l.ButtonFlag;
					ButtonCase(l);
				}
            }

            if (_entityDisplayed != null)
            {
                _entityDisplayed.Draw();
                //_entityDisplayed.DrawOutline();
            }
			
			
        }

        public void AddLabel(InGameLabel l)
        {
            l.X = l.X + _x;
			l.Y = l.Y + _y;
            _labels.Add(l);
        }
		
		public void ButtonCase (InGameLabel l)
		{
			switch (l.ButtonFlag)
			{
				case(UIButtonFlags.resume): l.ButtonFlag = UIButtonFlags.pause;
					l.Text = "PAUSE";
					break;
				case(UIButtonFlags.pause): l.ButtonFlag = UIButtonFlags.resume;
					l.Text = "RESUME";				
					break;
				case(UIButtonFlags.quit): _gameState = UIButtonFlags.quit;
						break;
				case(UIButtonFlags.finish): _gameState = UIButtonFlags.finish;
				break;
				default: break;	
			}			
		}
    }



}



// --------------------------------------- InGameLabel ------------------------------------------- //
using System;
using SwinGameSDK;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{

    class InGameLabel
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;
        private string _text;
		private string _alternateText;
        private string _dataOutput;
        private Color _color;
        private Color _textColor;
		private UIButtonFlags _buttonFlag;
		private int _padding;
		

        public InGameLabel(float relativeX, float relativeY, float width, float height, string textString)
        {
			_padding = 0;
			_buttonFlag = UIButtonFlags.none;
            _x = relativeX;
            _y = relativeY;
            Width = width;
            Height = height;
            Color = Color.White;
            TextColor = Color.Black;
            Text = textString;
            //default text clr
        }

		public int Padding
		{
			get
			{
				return _padding;
			}
			set
			{
				_padding = value;
			}
		}

		public UIButtonFlags ButtonFlag
		{
			get
			{
				return _buttonFlag;
			}
			
			set
			{
				_buttonFlag = value;
			}
		}

		public string AlternateText
		{
			get
			{
				return _alternateText;
			}
			set
			{
				_alternateText = value;
			}
		}

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
		

        public string DataOutput
        {
            get { return _dataOutput; }
            set { _dataOutput = value; }
        }

        public virtual void DrawLabel()
        {
            SwinGame.FillRectangle(Color, X, Y, (int)Width, (int)Height);
            SwinGame.DrawText(Text + _dataOutput, TextColor, (X + 5F) + _padding, _y + (Height / 2));
        }

        public void ChangeTextColor(Color clr)
        {
            TextColor = clr;
        }

        public void SetLabelColorRGBA(byte r, byte g, byte b, byte alpha)
        {
            Color = SwinGame.RGBAColor(r, g, b, alpha);
        }

        public bool IsAt(float x, float y) {
            //Check X/Y Positions are withing this labels range
            if((X > x) || (x > (X + Width)))
            {
                return false;
            }

            if ((Y > y) || (y > (Y + Height))) {
                return false;
            }
            return true;
        }
		
		
		

    }
    
}


// --------------------------------------- UIButoonflag ------------------------------------------- //
using System;

namespace MyGame
{
	public enum UIButtonFlags
	{
		quit, pause, resume, finish, none
	}
}
// --------------------------------------- WINDOWS FORMS UI ------------------------------------------- //
// -------------------------------------------------------- ------------------------------------------- //
// -------------------------------------------------------- ------------------------------------------- //
// -------------------------------------------------------- ------------------------------------------- //
// -------------------------------------------------------- ------------------------------------------- //


// --------------------------------------- Program ------------------------------------------- //

using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
	class MainClass
	{
		
		public static void Main ()
		{
		 	GetUserData userData = new GetUserData();
			OpeningPage n = new OpeningPage(userData);			
		
		
		}
	}
}

// --------------------------------------- CormacTownForm ------------------------------------------- //
using System;
using System.Windows.Forms;

namespace MyGame
{
	public class CormacTownForm : Form
	{
		
		private PageType _type;

		public PageType ReturnType {
			get {
				return _type;
			}
			set {
				_type = value;
			}
		}
		
		public CormacTownForm ()
		{
		}
	}
}


// --------------------------------------- CormacTownButton ------------------------------------------- //
using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyGame
{
	public class CormacTownButton : Button
	{
		public CormacTownButton () 
		{
			this.BackColor = Color.Black;
			this.Font = new Font("Broadway", 16);
			this.Width = 160;
			this.Height = 40;
			this.ForeColor = System.Drawing.Color.White;
		}
	}
	
	public class CormacTownLabel : Label
	{
		public CormacTownLabel (int fontSize) 
		{
			this.Font = new Font("Broadway", fontSize);
			this.Width = 350;
			this.Height = 40;
			this.ForeColor = System.Drawing.Color.White;
		}
	}
}


// --------------------------------------- Opening Page ------------------------------------------- //
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace MyGame
{
	public class OpeningPage : CormacTownForm
	{
        private GetUserData _userData;

		public OpeningPage (GetUserData userData)
		{
            _userData = userData;
			this.ReturnType = PageType.Open;
			// --------------- Create picture box and load from file ---------------- //
			PictureBox pictureBox1 = new PictureBox ();			
			pictureBox1.ImageLocation = @"C:\Users\corma\Desktop\CormacTownJoint\Resources\images\logo.png
";
			//Loaded first before window
			
			const int logoWidth = 307;
			this.Size = new Size (800, 600);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.Fixed3D;
			this.BackColor = Color.White;
			
			//Size up picture box
			pictureBox1.Size = new System.Drawing.Size(300, 200);
			pictureBox1.Location = new Point(this.Width/2 - (logoWidth/2), 120);

			
			
			
			//Underlay//
			CormacTownLabel underlay = new CormacTownLabel(24);
			underlay.Width = this.Width;
			underlay.Height = 100;
			underlay.BackColor = System.Drawing.Color.Black;
			underlay.Location = new System.Drawing.Point(0, 0);
			
			
			// ------------------ Title label --------------------------------------- //
			CormacTownLabel titleLabel = new CormacTownLabel(24);
			titleLabel.Text = "Cormac Town";
			titleLabel.BackColor = System.Drawing.Color.Black;
			titleLabel.Location = new System.Drawing.Point(this.Width/2 - (titleLabel.Width/3) - 15, 20);
		
			
			
			CormacTownButton button1 = new CormacTownButton ();
			button1.Text = "START";
			button1.Location = new Point (this.Width/2 - (button1.Width/2), this.Height - 200);			
			button1.Click += new EventHandler (OnClick);  //Event handler is the dleegate and been given particular implementaitotno of this method
			
			CormacTownButton button2 = new CormacTownButton();
			button2.Text = "STATISTICS";
			button2.Width = 200;
			button2.Location  = new Point (this.Width/2 - (button2.Width/2), this.Height - 150);	
			
			
			
			
			this.AcceptButton = button1;
			this.Controls.Add (button1);
			
			this.AcceptButton = button2;
			this.Controls.Add (button2);
			
			this.Controls.Add(titleLabel);
			this.Controls.Add(pictureBox1);	
			this.Controls.Add(underlay);
			//this.Controls.Add(instructionLabel);
			
			
			this.ShowDialog ();
	
		}
		
		public void OnClick(object sender, EventArgs e)
		{	
			//Create new page, hide the old one
			//Set new page to the window position of the old page.
			SelectionPage s = new SelectionPage(_userData);
			s.StartPosition = FormStartPosition.Manual;
			s.Left = this.Left;
			s.Top = this.Top;
			this.Visible = false;
			s.ShowDialog();
			this.Close();
		}
    }
}


// --------------------------------------- SelectionPage ------------------------------------------- //
using System;

using System.Windows.Forms;
using System.Threading.Tasks;
using System.Timers;
using SwinGameSDK;
using System.Drawing;

namespace MyGame
{
	public class SelectionPage : CormacTownForm
	{
		private CormacTownLabel _label1;
		private CormacTownLabel _label2;
		private CormacTownLabel _ColorLabel1;
		private CormacTownLabel _ColorLabel2;
		private CormacTownLabel _ColorLabel3;
		//private CormacTownLabel _label4;
		//private CormacTownLabel _label5;
		private int _shapesXstartPos;
		private int _shapesYStartPos;
		private bool _fullShapeList;
		//private bool _checked;
		private GetUserData _userData;
		private int _AttractivenssValue;
		private int _FitnessValue;
		private int _ColorValue1;
		private int _ColorValue2;
		private int _ColorValue3;
		private ProgressBar _progressBar;


		
		public SelectionPage (GetUserData userData)
		{
				
			
			// --------------------- To transfer all user selections to populate the game -------------------------- //
            _userData = userData;		

			//Distance from Max for placing the shapes when the user selects them
			_shapesXstartPos = 400;
			_shapesYStartPos = 460;
			
			
			
			
			// ------ Identical Form type -------------- //
			//------- Populate with same stats ---------//			
			this.Size = new System.Drawing.Size (800, 600);
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.Fixed3D;
			this.BackColor = System.Drawing.Color.White;
			
			//prepare progress bar - to be added when 'Start' button clicked
			_progressBar = new ProgressBar();
			_progressBar.Width = 400;
			_progressBar.Height = 40;
			_progressBar.Location = new System.Drawing.Point(this.Left+(this.Width/2)-  (_progressBar.Width/2), this.Top + (this.Height/2));
			_progressBar.Maximum = 30000;
			_progressBar.Step = 1;
			
			//Underlay//
			CormacTownLabel underlay = new CormacTownLabel(24);
			underlay.Width = this.Width;
			underlay.Height = 100;
			underlay.BackColor = System.Drawing.Color.Black;
			underlay.Location = new System.Drawing.Point(0, 0);
			
			
			// ------- Heading ----------- //
			CormacTownLabel titleLabel = new CormacTownLabel(24);
			titleLabel.Text = "Cormac Town";
			
			titleLabel.BackColor = System.Drawing.Color.Black;
			titleLabel.Location = new System.Drawing.Point((0 + this.Width/2) - (titleLabel.Width) + 200, 20);
			
			
			//------------ Instruction ----------- //
			CormacTownLabel instructionLabel = new CormacTownLabel(10);
			instructionLabel.Width = 500;
			instructionLabel.Height = 50;
			instructionLabel.BackColor = System.Drawing.Color.Black;
			instructionLabel.Width = this.Width;
			//instructionLabel.Font = new System.Drawing.Font("Calibri", 10);
			instructionLabel.Text = "Modify the type of your shape: Attractiveneess, Fitness & RGB color.\nThen select the entity type Triangle, Circle, Square.";
			instructionLabel.Location = new System.Drawing.Point(0, 75);
			
						
			// --------------- Start label ---------------------------- //
			CormacTownButton startButton = new CormacTownButton();
			startButton.Location = new System.Drawing.Point((this.Width - 200), (this.Height - 100));
			startButton.Text = "Start Game";
			startButton.Click += new EventHandler(OnStartClick);
				
			
			
			// -------------- Attractiveness ------------------- //	
			TrackBar trackBar1 = new TrackBar();
			trackBar1.BackColor =  System.Drawing.Color.FromArgb(0, 0, 0);
			trackBar1.Orientation = Orientation.Horizontal;
			trackBar1.SetRange(1, 100);
			trackBar1.Value = 50;
			_AttractivenssValue = trackBar1.Value;
			trackBar1.Location = new System.Drawing.Point(10, 160);
			trackBar1.Scroll += new EventHandler(trackBar1_Scroll);
			
			// -------------- Attractiveness Label-------------- //	
			_label1 = new CormacTownLabel(12); //Smaller font
			_label1.Height = 30;
			_label1.Width = 190;
			_label1.ForeColor = System.Drawing.Color.Black;
			_label1.Text = "Attractiveness: " + trackBar1.Value.ToString();
			_label1.Location = new System.Drawing.Point(120, 160);
			
			// -------------- Fitness ------------------- //
			
			TrackBar trackBar2 = new TrackBar();
			trackBar2.BackColor =  System.Drawing.Color.FromArgb(0, 0, 0);
			trackBar2.Orientation = Orientation.Horizontal;
			trackBar2.SetRange(1, 100);
			trackBar2.Value = 50;
			_FitnessValue = trackBar2.Value;
			trackBar2.Location = new System.Drawing.Point(10, 210);  			
			trackBar2.Scroll += new EventHandler(trackBar2_Scroll);		
			
			// -------------- Fitness Label --------------- //	
			
			_label2 = new CormacTownLabel(12); //Smaller font
			_label2.Height = 30;
			_label2.Width = 150;
			_label2.ForeColor = System.Drawing.Color.Black;
			_label2.Text = "Fitness: " + trackBar2.Value.ToString();
			_label2.Location = new System.Drawing.Point(120, 210);
			
			// -------------- Color ------------------- //		
			TrackBar trackBar3Color1 = new TrackBar();
			trackBar3Color1.BackColor = System.Drawing.Color.FromArgb(255, 4, 4);
			trackBar3Color1.Orientation = Orientation.Horizontal;
			trackBar3Color1.SetRange(1, 255);
			trackBar3Color1.Name = "Red";
			trackBar3Color1.Value = 35;
			trackBar3Color1.Width = 100;
			_ColorValue1 = trackBar3Color1.Value;
			trackBar3Color1.Location = new System.Drawing.Point(10, 260);				
			trackBar3Color1.Scroll += new EventHandler(trackBar3_Scroll);
			
			TrackBar trackBar3Color2 = new TrackBar();
			trackBar3Color2.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
			trackBar3Color2.Orientation = Orientation.Horizontal;
			trackBar3Color2.SetRange(1, 255);
			trackBar3Color2.Name = "Green";
			trackBar3Color2.Value = 15;
			trackBar3Color2.Width = 100;
			_ColorValue2 = trackBar3Color2.Value;
			trackBar3Color2.Location = new System.Drawing.Point(10, 310);				
			trackBar3Color2.Scroll += new EventHandler(trackBar3_Scroll);
			
			TrackBar trackBar3Color3 = new TrackBar();
			trackBar3Color3.BackColor = System.Drawing.Color.FromArgb(0, 0, 255);
			trackBar3Color3.Orientation = Orientation.Horizontal;
			trackBar3Color3.SetRange(1, 255);
			trackBar3Color3.Name = "Blue";
			trackBar3Color3.Value = 25;
			trackBar3Color3.Width = 100;
			_ColorValue3 = trackBar3Color3.Value;
			trackBar3Color3.Location = new System.Drawing.Point(10, 360);				
			trackBar3Color3.Scroll += new EventHandler(trackBar3_Scroll);
			
			
			// ---------------- RGB LABELS ------------ //
			//RGB value 1 //
			_ColorLabel1 = new CormacTownLabel(12); //Smaller font
			_ColorLabel1.Height = 30;
			_ColorLabel1.Width = 100;
			_ColorLabel1.ForeColor = System.Drawing.Color.Black;
			_ColorLabel1.Text = "Red: " + _ColorValue1.ToString();
			_ColorLabel1.Location = new System.Drawing.Point(120, 260);
			
			//RGB value 2//
			 _ColorLabel2 = new CormacTownLabel(12); //Smaller font
			_ColorLabel2.Height = 30;
			_ColorLabel2.Width = 120;
			_ColorLabel2.ForeColor = System.Drawing.Color.Black;
			_ColorLabel2.Text = "Green: " + _ColorValue2.ToString();
			_ColorLabel2.Location = new System.Drawing.Point(120, 310);
			
			
		    //RGB value 3//
			 _ColorLabel3 = new CormacTownLabel(12); //Smaller font
			_ColorLabel3.Height = 30;
			_ColorLabel3.Width = 100;
			_ColorLabel3.ForeColor = System.Drawing.Color.Black;
			_ColorLabel3.Text = "Blue: " + _ColorValue3.ToString();
			_ColorLabel3.Location = new System.Drawing.Point(120, 360);
			

			// ---------------- Triangle Button -------------------------- //
			CormacTownButton triangleEntityButton = new CormacTownButton();
			triangleEntityButton.Location = new System.Drawing.Point(15, this.Height - 100);
			triangleEntityButton.Text = "Triangle";	
			triangleEntityButton.Name = "Triangle";
			triangleEntityButton.Font = new System.Drawing.Font("Broadway", 10);
			triangleEntityButton.Width = 100;
			triangleEntityButton.Height = 40;
			triangleEntityButton.Click += new EventHandler (OnClickSHape);  //Event handler is the dleegate and been given particular implementaitotno of this method
			
			// ---------------- Circle Button -------------------------- //
			CormacTownButton circleEntityButton = new CormacTownButton();
			circleEntityButton.Location = new System.Drawing.Point(120, this.Height - 100);
			circleEntityButton.Text = "Circle";	
			circleEntityButton.Name = "Circle";
			circleEntityButton.Font = new System.Drawing.Font("Broadway", 10);
			circleEntityButton.Width = 100;
			circleEntityButton.Height = 40;
			circleEntityButton.Click += new EventHandler (OnClickSHape);  //Event handler is the dleegate and been given particular implementaitotno of this method
			
			// ---------------- Square Button -------------------------- //
			CormacTownButton squareEntityButton = new CormacTownButton();
			squareEntityButton.Location = new System.Drawing.Point(225, this.Height - 100);
			squareEntityButton.Text = "Square";	
			squareEntityButton.Name = "Square";
			squareEntityButton.Font = new System.Drawing.Font("Broadway", 10);
			squareEntityButton.Width = 100;
			squareEntityButton.Height = 40;
			squareEntityButton.Click += new EventHandler (OnClickSHape);  //Event handler is the dleegate and been given particular implementaitotno of this method

			
			
			
			this.Controls.Add(titleLabel);
			this.Controls.Add (_label1);
			this.Controls.Add (_label2);
			this.Controls.Add (_ColorLabel1);
			this.Controls.Add (_ColorLabel2);
			this.Controls.Add (_ColorLabel3);
			this.Controls.Add(startButton);
			
			this.Controls.Add (trackBar1);
			this.Controls.Add (trackBar2);
			this.Controls.Add (trackBar3Color1);
			this.Controls.Add (trackBar3Color2);
			this.Controls.Add (trackBar3Color3);
			
			
			this.Controls.Add (squareEntityButton);
			this.Controls.Add(circleEntityButton);
			this.Controls.Add(triangleEntityButton);
			this.Controls.Add(instructionLabel);
			this.Controls.Add(underlay);
			//this.ShowDialog ();
	
		}
		
		private void trackBar1_Scroll(object sender, System.EventArgs e)
	    {
	        // Display the trackbar value in the text box.
			_label1.Text = "Attractiveness: " + (sender as TrackBar).Value;
			_AttractivenssValue = (sender as TrackBar).Value;
	    }
		
		private void trackBar2_Scroll(object sender, System.EventArgs e)
	    {
	        // Display the trackbar value in the text box.
			_label2.Text = "Fitness: " + (sender as TrackBar).Value;
			_FitnessValue = (sender as TrackBar).Value;
	    }
		private void trackBar3_Scroll(object sender, System.EventArgs e)
	    {
	        // Display the trackbar value in the text box.
			
			
			
			string s = (sender as TrackBar).Name;
			switch(s){
			case ("Red"): _ColorValue1  =  (sender as TrackBar).Value;
									  _ColorLabel1.Text = "Red: " + (sender as TrackBar).Value;
				break;
			case ("Green"): _ColorValue2 =  (sender as TrackBar).Value;
				                      _ColorLabel2.Text = "Green: " + (sender as TrackBar).Value;
				break;
			case ("Blue"): _ColorValue3 =  (sender as TrackBar).Value;
				                      _ColorLabel3.Text = "Blue: " + (sender as TrackBar).Value;
				break;
			default: break;				
			}
			
	    }
		
		private void OnClickSHape (object sender, EventArgs e)
		{	
			
			if (_fullShapeList) {
				return;
			}
			
			Button sendingButton = (Button)sender;
			
			System.Drawing.Image shapeImage = System.Drawing.Image.FromFile (@"C:/Users/corma/Documents/Code/C\OOP/DemoForms/DemoForms/square.png");
			string s = sendingButton.Text;
			switch (s) {
			case ("Square"):
				shapeImage = System.Drawing.Image.FromFile (@"C:\Users\corma\Desktop\CormacTownJoint\Resources\images\square.png");
				break;
			case ("Circle"):
				shapeImage = System.Drawing.Image.FromFile (@"C:\Users\corma\Desktop\CormacTownJoint\Resources\images\circle.png");
				break;
			case ("Triangle"):
				shapeImage = System.Drawing.Image.FromFile (@"C:\Users\corma\Desktop\CormacTownJoint\Resources\images\triangle.png");
				break;
			default: shapeImage = null;
				break;
			}
			
			
			
			PictureBox pictureBox1 = new PictureBox ();	
			pictureBox1.Width = shapeImage.Width;
			pictureBox1.Height = shapeImage.Height;
			pictureBox1.Location = new System.Drawing.Point (this.Width - _shapesXstartPos, this.Height - _shapesYStartPos);
			pictureBox1.Image = shapeImage;
			this.Controls.Add (pictureBox1);
			
			_shapesXstartPos -= (shapeImage.Width + 5);
			
			//If x range gets close to edge of screen we reset it and move the Y value down
			if (_shapesXstartPos < 50) {
				_shapesXstartPos = 400; 
				_shapesYStartPos -= shapeImage.Height + 10;
			}
			
			
			if(_shapesYStartPos < 150)
			{
					_fullShapeList = true;	
			}
			
			_userData.AddDataEntity(s, _AttractivenssValue, _FitnessValue, new int[] {_ColorValue1, _ColorValue2, _ColorValue3});
			
		}
		
		private void OnStartClick (object sender, EventArgs e)
		{			
			
			//Also need to match up swinWindow size with this windows size	
			_userData.SwinWindowSizeX = this.Width;
			_userData.SwinWindowSizeY = this.Height;
			
			this.Controls.Add (_progressBar);
			_progressBar.BringToFront ();
			
			PrepareData prepData = new PrepareData ();
			RunTimer (_progressBar);
			
			prepData.GetData (_userData);			
			
			this.Visible = false;

			EntityEnvironment environ = prepData.GameData;
			
			EntityMain newGame = new EntityMain (environ);
		}
		
		private void RunTimer(ProgressBar b)
		{
			TimeSpan time = new TimeSpan (50000000); //10 seconds
			DateTime newTime = DateTime.Now;
			var timeDiff = newTime + time;
			
			bool breakTimer = false;
			
			while (b.Value < 30000)
			{
				
//				DateTime currTime = DateTime.Now;
//				var diff = currTime.Subtract (timeDiff);
//				if (diff.TotalMilliseconds > 0)
//				{
//					//b.Value = 6000;
//					breakTimer = true;
//				}
				b.PerformStep();
			}
			//return breakTimer;
		}
			

		public void CloseForm ()
		{
			this.Visible = false;
			Close ();
		}
	}
}


// --------------------------------------- LoadingTask ------------------------------------------- //
using System;
using System.Threading.Tasks;
using MyGame;

namespace MyGame
{
	public class LoadingTask
	{
		GetUserData _userData;
		
		public LoadingTask (GetUserData userdata)
		{
			_userData = userdata;
		}
		
		
		public async Task<EntityEnvironment> LoadGame ()
		{			
			 return await Task.Run(() => OpenSwinGameWindow(_userData));
		}
				
		private static async Task<EntityEnvironment> OpenSwinGameWindow(GetUserData userData)
		{
			PrepareData prepData = new PrepareData();
			prepData.GetData(userData);
			return prepData.GameData;
		}
	}
}
// --------------------------------------- GetUserData ------------------------------------------- //
using System;
using System.Collections.Generic;

namespace MyGame
{
	public class GetUserData
	{
		List<GameData> _userInputList;
		
		int _swinWindowSizeX;
		int _swinWindowSizeY;
		// ----------------------- NEED TO PASS IN EVENT WHICH WILL LOAD SWINGAME FROM FORM -------------------------------- //
		
		
		public GetUserData ()
		{			
			_userInputList = new List<GameData>();		
		}
		
		//Shape type, traits added with string reference
		public void AddDataEntity(string s, int a, int b, int[] rgbArray)
		{
			GameData geneTuple = new GameData(new Tuple<string, int> ("Attractiveness", a), 
				new Tuple<string, int>("Fitness", b), rgbArray, s);
			
			_userInputList.Add(geneTuple);
		}
		
		public int SwinWindowSizeX{
			get{return _swinWindowSizeX;}
			set{_swinWindowSizeX = value;}
		}

		public int SwinWindowSizeY{
			get{return _swinWindowSizeY;}
			set{_swinWindowSizeY = value;}
		}		
		
		public List<GameData> UserInputList{
			get{return _userInputList;}
			set{_userInputList = value;}
		}
		
		
	}
}
// --------------------------------------- GameData ------------------------------------------- //
using System;

namespace MyGame
{
	public struct GameData
	{
		Tuple<string, int> trait1;
		Tuple<string, int> trait2;
		int[] _colorRgbArray;
		string shapeType;
		
		public GameData(Tuple<string, int> t1, Tuple<string, int> t2, int[] intArr, string s){
			trait1 = t1;
			trait2 = t2;
			_colorRgbArray = intArr;		
			shapeType = s;
		}

		public Tuple<string, int> Trait1{
			get{return trait1;}
			set{trait1 = value;}
		}

		public Tuple<string, int> Trait2{
			get{return trait2;}
			set{trait2 = value;}
		}

		public int[] ColorRgbArray{
			get{return _colorRgbArray;}
		}

		public string ShapeType{
			get{return shapeType;}
			set{shapeType = value;}
		}

	}
}


// ------------------------------ TESTS --------------------------------------------- //
using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace MyGame
{
	
	[TestFixture ()]
	public class TestNewGenes
	{

		[Test()]
		public void AddGeneHighDifference()
		{
			Attractiveness a1 = new Attractiveness(5);
			int a = a1.GeneValue[0];
			Attractiveness a2 = new Attractiveness(100);
			a1.CombineGenes(a2);
			Assert.True(a1.GeneValue[0] >= 10);			
		}

		[Test()]
		public void AddGeneModDifference()
		{
			Attractiveness a1 = new Attractiveness(23);
			int a = a1.GeneValue[0];
			Attractiveness a2 = new Attractiveness(45);
			a1.CombineGenes(a2);
			Assert.True(a1.GeneValue[0] >= 23);			
		}
		
		[Test()]
		public void AddGeneSmallDifference()
		{
			Attractiveness a1 = new Attractiveness(40);
			int a = a1.GeneValue[0];
			Attractiveness a2 = new Attractiveness(45);
			a1.CombineGenes(a2);
			Assert.True(a1.GeneValue[0] >= 40);			
		}
		
		
		
		public void GenesMutate()
		{
	
		}
		
		// ------------------- Stronger Gene Should have a larger influence -------------------- //
		[Test()]
		public void FavourabilityToMate ()
		{
			GeneList newList = new GeneList ();
			ColorGene newGene = new ColorGene (34, 34, 12);
			Attractiveness aGene = new Attractiveness (12);
			PhysicalStrength strength1 = new PhysicalStrength (78);
			newList.AddGene (aGene);
			newList.AddGene (newGene);
			newList.AddGene (strength1);

			GeneList newList2 = new GeneList ();
			ColorGene newGene2 = new ColorGene (2, 1, 145);
			Attractiveness aGene2 = new Attractiveness (100);
			PhysicalStrength strength2 = new PhysicalStrength (88);
			newList2.AddGene (aGene2);
			newList2.AddGene (newGene2);
			newList2.AddGene (strength2);
			
			Square s = new Square (400, 100, newList);
			Square s1 = new Square (150, 200, newList2);
			
			// ----------------   ---------//
			int births1 = 0;
			int births2 = 0;
			for (int i = 0; i < 1000; i++)
			{	
				//Low attractivenss mate will usually want the better entity
				if (s.IsAttractedToEntity (s1))
				{
					births1++;
				}
				
				//high attractivity mate will usually not want the lower attractiveness entity
				if (s1.IsAttractedToEntity(s)){
					births2++;
				}
			}
			
			Assert.True((births2 < births1));    //Should be generaly a lower birthing as 1 entity is less attractive			
		}
		
		[Test()]
		public void FavourabilityToMate2 ()
		{
			GeneList newList = new GeneList ();
			ColorGene newGene = new ColorGene (34, 34, 12);
			Attractiveness aGene = new Attractiveness (50);
			PhysicalStrength strength1 = new PhysicalStrength (78);
			newList.AddGene (aGene);
			newList.AddGene (newGene);
			newList.AddGene (strength1);

			GeneList newList2 = new GeneList ();
			ColorGene newGene2 = new ColorGene (2, 1, 145);
			Attractiveness aGene2 = new Attractiveness (52);
			PhysicalStrength strength2 = new PhysicalStrength (88);
			newList2.AddGene (aGene2);
			newList2.AddGene (newGene2);
			newList2.AddGene (strength2);
			
			Square s = new Square (400, 100, newList);
			Square s1 = new Square (150, 200, newList2);
			
			// ----------------   ---------//
			int births1 = 0;
			int births2 = 0;
			for (int i = 0; i < 1000; i++)
			{	
				//Low attractivenss mate will usually want the better entity
				if (s.IsAttractedToEntity (s1))
				{
					births1++;
				}
				
				//high attractivity mate will usually not want the lower attractiveness entity
				if (s1.IsAttractedToEntity (s))
				{
					births2++;
				}
			}
			
			//Should both be more likely to be attracted to each other
			Assert.True (((births2 > 500) && (births1 > 500)));
		}
		
		[Test()]
		public void ColorTransfer(){
			ColorGene gene1 = new ColorGene(12, 230, 45);
			ColorGene gene2 = new ColorGene(45,67,89);
			ColorGene gene3 = new ColorGene(56, 190, 189);
			ColorGene gene4 = new ColorGene(89, 240, 120);
			
			
			gene1.CombineGenes(gene2);
			gene3.CombineGenes(gene4);
			
			bool diff1 =  ((gene1.GeneValue[0] != 12) && (gene1.GeneValue[1] != 230) && (gene1.GeneValue[2] != 45));
			bool diff2 =  ((gene3.GeneValue[0] != 56) && (gene3.GeneValue[1] != 190) && (gene3.GeneValue[2] != 189));
			
			Assert.True(diff1 && diff2);
			
		}
	}
}







