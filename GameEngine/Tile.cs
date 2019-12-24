using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Tile
	{
		public Tile(Bitmap bitmap)
		{
			Bitmap = bitmap;
		}
		public Bitmap Bitmap { get; }
		
	}
}
