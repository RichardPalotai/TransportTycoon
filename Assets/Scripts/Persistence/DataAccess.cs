using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DataAccess : IDataAccess
{
    public HashSet<(string name, DateTime timeOfSave)> GetSaves()
    {
        return Save.GetSaves();
    }

    public Game LoadGameAsync(string name)
    {
        return Load.LoadAsync(name);
    }

    public async Task SaveGameAsync(Game game)
    {
        await Save.SaveAsync(game);
    }
}