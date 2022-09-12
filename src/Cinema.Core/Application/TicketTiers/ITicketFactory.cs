using MovieTicketing.Application.Audience;

namespace MovieTicketing.Application.TicketTiers
{
    public interface ITicketFactory
    {
        public ITicket GenerateTicket(Customer customer);
    }

}


