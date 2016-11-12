using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Attractiveness : IAmGene
    {
		private List<int> _geneValue;
		private string _name;
		
		public Attractiveness(int value){
			_name = "Attractiveness";
			_geneValue = new List<int>();
			_geneValue.Add(value);
		}

		public List<int> GeneValue
		{
			get
			{
				return _geneValue;
			}
			set
			{
				_geneValue = value;
			}
		}
		
		//Need to code so small variance occurs with change of mutation
		public IAmGene CombineGenes(IAmGene g){

            //Apply CouldMutate Fuction to potentially mutate - else slightly change value from parents for child
			Attractiveness atr = new Attractiveness(CouldMutate(g.GeneValue[0], _geneValue[0]));
			return (atr as IAmGene);
		}

		public string Name
		{
			get{return _name;}
			set{_name = value;}
		}

        public int CouldMutate (int a, int b)
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
			int randNumber = newRand.Next (15);
			
			// ------------------- 1 in 15 change of RANDOM MUTATION ---------------------- //
			
			if (randNumber == 1)
			{
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
