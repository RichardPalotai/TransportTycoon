using System;
using System.Collections.Generic;

public sealed class Game : IUpdateable
{
    private Map _map;
    private Player _player;
    public HashSet<Save> Saves { get; private set; }
    public DateTime CurrentTime { get; private set; }
    public double TimeScale { get; set; } = 1.0;
    public bool IsPaused { get; private set; }

    public void NewGame()
    {
        CurrentTime = DateTime.Today;
        _map = new();
        _player = new();
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

    public void Update(double deltaTime)
    {
        CurrentTime = CurrentTime.AddSeconds(deltaTime * TimeScale);
    }
}
