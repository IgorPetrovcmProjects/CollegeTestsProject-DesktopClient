namespace Templates.Data.Exception;

using System;

public class ApplicationMiddlewareException : Exception
{
    public ApplicationMiddlewareException() {}

    public ApplicationMiddlewareException(string message) : base (message) {}
}