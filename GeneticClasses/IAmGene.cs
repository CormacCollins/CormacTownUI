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

