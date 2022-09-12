using MovieTicketing.Dto;
using System.Collections.Generic;

namespace MovieTicketProcessor
{
    public interface ITransactionReader
    {
        IEnumerable<TicketTransactionDto> LoadTransactions();
    }
}
