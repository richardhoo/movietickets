using MovieTicketing.Dto;
using System;
using System.Collections.Generic;


namespace MovieTicketing.Application.MovieTransaction
{
    public class MovieTransactionFactory : IMovieTransactionFactory
    {
        private readonly IEnumerable<IMovieTransaction> _movieTransactionProcessors;

        public MovieTransactionFactory(IEnumerable<IMovieTransaction> movieTransactionProcessors)
        {
            _movieTransactionProcessors = movieTransactionProcessors;
        }
        public IMovieTransaction CreateMovieTransaction(TicketTransactionDto ticketTransactionDto)
        {
            if (!Enum.TryParse(ticketTransactionDto.Rating, out MovieRating rating) || !Enum.IsDefined(typeof(MovieRating), rating))
            {
                throw new InvalidRatingException($"Invalid movie rating of {ticketTransactionDto.Rating}");
            }
            foreach (var movieTransactionProcessor in _movieTransactionProcessors)
            {
                if (movieTransactionProcessor.MovieRating == rating)
                {
                    movieTransactionProcessor.CreateNewTransaction();
                    return movieTransactionProcessor;
                }
            }
            throw new UnprocessableMovieRating($"We can't currently process movies of rating {ticketTransactionDto.Rating}");
        }
    }
}
