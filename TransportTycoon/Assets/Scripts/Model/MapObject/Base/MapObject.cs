public abstract class MapObject
{
    private int _id = -1;
    private static int _idCounter = 0;
    public int ID
    {
        get { return _id; }
        init
        {
            if (_id == -1)
                _id = value;
        }
    }

    protected MapObject()
    {
        ID = ++_idCounter;
    }
}

