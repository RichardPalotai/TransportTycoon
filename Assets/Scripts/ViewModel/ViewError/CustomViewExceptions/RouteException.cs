using System;

[Serializable]
public class RouteException : Exception
{
    public string Tag { get; }

    public RouteException()
    {
        Tag = "Route Error";
    }

    public RouteException(string message)
        : base(message) {}

    public RouteException(string message, Exception innerException)
        : base(message, innerException) {}
}