using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingQuotes.Model.Interfaces
{
    public interface ILoanOutput
    {
        void OutputLoan(Loan loan);
        void ShowError(string message);
    }
}
