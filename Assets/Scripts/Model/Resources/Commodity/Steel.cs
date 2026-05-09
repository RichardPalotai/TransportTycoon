public sealed class Steel : Commodity
{
    public readonly int NeedValue = 5;
    public static Steel Instance { get; } = new();

    public override string NameString => "Steel";

    private Steel() {}

}