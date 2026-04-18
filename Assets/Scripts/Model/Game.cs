using System;
using System.Collections.Generic;
using System.Diagnostics;

public sealed class Game : IUpdateable
{
    public static Game instance;
    private Map _map;
    public Map Map
    {
        get { return _map; }
    }
    
    public Player Player;
    public HashSet<(string name, DateTime timeOfSave)> Saves { get; private set; }
    public DateTime CurrentTime { get; private set; }
    private double _timeScale = 1.0;
    private Stopwatch _stopwatch;


    public double TimeScale
    {
        get { return _timeScale; }
        set { _timeScale = Math.Max(1, value); }
    }
    public bool IsPaused { get; private set; }

    public void NewGame()
    {
        CurrentTime = DateTime.Today;
        Logger.Log("Current time set to today");
        _map = new();
        Logger.Log("Map created");
        Player = new();
        Logger.Log("New player created");
        _stopwatch = new();
        Logger.Log("Stopwatch set");

    }
    public void ResumeGame()
    {
        IsPaused = false;
        TimeScale = 1.0f;
        Logger.Log("Resumed");

    }
    public void SaveGame()
    {
        //TODO
        throw new System.NotImplementedException();
    }
    public HashSet<(string name, DateTime timeOfSave)> GetSaves()
    {
        //TODO
        throw new System.NotImplementedException();
    }
    public HashSet<(string name, DateTime timeOfSave)> LoadGame(string name)
    {
        //TODO
        throw new System.NotImplementedException();
    }
    /// <summary>
    /// Updates time, vehicles and facilities in every deltaTime
    /// </summary>
    /// <param name="deltaTime"></param>
    public void UpdateGame(double deltaTime)
    {
        if (IsPaused) return;

        CurrentTime = CurrentTime.AddSeconds(deltaTime * TimeScale);

        foreach (var item in Player.Vehicles)
        {
            item.Update(deltaTime);
        }

        foreach (var item in Player.Facilities)
        {
            item.Update(deltaTime);
        }
        foreach (var item in _map.Crossroads)
        {
            item.Value.Update(deltaTime);
        }

    }
    public void EndGame()
    {
        throw new System.NotImplementedException();
    }
    public void PauseGame()
    {
        IsPaused = true;
        Logger.Log("Paused");
    }

    public void Update(double deltaTime)
    {
        CurrentTime = CurrentTime.AddSeconds(deltaTime * TimeScale);
    }

    //public void Loop()
    //{
    //    Logger.Log("Game loop started");
    //    _stopwatch.Start();

    //    double lastTime = _stopwatch.Elapsed.TotalSeconds;

    //    while (true)
    //    {
    //        double currentTime = _stopwatch.Elapsed.TotalSeconds;
    //        double deltaTime = currentTime - lastTime;
    //        lastTime = currentTime;

    //        Update(deltaTime);

    //        Thread.Sleep(16);
    //    }
    //}
}
