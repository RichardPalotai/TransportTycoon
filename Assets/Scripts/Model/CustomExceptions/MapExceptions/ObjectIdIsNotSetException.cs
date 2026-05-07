using System;

[Serializable]
public class ObjectIdIsNotSetException : Exception
{
    public string Tag { get; }
    public ObjectIdIsNotSetException ()
    {
        Tag = "Object Id Is Not Set Error";
    }

    public ObjectIdIsNotSetException (string message) 
        : base(message) {}

    public ObjectIdIsNotSetException (string message, Exception innerException)
        : base (message, innerException) {}
}
