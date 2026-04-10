using System;
using System.Collections.Generic;
using System.Linq;

public abstract partial class Vehicle : GameEntity, ITradeable, IUpdateable
{
    public void SetRoute(List<Facility> stops)
    {
        Route.Clear();
        stops.ForEach(Route.Enqueue);
    }
    public void SetToNextStop()
    {
        Route.Enqueue(Route.Dequeue());
    }
#nullable enable
    public Facility? GetNextStop() => Route.FirstOrDefault();
#nullable disable

    private (int x, int y) GetNextStep(Map map, Facility target)
    {
        //if (X is null || Y is null)
        //{
        //    throw new NoCoordsSetException();
        //}
        var neighbors = map.GetTilesNeighborRoadsCoords(X, Y);

        (int x, int y) best = neighbors.First();
        double bestDist = Distance(best.x, best.y, target.X, target.Y);

        foreach (var (neighborX, neighborY) in neighbors.Skip(1))
        {
            double dist = Distance(neighborX, neighborY, target.X, target.Y);


            if (dist < bestDist)
            {
                bestDist = dist;
                best = (neighborX, neighborY);
            }
        }

        return best;
    }
    public void Move(Map map)
    {
        var target = GetNextStop();
        var nextStep = GetNextStep(map, target);

        if (nextStep.x == target.X && nextStep.y == target.Y)
        {
            SetToNextStop();
        }
    }
    private double Distance(int neighborX, int neighborY, int x, int y)
    {
        return Math.Sqrt(Math.Pow(neighborX - x, 2) + Math.Pow(neighborY - y, 2));
    }
}