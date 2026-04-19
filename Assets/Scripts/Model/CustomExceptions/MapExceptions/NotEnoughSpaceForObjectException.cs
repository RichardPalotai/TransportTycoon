using System;

[Serializable]
public class NotEnoughSpaceForObjectException : Exception
{
    public string Tag { get; }
    public NotEnoughSpaceForObjectException()
	{
        Tag = "Not Enough Space For Object Error";
    }

    public NotEnoughSpaceForObjectException (string message) 
        : base(message) {}

    public NotEnoughSpaceForObjectException (string message, Exception innerException)
        : base (message, innerException) {}
}
