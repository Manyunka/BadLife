using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameEngine
{
	public class TileMap
	{
		int Row, Col, Size;
		int[,] TileData;
		Tile[] Tiles;

		public TileMap(Tile[] tiles, int row, int col, int size)
		{
			Row = row;
			Col = col;
			Size = size;

			TileData = new int[row, col];
			Tiles = tiles;
		}

		public void SetTile(int x, int y, int tile)
		{
			TileData[x, y] = tile;
		}
		public int GetTile(int x, int y)
		{
			return TileData[x, y];
		}

		public void Draw(Graphics g)
		{
			for (int x = 0; x < Row; x++)
				for (int y = 0; y < Col; y++)
					{
						Rectangle rect = new Rectangle(x * Size, y * Size, Size, Size);
						g.DrawImage(Tiles[TileData[x, y]].Bitmap, rect);
					}
		}
	}
}
