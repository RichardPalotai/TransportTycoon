using System;

[Serializable]
public class FieldOverrideException : Exception
{
    public string Tag { get; }
    public FieldOverrideException()
	{
        Tag = "Field Override Error";
    }
    public FieldOverrideException (string message) 
        : base(message) {}

    public FieldOverrideException (string message, Exception innerException)
        : base (message, innerException) {}
}
