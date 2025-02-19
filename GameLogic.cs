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
        public List<HarvestNotification> RecentHarvests { get; private set; }
        private Dictionary<string, bool> LastHarvestWasOdd { get; set; }

        public GameLogic()
        {
            Buildings = new List<BuildingDto>
            {
                new BuildingDto.Builder()
                    .WithName("Console.WriteLine")
                    .WithCount(1)
                    .WithProgress(0.0)
                    .WithCharactersPerHarvest(1)
                    .WithCost(100)
                    .WithProgressSpeed(1.0)
                    .Build(),
                new BuildingDto.Builder()
                    .WithName("Json.Parse")
                    .WithCount(0)
                    .WithProgress(0.0)
                    .WithCharactersPerHarvest(40)
                    .WithCost(500)
                    .WithProgressSpeed(0.2)
                    .Build(),
                new BuildingDto.Builder()
                    .WithName("ex.PrintStackTrace")
                    .WithCount(0)
                    .WithProgress(0.0)
                    .WithCharactersPerHarvest(200)
                    .WithCost(1000)
                    .WithProgressSpeed(0.1)
                    .Build(),
                new BuildingDto.Builder()
                    .WithName("Enumerable.Repeat")
                    .WithCount(0)
                    .WithProgress(0)
                    .WithCharactersPerHarvest(2000)
                    .WithCost(2000)
                    .WithProgressSpeed(0.01)
                    .Build()
            };
            Characters = 2000;
            SelectedBuildingIndex = 0; // Console.WriteLine is selected initially
            RecentHarvests = new List<HarvestNotification>();
            LastHarvestWasOdd = new Dictionary<string, bool>();
            foreach (var building in Buildings)
            {
                LastHarvestWasOdd[building.Name] = false;
            }
        }

        public void Update(double deltaTime)
        {
            foreach (var building in Buildings)
            {
                building.Progress += deltaTime * building.Count * building.ProgressSpeed;
                if (building.Progress >= 1)
                {
                    int multiplier = (int)Math.Floor(building.Progress);
                    int harvested = building.CharactersPerHarvest * multiplier;
                    Characters += harvested;
                    building.Progress -= multiplier;
                    LastHarvestWasOdd[building.Name] = !LastHarvestWasOdd[building.Name];
                    RecentHarvests = RecentHarvests.Where(hn => hn.BuildingName != building.Name).ToList();
                    RecentHarvests.Add(new HarvestNotification(building.Name, harvested, LastHarvestWasOdd[building.Name]));
                    if (RecentHarvests.Count > 10)
                    {
                        RecentHarvests.RemoveAt(0);
                    }
                }
            }

            // Update and remove expired notifications
            for (int i = RecentHarvests.Count - 1; i >= 0; i--)
            {
                RecentHarvests[i].TimeRemaining -= deltaTime;
                if (RecentHarvests[i].TimeRemaining <= 0)
                {
                    RecentHarvests.RemoveAt(i);
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

    public class HarvestNotification
    {
        public string BuildingName { get; }
        public int HarvestedAmount { get; }
        public double TimeRemaining { get; set; }
        public bool IsOdd { get; }

        public HarvestNotification(string buildingName, int harvestedAmount, bool isOdd)
        {
            BuildingName = buildingName;
            HarvestedAmount = harvestedAmount;
            TimeRemaining = 3.0; // Display for 3 seconds
            IsOdd = isOdd;
        }
    }
}
