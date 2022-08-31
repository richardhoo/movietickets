using MovieTicketing.Application.Dicounts;
using MovieTicketing.Application.TicketTiers;

namespace MovieTicketing.Application.MovieTransaction
{
    public interface IMovieTransaction
    {
        Basket Basket { get; set; }
        MovieRating MovieRating { get; }
        void AddToBasket(Adult adult);
        void AddToBasket(Child children);
        void AddToBasket(Senior senior);
        void AddToBasket(Teenager teen);
        void ApplyDiscount(IPromotion discount);
    }


}