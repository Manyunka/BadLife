using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Plant : Creature
	{
		public Plant(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
		}

		public override void Update(ref ICollection<Creature> creatures, ref ICollection<MapObject> mapObjects)
		{
		}
	}
}
