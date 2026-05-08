public sealed class Iron : Commodity
{
    public readonly int NeedValue = 3;
    public static Iron Instance { get; } = new();

    public override string NameString => "Iron";

    private Iron() {}

}