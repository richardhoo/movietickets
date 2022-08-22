using AutoFixture;
using MovieTickets.CostAnalyzer.Domains;
using MovieTickets.CostAnalyzer.Dtos;
using MovieTickets.CostAnalyzer.Services;
using NUnit.Framework;
using System.Linq;

namespace MovieTickets.CostAnalyzer.Tests.Unit
{
    public class TicketTransactionCalculatorTests
    {
        [TestCase(70, ExpectedResult = 17.5)]
        [TestCase(65, ExpectedResult = 17.5)]
        [TestCase(64, ExpectedResult = 25)]
        [TestCase(18, ExpectedResult = 25)]
        [TestCase(11, ExpectedResult = 12)]
        [TestCase(17, ExpectedResult = 12)]
        [TestCase(10, ExpectedResult = 5)]
        [TestCase(6, ExpectedResult = 5)]
        public decimal GivenProvidedAge_WhenPriceIsCalculated_ThenExpectedResultIsReturned(int age)
        {
            var fixture = new Fixture();
            var customers = fixture.Build<Customer>().With(c => c.Age, age).CreateMany(1);
            var ticketTransaction = fixture.Build<TicketTransaction>().With(t => t.Customers, customers).Create();
            var actual = TicketTransactionCalculator.CaculateTransactionPrice(ticketTransaction);
            return actual.TotalPrice;
        }

        [Test]
        public void GivenAllTicketTypes_WhenPriceIsCalculated_ThenExpectedProjectionIsReturned()
        {
            var expected = TestData.GetTransactionWithAllTicketTypes();
            var actual = TicketTransactionCalculator.CaculateTransactionPrice(expected);
            VerifyOutput(actual, expected);
        }

        [Test]
        public void GivenTwoChildrenTickets_WhenPriceIsCalculated_ThenNoDiscountIsApplied()
        {
            var expected = TestData.GetTransacitonWithTwoChildrenTicket();
            var actual = TicketTransactionCalculator.CaculateTransactionPrice(expected);
            VerifyOutput(actual, expected);
        }

        [Test]
        public void GivenThreeChildrenTickets_WhenPriceIsCalculated_ThenExpectedDiscountIsApplied()
        {
            var expected = TestData.GetTransacitonWithThreeChildrenTickets();
            var actual = TicketTransactionCalculator.CaculateTransactionPrice(expected);
            VerifyOutput(actual, expected);
        }

        public void VerifyOutput(TransactionPriceProjection actual, TicketTransaction expected)
        {
            var expectedAdultTicketCounts = expected.Customers.Where(c => c.Age >= 18 && c.Age < 65).Count();
            var actualAdultTicketProjection = actual.TicketTypePriceProjections.SingleOrDefault(x => x.TicketType == TicketType.Adult);
            var expectedAdultSubTotal = 0M;

            if (expectedAdultTicketCounts == 0)
            {
                Assert.IsNull(actualAdultTicketProjection);
            }
            else {
                Assert.IsNotNull(actualAdultTicketProjection);
                expectedAdultSubTotal = expectedAdultTicketCounts * 25M;
                var actualAdultSubTotal = actualAdultTicketProjection.SubTotalPrice;
                var actualAdultTicketCounts = actualAdultTicketProjection.Quantity;
                Assert.AreEqual(actualAdultSubTotal, expectedAdultSubTotal);
                Assert.AreEqual(actualAdultTicketCounts, expectedAdultTicketCounts);
            }
            

            var expectedChildrenTicketCounts = expected.Customers.Where(c => c.Age < 11).Count();
            var actualChildrenTicketProjection = actual.TicketTypePriceProjections.SingleOrDefault(x => x.TicketType == TicketType.Children);
            var expectedChildrenSubTotal = 0M;

            if (expectedChildrenTicketCounts == 0)
            {
                Assert.IsNull(actualChildrenTicketProjection);
            }
            else
            {
                Assert.IsNotNull(actualChildrenTicketProjection);
                var childrenTicketDiscount = expectedChildrenTicketCounts >= 3 ? 0.75M : 1M;
                expectedChildrenSubTotal = expectedChildrenTicketCounts * 5 * childrenTicketDiscount;
                var actualChildrenSubTotal = actualChildrenTicketProjection.SubTotalPrice;
                var actualChildrenTicketCounts = actualChildrenTicketProjection.Quantity;
                Assert.AreEqual(actualChildrenSubTotal, expectedChildrenSubTotal);
                Assert.AreEqual(actualChildrenTicketCounts, expectedChildrenTicketCounts);
            }

            var expectedSeniorTicketCounts = expected.Customers.Where(c => c.Age >= 65).Count();
            var actualSeniorTicketProjection = actual.TicketTypePriceProjections.SingleOrDefault(x => x.TicketType == TicketType.Senior);
            var expectedSeniorSubTotal = 0M;

            if (expectedSeniorTicketCounts == 0)
            {
                Assert.IsNull(actualSeniorTicketProjection);
            }
            else
            {
                Assert.IsNotNull(actualSeniorTicketProjection);
                expectedSeniorSubTotal = expectedSeniorTicketCounts * 25M * 0.7M;
                var actualSeniorSubTotal = actualSeniorTicketProjection.SubTotalPrice;
                var actualSeniorTicketCounts = actualSeniorTicketProjection.Quantity;
                Assert.AreEqual(actualSeniorSubTotal, expectedSeniorSubTotal);
                Assert.AreEqual(actualSeniorTicketCounts, expectedSeniorTicketCounts);
            }

            var expectedTeenTicketCounts = expected.Customers.Where(c => c.Age >= 11 && c.Age < 18).Count();
            var actualTeenTicketProjection = actual.TicketTypePriceProjections.SingleOrDefault(x => x.TicketType == TicketType.Teenager);
            var expectedTeenSubTotal = 0M;

            if (expectedTeenTicketCounts == 0)
            {
                Assert.IsNull(actualTeenTicketProjection);
            }
            else
            {
                Assert.IsNotNull(actualTeenTicketProjection);
                expectedTeenSubTotal = expectedTeenTicketCounts * 12M;
                var actualTeenSubTotal = actualTeenTicketProjection.SubTotalPrice;
                var actualTeenTicketCounts = actualTeenTicketProjection.Quantity;
                Assert.AreEqual(actualTeenSubTotal, expectedTeenSubTotal);
                Assert.AreEqual(actualTeenTicketCounts, expectedTeenTicketCounts);
            }

            Assert.AreEqual(actual.TransactionId, expected.TransactionId);
            var expectedTotalPrice = expectedAdultSubTotal +expectedChildrenSubTotal + expectedSeniorSubTotal + expectedTeenSubTotal;
            Assert.AreEqual(actual.TotalPrice, expectedTotalPrice);
        }
    }
}