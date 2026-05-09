using System;
using System.Collections.Generic;

public static class EntityFactory
{
    public static string CreateFacilityTypeStringForSave(Facility facility)
    {
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

    public static Facility CreateFacilityFromSave(string type, int id, int x, int y, string dir, bool isGenerated)
    {
        if (!type.Contains(':'))
        {
            switch (type)
            {
                case "Road":
                    return new Road(isGenerated, id, x, y);
                case "TrafficLight":
                    return new TrafficLight(isGenerated, id, x, y, Enum.Parse<Direction>(dir));
                case "BusStop":
                    return new BusStop(isGenerated, id, x, y);
            }
        }
        else
        {
            string typeName = type.Split(':')[0];
            string genType = type.Split(':')[1];

            switch (genType)
            {
                case "Iron":
                    return typeName == "Factory" ?
                        new Factory<Iron>(id, x, y, isGenerated) : typeName == "Mine" ?
                        new Mine<Iron>(id, x, y, isGenerated) : new LumberMill<Iron>(id, x, y, isGenerated);
                case "Paper":
                    return typeName == "Factory" ?
                        new Factory<Paper>(id, x, y, isGenerated) : typeName == "Mine" ?
                        new Mine<Paper>(id, x, y, isGenerated) : new LumberMill<Paper>(id, x, y, isGenerated);
                case "Steel":
                    return typeName == "Factory" ?
                        new Factory<Steel>(id, x, y, isGenerated) : typeName == "Mine" ?
                        new Mine<Steel>(id, x, y, isGenerated) : new LumberMill<Steel>(id, x, y, isGenerated);
                case "Wood":
                    return typeName == "Factory" ?
                    new Factory<Wood>(id, x, y, isGenerated) : typeName == "Mine" ?
                    new Mine<Wood>(id, x, y, isGenerated) : new LumberMill<Wood>(id, x, y, isGenerated);
                case "Egg":
                    return new Farm<Egg>(id, x, y, isGenerated);
                case "Cheese":
                    return new Farm<Cheese>(id, x, y, isGenerated);
                case "Milk":
                    return new Farm<Milk>(id, x, y, isGenerated);
            }
        }
        throw new Exception("Something went wrong!");
    }
}