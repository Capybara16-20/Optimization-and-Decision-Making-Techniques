namespace Rating
{
    public struct Criterion
    {
        public string Name { get; }
        public int Weight { get; }

        public Criterion(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }
    }
}
