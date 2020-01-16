using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public abstract class Creature : Sprite
	{
		public Creature(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
		}

		public abstract void Update(ref ICollection<Creature> creatures, ref ICollection<MapObject> mapObjects);
		/*public override void Draw(Graphics g)
		{
			base.Draw(g);
		}*/
	}
}
