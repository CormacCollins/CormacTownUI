using System;
using SwinGameSDK;
using System.Collections.Generic;
using System.Threading;
using System.Security.Cryptography;

namespace MyGame
{
	
	public static class Move 
	{		
		public static void MovementChange (IGameObject gameEnt, EntityEnvironment e)
		{
			//Check Ent against each other entity in List
					
			//No movement while 'Paused'
//			if (e.GameState != UIButtonFlags.pause)
//			{
//				return;
//			}
			foreach (IGameObject g in e.GameEntities)
			{
				if ((g as GameEntity).ID != (gameEnt as GameEntity).ID)
				{
					(gameEnt as GameEntity).UpdateEntity (g, e);
				}
			}
			
		}
	}
}

