using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
	public abstract class Sprite : Layer
	{
		readonly Bitmap Bitmap;
		protected int frameTime, currentFrame, frameIndex, frameLength;

		public Sprite(Bitmap bitmap)
		{
			Bitmap = bitmap;
			Width = bitmap.Width;
			Height = bitmap.Height;
			frameTime = currentFrame = frameIndex = frameLength = 0;
		}

		public Sprite(Bitmap bitmap, int width, int height)
		{
			Bitmap = bitmap;
			Width = width;
			Height = height;
			frameTime = currentFrame = frameIndex = 0;
			frameLength = bitmap.Width / Width;
		}

		public void SetFPS(int fps)
		{
			if (fps > 0) frameTime = 1000 / fps;
			else frameTime = 0;
		}
		public void NextFrame()
		{
			if (frameTime > 0)
				currentFrame = Environment.TickCount / frameTime % frameLength;
		}
		public void SetFrame(int frame)
		{
			currentFrame = frame;
			if (currentFrame < 0 || currentFrame >= frameLength)
				currentFrame = 0;
		}

		public void SetFrameIndex(int index)
		{
			frameIndex = index;
			if (frameIndex < 0 || frameIndex >= Bitmap.Height / Height)
				frameIndex = 0;
		}

		public override void Draw(Graphics g)
		{
			if (!Visible) return;

			Rectangle destRect = new Rectangle(X, Y, Width, Height);
			Rectangle srcRect = new Rectangle(currentFrame * Width, frameIndex * Height, Width, Height);
			g.DrawImage(Bitmap, destRect, srcRect, GraphicsUnit.Pixel);
		}
	}
}
