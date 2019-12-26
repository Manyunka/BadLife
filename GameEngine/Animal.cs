using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameEngine
{
	public class Animal : Sprite
	{
		int dx = 0, dy = 0;
		readonly Random random;

		protected int Speed { get; set; }
		protected int searchRadius;
		protected bool foodFound;
		Plant Goal = null;

		public double Satiety { get; protected set; }
		public Animal(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
			Satiety = 1;
			Speed = 1;
			searchRadius = 300;
			//foodFound = false;

			random = new Random(DateTime.Now.Millisecond);
			switch (random.Next(5))
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

		public void Update(ref ICollection<Plant> gameObjects)
		{
			Satiety -= 0.0005;
			if (Satiety <= 0.98)
			{
				//Speed = 2;
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
				//Speed = 1;
				MoveRandom();
			}

			Move();
		}

		public void SearchFood(ref ICollection<Plant> gameObjects)
		{
			long min;
			if (Goal != null)
				min = (GetCenterX() - Goal.GetCenterX()) * (GetCenterX() - Goal.GetCenterX())
					+ (GetCenterY() - Goal.GetCenterY()) * (GetCenterY() - Goal.GetCenterY());
			else min = 1000000000;
			foreach (Plant gameObject in gameObjects)
			{
				long dist = (GetCenterX() - gameObject.GetCenterX()) * (GetCenterX() - gameObject.GetCenterX())
					+ (GetCenterY() - gameObject.GetCenterY()) * (GetCenterY() - gameObject.GetCenterY());
				if (dist <= searchRadius * searchRadius
					&& dist < min)
				{
					Goal = gameObject;
					//foodFound = true;
				}
			}
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
				switch (random.Next(5))
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
			else if (Y >= 2048 - Width && frameIndex == 0)
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

			if (Math.Abs(dx) > 0 || Math.Abs(dy) > 0) NextFrame();
			else SetFrame(1);
		}

		private void MoveToFood(ref ICollection<Plant> gameObjects)
		{
			if (Math.Abs(GetCenterX() - Goal.GetCenterX()) > 3 && Math.Abs(GetCenterY() - Goal.GetCenterY()) > 3)
			{
				if ((GetCenterX() - Goal.GetCenterX()) * (GetCenterX() - Goal.GetCenterX())
					> (GetCenterY() - Goal.GetCenterY()) * (GetCenterY() - Goal.GetCenterY()))
				{
					if (GetCenterX() - Goal.GetCenterX() < 0)
					{
						dx = Speed;
						dy = 0;
						SetFrameIndex(2);
					}
					else if (GetCenterX() - Goal.GetCenterX() > 0)
					{
						dx = -Speed;
						dy = 0;
						SetFrameIndex(1);
					}
				}
				else
				{
					if (GetCenterY() - Goal.GetCenterY() < 0)
					{
						dx = 0;
						dy = Speed;
						SetFrameIndex(0);

					}
					else if (GetCenterY() - Goal.GetCenterY() > 0)
					{
						dx = 0;
						dy = -Speed;
						SetFrameIndex(3);
					}
				}

				NextFrame();
			}
			else
			{
				Satiety = 1;
				gameObjects.Remove(Goal);
				Goal = null;
			}
		}

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
