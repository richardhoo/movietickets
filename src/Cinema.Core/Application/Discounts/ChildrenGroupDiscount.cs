using MovieTicketing.Application.MovieTransaction;
using MovieTicketing.Shared;

namespace MovieTicketing.Application.Dicounts
{
    public class ChildrenGroupDiscount : IPromotion
    {
        public void Apply(IMovieTransaction movieCheckoutBasket)
        {
            if (movieCheckoutBasket.Basket.Children.Count >= 3)
            {
                foreach (var childTicket in movieCheckoutBasket.Basket.Children)
                {
                    childTicket.AddDiscount(childTicket.TicketPrice.GetDiscount(25));
                }
            }
        }
    }
}