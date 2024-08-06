using System;

namespace ConsoleIncremental
{
    public class InputHandler
    {
        public bool IsRunning { get; private set; } = true;

        public void HandleInput(GameLogic game, Renderer renderer)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        game.SelectPreviousBuilding();
                        renderer.UpdateSelection(game);
                        break;
                    case ConsoleKey.DownArrow:
                        game.SelectNextBuilding();
                        renderer.UpdateSelection(game);
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
                        IsRunning = false;
                        break;
                }
            }
        }
    }
}
