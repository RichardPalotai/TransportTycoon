
public sealed class Tile
{
    public bool IsFree { get; set; }
    public int X { get; init; }
    public int Y { get; init; }
    public int ObjectId { get; private set; }
    #nullable enable
    private MapObject? _object;
    public MapObject? Object
    {
        get => _object; 
        private set
        {
            if (_object is not null)
            {
                throw new FieldOverrideException();
            }
            _object = value;
        }
    }
    #nullable disable
    public Tile(int x, int y, MapObject @object, int _objectId)
    {
        X = x;
        Y = y;
        Object = @object;
        ObjectId = _objectId;
        IsFree = false;
    }
    public Tile(int x, int y)
    {
        X = x;
        Y = y;
        Object = null;
        ObjectId = -1;
        IsFree = true;
    }
}
