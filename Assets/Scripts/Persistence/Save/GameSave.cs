using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public sealed partial class Game
{
    public SaveData CreateSaveData()
    {
        return new SaveData
        {
            MapSize = _map.Size,
            Tiles = _map.CreateTileData(),
            PlayerMoney = _player.Money,
            CurrentTime = CurrentTime
        };
    }

    public void SaveGame(string path)
    {
        var data = CreateSaveData();

        var json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

        File.WriteAllText(path, json);
    }

    public HashSet<Save> LoadGame(Save save)
    {
        string path = save.FilePath;

        var json = File.ReadAllText(path);
        var data = JsonConvert.DeserializeObject<SaveData>(json);

        _map = new Map(data.MapSize);
        _player = new Player();

        foreach (var tile in data.Tiles)
        {
            var entity = EntityFactory.Create(tile.EntityType);
            _map.PlaceObject(tile.X, tile.Y, entity);

            if (entity is Facility f)
                _player.Facilities.Add(f);
        }

        _player.Money = data.PlayerMoney;
        CurrentTime = data.CurrentTime;

        return new HashSet<Save> { save };
    }
}