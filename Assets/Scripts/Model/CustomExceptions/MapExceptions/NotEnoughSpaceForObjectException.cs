using System;

[Serializable]
public class NotEnoughSpaceForObjectException : Exception
{
    public NotEnoughSpaceForObjectException () {}

    public NotEnoughSpaceForObjectException (string message) 
        : base(message) {}

    public NotEnoughSpaceForObjectException (string message, Exception innerException)
        : base (message, innerException) {}
}
