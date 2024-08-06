using System;
using System.Collections.Generic;

namespace ConsoleIncremental
{
    public class GameLogic
    {
        public List<BuildingDto> Buildings { get; private set; }
        public int Characters { get; private set; }
        public int SelectedBuildingIndex { get; private set; }
        public bool IsConsoleReadKeySelected { get; private set; }

        public GameLogic()
        {
            Buildings = new List<BuildingDto>
            {
                new BuildingDto.Builder()
                    .WithName("Console.WriteLine")
                    .WithCount(1)
                    .WithProgress(0.0)
                    .WithCharactersPerHarvest(10)
                    .WithCost(100)
                    .WithProgressSpeed(1.0)
                    .Build(),
                new BuildingDto.Builder()
                    .WithName("Json.Parse")
                    .WithCount(0)
                    .WithProgress(0.0)
                    .WithCharactersPerHarvest(50)
                    .WithCost(500)
                    .WithProgressSpeed(0.8)
                    .Build(),
                new BuildingDto.Builder()
                    .WithName("ex.PrintStackTrace")
                    .WithCount(0)
                    .WithProgress(0.0)
                    .WithCharactersPerHarvest(100)
                    .WithCost(1000)
                    .WithProgressSpeed(0.6)
                    .Build(),
                new BuildingDto.Builder()
                    .WithName("Enumerable.Repeat")
                    .WithCount(0)
                    .WithProgress(0)
                    .WithCharactersPerHarvest(200)
                    .WithCost(2000)
                    .WithProgressSpeed(0.4)
                    .Build()
            };
            Characters = 2000;
            SelectedBuildingIndex = 0; // Console.WriteLine is selected initially
        }

        public void Update(double deltaTime)
        {
            foreach (var building in Buildings)
            {
                building.Progress += deltaTime * building.Count * building.ProgressSpeed;
                if (building.Progress >= 1)
                {
                    int harvested = (int)building.CharactersPerHarvest * building.Count;
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

        public void SelectPreviousBuilding()
        {
            if (SelectedBuildingIndex == 0 && !IsConsoleReadKeySelected)
            {
                IsConsoleReadKeySelected = true;
            }
            else if (IsConsoleReadKeySelected)
            {
                IsConsoleReadKeySelected = false;
                SelectedBuildingIndex = Buildings.Count - 1;
            }
            else
            {
                SelectedBuildingIndex = (SelectedBuildingIndex - 1 + Buildings.Count) % Buildings.Count;
            }
        }

        public void SelectNextBuilding()
        {
            if (IsConsoleReadKeySelected)
            {
                IsConsoleReadKeySelected = false;
                SelectedBuildingIndex = 0;
            }
            else if (SelectedBuildingIndex == Buildings.Count - 1)
            {
                IsConsoleReadKeySelected = true;
            }
            else
            {
                SelectedBuildingIndex = (SelectedBuildingIndex + 1) % Buildings.Count;
            }
        }

        public void ClickConsoleReadKey()
        {
            if (IsConsoleReadKeySelected)
            {
                Characters++;
            }
        }
    }
}
