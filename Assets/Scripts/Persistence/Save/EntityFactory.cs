using System;

public static class EntityFactory
{
    public static string CreateFacilityTypeStringForSave(Facility facility)
    {
        Type type = facility.GetType();

        if (!type.IsGenericType)
        {
            return type.Name;
        }

        string baseTypeName =
            type.Name.Split('`')[0];

        string genericArgument =
            type.GetGenericArguments()[0].Name;

        return $"{baseTypeName}:{genericArgument}";
    }
}