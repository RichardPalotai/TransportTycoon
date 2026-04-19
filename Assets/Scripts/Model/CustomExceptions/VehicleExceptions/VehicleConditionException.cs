using System;

[Serializable]
public class VehicleConditionException : Exception
{
    public string Tag { get; }
    public VehicleConditionException()
    {
        Tag = "Vehicle Condition Error";
    }

    public VehicleConditionException(string message)
        : base(message) { }

    public VehicleConditionException(string message, Exception innerException)
        : base(message, innerException) { }
}
