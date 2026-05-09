public sealed class Wood : Commodity
{
    public readonly int NeedValue = 4;
    public static Wood Instance { get; } = new();

    public override string NameString => "Wood";

    private Wood() {}

}