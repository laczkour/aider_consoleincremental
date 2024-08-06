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

        private const double FastHarvestThreshold = 10; // Threshold for fast harvesting
        private const int FlowAnimationUpdateInterval = 4;
        private int flowAnimationUpdateCounter = 0;
        private int flowAnimationFrame = 0;

        private void RenderBuildingProgress(GameLogic game)
        {
            for (int i = 0; i < game.Buildings.Count; i++)
            {
                var building = game.Buildings[i];
                string prefix = i == game.SelectedBuildingIndex ? "[ " : "  ";
                string suffix = i == game.SelectedBuildingIndex ? " ]" : "  ";
                string count = $"{building.Count}x".PadLeft(5);
                string progress = RenderProgressBar(building, 20);
                Console.SetCursorPosition(22, i + 1);
                Console.Write($"{count}    |{progress}|{suffix}");
                Console.SetCursorPosition(0, i + 1);
                Console.Write(prefix);
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
                return new string('#', filledWidth) + new string('-', width - filledWidth);
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
                result += frame[(startIndex + i) % frameLength];
            }
            return result;
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
