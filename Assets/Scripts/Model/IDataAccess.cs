using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDataAccess
{
    HashSet<(string name, DateTime timeOfSave)> GetSaves();
    Task<(Map, List<GameEntity>)> LoadGameAsync(string name);
    Task SaveGameAsync(string name);

}
