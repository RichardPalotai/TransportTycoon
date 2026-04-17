using System;
using System.Collections.Generic;
using System.Linq;
public class City : GameEntity, IUpdateable, IBuildable
{
    private const int _max_need = 5000;
    public Dictionary<Resource, int> Need { get; private set; }
    public const double _inGameDayInSecs = 86400;
    public double _currentTime;
    public double Satisfaction()
    {
        var needs = new Dictionary<Resource, double>();

        foreach (var item in Need)
        {
            if (item.Value == _max_need) continue;
            double needPerc = (1.0 - (double)item.Value / _max_need) * 100;
            needs.Add(item.Key, Math.Round(needPerc, 2));
        }
        return needs.Average(x => x.Value);
    }
    public City()
    {
        Need = new();
        _currentTime = 0;
    }

    public void DeliverResource(Resource res, int amount)
    {
        if (Need.ContainsKey(res))
        {
            if (Need[res] + amount >= _max_need)
                Need[res] = _max_need;
            else
                Need[res] += amount;
        }
        else
        {
            Need.Add(res, amount);
        }
    }

    public void Update(double deltaTime)
    {
        _currentTime += deltaTime;
        if (_currentTime < _inGameDayInSecs) return;

        foreach (var item in Need)
        {
            Need[item.Key] = item.Key is Food ? item.Value / 2 : item.Value / 3;
        }

        _currentTime -= _inGameDayInSecs;
    }

    public void Build(Map map, Tile tile)
    {
        X = tile.X;
        Y = tile.Y;
        map.PlaceObject(tile.X, tile.Y, this);
    }
}

