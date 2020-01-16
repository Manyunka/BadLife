using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public abstract class Human : Animal<Creature>
	{
		Human Partner;
		bool WantRelations;
		public House Home;

		public Human(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
			Speed = 2;
			searchRadius = 500;
			Partner = null;
			WantRelations = true;
		}

		public override void SearchFood(ref ICollection<Creature> gameObjects)
		{
			long min;
			if (Goal != null && gameObjects.Contains(Goal))
				min = (GetCenterX() - Goal.GetCenterX()) * (GetCenterX() - Goal.GetCenterX())
					+ (GetCenterY() - Goal.GetCenterY()) * (GetCenterY() - Goal.GetCenterY());
			else
			{
				min = 1000000000;
				Goal = null;
			}
			foreach (Creature gameObject in gameObjects)
			{
				if (gameObject.GetType() == typeof(Predator) || gameObject.GetType() == typeof(Herbivorous)
					|| gameObject.GetType() == typeof(Plant))
				{
					long dist = (GetCenterX() - gameObject.GetCenterX()) * (GetCenterX() - gameObject.GetCenterX())
						+ (GetCenterY() - gameObject.GetCenterY()) * (GetCenterY() - gameObject.GetCenterY());

					int e = 0;
					if (gameObject.GetType() == typeof(Herbivorous)) e = 10;
					else if (gameObject.GetType() == typeof(Herbivorous)) e = 25;

					if (dist <= searchRadius * searchRadius
						&& dist + e < min)
					{
						Goal = gameObject;
					}
				}
			}
		}

		public override void Update(ref ICollection<Creature> creatures, ref ICollection<MapObject> mapObjects)
		{
			Satiety -= 0.0005;
			if (Satiety <= 0.9)
			{
				if (Satiety <= 0) Visible = false;
				if (currentFrame == 1)
				{
					SearchFood(ref creatures);
					if (Goal != null) MoveToFood(ref creatures);
					else MoveRandom();
				}
				else NextFrame();
			}
			else
			{
				if (Partner != null)
					MoveToPartner(ref mapObjects);
				else
				{
					//if (random.Next(5) == 0) WantRelations = !WantRelations;
					if (WantRelations) SearchPartner(ref creatures);
					MoveRandom();
				}
			}

			Move();
		}

		public void SearchPartner(ref ICollection<Creature> creatures)
		{
			foreach (Creature creature in creatures)
			{
				if ((creature.GetType() == typeof(Woman) && GetType() == typeof(Man))
					|| (creature.GetType() == typeof(Man) && GetType() == typeof(Woman)))
				{
					long dist = (GetCenterX() - creature.GetCenterX()) * (GetCenterX() - creature.GetCenterX())
						+ (GetCenterY() - creature.GetCenterY()) * (GetCenterY() - creature.GetCenterY());
					if (((Human)creature).WantRelations && ((Human)creature).Partner == null && dist <= searchRadius * searchRadius)
					{
						Partner = (Human)creature;
						((Human)creature).Partner = this;
						break;
					}
				}
			}
		}

		public void MoveToPartner(ref ICollection<MapObject> mapObjects)
		{
			Sprite goal;
			if (Home != null) goal = Home;
			else goal = Partner;

			if ((GetCenterX() - goal.GetCenterX()) * (GetCenterX() - goal.GetCenterX())
					+ (GetCenterY() - goal.GetCenterY()) * (GetCenterY() - goal.GetCenterY()) > 30)
			{
				if ((GetCenterX() - goal.GetCenterX()) * (GetCenterX() - goal.GetCenterX())
					> (GetCenterY() - goal.GetCenterY()) * (GetCenterY() - goal.GetCenterY()))
				{
					if (GetCenterX() - goal.GetCenterX() < 0)
					{
						dx = Speed;
						dy = 0;
						SetFrameIndex(2);
					}
					else if (GetCenterX() - goal.GetCenterX() > 0)
					{
						dx = -Speed;
						dy = 0;
						SetFrameIndex(1);
					}
				}
				else
				{
					if (GetCenterY() - goal.GetCenterY() < 0)
					{
						dx = 0;
						dy = Speed;
						SetFrameIndex(0);

					}
					else if (GetCenterY() - goal.GetCenterY() > 0)
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
				dx = 0;
				dy = 0;
				if (GetType() == typeof(Man))
				{
					if (Home == null)
					{
						((Man)this).BuildHouse(ref mapObjects);
					}
					else
					{
						if (random.Next(100) == 0)
							((Woman)Partner).Childbearing();
					}
				}
			}
		}

		public void Reproduction(ref ICollection<Creature> creatures)
		{
			if (Partner.GetType() == typeof(Woman) && GetType() == typeof(Man))
			{
				((Woman)Partner).GiveBirth();
			}
		}

		public override void Draw(Graphics g)
		{
			base.Draw(g);
			var fontFamily = new FontFamily("Times New Roman");
			var font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
			StringFormat sf = new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			};

			string haveParther;
			if (Partner != null) haveParther = "есть";
			else haveParther = "нет";

			g.DrawString("Партнёр: " + haveParther, font, Brushes.White, GetCenterX(), Y - 16, sf);
		}
	}
}
