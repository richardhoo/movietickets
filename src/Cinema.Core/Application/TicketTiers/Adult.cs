using MovieTicketing.Application.Audience;
using MovieTicketing.Application.MovieTransaction;


namespace MovieTicketing.Application.TicketTiers
{
    public class Adult : TicketBase, ITicket
    {
        public Adult(Customer customer) : base(customer) { }
        
        public override decimal TicketPrice => 25;

        public override void Apply(IMovieTransaction checkoutBasket)
        {
            checkoutBasket.AddToBasket(this);
        }
    }

}


