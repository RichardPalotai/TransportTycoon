using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

public sealed class Game : IUpdateable
{
    private Map _map;
    private Player _player;
    public HashSet<Save> Saves { get; private set; }
    public DateTime CurrentTime { get; private set; }
    private double _timeScale = 1.0;
    private Stopwatch _stopwatch;


    public double TimeScale
    {
        get { return _timeScale; }
        set { _timeScale = Math.Max(0, value); }
    }
    public bool IsPaused { get; private set; }

    public void NewGame()
    {
        CurrentTime = DateTime.Today;
        _map = new();
        _player = new();
        _stopwatch = new();

    }
    public void ResumeGame() => IsPaused = false;
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
    public void UpdateGame(double deltaTime)
    {
        if (IsPaused) return;

        CurrentTime = CurrentTime.AddSeconds(deltaTime * TimeScale);

        foreach (var item in _player.Vehicles)
        {
            item.Update(deltaTime);
        }

        foreach (var item in _player.Facilities)
        {
            item.Update(deltaTime);
        }
    }
    public void EndGame()
    {
        throw new System.NotImplementedException();
    }
    public void PauseGame() => IsPaused = true;

    public void Update(double deltaTime)
    {
        CurrentTime = CurrentTime.AddSeconds(deltaTime * TimeScale);
    }

    public void Loop()
    {
        _stopwatch.Start();

        double lastTime = _stopwatch.Elapsed.TotalSeconds;

        while (true)
        {
            double currentTime = _stopwatch.Elapsed.TotalSeconds;
            double deltaTime = currentTime - lastTime;
            lastTime = currentTime;

            Update(deltaTime);

            Thread.Sleep(16);
        }
    }
}
