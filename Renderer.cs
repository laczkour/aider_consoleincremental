using System;

namespace ConsoleIncremental
{
    public class Renderer
    {
        private const int ConsoleWidth = 80;
        private const int ConsoleHeight = 25;

        public void Render(GameLogic game)
        {
            Console.Clear();
            RenderBuildings(game);
            RenderBuyOption(game);
            RenderCharacters(game);
        }

        private void RenderBuildings(GameLogic game)
        {
            for (int i = 0; i < game.Buildings.Count; i++)
            {
                var building = game.Buildings[i];
                string prefix = i == game.SelectedBuildingIndex ? "[ " : "  ";
                string suffix = i == game.SelectedBuildingIndex ? " ]" : "  ";
                string name = building.Name.PadRight(20);
                string count = $"{building.Count}x".PadLeft(5);
                string progress = RenderProgressBar(building.Progress, 20);
                Console.SetCursorPosition(0, i * 3);
                Console.Write($"{prefix}{name}{count}    |{progress}|{suffix}");
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
            Console.WriteLine($"Buy 1 {selectedBuilding.Name.PadRight(20)} {selectedBuilding.Cost} Characters");
        }

        private void RenderCharacters(GameLogic game)
        {
            Console.SetCursorPosition(0, ConsoleHeight - 1);
            Console.WriteLine($"Characters: {game.Characters}");
        }
    }
}
