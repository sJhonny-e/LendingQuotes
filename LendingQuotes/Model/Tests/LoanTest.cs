using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingQuotes.Model.Tests
{
    [TestFixture]
    public class LoanTest
    {
        Lender Donald = new Lender("first", 100, 0.1);
        Lender Ricky = new Lender("second", 200, 0.05);

        [Test]
        public void TotalRateWithOneLender()
        {
            var lendersWithAmounts = new Dictionary<Lender, int> { { Donald, 1500 } };
            var loan = new Loan(lendersWithAmounts, 1);

            Assert.AreEqual(Donald.Rate, loan.Rate);
        }

        [Test]
        public void TotalRateWithLendersWithDifferentWeights()
        {
            var lendersWithAmounts = new Dictionary<Lender, int> { { Donald, 50 }, { Ricky, 10 } };
            var loan = new Loan(lendersWithAmounts, 1);

            // = 0.1 * 50/60 + 0.05 * 10/60 = 0.0916666
            Assert.AreEqual(0.0917, Math.Round(loan.Rate, 4));
        }
        // TODO: A few more examples of different rates

        [Test]
        public void MonthlyRepaymentWith1500On5PercentOver36Months()
        {
            var lendersWithAmounts = new Dictionary<Lender, int> { { Ricky, 1500 } };
            var loan = new Loan(lendersWithAmounts, months: 36);

            Assert.AreEqual(78.75, loan.MonthlyRepayment);
        }

        // TODO: A few more examples of repayments
        // TODO: test total repayment property
    }
}
