using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDataAccess
{
    HashSet<(string name, DateTime timeOfSave)> GetSaves();
    Game LoadGameAsync(string name);
    Task SaveGameAsync(Game game);

}