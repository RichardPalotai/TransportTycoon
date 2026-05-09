using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UIElements;

public sealed partial class Game : IUpdateable
{
    public static Game instance;
    public IDataAccess _dataAccess;
    private Map _map;
    public Map Map
    {
        get { return _map; }
        set { _map = value; }
    }

    public Player Player;
    public HashSet<(string name, DateTime timeOfSave)> Saves => _dataAccess.GetSaves();
    public DateTime CurrentTime { get; set; }
    public int AccountBalance { get { return Player.Money; } set { Player.Money = value; } }
    private double _timeScale = 1.0;
    private Stopwatch _stopwatch;

    public event Action OnGameOver;

    public double TimeScale
    {
        get { return _timeScale; }
        set { _timeScale = Math.Max(1, value); }
    }
    public bool IsPaused { get; set; }

    public void NewGame(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
        CurrentTime = DateTime.Today;
#if DEBUG
        Logger.Log("Current time set to today");
#endif
        _map = new();
#if DEBUG
        Logger.Log("Map created");
#endif
        Player = new();
#if DEBUG
        Logger.Log("New player created");
#endif
        _stopwatch = new();
#if DEBUG
        Logger.Log("Stopwatch set");
#endif
        Factory<Steel> fac = new();
        Player.Facilities.Add(fac);
        fac.Build(_map, _map.GetTile(25, 25));
        Road road = new(false);
        Player.Facilities.Add(road);
        road.Build(_map, _map.GetTile(0, 5));
    }
    public void ResumeGame()
    {
        IsPaused = false;
        TimeScale = 1.0f;
#if DEBUG
        Logger.Log("Resumed");
#endif

    }
    public void SaveGame()
    {
        _dataAccess.SaveGameAsync(this);
    }
    public static HashSet<(string name, DateTime timeOfSave)> GetSaves(IDataAccess da) => da.GetSaves();
    public static Game LoadGame(IDataAccess _dataAccess, string name)
    {
        return _dataAccess.LoadGameAsync(name);
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
    public void PauseGame()
    {
        IsPaused = true;
#if DEBUG
        Logger.Log("Paused");
#endif
    }

    public void Update(double deltaTime)
    {
        CurrentTime = CurrentTime.AddSeconds(deltaTime * TimeScale);
    }

    public bool IsGameOver()
    {
        if (Player.Money <= 0)
            return true;

        foreach (var item in Enum.GetValues(typeof(Prices)))
        {
            if ((int)item <= Player.Money)
                return false;
        }

        return true;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
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
