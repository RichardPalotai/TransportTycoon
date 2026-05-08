public sealed class Paper : Commodity
{
    public readonly int NeedValue = 2;
    public static Paper Instance { get; } = new();

    public override string NameString => "Paper";

    private Paper() {}

}