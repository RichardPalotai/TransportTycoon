using System;
using System.Collections.Generic;
public class City : MapObject
{
    public Dictionary<Resource, int> Need { get; private set; }
    public Dictionary<Resource, double> NeedPerc()
    {
        throw new NotImplementedException();
    }

    public void DeliverResource(Resource res, int amount)
    {
        if (Need.ContainsKey(res))
        {
            Need[res] += amount;
        }
        else
        {
            Need.Add(res, amount);
        }
    }
}

