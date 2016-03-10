using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingQuotes.Model
{
    public class Lender
    {
        public string Name { get; set; }
        public double Rate { get; set; }
        public int Amount { get; set; }

        public Lender()
        {

        }

        public Lender(string name, int amount, double rate)
        {
            Name = name;
            Amount = amount;
            Rate = rate;
        }
    }
}
