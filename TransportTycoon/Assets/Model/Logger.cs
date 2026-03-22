using System;
using UnityEngine;

public static class Logger
{
    public static void Log(string message)
    {
        Debug.Log($"[GAME] {message}");
    }

    public static void ProductionLog(Type facilityType, int amount)
    {
        Debug.Log($"[PROD] {facilityType.Name} facility produced:\t{amount}\t");
    }

    public static void LogTime(DateTime currentTime)
    {
        Debug.Log($"[TIME] {currentTime:dddd, yyyy MMMM dd HH:mm:ss}");
    }
}
