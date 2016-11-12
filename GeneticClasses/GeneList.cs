using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyGame
{
	public class GeneList
	{
		//Need to fugure out how Mutation will be implimented

		private List<IAmGene> _genes = new List<IAmGene>(); 

        public GeneList()
        {
        }
		
		/// <summary>
		/// r, g, b ints represent 256 Hex color values
		/// </summary>
		/// <param name="r">The red component.</param>
		/// <param name="g">The green component.</param>
		/// <param name="b">The blue component.</param>


		public List<IAmGene> GetGenesList
		{
			get{return _genes;}
		}
		/// <summary>
		/// Returns a newGene from the 2 parents - random weighting in favor of stronger gene dominance
		/// </summary>
		/// <returns>The over.</returns>
		public void CrossOver (IAmGene a, IAmGene b)
		{
//			int geneVal = 0;
//			var newRand = new Random ();
//			if (a.IAmGene >= b.GeneValue) {
//				geneVal = b.GeneValue*(newRand.Next (b.GeneValue));
//				geneVal = geneVal + a.GeneValue;
//				//Gene value can not be over 255 at this stage!
//				if (geneVal >= 255) {
//					geneVal = 255;
//				}
//				return geneVal;
//			} 
//			else { 
//				geneVal = a.GeneValue * (newRand.Next (a.GeneValue));
//				geneVal = geneVal + b.GeneValue;
//				//Gene value can not be over 255 at this stage!
//				if (geneVal >= 255) {
//					geneVal = 255;
//				}
//				return geneVal;	
//			}
		}
		
		public void AddGene(IAmGene g)
		{
			_genes.Add(g);
		}
		
		public bool ContainsGeneInstance (IAmGene g)
		{
			// ------------ Is this better / faster than foreach through a loop
			return Object.ReferenceEquals (this, g);
		}
		
		//Should return a Gene from which you search i.e. Type = ColorGene it will return it from the geneList
		public IAmGene ReturnGene (string t)
		{
			foreach (IAmGene g in _genes)
			{
				if (g.Name == t)
				{
					return  g;
				}
			}
			System.Diagnostics.Debug.WriteLine("Did not find Gene");
			return null;
		}
		
		public GeneList CombineGeneLists (GeneList list)
		{
			GeneList newList = new GeneList();
			//Fix while figuring bug!
			IAmGene newgene = list.GetGenesList[0];
			IAmGene newgene2 = list.GetGenesList[1];
            IAmGene newgene3 = list.GetGenesList[2];

            IAmGene currentListGene = _genes[0];
			IAmGene currentListGene2 = _genes[1];
            IAmGene currentListGene3 = _genes[2];

            newList.AddGene(newgene.CombineGenes(currentListGene));
			newList.AddGene(newgene2.CombineGenes(currentListGene2));
            newList.AddGene(newgene3.CombineGenes(currentListGene3));

            return newList;
			
		}
		
		public void MutateGene (IAmGene g, IAmGene g2)
		{
			//Incomplete
		}
	


	}
}

