using System;
using System.Collections.Generic;
public class City : GameEntity, IUpdateable
{
    private const int _max_need = 5000; //TODO
    public Dictionary<Resource, int> Need { get; private set; }
    public Dictionary<Resource, double> Satisfaction()
    {
        var needs = new Dictionary<Resource, double>();

        foreach (var item in Need)
        {
            if (item.Value == _max_need) continue;
            double needPerc = (1.0 - (double)item.Value / _max_need) * 100;
            needs.Add(item.Key, Math.Round(needPerc, 2));
        }
        return needs;
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
        //TODO
        throw new NotImplementedException();
    }
}

