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
                new BuildingDto("Console.WriteLine", 8, 0.4, 10, 100),
                new BuildingDto("Json.Parse", 2, 0.8, 50, 500),
                new BuildingDto("ex.PrintStackTrace", 1, 0.4, 100, 1000),
                new BuildingDto("Enumerable.Repeat", 1, 0, 200, 2000)
            };
            Characters = 21200;
            SelectedBuildingIndex = 1; // Json.Parse is selected initially
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
