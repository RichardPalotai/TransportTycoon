using System;

[Serializable]
public class AreaIsNotFreeException : Exception
{
    public AreaIsNotFreeException () {}

    public AreaIsNotFreeException (string message) 
        : base(message) {}

    public AreaIsNotFreeException (string message, Exception innerException)
        : base (message, innerException) {}
}
