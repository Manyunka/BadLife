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
		//ICollection<Plant> Plants;

		Bitmap BunnyBitmap;
		//ICollection<Herbivorous> Bunnies;

		Bitmap FoxBitmap;
		//ICollection<Predator> Foxes;

		Bitmap WomanBitmap;
		Bitmap ManBitmap;
		//ICollection<Human> People;

		ICollection<Creature> Creatures;

		ICollection<Creature> Borns;

		ICollection<Creature> Deads;

		ICollection<MapObject> MapObjects;

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

			/*foreach (Plant p in Plants)
			{
				p.Draw(g);
			}

			foreach (Herbivorous b in Bunnies)
			{
				b.Draw(g);
			}*/

			/*foreach (Predator f in Foxes)
			{
				f.Draw(g);
			}*/

			foreach (MapObject o in MapObjects)
			{
				o.Draw(g);
			}

			foreach (Creature d in Creatures)
			{
				d.Draw(g);
			}

			CreateGraphics().DrawImage(backBuffer, GameMap);
		}

		public override void OnUpdate()
		{
			foreach (Creature d in Creatures)
			{
				d.Update(ref Creatures, ref MapObjects);

				if (d.GetType() == typeof(Woman) && ((Woman)d).IsPregnant)
				{
					Borns.Add(((Woman)d).GiveBirth());
				}

				if (!d.Visible) Deads.Add(d);
			}

			foreach (Creature d in Borns)
			{
					Creatures.Add(d);
			}

			foreach (Creature d in Deads)
			{
				if (Creatures.Contains(d))
					Creatures.Remove(d);
			}

			Deads.Clear();
			Borns.Clear();

			if (random.Next(10) == 0)
			{
				Plant p = new Plant(PlantBitmap, 32, 32);

				int x = random.Next(16, 2048);
				int y = random.Next(16, 2048);
				p.SetLocation(x, y);

				int frame = random.Next(7);
				p.SetFrame(frame);
				Creatures.Add(p);
			}
			if (random.Next(20) == 0)
			{
				Herbivorous b = new Herbivorous(BunnyBitmap, 48, 48);

				int x = random.Next(16, 2048);
				int y = random.Next(16, 2048);
				b.SetLocation(x, y);

				int ind = random.Next(4);
				b.SetFrameIndex(ind);
				b.SetFPS(6);

				Creatures.Add(b);
			}
			if (random.Next(20) == 0)
			{
				Predator p = new Predator(FoxBitmap, 48, 48);

				int x = random.Next(16, 2048);
				int y = random.Next(16, 2048);
				p.SetLocation(x, y);

				int ind = random.Next(4);
				p.SetFrameIndex(ind);
				p.SetFPS(6);

				Creatures.Add(p);
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

			Creatures = new List<Creature>();

			PlantBitmap = new Bitmap(@"Images/plants.png");
			for (int i = 0; i < 300; i++)
			{
				Plant p = new Plant(PlantBitmap, 32, 32);

				int x = random.Next(16, 2048);
				int y = random.Next(16, 2048);
				p.SetLocation(x, y);

				int frame = random.Next(7);
				p.SetFrame(frame);
				Creatures.Add(p);
			}

			BunnyBitmap = new Bitmap(@"Images/bunny.png");
			for (int i = 0; i < 30; i++)
			{
				Herbivorous b = new Herbivorous(BunnyBitmap, 48, 48);

				int x = random.Next(16, 2048) + 16;
				int y = random.Next(16, 2048) + 16;
				b.SetLocation(x, y);

				int ind = random.Next(4);
				b.SetFrameIndex(ind);
				b.SetFPS(6);

				Creatures.Add(b);
			}

			FoxBitmap = new Bitmap(@"Images/fox.png");
			for (int i = 0; i < 20; i++)
			{
				Predator f = new Predator(FoxBitmap, 48, 48);

				int x = random.Next(16, 2048) + 16;
				int y = random.Next(16, 2048) + 16;
				f.SetLocation(x, y);

				int ind = random.Next(4);
				f.SetFrameIndex(ind);
				f.SetFPS(6);
				Creatures.Add(f);
			}

			ManBitmap = new Bitmap(@"Images/man.png");
			WomanBitmap = new Bitmap(@"Images/woman.png");
			for (int i = 0; i < 15; i++)
			{
				Human h;
				if (random.Next(2) == 0)
					h = new Man(ManBitmap, 32, 48);
				else h = new Woman(WomanBitmap, 32, 48);

				int x = random.Next(16, 2048) + 16;
				int y = random.Next(16, 2048) + 16;
				h.SetLocation(x, y);

				int ind = random.Next(4);
				h.SetFrameIndex(ind);
				h.SetFPS(6);
				Creatures.Add(h);
			}
			/*Fox = new Predator(FoxBitmap, 48, 48);
			Fox.SetFrameIndex(2);
			Fox.SetFrame(1);
			Fox.SetLocation(500, 500);
			Fox.SetFPS(6);*/

			MapObjects = new List<MapObject>();

			Borns = new List<Creature>();
			Deads = new List<Creature>();
		}

		public override void OnUnloadContent()
		{
			TileBitmap.Dispose();

			PlantBitmap.Dispose();

			BunnyBitmap.Dispose();

			FoxBitmap.Dispose();
		}

	}
}
