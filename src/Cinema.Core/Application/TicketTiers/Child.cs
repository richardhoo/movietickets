using MovieTicketing.Application.Audience;
using MovieTicketing.Application.MovieTransaction;


namespace MovieTicketing.Application.TicketTiers
{
    public class Child : TicketBase, ITicket
    {

        public Child(Customer customer) : base(customer) { }
        public override decimal TicketPrice => 5;

        public override void Apply(IMovieTransaction checkoutBasket)
        {
            checkoutBasket.AddToBasket(this);
        }
    }

}


