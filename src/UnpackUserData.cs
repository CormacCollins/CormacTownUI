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
				Random RandomY = new Random((int)DateTime.Now.Ticks);
				int x = GetAndRemove(_xPos, RandomX.Next(1, _xPos.Count-1));
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
			for (int i = 1; i < 58; i++)
			{
				s.Add(i*10);
			}			
		}
		public void FillY (List<int> s)
		{
			for (int i = 1; i < 59; i++)
			{
				s.Add(i*10);
			}			
		}

		public int GetAndRemove(List<int> list, int index){
			int val = list[index];
			list.RemoveAt(index);
			return val;
		}
					
	}
}

