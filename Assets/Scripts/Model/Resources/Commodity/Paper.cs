public sealed class Paper : Commodity
{
    public readonly int NeedValue = 2;
    public static Paper Instance { get; } = new();
    private Paper() {}

}