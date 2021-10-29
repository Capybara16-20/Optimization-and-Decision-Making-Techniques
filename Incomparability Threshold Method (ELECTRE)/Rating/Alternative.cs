namespace Rating
{
    public struct Alternative
    {
        public string Name { get; }
        public int[] Rating { get; }
        
        public Alternative(string name, int[] rating)
        {
            Name = name;
            Rating = rating;
        }
    }
}
