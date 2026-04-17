using System;

public static class EntityFactory
{
    public static GameEntity Create(string type)
    {
        return type switch
        {
            "Road" => new Road(false),
            "TrafficLight" => new TrafficLight(false, TrafficLight.LightDirection.NORTH),
            "BusStop" => new BusStop(false),
            "City" => new City(),
            //TODO
            //"Factory" => new Factory<>(false),
            //"Farm" => new Farm<>(false),
            //"LumberMill" => new LumberMill<>(false),
            //"Mine" => new Mine<>(false),

            _ => throw new Exception($"Unknown entity type: {type}")
        };
    }
}