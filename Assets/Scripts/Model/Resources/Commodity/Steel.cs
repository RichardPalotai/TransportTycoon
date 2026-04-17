public sealed class Steel : Commodity
{
    public readonly int NeedValue = 5;
    public static Steel Instance { get; } = new();
    private Steel() {}

}