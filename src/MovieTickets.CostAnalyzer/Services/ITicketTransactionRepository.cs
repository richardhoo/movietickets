using MovieTickets.CostAnalyzer.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTickets.CostAnalyzer.Services
{
    public interface ITicketTransactionRepository
    {
        Task<List<TicketTransaction>> LoadTransactions();
    }
}
