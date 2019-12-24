using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine
{
    public class GameEngine
    {
		int Width, Height;

		GameView GameView;

		bool IsRunning = false;
		int FrameTime;

		Bitmap BackBuffer;
		Graphics G;
		public GameEngine(int width, int height, GameView gameView)
		{
			Width = width;
			Height = height;

			GameView = gameView;

			FrameTime = 1000 / 60;

			BackBuffer = new Bitmap(Width, Height);
			G = Graphics.FromImage(BackBuffer);
		}

		public void Run()
		{
			if (IsRunning) return;
			IsRunning = true;

			GameView.Show();
			GameView.OnLoadContent();

			int startFrameTime, sleepTime;

			while (IsRunning && !GameView.IsDisposed)
			{
				startFrameTime = Environment.TickCount;

				Update();
				Draw();
				Application.DoEvents();

				sleepTime = FrameTime - (Environment.TickCount - startFrameTime);
				if (sleepTime > 0)
					System.Threading.Thread.Sleep(sleepTime);
			}
			GameView.OnUnloadContent();
		}

		public void Update()
		{
			GameView.OnUpdate();
		}

		public void Draw()
		{
			GameView.OnDraw(G, BackBuffer);
		}

	}
}
