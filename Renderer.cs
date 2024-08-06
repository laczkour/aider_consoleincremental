using System;
using System.Runtime.CompilerServices;

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
                RenderSelection(game);
                isFirstRender = false;
            }
            RenderBuildingProgress(game);
            RenderBuyOption(game);
            RenderCharacters(game);
        }

        public void UpdateSelection(GameLogic game)
        {
            RenderSelection(game);
        }

        private void RenderBuildingNames(GameLogic game)
        {
            Console.SetCursorPosition(2, 1);
            Console.Write("Console.ReadKey".PadRight(20));

            for (int i = 0; i < game.Buildings.Count; i++)
            {
                var building = game.Buildings[i];
                string name = building.Name.PadRight(20);
                Console.SetCursorPosition(2, i + 2);
                Console.Write(name);
            }
        }

        private const double FastHarvestThreshold = 10; // Threshold for fast harvesting
        private const int FlowAnimationUpdateInterval = 4;
        private int flowAnimationUpdateCounter = 0;
        private int flowAnimationFrame = 0;

        private void RenderBuildingProgress(GameLogic game)
        {
            Console.SetCursorPosition(24, 1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Click to get 1 Character".PadRight(29));
            Console.ResetColor();

            for (int i = 0; i < game.Buildings.Count; i++)
            {
                var building = game.Buildings[i];
                string count = $"{building.Count}x".PadLeft(5);
                string progress = RenderProgressBar(building, 20);
                Console.SetCursorPosition(24, i + 2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{count}    |");
                Console.ResetColor();
                Console.Write(progress);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("|");
                Console.ResetColor();
            }
            if (++flowAnimationUpdateCounter >= FlowAnimationUpdateInterval)
            {
                flowAnimationUpdateCounter = 0;
                flowAnimationFrame = (flowAnimationFrame + 1) % 4; 
            }
        }

        private string RenderProgressBar(BuildingDto building, int width)
        {
            if (building.ProgressSpeed * building.Count > FastHarvestThreshold)
            {
                return RenderFlowingAnimation(width);
            }
            else
            {
                int filledWidth = (int)(building.Progress * width);
                Console.ForegroundColor = ConsoleColor.Blue;
                string filled = new string('#', filledWidth);
                Console.ResetColor();
                string empty = new string('-', width - filledWidth);
                return filled + empty;
            }
        }

        private string RenderFlowingAnimation(int width)
        {
            string frame = "-####";
            int frameLength = frame.Length;
            int startIndex = flowAnimationFrame % frameLength;
            string result = "";
            for (int i = 0; i < width; i++)
            {
                char c = frame[(startIndex + i) % frameLength];
                if (c == '#')
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ResetColor();
                }
                result += c;
            }
            Console.ResetColor();
            return result;
        }

        private void RenderBuyOption(GameLogic game)
        {
            var selectedBuilding = game.Buildings[game.SelectedBuildingIndex];
            Console.SetCursorPosition(0, ConsoleHeight - 4);
            Console.Write(new string(' ', ConsoleWidth)); // Clear the line
            Console.SetCursorPosition(0, ConsoleHeight - 4);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Buy 1 {selectedBuilding.Name.PadRight(20)} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{selectedBuilding.Cost} Characters");
            Console.ResetColor();
        }

        private void RenderCharacters(GameLogic game)
        {
            Console.SetCursorPosition(0, ConsoleHeight - 1);
            Console.Write(new string(' ', ConsoleWidth)); // Clear the line
            Console.SetCursorPosition(0, ConsoleHeight - 1);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"Characters: {game.Characters}");
            Console.ResetColor();
        }
    }

    private void RenderSelection(GameLogic game)
    {
        Console.SetCursorPosition(0, 1);
        string prefix = game.IsConsoleReadKeySelected ? "[ " : "  ";
        string suffix = game.IsConsoleReadKeySelected ? " ]" : "  ";
        Console.Write(prefix);
        Console.SetCursorPosition(22, 1);
        Console.Write(suffix);

        for (int i = 0; i < game.Buildings.Count; i++)
        {
            Console.SetCursorPosition(0, i + 2);
            prefix = i == game.SelectedBuildingIndex && !game.IsConsoleReadKeySelected ? "[ " : "  ";
            suffix = i == game.SelectedBuildingIndex && !game.IsConsoleReadKeySelected ? " ]" : "  ";
            Console.Write(prefix);
            Console.SetCursorPosition(22, i + 2);
            Console.Write(suffix);
        }
    }
}
