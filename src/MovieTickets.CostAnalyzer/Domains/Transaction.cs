using System.Collections.Generic;

namespace MovieTickets.CostAnalyzer.Domains
{
    public class TicketTransaction
    {
        public int TransactionId { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}
