using System;

[Serializable]
public class NotEnoughMoneyException : Exception
{
    public string Tag { get; }
    public NotEnoughMoneyException ()
    {
        Tag = "Not Enough Money Error";
    }

    public NotEnoughMoneyException (string message) 
        : base(message) {}

    public NotEnoughMoneyException (string message, Exception innerException)
        : base (message, innerException) {}
}
