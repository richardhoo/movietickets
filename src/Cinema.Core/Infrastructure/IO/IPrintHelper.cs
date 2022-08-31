using MovieTicketing.Application.MovieTransaction;

namespace MovieTicketing.Infrastructure.IO
{
    public interface ITransactionPrinter
    {
        void Print(long id, IMovieTransaction movieCheckoutBasket);
    }
}