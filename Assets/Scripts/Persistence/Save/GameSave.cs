using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public sealed partial class GameSave
{
    public SaveData CreateSaveData(Game game)
    {
        return new SaveData
        {
            MapSize = game.Map.Size,
            Tiles = MapSave.CreateTileData(game.Map),
            PlayerMoney = game.Player.Money,
            CurrentTime = game.CurrentTime
        };
    }

    public void SaveGame(Game game, string path)
    {
        var data = CreateSaveData(game);

        var json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

        File.WriteAllText(path, json);
    }

    public HashSet<Save> LoadGame(Save save, Game game)
    {
        string path = save.FilePath;

        var json = File.ReadAllText(path);
        var data = JsonConvert.DeserializeObject<SaveData>(json);

        game.Map = new Map(data.MapSize);
        game.Player = new Player();

        foreach (var tile in data.Tiles)
        {
            var entity = EntityFactory.Create(tile.EntityType);
            game.Map.PlaceObject(tile.X, tile.Y, entity);

            if (entity is Facility f)
                game.Player.Facilities.Add(f);
        }

        game.Player.Money = data.PlayerMoney;
        game.CurrentTime = data.CurrentTime;

        return new HashSet<Save> { save };
    }
}