using System;
using System.Collections.Generic;

namespace MyGame
{
	public interface IGameObject
	{		
		/// <summary>
		/// Originally planned to be implemented so there could be objects other than Entities that were Collissionable
		/// Needs to factored out in next iteration
		/// </summary>
		/// <returns><c>true</c> if this instance is at the specified x y; otherwise, <c>false</c>.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		bool IsAt(float x, float y);
		void Draw();
		
		float X{
			get;
			set;
		}
		float Y{
			get;
			set;
		}
	}
	
}

