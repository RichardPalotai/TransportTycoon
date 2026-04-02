public abstract class GameEntity
{
    private static int _idCounter = 0;
    public int ID { get; init; }
    /// <summary>
    /// When loading a saved game state ResetId should be called first!
    /// It sets the _idCounter field 0.
    /// </summary>
    public void ResetId() => _idCounter = 0;
    /// <summary>
    /// Auto generated id
    /// </summary>
    protected GameEntity()
    {
        ID = ++_idCounter;
    }
    /// <summary>
    /// Given id (when loading a game state)
    /// </summary>
    /// <param name="id"></param>
    protected GameEntity(int id)
    {
        if (_idCounter < id)
        {
            _idCounter = id;
        }
        ID = id;
    }
}

