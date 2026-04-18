[System.Serializable]
public class InvalidGreenIntervalException : System.Exception
{
	public InvalidGreenIntervalException() { }
	public InvalidGreenIntervalException(string message) : base(message) { }
	public InvalidGreenIntervalException(string message, System.Exception inner) : base(message, inner) { }
	protected InvalidGreenIntervalException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}