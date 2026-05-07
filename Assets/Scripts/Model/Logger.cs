using System;
using System.Text;
using UnityEngine;

public static class Logger
{
    public static void Log(string message)
    {
        Debug.Log($"[GAME] {message}");
    }

    public static void ProductionLog(Type facilityType, int amount)
    {
        Debug.Log($"[PROD] {facilityType.Name} facility produced: {amount}");
    }
    public static void ObjectPlacedLog(Type @object, int toX, int toY)
    {
        Debug.Log($"[PLCD] {@object.Name} placed to X: {toX}, Y: {toY}");
    }

    public static void LogTime(DateTime currentTime)
    {
        Debug.Log($"[TIME] {currentTime:dddd, yyyy MMMM dd HH:mm:ss}");
    }
    /// <summary>
    /// 'X' means occupied field, 'o' means free.
    /// </summary>
    /// <param name="map"></param>
    public static void FreeMap(Map map)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < map.Size; i++)
        {
            for (int j = 0; j < map.Size; j++)
            {
                sb.Append(map.GetTile(i, j).IsFree ? 'o' : 'X');
            }
            Debug.Log(sb.ToString());
            sb.Clear();
        }
    }
    public static void LogMap(Map map, bool? onlyFrees = null, int? x = null, int? y = null)
    {
        for (int i = 0; i < map.Size; i++)
        {
            for (int j = 0; j < map.Size; j++)
            {
                if ((onlyFrees is null && x is null && y is null) ||
                    onlyFrees == map.GetTile(i, j).IsFree ||
                    x == i || y == j)
                    Debug.Log($"[GAME]: X: {i}, Y: {j}, IsFree: {map.GetTile(i, j).IsFree}, Obj: {map.GetTile(i, j).Entity}, ObjId: {map.GetTile(i, j).ObjectId}");
            }
        }
    }
}
