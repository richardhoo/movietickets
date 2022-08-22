using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTickets.CostAnalyzer.Dtos
{
    public class TransactionPriceProjection
    {
        public int TransactionId { get; set; }
        public List<TicketTypePriceProjection> TicketTypePriceProjections { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
