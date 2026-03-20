using System;
using System.Threading.Tasks;

public sealed class Save
{
    public string Name { get; set; }
    public DateTime TimeOfSave { get; init; }

    public async Task SaveAsync(string name, DateTime time)
    {
        
    }
}
