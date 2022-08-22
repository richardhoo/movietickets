using System.Threading.Tasks;

namespace MovieTickets.CostAnalyzer.Services
{
    public interface ITicketTransactionProcessor
    {
        Task<string> GenerateTransactionPriceProjectionAsync();
    }
}
