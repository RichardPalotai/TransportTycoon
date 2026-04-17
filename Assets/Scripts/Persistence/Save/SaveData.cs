using System;
using System.Collections.Generic;

public class SaveData
{
    public int MapSize { get; set; }
    public List<TileData> Tiles { get; set; }

    public double PlayerMoney { get; set; }
    public DateTime CurrentTime { get; set; }
}