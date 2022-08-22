using MovieTickets.CostAnalyzer.Domains;

namespace MovieTickets.CostAnalyzer.Dtos
{
    public class TicketTypePriceProjection
    {
        public TicketType TicketType { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotalPrice { get; set; }
    }
}
