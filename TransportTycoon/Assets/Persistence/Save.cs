using System.Threading.Tasks;

public sealed class Save
{
    public string Name { get; set; }
    public ulong TimeOfSave { get; init; }

    public async Task SaveAsync(string name, ulong time)
    {
        
    }
}
