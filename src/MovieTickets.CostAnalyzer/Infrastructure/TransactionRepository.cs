using MovieTickets.CostAnalyzer.Domains;
using MovieTickets.CostAnalyzer.Services;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieTickets.CostAnalyzer.Infrastructure
{
    public class TicketTransactionRepository : ITicketTransactionRepository
    {
        public async Task<List<TicketTransaction>> LoadTransactions()
        {
            const string fileName = "transactions.json";
            using FileStream fileStream = File.OpenRead(fileName);
            return await JsonSerializer.DeserializeAsync<List<TicketTransaction>>(fileStream);
        }
    }
}
