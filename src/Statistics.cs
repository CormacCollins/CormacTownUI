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
