public class MapObject
{
    private int _id = -1;
    public int ID
    {
        get { return _id; }
        set
        {
            if (_id == -1)
                _id = value;
        }
    }
    
}

