using System;

[Serializable]
public class FieldOverrideException : Exception
{
    public FieldOverrideException () {}

    public FieldOverrideException (string message) 
        : base(message) {}

    public FieldOverrideException (string message, Exception innerException)
        : base (message, innerException) {}
}
