using Microsoft.Extensions.Logging;
using MovieTickets.CostAnalyzer.Dtos;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTickets.CostAnalyzer.Services
{
    public class TicketTransactionProcessor : ITicketTransactionProcessor
    {
        private readonly ILogger<TicketTransactionProcessor> _logger;
        private readonly ITicketTransactionRepository _ticketTransactionRepository;

        public TicketTransactionProcessor(
            ILogger<TicketTransactionProcessor> logger,
            ITicketTransactionRepository ticketTransactionRepository)
        {
            _logger = logger;
            _ticketTransactionRepository = ticketTransactionRepository;
        }

        public async Task<string> GenerateTransactionPriceProjectionAsync()
        {
            try
            {
                var transactions = await _ticketTransactionRepository.LoadTransactions();
                var output = new StringBuilder();
                bool isFirstTransaction = true;

                foreach (var transaction in transactions)
                {
                    var tranctionProjection = TicketTransactionCalculator.CaculateTransactionPrice(transaction);
                    
                    if (!isFirstTransaction)
                        output.AppendLine();

                    PrintTicketPrice(output, tranctionProjection);
                    isFirstTransaction = false;
                }

                return output.ToString();
            }
            catch(Exception ex) {
                _logger.LogError(ex, $"Error occured when generating ticket transaction projections in {nameof(GenerateTransactionPriceProjectionAsync)}");
                throw ex;
            }
        }

        private static void PrintTicketPrice(StringBuilder output, TransactionPriceProjection transactionPriceProjection)
        {
            output.Append("## Transaction ")
                .Append(transactionPriceProjection.TransactionId)
                .AppendLine(" ##");

            var ticketTypePriceProjections = transactionPriceProjection.TicketTypePriceProjections.OrderBy(x => x.TicketType.ToString());

            foreach (var ticketTypePriceProjection in ticketTypePriceProjections)
            {
                output.Append(ticketTypePriceProjection.TicketType.ToString())
                    .Append(" ticket x ")
                    .Append(ticketTypePriceProjection.Quantity)
                    .Append(": ")
                    .AppendLine(ticketTypePriceProjection.SubTotalPrice.ToString("c"));
            }
            
            output.AppendLine();
            output.Append("Projected total cost: ")
                .AppendLine(transactionPriceProjection.TotalPrice.ToString("c"));
        }
    }
}
