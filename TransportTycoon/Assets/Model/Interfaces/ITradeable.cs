public interface ITradeable
{
    public int Cost { get; }
    public void Purchase(Player player);
    public void Sell();
}