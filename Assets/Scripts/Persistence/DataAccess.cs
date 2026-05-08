using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DataAccess : IDataAccess
{
    public async Task<HashSet<(string name, DateTime timeOfSave)>> GetSaves()
    {
        return await Save.GetSaves();
    }

    public Task<(Map, List<GameEntity>)> LoadGameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task SaveGameAsync(Game game)
    {
        await Save.SaveAsync(game);
    }
}