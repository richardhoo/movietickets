using System;

public class InvalidRatingException : Exception
{
    public InvalidRatingException()
    {
    }

    public InvalidRatingException(string message)
        : base(message)
    {
    }

    public InvalidRatingException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class InvalidTransactionException : Exception
{
    public InvalidTransactionException()
    {
    }

    public InvalidTransactionException(string message)
        : base(message)
    {
    }

    public InvalidTransactionException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
