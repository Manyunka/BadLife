using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GameEngine;

namespace BadLife
{
	public class Game: GameView
	{
		Bitmap TileBitmap;
		Tile[] Tiles;
		TileMap TileMap;

		Bitmap PlantBitmap;
		ICollection<Plant> Plants;

		Bitmap BunnyBitmap;
		Animal Bunny;
		int index = 1;
		int dx = -1;
		int dy = 0;

		public Game()
		{
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
		}
		public override void OnDraw(Graphics g, Bitmap backBuffer)
		{
			g.Clear(Color.Green);
			MoveMap();

			TileMap.Draw(g);

			foreach (Plant p in Plants)
			{
				p.Draw(g);
			}

			Bunny.Draw(g);
			CreateGraphics().DrawImage(backBuffer, GameMap);
		}

		public override void OnUpdate()
		{
			Bunny.Move();

		}

		public override void OnLoadContent()
		{
			int tileSize = 64;
			TileBitmap = new Bitmap(@"Images/tiles.png");
			Tiles = new Tile[3];
			Tiles[0] = new Tile(TileBitmap.Clone(new Rectangle(0, 0, tileSize, tileSize), System.Drawing.Imaging.PixelFormat.Undefined));
			Tiles[1] = new Tile(TileBitmap.Clone(new Rectangle(tileSize, 0, tileSize, tileSize), System.Drawing.Imaging.PixelFormat.Undefined));
			Tiles[2] = new Tile(TileBitmap.Clone(new Rectangle(tileSize * 2, 0, tileSize, tileSize), System.Drawing.Imaging.PixelFormat.Undefined));

			int row = 2048 / tileSize;
			int col = 2048 / tileSize;
			TileMap = new TileMap(Tiles, row, col, tileSize);

			Random random = new Random();
			for (int i = 0; i < row; i++)
				for (int j = 0; j < col; j++)
					if (random.Next(3) == 0)
					{
						if (random.Next(2) == 0)
							TileMap.SetTile(i, j, 1);
						else TileMap.SetTile(i, j, 2);
					}

			BunnyBitmap = new Bitmap(@"Images/bunny.png");
			Bunny = new Animal(BunnyBitmap, 48, 48);
			Bunny.SetFrameIndex(2);
			Bunny.SetFrame(1);
			Bunny.SetFPS(6);
			Bunny.SetLocation(200, 200);

			PlantBitmap = new Bitmap(@"Images/plants.png");
			Plants = new List<Plant>();
			for (int i = 0; i < 100; i++)
			{
				Plant p = new Plant(PlantBitmap, 32, 32);

				int x = random.Next(2032) + 16;
				int y = random.Next(2032) + 16;
				p.SetLocation(x, y);

				int frame = random.Next(7);
				p.SetFrame(frame);
				Plants.Add(p);
			}
		}

		public override void OnUnloadContent()
		{
			TileBitmap.Dispose();

			PlantBitmap.Dispose();

			BunnyBitmap.Dispose();
		}

	}
}
