using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public abstract class Layer
	{
		public Layer()
		{
			X = Y = Width = Height = 0;
			Visible = true;
		}

		public int X { get; protected set; }
		public int Y { get; protected set; }
		public int Width { get; protected set; }
		public int Height { get; protected set; }

		public bool Visible { get; set; }

		public int GetCenterX()
		{
			return X + Width / 2;
		}
		public int GetCenterY()
		{
			return Y + Height / 2;
		}

		public void SetLocation(int x, int y)
		{
			X = x;
			Y = y;
		}

		public abstract void Draw(System.Drawing.Graphics g);
		//public abstract void Update();
	}
}
