public sealed class Milk : Food
{
    public readonly int NeedValue = 2;
    public static Milk Instance { get; } = new();

    public override string NameString => "Milk";

    private Milk() {}

}