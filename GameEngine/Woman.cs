using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Woman : Human
	{
		public bool IsPregnant { get; private set; }
		public Woman(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
			IsPregnant = false;
		}

		public void Childbearing()
		{
			IsPregnant = true;
		}

		public Human GiveBirth()
		{
			Human child;
			if (random.Next(2) == 0)
				child = new Man(new Bitmap(@"Images/man.png"), 32, 48);
			else child = new Woman(new Bitmap(@"Images/woman.png"), 32, 48);

			int x = X;
			int y = Y;
			child.SetLocation(x, y);

			int ind = random.Next(4);
			child.SetFrameIndex(ind);
			child.SetFPS(6);

			IsPregnant = false;

			return child;
		}
	}
}
