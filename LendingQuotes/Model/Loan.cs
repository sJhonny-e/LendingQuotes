using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingQuotes.Model
{
    public class Loan
    {
        public IDictionary<Lender, int> AmountPerLender { get; private set; }
        public int Months { get; private set; }

        public int TotalAmount { get; private set; }
        public double Rate { get; private set; }
        public double MonthlyRepayment { get; private set; }
        public double TotalRepayment { get { return MonthlyRepayment * Months; } }

        public Loan(IDictionary<Lender, int> amountPerLender, int months)
        {
            AmountPerLender = amountPerLender;
            Months = months;

            TotalAmount = amountPerLender.Values.Sum();
            SetRate();
            SetMonthlyRepayment();
        }

        // These two methods can be furhter abstracted by using a dependency to calculate them.
        // i.e Rate = RateCalculator.CalculateRate(AmountPerLender)
        // and MonthlyRepayment = MonthlyRepaymentCalculator.CalculateMonthlyRepayment(TotalAmount, Rate, Months)
        private void SetRate()
        {
            // Total rate is the weighed average of the lenders' rates 
            Rate = AmountPerLender
                .Sum(lenderWithAmount => lenderWithAmount.Key.Rate * lenderWithAmount.Value / TotalAmount);
        }

        private void SetMonthlyRepayment()
        {
            // I didn't come up with this formula by myself; I got it at http://math.stackexchange.com/a/685
            // I realize that this does NOT give the same result as in the example given, but it was as close as I could get..
            MonthlyRepayment = TotalAmount * Math.Pow(1 + Rate, Months) * Rate / Math.Pow(1 + Rate, Months - 1);
        }
    }
}
