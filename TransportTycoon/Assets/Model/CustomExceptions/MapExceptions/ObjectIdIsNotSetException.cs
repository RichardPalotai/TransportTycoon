using System;

[Serializable]
public class ObjectIdIsNotSetException : Exception
{
    public ObjectIdIsNotSetException () {}

    public ObjectIdIsNotSetException (string message) 
        : base(message) {}

    public ObjectIdIsNotSetException (string message, Exception innerException)
        : base (message, innerException) {}
}
