using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Plant : Sprite
	{
		protected double Health { get; set; }
		public Plant(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
			Health = 1;
		}
	}
}
