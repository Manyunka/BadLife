using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Herbivorous : Animal
	{
		public Herbivorous(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
		}

		public override void SearchFood<T>(ref ICollection<T> gameObjects)
		{
			var objects = gameObjects as ICollection<Plant>;
			long min;
			if (Goal != null && objects.Contains(Goal))
				min = (GetCenterX() - Goal.GetCenterX()) * (GetCenterX() - Goal.GetCenterX())
					+ (GetCenterY() - Goal.GetCenterY()) * (GetCenterY() - Goal.GetCenterY());
			else
			{
				min = 1000000000;
				Goal = null;
			}
			foreach (T gameObject in gameObjects)
			{
				var obj = gameObject as Plant;
				long dist = (GetCenterX() - obj.GetCenterX()) * (GetCenterX() - obj.GetCenterX())
					+ (GetCenterY() - obj.GetCenterY()) * (GetCenterY() - obj.GetCenterY());
				if (dist <= searchRadius * searchRadius
					&& dist < min)
				{
					Goal = obj;
				}
			}
		}

		public override void MoveToFood<T>(ref ICollection<T> gameObjects)
		{
			if ((GetCenterX() - Goal.GetCenterX()) * (GetCenterX() - Goal.GetCenterX())
					+ (GetCenterY() - Goal.GetCenterY()) * (GetCenterY() - Goal.GetCenterY()) > 10)
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
				foreach (T gameObj in gameObjects)
				{
					var obj = gameObj as Plant;
					if (Goal == obj)
					{
						gameObjects.Remove(gameObj);
						break;
					}
				}
				Goal = null;
			}
		}
	}
}
