using MovieTicketing.Dto;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace MovieTicketProcessor
{
    public class TransactionReader : ITransactionReader
    {
        public IEnumerable<TicketTransactionDto> LoadTransactions()
        {
            using StreamReader r = new StreamReader("transactions.json");
            string json = r.ReadToEnd();
            return JsonSerializer.Deserialize<List<TicketTransactionDto>>(json);
        }
    }
}
