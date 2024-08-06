using System;

namespace ConsoleIncremental
{
    public class BuildingDto
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Progress { get; set; }
        public int CharactersPerHarvest { get; set; }
        public int Cost { get; set; }

        public BuildingDto(string name, int count, double progress, int charactersPerHarvest, int cost)
        {
            Name = name;
            Count = count;
            Progress = progress;
            CharactersPerHarvest = charactersPerHarvest;
            Cost = cost;
        }
    }
}
