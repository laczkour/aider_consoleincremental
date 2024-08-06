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
            InputHandler inputHandler = new InputHandler();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (inputHandler.IsRunning)
            {
                double deltaTime = stopwatch.Elapsed.TotalSeconds;
                stopwatch.Restart();

                game.Update(deltaTime);
                renderer.Render(game);

                inputHandler.HandleInput(game, renderer);

                System.Threading.Thread.Sleep(16); // Cap at roughly 60 FPS
            }
        }
    }
}
