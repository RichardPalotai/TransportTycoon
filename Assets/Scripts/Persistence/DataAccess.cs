using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DataAccess : IDataAccess
{
    public HashSet<(string name, DateTime timeOfSave)> GetSaves()
    {
        throw new NotImplementedException();
    }

    public Task<(Map, List<GameEntity>)> LoadGameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task SaveGameAsync(DateTime time, Map map, Game game)
    {
        await Save.SaveAsync(time, map, game);
    }
}