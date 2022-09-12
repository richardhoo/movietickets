using MovieTicketing.Application.MovieTransaction;

namespace MovieTicketing.Application.Dicounts
{
    public interface IPromotion
    {
        void Apply(IMovieTransaction movieCheckoutBasket);
    }
}