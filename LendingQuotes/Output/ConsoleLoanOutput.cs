using LendingQuotes.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingQuotes.Model;
using System.Globalization;

namespace LendingQuotes.Output
{
    public class ConsoleLoanOutput : ILoanOutput
    {
        public CultureInfo Culture { get; private set;  }
        public int RatePrecision { get; private set; }
        public int AmountsPrecision { get; private set; }

        public ConsoleLoanOutput(CultureInfo cultureInfo = null , int ratePrecision = 1, int amountsPrecision = 2 )
        {
            Culture = cultureInfo ?? CultureInfo.CreateSpecificCulture("en-gb");
            RatePrecision = ratePrecision;
            AmountsPrecision = amountsPrecision;
        }

        public void OutputLoan(Loan loan)
        {
            var currencyString = "C" + AmountsPrecision;
            var percentString = "P" + RatePrecision;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Requested amount: {0}\n", loan.TotalAmount.ToString("C0", Culture));
            stringBuilder.AppendFormat("Rate: {0}\n", loan.Rate.ToString(percentString));
            stringBuilder.AppendFormat("Monthly repayment: {0}\n", loan.MonthlyRepayment.ToString(currencyString));
            stringBuilder.AppendFormat("Total repayment: {0}\n", loan.TotalRepayment.ToString(currencyString));

            Console.WriteLine(stringBuilder.ToString());
        }

        public void ShowError(string message)
        {
            Console.WriteLine("Error: " + message);
        }
    }
}
