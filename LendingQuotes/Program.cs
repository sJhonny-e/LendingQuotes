using LendingQuotes.DAL;
using LendingQuotes.Model;
using LendingQuotes.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingQuotes
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: LendingQuotes.exe <csv file name> <amount to borrow>");
                return;
            }

            int amount;
            if (!int.TryParse(args[1], out amount))
            {
                Console.WriteLine("Requested amount must be an integer. Actual value: " + args[1]);
                return;
            }

            // TODO: use proper DI
            var main = new Main(new CsvLendersRepository(args[0]), new ConsoleLoanOutput());
            main.CreateAndDisplayLoan(amount);
        }
    }
}
