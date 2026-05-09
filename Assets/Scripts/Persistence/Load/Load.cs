using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Load
{
    public static Game LoadAsync(string name)
    {
        string path = Path.Combine(
            Application.persistentDataPath,
            "saves",
            $"{name}.txt");

        using StreamReader reader = new(path, Encoding.UTF8);

        // GAME
        DateTime currentTime = DateTime.Parse(reader.ReadLine());

        int accountBalance = int.Parse(reader.ReadLine() ?? "0");

        double timeScale = double.Parse(reader.ReadLine() ?? "1");

        bool isPaused = bool.Parse(reader.ReadLine() ?? "false");

        // FACILITIES
        int facilityCount = int.Parse(reader.ReadLine() ?? "0");

        Dictionary<int, GameEntity> entities = new();

        List<Facility> facilities = new();

        for (int i = 0; i < facilityCount; i++)
        {
            string line = reader.ReadLine()
                ?? throw new Exception("Unexpected EOF");

            string[] parts = line.Split(';');

            int id = int.Parse(parts[0]);
            bool isGenerated = parts[1] == "1";
            int x = int.Parse(parts[2]);
            int y = int.Parse(parts[3]);
            string dir = parts[4];
            string type = parts[5];

            Facility facility =
                EntityFactory.CreateFacilityFromSave(
                    type,
                    id,
                    x,
                    y,
                    dir,
                    isGenerated);

            facilities.Add(facility);
            entities.Add(id, facility);
        }

        

        // MAP
        int mapSize = int.Parse(reader.ReadLine() ?? "0");

        if (mapSize == 0)
            throw new Exception("Maps size is 0. Something went wrong!");

        Map map = new(mapSize);

        for (int i = 0; i < mapSize * mapSize; i++)
        {
            string line = reader.ReadLine()
                ?? throw new Exception("Unexpected EOF");

            string[] parts = line.Split(';');

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            bool isFree = bool.Parse(parts[2]);
            int objectId = int.Parse(parts[3]);

            Tile tile;

            if (isFree)
            {
                tile = new Tile(x, y);
            }
            else
            {
                tile = new Tile(x, y, objectId);
            }

            map.SetTile(x, y, tile);
        }

        // VEHICLES
        int vehicleCount =
            int.Parse(reader.ReadLine() ?? "0");

        List<Vehicle> vehicles = new();

        for (int i = 0; i < vehicleCount; i++)
        {
            string line = reader.ReadLine()
                ?? throw new Exception("Unexpected EOF");

            string[] parts = line.Split(';');

            string vehicleType = parts[0];
            int id = int.Parse(parts[1]);
            int x = int.Parse(parts[2]);
            int y = int.Parse(parts[3]);
            string cargo = parts[4];
            double condition = double.Parse(parts[5]);
            Direction direction =
                Enum.Parse<Direction>(parts[6]);

            string[] routeData = parts[7].Split('|');

            string destinationString = routeData[0];

#nullable enable
            Facility? destination = null;
#nullable disable
            if (destinationString != "null")
            {
                int destinationId = int.Parse(destinationString);

                destination = (Facility)entities[destinationId];
            }

            List<Facility> route = new();

            if (routeData.Length > 1 && !String.IsNullOrWhiteSpace(routeData[1]))
            {
                route = routeData[1]
                    .Split(',')
                    .Select(idString => (Facility)entities[int.Parse(idString)])
                    .ToList();
            }

            Vehicle vehicle =
                EntityFactory.CreateVehicleFromSave(
                    vehicleType,
                    id,
                    x,
                    y,
                    cargo,
                    condition,
                    direction,
                    destination,
                    route,
                    map);

            vehicles.Add(vehicle);
            entities.Add(id, vehicle);
        }

        // CROSSROADS
        int crossroadCount = int.Parse(reader.ReadLine() ?? "0");

        for (int i = 0; i < crossroadCount; i++)
        {
            string line = reader.ReadLine()
                ?? throw new Exception("Unexpected EOF");

            string[] parts = line.Split(';');

            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);

            string[] data = parts[2].Split('|');

            double greenInterval = double.Parse(data[0]);

            List<int> lightIds = data[1]
                .Split(',')
                .Select(int.Parse)
                .ToList();

            Crossroad crossroad = new()
            {
                GreenInterval = greenInterval
            };

            foreach (int lightId in lightIds)
            {
                crossroad.TrafficLights.Add((TrafficLight)entities[lightId]);
            }

            map.Crossroads.Add((x, y), crossroad);
        }

        // SET EVERYTING
        Game game = new();
        game.Player = new();
        game.Map = map;

        game.CurrentTime = currentTime;
        game.AccountBalance = accountBalance;
        game.TimeScale = timeScale;
        game.IsPaused = isPaused;

        game.Player.Facilities.AddRange(facilities);
        game.Player.Vehicles.AddRange(vehicles);

        return game;
    }
}
