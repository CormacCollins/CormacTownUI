using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PhysicalStrength : IAmGene
    {
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

		public bool IsMutated
		{
			get
			{
				return _isMutated;
			}
			set
			{
				_isMutated = value;
			}
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

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public IAmGene CombineGenes (IAmGene g)
		{
			int gVal = CouldMutate (g.GeneValue [0], _geneValue [0]);
			if (gVal > 100){
				gVal = 100;
			}
            //Apply CouldMutate Fuction to potentially mutate - else slightly change value from parents for child
            PhysicalStrength atr = new PhysicalStrength(gVal, _isMutated);
            return (atr as IAmGene);
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
				_isMutated = true;
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
