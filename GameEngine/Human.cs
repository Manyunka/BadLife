using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	class Human : Animal
	{
		public Human(Bitmap bitmap, int width, int height) : base(bitmap, width, height)
		{
		}

		public override void MoveToFood<T>(ref ICollection<T> gameObjects)
		{
			throw new NotImplementedException();
		}

		public override void SearchFood<T>(ref ICollection<T> gameObjects)
		{
			throw new NotImplementedException();
		}
	}
}
