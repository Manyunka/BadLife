using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public class Herbivorous : Animal<Plant>
	{
		public Herbivorous(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
		}

		/*public override void SearchFood(ref ICollection<Plant> gameObjects)
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
			foreach (Plant gameObject in gameObjects)
			{
				//var obj = gameObject as Plant;
				long dist = (GetCenterX() - gameObject.GetCenterX()) * (GetCenterX() - gameObject.GetCenterX())
					+ (GetCenterY() - gameObject.GetCenterY()) * (GetCenterY() - gameObject.GetCenterY());
				if (dist <= searchRadius * searchRadius
					&& dist < min)
				{
					Goal = gameObject;
				}
			}
		}*/

		/*public override void MoveToFood(ref ICollection<Plant> gameObjects)
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
				foreach (Plant gameObj in gameObjects)
				{
					//var obj = gameObj as Plant;
					if (Goal == gameObj)
					{
						gameObjects.Remove(gameObj);
						break;
					}
				}
				Goal = null;
			}
		}*/
	}
}
