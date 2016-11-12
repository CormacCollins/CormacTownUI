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

