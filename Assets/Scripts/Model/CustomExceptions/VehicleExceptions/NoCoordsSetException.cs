[System.Serializable]
public class NoCoordsSetException : System.Exception
{
    public string Tag { get; }
    public NoCoordsSetException()
	{
        Tag = "No Coords Set Error";
    }
    public NoCoordsSetException(string message) : base(message) { }
	public NoCoordsSetException(string message, System.Exception inner) : base(message, inner) { }
	protected NoCoordsSetException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}