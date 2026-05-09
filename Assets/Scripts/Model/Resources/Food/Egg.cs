public sealed class Egg : Food
{
    public readonly int NeedValue = 1;
    public static Egg Instance { get; } = new();

    public override string NameString => "Egg";

    private Egg() {}

}