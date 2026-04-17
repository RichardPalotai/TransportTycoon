public sealed class Cheese : Food
{
    public readonly int NeedValue = 3;
    public static Cheese Instance { get; } = new();
    private Cheese() {}

}