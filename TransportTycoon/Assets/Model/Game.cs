using System.Collections.Generic;

public sealed class Game
{
    private Map _map;
    private Player _player;
    private HashSet<Save> _saves;
    public ulong CurrentTime { get; private set; }
    public bool IsPaused { get; private set; }

    public void NewGame()
    {
        throw new System.NotImplementedException();
    }
    public void ResumeGame()
    {
        throw new System.NotImplementedException();
    }
    public void SaveGame()
    {
        throw new System.NotImplementedException();
    }
    public HashSet<Save> GetSaves()
    {
        throw new System.NotImplementedException();
    }
    public HashSet<Save> LoadGame(Save save)
    {
        throw new System.NotImplementedException();
    }
    public void UpdateGame()
    {
        throw new System.NotImplementedException();
    }
    public void EndGame()
    {
        throw new System.NotImplementedException();
    }
    public void PauseGame()
    {
        throw new System.NotImplementedException();
    }
}
