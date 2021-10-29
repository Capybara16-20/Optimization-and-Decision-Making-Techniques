using System.Collections.Generic;
using System.Linq;

namespace Analyzer
{
    public class MarginAnalyzer
    {
        public int ConstCosts { get; }
        public List<Production> productions { get; private set; }

        public MarginAnalyzer(int constCosts)
        {
            ConstCosts = constCosts;
            productions = new List<Production> { new Production(0, 0, 0) };
        }

        public void AddProduction(Production production)
        {
            productions.Add(production);
        }

        public void RemoveProduction()
        {
            productions.Remove(productions.Last());
        }
    }
}
