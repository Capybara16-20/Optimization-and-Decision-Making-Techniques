namespace Analyzer
{
    public class Production
    {
        public int Price { get; private set; }
        public int Number { get; private set; }
        public int VariableCost { get; private set; }

        public Production(int price, int number, int variableCost)
        {
            Price = price;
            Number = number;
            VariableCost = variableCost;
        }

        public int Proceeds
        {
            get { return Price * Number; }
        }

        public int Profit
        {
            get { return Proceeds - VariableCost; }
        }

        public double MarginalCost
        {
            get { return (double)VariableCost / Number; }
        }
    }
}
