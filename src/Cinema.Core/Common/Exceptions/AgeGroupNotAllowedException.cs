using System;

public class AgeGroupNotAllowedException : Exception
{
    public AgeGroupNotAllowedException()
    {
    }

    public AgeGroupNotAllowedException(string message)
        : base(message)
    {
    }

    public AgeGroupNotAllowedException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
