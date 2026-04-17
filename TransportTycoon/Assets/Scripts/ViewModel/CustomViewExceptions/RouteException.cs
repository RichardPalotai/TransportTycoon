using System;

[Serializable]
public class RouteException : Exception
{
    public RouteException () {}

    public RouteException (string message) 
        : base(message) {}

    public RouteException (string message, Exception innerException)
        : base (message, innerException) {}
}