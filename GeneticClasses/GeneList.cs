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

