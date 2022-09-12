using MovieTicketing.Dto;


namespace MovieTicketing.Application.MovieTransaction
{
    public interface IMovieTransactionFactory
    {
        IMovieTransaction CreateMovieTransaction(TicketTransactionDto ticketTransactionDto);
    }
}
