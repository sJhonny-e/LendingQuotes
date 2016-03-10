# LendingQuotes
A small console program that calculates optimal loans for borrowers, given a database of lenders and the requested amount.  
Given a comma-separated list of lenders (each with a desired interest rate and available funds), and the amount required to borrow, the program will calculate how a loan can be constructed with the lowest possible interest rate.  

## Installation
Just clone and restore NuGet packages

## Running
Run the program like so: `LendingQuotes.exe <csv file name> <amount to borrow>` (`LendingQuotes.exe myCsv.csv 1000`)
where your CSV file is of the following format:
```
Lender,Rate,Available
Bob,0.075,640
Jane,0.069,480
```

The program will output the best available rate for the requested amount, along with the monthly repayment amount and the total repayment amount.

## Structure
The bulk of the work is done by the Model classes.
`LoanBuilder` is used to construct a `Loan` object using the optimal (= lowest interest) available lenders for the given amount.  
Once constructed, the `Loan` object calculates the rate, monthly and total repayments, based on the given lenders.

The code structure is inspired by [onion architecture](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/).

### Extending the current code
This is obviously a very simple program. To extend it to a full-blown application you'd probably want to do the following:
+ Move `Model` , `DAL` and `Output` to their own projects (DLLs)
+ Move tests to a separate DLL
+ Create tests for the repository and output classes
+ Use a proper DI mechanism
+ Consider moving the logic of calculating rate and repayments out of the `Loan` model. This will allow for a little more testability, but especially- for seemlesly changing the way these are calculated (using the [Strategy pattern](http://www.oodesign.com/strategy-pattern.html))

