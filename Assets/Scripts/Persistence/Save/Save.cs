using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public sealed class Save
{
    public static async Task SaveAsync(DateTime time, Map map, Game game)
    {
        //MAP
        string saveName = time.ToString("yyyy.MM.dd - HH.mm.ss");
        string saveFolder = Path.Combine(Application.persistentDataPath, "saves");

        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }

        string path = Path.Combine(saveFolder, $"{saveName}.txt");

        await using StreamWriter writer = new(path, false, Encoding.UTF8);

        await writer.WriteLineAsync(map.Size.ToString());

        for (int x = 0; x < map.Size; x++)
        {
            for (int y = 0; y < map.Size; y++)
            {
                Tile tile = map.GetTile(x, y);

                string line =
                    $"{tile.X};" +
                    $"{tile.Y};" +
                    $"{tile.IsFree};" +
                    $"{tile.ObjectId}"; //Entity's ID

                await writer.WriteLineAsync(line);
            }
        }

        //CROSSROADS

        await writer.WriteLineAsync(game.Map.Crossroads.Count.ToString());
        foreach (var item in game.Map.Crossroads)
        {
            int x = item.Key.Item1;
            int y = item.Key.Item2;

            Crossroad crossroad = item.Value;

            string lights =
                string.Join(',', crossroad.TrafficLights.Select(l => l.ID));

            string line =
                $"{x};{y};{crossroad.GreenInterval}|{lights}";

            await writer.WriteLineAsync(line);
        }

        //GAME

        await writer.WriteLineAsync(game.CurrentTime.ToString());
        await writer.WriteLineAsync(game.AccountBalance.ToString());
        await writer.WriteLineAsync(game.TimeScale.ToString());
        await writer.WriteLineAsync(game.IsPaused.ToString());

        //FACILITIES

        await writer.WriteLineAsync(game.Player.Facilities.Count.ToString());
        foreach (var facility in game.Player.Facilities)
        {
            string line =
                $"{facility.ID};" +
                $"{(facility.IsGenerated ? 1 : 0)};" +
                $"{facility.X};" +
                $"{facility.Y};" +
                $"{EntityFactory.CreateFacilityTypeStringForSave(facility)}";

            await writer.WriteLineAsync(line);

        }

        //VEHICLES

        await writer.WriteLineAsync(game.Player.Vehicles.Count.ToString());
        foreach (var vehicle in game.Player.Vehicles)
        {
            string line =
                $"{vehicle.GetType().Name};" +
                $"{vehicle.ID};" +
                $"{vehicle.X};" +
                $"{vehicle.Y};" +
                $"{vehicle.Condition};" +
                $"{vehicle.Direction};" +
                $"{(vehicle.Destination is null ? "null" : vehicle.Destination.ID)}|" +
                $"{String.Join(',', vehicle.Route.Select(x => x.ID))}";


            await writer.WriteLineAsync(line);
        }

        game.Saves.Add((saveName, time));
    }
    public static async Task<HashSet<(string name, DateTime timeOfSave)>> GetSaves()
    {
        return await Task.Run(() =>
        {
            string path = Path.Combine(Application.persistentDataPath, "saves");
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
        });
    }
}
