namespace Templates.Data.Exception;

using System;

public class CreateTestHandlerException : Exception
{
    public CreateTestHandlerException(string message) : base (message) {}
}