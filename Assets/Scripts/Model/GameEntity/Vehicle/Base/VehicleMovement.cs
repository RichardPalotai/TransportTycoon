using System;
using System.Collections.Generic;
using System.Linq;

public abstract partial class Vehicle : GameEntity, ITradeable, IUpdateable
{
    private double _waitTimer = 0;
    private bool _isWaiting = false;
    public void SetRoute(LinkedList<int> ids, List<Facility> facilities)
    {
        Route.Clear();

        var facilityMap = facilities.ToDictionary(f => f.ID);

        foreach (int id in ids)
        {
            if (facilityMap.TryGetValue(id, out var facility))
            {
                Route.Enqueue(facility);
            }
        }
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
        double bestDist = Distance(best.x, best.y, target);

        foreach (var (neighborX, neighborY) in neighbors.Skip(1))
        {
            double dist = Distance(neighborX, neighborY, target);


            if (dist < bestDist)
            {
                bestDist = dist;
                best = (neighborX, neighborY);
            }
        }
        Direction = calcDir((X, Y), best) ?? Direction;

        var v = Player.instance.Vehicles.FirstOrDefault(x => x.X == best.x && x.Y == best.y);
        var trafLigLoc = PotentialTrafficLightLocation(X, Y, map.Size);
        if ((v is not null && v.Direction == Direction) ||
            (trafLigLoc is not null && map.GetTile(trafLigLoc.Value.x, trafLigLoc.Value.y).Entity is TrafficLight trafficLight
            && trafficLight.Color == TrafficLight.LightColor.RED))
        {
            return (X, Y);
        }



        return best;
    }
    private (int x, int y)? PotentialTrafficLightLocation(int fromX, int fromY, int mapSize) => Direction switch
    {
        Direction.NORTH => fromX + 1 < mapSize ? (fromX + 1, fromY) : null,
        Direction.EAST => fromY - 1 < mapSize ? (fromX, fromY - 1) : null,
        Direction.SOUTH => fromX - 1 < mapSize ? (fromX - 1, fromY) : null,
        Direction.WEST => fromY + 1 < mapSize ? (fromX, fromY + 1) : null,
        _ => null
    };
    private Direction? calcDir((int x, int y) from, (int x, int y) to) =>
        (to.x - from.x, to.y - from.y) switch
        {
            (0, -1) => Direction.NORTH,
            (0, 1) => Direction.SOUTH,
            (1, 0) => Direction.EAST,
            (-1, 0) => Direction.WEST,
            _ => null
        };

    public void Move(Map map, double deltaTime)
    {
        var target = GetNextStop();
        if (target == null) return;

        if (_isWaiting)
        {
            _waitTimer -= deltaTime;

            if (_waitTimer <= 0)
            {
                _isWaiting = false;

                if (Destination == target)
                {
                    Route = new Queue<Facility>(Route.Reverse());
                    Destination = Route.First();
                }

                SetToNextStop();
            }

            return;
        }


        if (IsNextToTarget((X, Y), target))
        {
            StartWaiting(target);
            return;
        }


        var nextStep = GetNextStep(map, target);

        if (nextStep.x == X && nextStep.y == Y)
            return;

        X = nextStep.x;
        Y = nextStep.y;


        //itt target area kell nem konkrét match
        //amikor facility mellé érünk és megfelel a jármű-facility típus, akkor interakció
        //5 mp vár, valamit csinál, majd tovább for haladni
        //Városban csak a + 4 oldalában tesz le, középen ne
    }
    private double Distance(int x, int y, Facility target)
    {
        int dx = Math.Max(Math.Max(target.X - x, 0), x - (target.X + Map.GetAreaSize(target) - 1));
        int dy = Math.Max(Math.Max(target.Y - y, 0), y - (target.Y + Map.GetAreaSize(target) - 1));
        return Math.Sqrt(dx * dx + dy * dy);
    }
    private void StartWaiting(Facility target)
    {
        _isWaiting = true;
        _waitTimer = 5.0;

        FacilityInteraction(target);
    }

    private void FacilityInteraction(Facility target)
    {
        if (this is PassengerVehicle passVehi && target is IPassengerInteractable passengerInteractable)
        {
            int newPassengersCount = passengerInteractable.PassInteract() + passVehi.PassengersCount;
            if (newPassengersCount <= 0)
            {
                passVehi.PassengersCount = 0;
            }
            else if (newPassengersCount >= passVehi.Seats)
            {
                passVehi.PassengersCount = passVehi.Seats;
            }
            else
            {
                passVehi.PassengersCount = newPassengersCount;
            }
        }
        else if (this is TransportVehicle transVehi && target is IProdInteractable prodInteractable)
        {
            transVehi.CurrentCargo = prodInteractable.ProdInteract(transVehi.CargoCapacity - transVehi.CurrentCargo);
        }
    }

    bool IsNextToTarget((int x, int y) pos, Facility target)
    {
        bool isHorizontallyAdjacent =
            pos.x >= target.X &&
            pos.x < target.X + Map.GetAreaSize(target) &&
            (pos.y == target.Y - 1 || pos.y == target.Y + Map.GetAreaSize(target));

        bool isVerticallyAdjacent =
            pos.y >= target.Y &&
            pos.y < target.Y + Map.GetAreaSize(target) &&
            (pos.x == target.X - 1 || pos.x == target.X + Map.GetAreaSize(target));

        return isHorizontallyAdjacent || isVerticallyAdjacent;
    }
}