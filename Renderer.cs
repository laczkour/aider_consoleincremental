using System;

namespace ConsoleIncremental
{
    public class Renderer
    {
        private const int ConsoleWidth = 80;
        private const int ConsoleHeight = 25;

        private bool isFirstRender = true;

        public void Render(GameLogic game)
        {
            if (isFirstRender)
            {
                Console.Clear();
                RenderBuildingNames(game);
                isFirstRender = false;
            }
            RenderBuildingProgress(game);
            RenderBuyOption(game);
            RenderCharacters(game);
        }

        private void RenderBuildingNames(GameLogic game)
        {
            for (int i = 0; i < game.Buildings.Count; i++)
            {
                var building = game.Buildings[i];
                string name = building.Name.PadRight(20);
                Console.SetCursorPosition(0, i + 1);
                Console.Write($"  {name}");
            }
        }

        private void RenderBuildingProgress(GameLogic game)
        {
            for (int i = 0; i < game.Buildings.Count; i++)
            {
                var building = game.Buildings[i];
                string prefix = i == game.SelectedBuildingIndex ? "[ " : "  ";
                string suffix = i == game.SelectedBuildingIndex ? " ]" : "  ";
                string count = $"{building.Count}x".PadLeft(5);
                string progress = RenderProgressBar(building.Progress, 20);
                string speed = $"{building.ProgressSpeed:F2}x".PadLeft(6);
                Console.SetCursorPosition(22, i + 1);
                Console.Write($"{count}    |{progress}| {speed}{suffix}");
                Console.SetCursorPosition(0, i + 1);
                Console.Write(prefix);
            }
        }

        private string RenderProgressBar(double progress, int width)
        {
            int filledWidth = (int)(progress * width);
            return new string('#', filledWidth) + new string('-', width - filledWidth);
        }

        private void RenderBuyOption(GameLogic game)
        {
            var selectedBuilding = game.Buildings[game.SelectedBuildingIndex];
            Console.SetCursorPosition(0, ConsoleHeight - 4);
            Console.Write(new string(' ', ConsoleWidth)); // Clear the line
            Console.SetCursorPosition(0, ConsoleHeight - 4);
            Console.Write($"Buy 1 {selectedBuilding.Name.PadRight(20)} {selectedBuilding.Cost} Characters");
        }

        private void RenderCharacters(GameLogic game)
        {
            Console.SetCursorPosition(0, ConsoleHeight - 1);
            Console.Write(new string(' ', ConsoleWidth)); // Clear the line
            Console.SetCursorPosition(0, ConsoleHeight - 1);
            Console.Write($"Characters: {game.Characters}");
        }
    }
}
