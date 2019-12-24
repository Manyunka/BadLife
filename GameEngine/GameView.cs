using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameEngine
{
	public class GameView: Form
	{
		public enum Direction { Right, Left, Up, Down }
		protected bool right, left, up, down;
		protected Rectangle GameMap;
		public GameView()
		{
			Size = new Size(896, 640);
			right = left = up = down = false;
			KeyDown += new KeyEventHandler(GameView_KeyDown);
			KeyUp += new KeyEventHandler(GameView_KeyUp);

			GameMap = new Rectangle(0, 0, 2048, 2048);
		}

		public virtual void OnUpdate()
		{

		}

		public virtual void OnDraw(Graphics g, Bitmap backBuffer)
		{

		}

		public virtual void OnLoadContent()
		{

		}

		public virtual void OnUnloadContent()
		{

		}

		private void GameView_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Right:
					right = true;
					break;
				case Keys.Left:
					left = true;
					break;
				case Keys.Up:
					up = true;
					break;
				case Keys.Down:
					down = true;
					break;
			}
		}

		private void GameView_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Right:
					right = false;
					break;
				case Keys.Left:
					left = false;
					break;
				case Keys.Up:
					up = false;
					break;
				case Keys.Down:
					down = false;
					break;
			}
		}

		public void MoveMap()
		{
			if (right && GameMap.X > -2048 + ClientSize.Width) { GameMap.X -= 8; }
			if (left && GameMap.X < 0) { GameMap.X += 8; }
			if (up && GameMap.Y < 0) { GameMap.Y += 8; }
			if (down && GameMap.Y > -2048 + ClientSize.Height) { GameMap.Y -= 8; }
		}

	}
}
