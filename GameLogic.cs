using System;
using System.Collections.Generic;

namespace ConsoleIncremental
{
    public class GameLogic
    {
        public List<BuildingDto> Buildings { get; private set; }
        public int Characters { get; private set; }
        public int SelectedBuildingIndex { get; private set; }

        public GameLogic()
        {
            Buildings = new List<BuildingDto>
            {
                new BuildingDto.Builder()
                    .WithName("Console.WriteLine")
                    .WithCount(1)
                    .WithProgress(0.4)
                    .WithCharactersPerHarvest(10)
                    .WithCost(100)
                    .Build(),
                new BuildingDto.Builder()
                    .WithName("Json.Parse")
                    .WithCount(0)
                    .WithProgress(0.8)
                    .WithCharactersPerHarvest(50)
                    .WithCost(500)
                    .Build(),
                new BuildingDto.Builder()
                    .WithName("ex.PrintStackTrace")
                    .WithCount(0)
                    .WithProgress(0.4)
                    .WithCharactersPerHarvest(100)
                    .WithCost(1000)
                    .Build(),
                new BuildingDto.Builder()
                    .WithName("Enumerable.Repeat")
                    .WithCount(0)
                    .WithProgress(0)
                    .WithCharactersPerHarvest(200)
                    .WithCost(2000)
                    .Build()
            };
            Characters = 0;
            SelectedBuildingIndex = 0; // Console.WriteLine is selected initially
        }

        public void Update(double deltaTime)
        {
            foreach (var building in Buildings)
            {
                building.Progress += deltaTime * building.Count;
                if (building.Progress >= 1)
                {
                    int harvested = (int)(building.Progress * building.CharactersPerHarvest);
                    Characters += harvested;
                    building.Progress %= 1;
                    // TODO: Implement harvest notification
                }
            }
        }

        public bool BuyBuilding(int index)
        {
            if (index < 0 || index >= Buildings.Count)
                return false;

            var building = Buildings[index];
            if (Characters >= building.Cost)
            {
                Characters -= building.Cost;
                building.Count++;
                building.Cost = (int)(building.Cost * 1.15); // 15% price increase
                return true;
            }
            return false;
        }

        public void SelectNextBuilding()
        {
            SelectedBuildingIndex = (SelectedBuildingIndex + 1) % Buildings.Count;
        }

        public void SelectPreviousBuilding()
        {
            SelectedBuildingIndex = (SelectedBuildingIndex - 1 + Buildings.Count) % Buildings.Count;
        }
    }
}
