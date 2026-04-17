using System.Collections.Generic;
using Unity.VisualScripting;

public sealed partial class Map
{
    public List<TileData> CreateTileData()
    {
        var tiles = new List<TileData>();

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                var tile = _map[i, j];
                if (tile.Entity == null) continue;

                var entity = tile.Entity;

                tiles.Add(new TileData
                {
                    X = i,
                    Y = j,
                    EntityType = entity.GetType().Name,
                    GenericType = GetGenericTypeName(entity),

                    IsGenerated = entity is Facility f ? f.IsGenerated : null,
                });
            }
        }

        return tiles;
    }
    private string GetGenericTypeName(GameEntity entity)
    {
        var type = entity.GetType();

        if (type.IsGenericType)
        {
            return type.GetGenericArguments()[0].Name;
        }

        return null;
    }

}