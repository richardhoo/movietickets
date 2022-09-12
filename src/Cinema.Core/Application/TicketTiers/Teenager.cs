using MovieTicketing.Application.Audience;
using MovieTicketing.Application.MovieTransaction;

namespace MovieTicketing.Application.TicketTiers
{
    public class Teenager : TicketBase, ITicket
    {
     
        public Teenager(Customer customer):base(customer)  { }
        public override decimal TicketPrice => 12;

        public override void Apply(IMovieTransaction checkoutBasket)
        {
            checkoutBasket.AddToBasket(this);
        }
    }

}


