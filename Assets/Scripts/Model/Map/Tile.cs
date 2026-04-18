using System;

public sealed class Tile
{
    public bool IsFree { get; set; }
    public int X { get; init; }
    public int Y { get; init; }
    public int ObjectId { get; private set; }
    #nullable enable
    private GameEntity? _entity;
    public GameEntity? Entity
    {
        get => _entity; 
        private set
        {
            if (_entity is not null)
            {
                throw new FieldOverrideException();
            }
            _entity = value;
        }
    }
    #nullable disable
    public Tile(int x, int y, GameEntity @object)
    {
        X = x;
        Y = y;
        Entity = @object; //W
        IsFree = false;
        ObjectId = @object.ID;
    }
    public Tile(int x, int y, int mapObjectId)
    {
        X = x;
        Y = y;
        Entity = null;
        IsFree = false;
        ObjectId = mapObjectId;
    }
    public Tile(int x, int y)
    {
        X = x;
        Y = y;
        Entity = null;
        ObjectId = -1;
        IsFree = true;
    }

    public enum ObjectPosition
    {
        UPPER_LEFT,
        OTHER
    }
}
