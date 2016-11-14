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

