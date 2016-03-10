using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingQuotes.Model
{
    public class LoanBuilder
    {
        private IEnumerable<Lender> lenders;
        
        public LoanBuilder(IEnumerable<Lender> allLenders)
        {
            // Store lenders in the order in which we want to iterate them - from the lowest to highest interet rate
            lenders = allLenders.OrderBy(l => l.Rate);
        }

        public Loan BuildLoan(int amountToBorrow, int months = 36)
        {
            var remaining = amountToBorrow;
            IDictionary<Lender, int> lendersWithAmounts = new Dictionary<Lender, int>();

            // To satisfy the loan as cheaply as possible, we try to get the maximum amount from lenders with lowest interest rates
            foreach (var lender in lenders)
            {
                if (remaining <= lender.Amount)
                {
                    // this is the last lender
                    lendersWithAmounts.Add(lender, remaining);
                    remaining = 0;
                    break;
                }

                lendersWithAmounts.Add(lender, lender.Amount);
                remaining -= lender.Amount;
            }

            if (remaining > 0)
            {
                throw new ArgumentOutOfRangeException("amountToBorrow", "Not enough money provided by lenders to cover amount of " + amountToBorrow);
            }
            return new Loan(lendersWithAmounts, months);
        }
    }
}
