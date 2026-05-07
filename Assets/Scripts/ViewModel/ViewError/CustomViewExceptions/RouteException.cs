using System;

[Serializable]
public class RouteException : Exception
{
    public string Tag { get; }

    public RouteException()
    {
        Tag = "Route Error";
    }

    public RouteException(string message, string tag = "Route Error")
        : base(message)
    {
        Tag = tag;
    }

    public RouteException(string message, Exception innerException)
        : base(message, innerException)
    {
        Tag = "Route Error";
    }
}