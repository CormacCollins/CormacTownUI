using System;
using System.Collections.Generic;

namespace MyGame
{
	public interface IAmGene
	{
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

