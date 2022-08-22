using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using MovieTickets.CostAnalyzer.Services;
using NUnit.Framework;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.CostAnalyzer.Tests.Unit
{
    public class TicketTransactionProcessorTests
    {
        private IFixture _fixture;
        private Mock<ITicketTransactionRepository> _ticketTransactionRepositoryMock;
        private ITicketTransactionProcessor _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _ticketTransactionRepositoryMock = _fixture.Freeze<Mock<ITicketTransactionRepository>>();
            _sut = _fixture.Create<TicketTransactionProcessor>();
        }

        [Test]
        public async Task WhenTransactionPriceProjectionIsGenerated_ThenOutputsExpectedInfo()
        {
            _ticketTransactionRepositoryMock.Setup(m => m.LoadTransactions()).ReturnsAsync(TestData.GetTicketTransactions());
            var actual = await _sut.GenerateTransactionPriceProjectionAsync();

            var expected = new StringBuilder();
            expected.AppendLine("## Transaction 1 ##");
            expected.AppendLine("Children ticket x 2: $10.00");
            expected.AppendLine("Senior ticket x 1: $17.50");
            expected.AppendLine();
            expected.AppendLine("Projected total cost: $27.50");
            expected.AppendLine();
            expected.AppendLine("## Transaction 2 ##");
            expected.AppendLine("Adult ticket x 1: $25.00");
            expected.AppendLine("Children ticket x 3: $11.25");
            expected.AppendLine("Teenager ticket x 1: $12.00");
            expected.AppendLine();
            expected.AppendLine("Projected total cost: $48.25");
            expected.AppendLine();
            expected.AppendLine("## Transaction 3 ##");
            expected.AppendLine("Adult ticket x 1: $25.00");
            expected.AppendLine("Children ticket x 1: $5.00");
            expected.AppendLine("Senior ticket x 1: $17.50");
            expected.AppendLine("Teenager ticket x 1: $12.00");
            expected.AppendLine();
            expected.AppendLine("Projected total cost: $59.50");
            Assert.IsNotEmpty(actual);
            Assert.AreEqual(actual, expected.ToString());
        }

        [Test]
        public void GivenRepoError_WhenTransactionPriceProjectionIsGenerated_ThenThrowsException()
        {
            _ticketTransactionRepositoryMock.Setup(m => m.LoadTransactions()).ThrowsAsync(new ApplicationException("Test Exception"));
            var ex = Assert.ThrowsAsync<ApplicationException>(() => _sut.GenerateTransactionPriceProjectionAsync());
            Assert.That(ex.Message, Is.EqualTo("Test Exception"));
        }
    }
}