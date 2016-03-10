using LendingQuotes.Model;
using LendingQuotes.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingQuotes
{
    public class Main
    {
        private ILendersRepository repository;
        private ILoanOutput output;

        public Main(ILendersRepository repository, ILoanOutput output)
        {
            this.repository = repository;
            this.output = output;
        }

        public void CreateAndDisplayLoan(int amount)
        {
            var loan = CreateLoan(amount);
            if (loan != null)
            {
                output.OutputLoan(loan);
            }
        }

        private Loan CreateLoan(int amount)
        {
            var amountErrors = GetAmountErrors(amount);
            if (amountErrors.Any())
            {
                output.ShowError(string.Join(", ", amountErrors));
                return null;
            }

            //TODO: could be injected as well
            var loanBuilder = new LoanBuilder(repository.GetAllLenders());
            Loan loan;
            try
            {
                loan = loanBuilder.BuildLoan(amount);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // TODO: log
                output.ShowError("Could not satisfy requirements: " + ex.Message);
                return null;
            }
            return loan;

        }

        // TODO: this can be abstracted in a different class
        private const int MINIMUM_AMOUNT = 1000;
        private const int MAXIMUM_AMOUNT = 15000;
        private static IEnumerable<string> GetAmountErrors(int amount)
        {
            var errors = new List<string>();
            if (amount % 100 != 0)
            {
                errors.Add("Amount must be set in 100 increments");
            }
            if (amount < MINIMUM_AMOUNT || amount > MAXIMUM_AMOUNT)
            {
                errors.Add("Amount must be in the range of " + MINIMUM_AMOUNT + " to " + MAXIMUM_AMOUNT);
            }
            return errors;
        }
    }
}
