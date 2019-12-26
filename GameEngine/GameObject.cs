using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public abstract class GameObject : Sprite
	{
		public GameObject(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
		}

		public abstract void Update<T>(ref ICollection<T> gameObjects) where T : GameObject;
	}
}
