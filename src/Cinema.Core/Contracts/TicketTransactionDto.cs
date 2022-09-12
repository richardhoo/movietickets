using System.Collections.Generic;

namespace MovieTicketing.Dto
{
    public class TicketTransactionDto
    {
        public int TransactionId { get; set; }
        public IEnumerable<CustomerDto> Customers { get; set; }

        public string Rating { get; set; } = "G";

    }
}
