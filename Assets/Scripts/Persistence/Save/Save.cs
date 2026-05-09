using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Save
{
    public static async Task SaveAsync(Game game)
    {
        DateTime time = DateTime.Now;
        string saveName = time.ToString("yyyy.MM.dd - HH.mm.ss");

        string saveFolder = Path.Combine(Application.persistentDataPath, "saves");

        //Logger.Log("==================================================================");
        //Logger.Log(Application.persistentDataPath);
        //Logger.Log("==================================================================");

        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }

        string path = Path.Combine(saveFolder, $"{saveName}.txt");

        await using StreamWriter writer = new(path, false, Encoding.UTF8);

        // GAME
        await writer.WriteLineAsync(game.CurrentTime.ToString());
        await writer.WriteLineAsync(game.AccountBalance.ToString());
        await writer.WriteLineAsync(game.TimeScale.ToString());
        await writer.WriteLineAsync(game.IsPaused.ToString());

        // FACILITIES
        await writer.WriteLineAsync(game.Player.Facilities.Count.ToString());
        foreach (var facility in game.Player.Facilities)
        {
            string dir = facility is TrafficLight tl ? tl.FacingDirection.ToString() : "null";
            string line =
                $"{facility.ID};" +
                $"{(facility.IsGenerated ? 1 : 0)};" +
                $"{facility.X};" +
                $"{facility.Y};" +
                $"{dir}" +
                $"{EntityFactory.CreateFacilityTypeStringForSave(facility)}";

            await writer.WriteLineAsync(line);
        }

        // MAP
        await writer.WriteLineAsync(game.Map.Size.ToString());

        for (int x = 0; x < game.Map.Size; x++)
        {
            for (int y = 0; y < game.Map.Size; y++)
            {
                Tile tile = game.Map.GetTile(x, y);

                string line =
                    $"{tile.X};" +
                    $"{tile.Y};" +
                    $"{tile.IsFree};" +
                    $"{tile.ObjectId}";

                await writer.WriteLineAsync(line);
            }
        }

        // VEHICLES
        await writer.WriteLineAsync(game.Player.Vehicles.Count.ToString());
        foreach (var vehicle in game.Player.Vehicles)
        {
            string destination =
                vehicle.Destination is null
                    ? "null"
                    : vehicle.Destination.ID.ToString();

            string route =
                string.Join(',', vehicle.Route.Select(x => x.ID));

            string cargo = vehicle is TransportVehicle transport
                            ? transport.CargoType.NameString
                            : "null";

            string line =
                $"{vehicle.GetType().Name};" +
                $"{vehicle.ID};" +
                $"{vehicle.X};" +
                $"{vehicle.Y};" +
                $"{cargo}" +
                $"{vehicle.Condition};" +
                $"{vehicle.Direction};" +
                $"{destination}|{route}";

            await writer.WriteLineAsync(line);
        }


        // CROSSROADS
        await writer.WriteLineAsync(game.Map.Crossroads.Count.ToString());
        foreach (var ((x, y), crossroad) in game.Map.Crossroads)
        {
            string lights =
                string.Join(',', crossroad.TrafficLights.Select(l => l.ID));

            string line =
                $"{x};{y};{crossroad.GreenInterval}|{lights}";

            await writer.WriteLineAsync(line);
        }
    }
    public static HashSet<(string name, DateTime timeOfSave)> GetSaves()
    {
        string path = Path.Combine(Application.persistentDataPath, "saves");
        if (!Directory.Exists(path) || !Directory.EnumerateFileSystemEntries(path).Any())
            throw new Exception("No saves yet!");

        var saveFileNames = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
        HashSet<(string name, DateTime timeOfSave)> saves = new();
        foreach (var item in saveFileNames)
        {
            string fileName = Path.GetFileNameWithoutExtension(item);
            saves.Add((fileName,
                        DateTime.ParseExact(
                            fileName,
                            "yyyy.MM.dd - HH.mm.ss",
                            System.Globalization.CultureInfo.InvariantCulture)
                        ));
        }

        return saves;
    }
}
