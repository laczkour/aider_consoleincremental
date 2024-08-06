using System;

namespace ConsoleIncremental
{
    public class BuildingDto
    {
        public string Name { get; private set; }
        public int Count { get; set; }
        public double Progress { get; set; }
        public int CharactersPerHarvest { get; private set; }
        public int Cost { get; set; }

        private BuildingDto() { }

        public class Builder
        {
            private BuildingDto building = new BuildingDto();

            public Builder WithName(string name)
            {
                building.Name = name;
                return this;
            }

            public Builder WithCount(int count)
            {
                building.Count = count;
                return this;
            }

            public Builder WithProgress(double progress)
            {
                building.Progress = progress;
                return this;
            }

            public Builder WithCharactersPerHarvest(int charactersPerHarvest)
            {
                building.CharactersPerHarvest = charactersPerHarvest;
                return this;
            }

            public Builder WithCost(int cost)
            {
                building.Cost = cost;
                return this;
            }

            public BuildingDto Build()
            {
                return building;
            }
        }
    }
}
