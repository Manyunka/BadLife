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
		ICollection<Herbivorous> Bunnies;

		Bitmap FoxBitmap;
		Predator Fox;

		ICollection<Sprite> Deads;

		static Random random;

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

			foreach (Herbivorous b in Bunnies)
			{
				b.Draw(g);
			}
			Fox.Draw(g);

			CreateGraphics().DrawImage(backBuffer, GameMap);
		}

		public override void OnUpdate()
		{
			foreach (Herbivorous b in Bunnies)
			{
				b.Update(ref Plants);
				if (b.Satiety <= 0) Deads.Add(b);
			}
			Fox.Update(ref Bunnies);

			foreach (Sprite d in Deads)
			{
				if (Bunnies.Contains(d))
					Bunnies.Remove((Herbivorous)d); 
			}

			Deads.Clear();

			if (random.Next(10) == 0)
			{
				Plant p = new Plant(PlantBitmap, 32, 32);

				int x = random.Next(16, 2048);
				int y = random.Next(16, 2048);
				p.SetLocation(x, y);

				int frame = random.Next(7);
				p.SetFrame(frame);
				Plants.Add(p);
			}

			if (random.Next(20) == 0)
			{
				Herbivorous b = new Herbivorous(BunnyBitmap, 48, 48);

				int x = random.Next(16, 2048) + 16;
				int y = random.Next(16, 2048) + 16;
				b.SetLocation(x, y);

				int ind = random.Next(4);
				b.SetFrameIndex(ind);
				b.SetFPS(6);

				Bunnies.Add(b);
			}
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

			random = new Random();
			for (int i = 0; i < row; i++)
				for (int j = 0; j < col; j++)
					if (random.Next(3) == 0)
					{
						if (random.Next(2) == 0)
							TileMap.SetTile(i, j, 1);
						else TileMap.SetTile(i, j, 2);
					}

			PlantBitmap = new Bitmap(@"Images/plants.png");
			Plants = new List<Plant>();
			for (int i = 0; i < 500; i++)
			{
				Plant p = new Plant(PlantBitmap, 32, 32);

				int x = random.Next(16, 2048);
				int y = random.Next(16, 2048);
				p.SetLocation(x, y);

				int frame = random.Next(7);
				p.SetFrame(frame);
				Plants.Add(p);
			}

			BunnyBitmap = new Bitmap(@"Images/bunny.png");
			Bunnies = new List<Herbivorous>();
			for (int i = 0; i < 50; i++)
			{
				Herbivorous b = new Herbivorous(BunnyBitmap, 48, 48);

				int x = random.Next(16, 2048) + 16;
				int y = random.Next(16, 2048) + 16;
				b.SetLocation(x, y);

				int ind = random.Next(4);
				b.SetFrameIndex(ind);
				b.SetFPS(6);

				Bunnies.Add(b);
			}

			FoxBitmap = new Bitmap(@"Images/fox.png");
			Fox = new Predator(FoxBitmap, 48, 48);
			Fox.SetFrameIndex(2);
			Fox.SetFrame(1);
			Fox.SetLocation(500, 500);
			Fox.SetFPS(6);

			Deads = new List<Sprite>();
		}

		public override void OnUnloadContent()
		{
			TileBitmap.Dispose();

			PlantBitmap.Dispose();

			BunnyBitmap.Dispose();
		}

	}
}
