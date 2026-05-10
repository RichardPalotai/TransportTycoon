using System;

[Serializable]
public class NotEnoughMoneyException : Exception
{
    public string Tag { get; }
    public NotEnoughMoneyException ()
    {
        Tag = "Not Enough Money Error";
    }

    public NotEnoughMoneyException (string message, string tag = "Not Enough Money Error") 
        : base(message)
    {
        Tag = tag;
    }

    public NotEnoughMoneyException (string message, Exception innerException)
        : base (message, innerException)
    {
        Tag = "Not Enough Money Error";
    }
}
