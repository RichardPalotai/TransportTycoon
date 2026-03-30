public abstract class MapObject
{
    private static int _idCounter = 0;
    public int ID { get; init; }

    protected MapObject()
    {
        ID = ++_idCounter;
    }
}

