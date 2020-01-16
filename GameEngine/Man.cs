using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Man : Human
	{
		public Man(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
			
		}

		public void BuildHouse(ref ICollection<MapObject> mapObjects)
		{
			Home = new House(new Bitmap(@"Images/house.png"));
			Home.SetLocation(X, Y);
			mapObjects.Add(Home);
		}
	}
}
