using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine
{
	public class Animal : Sprite
	{
		int dx = 0, dy = 0;
		public Animal(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{

		}

		public void Move()
		{
			Random random = new Random();


			if (random.Next(100) == 0)
			{
				switch (random.Next(5))
				{
					case 0:
						dx = 0;
						dy = -1;
						break;
					case 1:
						dx = 1;
						dy = 0;
						break;
					case 2:
						dx = -1;
						dy = 0;
						break;
					case 3:
						dx = 0;
						dy = 1;
						break;
					case 4:
						dx = 0;
						dy = 0;
						break;
				}
			}


			if (X <= 0 && frameIndex == 1)
			{
				switch (random.Next(3))
				{
					case 0:
						dx = 1;
						dy = 0;
						break;
					case 1:
						dx = 0;
						dy = 1;
						break;
					case 2:
						dx = 0;
						dy = -1;
						break;
				}

			}
			else if (X >= 2048 - Width && frameIndex == 2)
			{
				switch (random.Next(3))
				{
					case 0:
						dx = -1;
						dy = 0;
						break;
					case 1:
						dx = 0;
						dy = 1;
						break;
					case 2:
						dx = 0;
						dy = -1;
						break;
				}
			}

			if (Y <= 0 && frameIndex == 3)
			{
				switch (random.Next(3))
				{
					case 0:
						dx = 0;
						dy = 1;
						break;
					case 1:
						dx = 1;
						dy = 0;
						break;
					case 2:
						dx = -1;
						dy = 0;
						break;
				}

			}
			else if (Y >= 2048 - Width && frameIndex == 0)
			{
				switch (random.Next(3))
				{
					case 0:
						dx = 0;
						dy = -1;
						break;
					case 1:
						dx = 1;
						dy = 0;
						break;
					case 2:
						dx = -1;
						dy = 0;
						break;
				}
			}			

			X += dx;
			Y += dy;

			if (dx > 0) SetFrameIndex(2);
			if (dx < 0) SetFrameIndex(1);
			if (dy < 0) SetFrameIndex(3);
			if (dy > 0) SetFrameIndex(0);

			if (Math.Abs(dx) > 0 || Math.Abs(dy) > 0) NextFrame();
			else SetFrame(1);
		}
	}
}
