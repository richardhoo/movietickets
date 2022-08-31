using MovieTicketing.Application.MovieTransaction;
using MovieTicketing.Shared;
using MovieTicketing.Application.Audience;

namespace MovieTicketing.Application.TicketTiers
{
    public class Senior : Adult
    {
        private decimal _dicountPercent = 25;
        public Senior(Customer customer) : base(customer)
        {
            Discounts.Add(SeniorDiscount(TicketPrice));
        }
        public override void Apply(IMovieTransaction checkoutBasket)
        {
            checkoutBasket.AddToBasket(this);
        }

        private decimal SeniorDiscount(decimal TicketPrice)
        {
            return TicketPrice.GetDiscount(_dicountPercent);
        }

    }

}


