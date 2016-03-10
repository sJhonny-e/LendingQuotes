using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendingQuotes.Model.Tests
{
    [TestFixture]
    public class LoanBuilderTest
    {
        Lender First = new Lender("first", 100, 0.1);
        Lender Second = new Lender("second", 200, 0.05);
        Lender Third = new Lender("third", 100, 0.04);
        LoanBuilder Builder;

        [SetUp]
        public void Init()
        {
            Builder = new LoanBuilder(new[] { First, Second, Third });
        }

        [Test]
        public void BuildWithAmountCoveredBySingleLender()
        {
            var loan = Builder.BuildLoan(50);
            Assert.AreEqual(1, loan.AmountPerLender.Count);
            Assert.That(loan.AmountPerLender.ContainsKey(Third));
            Assert.AreEqual(loan.AmountPerLender[Third], 50);
        }

        [Test]
        public void BuildWithAmountPartiallyCoveredByLastLender()
        {
            var loan = Builder.BuildLoan(350);

            var lendersForLoan = loan.AmountPerLender;
            Assert.AreEqual(3, lendersForLoan.Count);
            Assert.AreEqual(100, lendersForLoan[Third]);
            Assert.AreEqual(200, lendersForLoan[Second]);
            Assert.AreEqual(50, lendersForLoan[First]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowsExceptionIfRequestedAmountTooLarge()
        {
            Builder.BuildLoan(401);
        }
    }
}
