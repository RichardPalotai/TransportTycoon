using System;

[Serializable]
public class VehicleConditionException : Exception
{
    public VehicleConditionException() { }

    public VehicleConditionException(string message)
        : base(message) { }

    public VehicleConditionException(string message, Exception innerException)
        : base(message, innerException) { }
}
