using System;
using System.Diagnostics;

namespace ConsoleIncremental
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);
            Console.CursorVisible = false;

            GameLogic game = new GameLogic();
            Renderer renderer = new Renderer();

            bool running = true;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (running)
            {
                double deltaTime = stopwatch.Elapsed.TotalSeconds;
                stopwatch.Restart();

                game.Update(deltaTime);
                renderer.Render(game);

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            game.SelectPreviousBuilding();
                            break;
                        case ConsoleKey.DownArrow:
                            game.SelectNextBuilding();
                            break;
                        case ConsoleKey.Spacebar:
                            if (game.IsConsoleReadKeySelected)
                            {
                                game.ClickConsoleReadKey();
                            }
                            else
                            {
                                game.BuyBuilding(game.SelectedBuildingIndex);
                            }
                            break;
                        case ConsoleKey.Escape:
                            running = false;
                            break;
                    }
                }

                System.Threading.Thread.Sleep(16); // Cap at roughly 60 FPS
            }
        }
    }
}
