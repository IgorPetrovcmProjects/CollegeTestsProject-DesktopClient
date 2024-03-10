namespace Templates.Data.Exception;

using System;

public class ApplicationRoutesException : Exception
{
    public ApplicationRoutesException() {}

    public ApplicationRoutesException(string message) : base (message) {}
}