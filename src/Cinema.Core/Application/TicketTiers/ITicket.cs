using MovieTicketing.Application.MovieTransaction;
using MovieTicketing.Application.Audience;

namespace MovieTicketing.Application.TicketTiers
{
    public interface ITicket
    {
        decimal GetTicketPrice();
        void Apply(IMovieTransaction checkoutBasket);
        void AddDiscount(decimal discountAmout);
        Customer GetCustomer();
    }

}


