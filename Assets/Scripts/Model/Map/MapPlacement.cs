using System;
using System.Linq;
using UnityEngine;

public sealed partial class Map
{
    public void PlaceObject(int x, int y, GameEntity entity)
    {
        int areaSize = GetAreaSize(entity);


        if (x < 0 || y < 0 || x >= Size || y >= Size)
        {
            throw new IndexOutOfRangeException($"X or Y values are out of bounds. Values:\n X: {x},\nY: {y}");
        }

        if (!IsFree(x, y, areaSize))
            throw new NotEnoughSpaceForObjectException();


        if (entity is TrafficLight trafficLight)
        {
            AddToCrossRoadIfNeeded(x, y, trafficLight);
        }

        _map[x, y] = new(x, y, entity);
        MarkAreaTilesWithId(x, y, areaSize);

        if (entity is Road road)
        {
            road.IsCrossRoad = IsCrossRoad(x, y);
            if (road.IsCrossRoad)
            {
                var maxNeighborRoadsCoords = GetTheMiddleOfTheCrossroad(x, y);

                if (!Crossroads.ContainsKey(maxNeighborRoadsCoords))
                {
                    Crossroads.Add(maxNeighborRoadsCoords, new Crossroad());
                }
            }
        }

        if (entity is City city)
            MarkCity(x, y, areaSize);
#if DEBUG
        //Logger.ObjectPlacedLog(entity.GetType(), x, y);
#endif
    }

    public void RemoveObject(GameEntity entity)
    {
        for (int i = 0; i < _map.GetLength(0); i++)
        {
            for (int j = 0; j < _map.GetLength(1); j++)
            {
                if (_map[i,j].ObjectId == entity.ID)
                {
                    _map[i, j] = new(i,j);
                }
            }
        }

        if (entity is TrafficLight tl)
        {
            tl.Crossroad.TrafficLights.Remove(tl);
        }

        Player.instance.Facilities.Remove(entity as Facility);
    }

    private Direction SetLightDirection(int lightX, int lightY, (int x, int y) middleOfCrossroad)
    {
        if (lightX - middleOfCrossroad.x <= 0 && lightY - middleOfCrossroad.y > 0) //We are left and down from the center
            return Direction.WEST;
        else if (lightX - middleOfCrossroad.x >= 0 && lightY - middleOfCrossroad.y < 0) //We are right and up from the center
            return Direction.EAST;
        else if (lightX - middleOfCrossroad.x < 0 && lightY - middleOfCrossroad.y <= 0) //We are left and up from the center
            return Direction.NORTH;
        //else if (lightX - middleOfCrossroad.x > 0 && lightY - middleOfCrossroad.y >= 0) //We are right and down from the center
        return Direction.SOUTH; //Default dir
    }

    private bool IsFree(int x, int y, int areaSize)
    {
        if (x + areaSize > Size || y + areaSize > Size)
        {
            return false;
        }
        for (int i = x; i < x + areaSize; ++i)
        {
            for (int j = y; j < y + areaSize; ++j)
            {
                if (!_map[i, j].IsFree)
                {
                    UnityEngine.Debug.Log(i + " " + j + " " + _map[i, j].IsFree + " " + _map[i, j].Entity);
                    return false;
                }
            }
        }
        return true;
    }
    private void MarkAreaTilesWithId(int x, int y, int areaSize)
    {
        for (int i = x; i < x + areaSize; ++i)
        {
            for (int j = y; j < y + areaSize; ++j)
            {
                if (x != i || y != j)
                    _map[i, j] = new(i, j, _map[x, y].Entity.ID);
            }
        }
    }
    public static int GetAreaSize(GameEntity entity)
    {
        if (entity is ProdFacility) return 2;
        else if (entity is City) return 3;
        return 1;
    }
}