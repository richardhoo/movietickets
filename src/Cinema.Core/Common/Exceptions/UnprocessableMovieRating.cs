using System;

public class UnprocessableMovieRating : Exception
{
    public UnprocessableMovieRating()
    {
    }

    public UnprocessableMovieRating(string message)
            : base(message)
    {
    }

    public UnprocessableMovieRating(string message, Exception inner)
            : base(message, inner)
    {
    }
}