using System;
using System.Collections.Generic;

public static class EntityFactory
{
    public static string CreateFacilityTypeStringForSave(Facility facility)
    {
        //TODO Debug!
        Type type = facility.GetType();

        if (!type.IsGenericType)
        {
            return type.Name;
        }

        string baseTypeName =
            type.Name.Split('`')[0];

        string genericArgument =
            type.GetGenericArguments()[0].Name;

        return $"{baseTypeName}:{genericArgument}";
    }

    public static Vehicle CreateVehicleFromSave(string vehicleType, int id, int x, int y, string cargo, double condition, Direction direction, Facility destination, List<Facility> route, Map map)
    {
        switch (vehicleType)
        {
            case "Bus":
                return new Bus(40, id, x, y, condition, direction, destination, route, map);
            case "Car":
                return new Car(5, id, x, y, condition, direction, destination, route, map);
            case "Minivan":
                Resource res = CreateResourceFromString(cargo);
                return new Minivan(res, id, x, y, condition, direction, destination, route, map);
            case "Truck":
                res = CreateResourceFromString(cargo);
                return new Truck(res, id, x, y, condition, direction, destination, route, map);
            default: throw new Exception($"This is not a type of vehicle: {vehicleType}");
        }
    }

    private static Resource CreateResourceFromString(string cargo)
    {
        switch (cargo)
        {
            case "Iron":
                return Iron.Instance;
            case "Paper":
                return Paper.Instance;
            case "Wood":
                return Wood.Instance;
            case "Steel":
                return Steel.Instance;
            case "Cheese":
                return Cheese.Instance;
            case "Egg":
                return Egg.Instance;
            case "Milk":
                return Milk.Instance;
            default:
                throw new Exception($"This cargo type does not exist: {cargo}");
        }
    }

    public static Facility CreateFacilityFromSave(string type, int id, int x, int y, bool isGenerated)
    {
        //TODO
        throw new NotImplementedException();
    }
}