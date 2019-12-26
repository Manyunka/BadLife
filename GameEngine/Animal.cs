using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameEngine
{
	public abstract class Animal : Sprite
	{
		protected int dx = 0, dy = 0;
		static protected Random random;

		protected Sprite Goal;
		protected int Speed { get; set; }
		protected int searchRadius;
		protected bool foodFound;

		public double Satiety { get; protected set; }
		public Animal(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
			random = new Random(Environment.TickCount);

			Speed = 1;

			MoveRandom();
			switch (random.Next(3))
			{
				case 0:
					dx = 0;
					dy = -Speed;
					break;
				case 1:
					dx = Speed;
					dy = 0;
					break;
				case 2:
					dx = -Speed;
					dy = 0;
					break;
				case 3:
					dx = 0;
					dy = Speed;
					break;
			}

			Satiety = 1;
			searchRadius = 300;

			for (int i = 0; i < 1000; i++);
		}

		public void Update<T>(ref ICollection<T> gameObjects)
		{
			Satiety -= 0.0005;
			if (Satiety <= 0.8)
			{
				if (Satiety <= 0) Visible = false;
				if (currentFrame == 1)
				{
					SearchFood(ref gameObjects);
					if (Goal != null) MoveToFood(ref gameObjects);
					else MoveRandom();
				}
				else NextFrame();
			}
			else
			{
				MoveRandom();
			}

			Move();
		}

		private void Move()
		{
			X += dx;
			Y += dy;
		}

		private void MoveRandom()
		{
			if (random.Next(100) == 0)
			{
				switch (random.Next(4))
				{
					case 0:
						dx = 0;
						dy = -Speed;
						break;
					case 1:
						dx = Speed;
						dy = 0;
						break;
					case 2:
						dx = -Speed;
						dy = 0;
						break;
					case 3:
						dx = 0;
						dy = Speed;
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
						dx = Speed;
						dy = 0;
						break;
					case 1:
						dx = 0;
						dy = Speed;
						break;
					case 2:
						dx = 0;
						dy = -Speed;
						break;
				}

			}
			else if (X >= 2048 - Width && frameIndex == 2)
			{
				switch (random.Next(3))
				{
					case 0:
						dx = -Speed;
						dy = 0;
						break;
					case 1:
						dx = 0;
						dy = Speed;
						break;
					case 2:
						dx = 0;
						dy = -Speed;
						break;
				}
			}

			if (Y <= 0 && frameIndex == 3)
			{
				switch (random.Next(3))
				{
					case 0:
						dx = 0;
						dy = Speed;
						break;
					case 1:
						dx = Speed;
						dy = 0;
						break;
					case 2:
						dx = -Speed;
						dy = 0;
						break;
				}

			}
			else if (Y >= 2048 - Height && frameIndex == 0)
			{
				switch (random.Next(3))
				{
					case 0:
						dx = 0;
						dy = -Speed;
						break;
					case 1:
						dx = Speed;
						dy = 0;
						break;
					case 2:
						dx = -Speed;
						dy = 0;
						break;
				}
			}

			if (dx > 0) SetFrameIndex(2);
			if (dx < 0) SetFrameIndex(1);
			if (dy < 0) SetFrameIndex(3);
			if (dy > 0) SetFrameIndex(0);

			if (Math.Abs(dx) > 0 || Math.Abs(dy) > 0)
			NextFrame();
			else SetFrame(1);
		}

		public abstract void SearchFood<T>(ref ICollection<T> gameObjects);
		public abstract void MoveToFood<T>(ref ICollection<T> gameObjects);

		public override void Draw(Graphics g)
		{
			var fontFamily = new FontFamily("Times New Roman");
			var font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
			StringFormat sf = new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			};
			g.DrawString("Сытость: " + Math.Ceiling(Satiety * 100) + "%", font, Brushes.White, GetCenterX(), Y - 5, sf);

			base.Draw(g);
		}
	}
}
