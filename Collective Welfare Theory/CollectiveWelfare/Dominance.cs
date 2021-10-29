namespace CollectiveWelfare
{
    public struct Dominance
    {
        public int Dominant { get; }
        public int Dominated { get; }

        public Dominance(int dominant, int dominated)
        {
            Dominant = dominant;
            Dominated = dominated;
        }
    }
}
