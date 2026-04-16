using System;
using System.Collections.Generic;
using System.Linq;

public abstract partial class Vehicle : GameEntity, ITradeable, IUpdateable
{
    public void SetRoute(List<Facility> stops)
    {
        Route.Clear();
        Destination = stops.FirstOrDefault();
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
        Direction = calcDir((X, Y), best) ?? Direction;
        //a bestet kell csekkolni
        //itt early return ha áll már a tileon egy kocsi ami ugyanabba az irányba
        // megy mint ez a jármű, vagy piros a lámpa


        return best;
    }
    private Direction? calcDir((int x, int y) from, (int x, int y) to) =>
        (to.x - from.x, to.y - from.y) switch
        {
            (0, -1) => Direction.NORTH,
            (0, 1) => Direction.SOUTH,
            (1, 0) => Direction.EAST,
            (-1, 0) => Direction.WEST,
            _ => null
        };

    public void Move(Map map)
    {
        var target = GetNextStop();
        var nextStep = GetNextStep(map, target);

        if (nextStep.x == X && nextStep.y == Y)
            return;


        //itt target area kell nem konkrét match
        //amikor facility mellé érünk és megfelel a jármű-facility típus, akkor interakció
        //5 mp vár, valamit csinál, majd tovább for haladni
        //Városban csak a + 4 oldalában tesz le, középen ne
        if (nextStep.x == target.X && nextStep.y == target.Y)
        {
            SetToNextStop();
        }

        if (Destination == target)
        {
            Route.Reverse();
            Destination = Route.First();
        }
    }
    private double Distance(int neighborX, int neighborY, int x, int y)
    {
        return Math.Sqrt(Math.Pow(neighborX - x, 2) + Math.Pow(neighborY - y, 2));
    }
}