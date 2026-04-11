using System.Collections.Generic;

public class PathFinder
{
    private readonly Map _map;

    public PathFinder(Map map)
    {
        _map = map;
    }
    /// <summary>
    /// Determines whether there is contiguous path (road) between 'from' and 'to' coordinates.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public bool HasPathBetweenFacilities(Facility from, Facility to)
    {
        var starts = _map.GetFacilityNeighborRoads(from);
        var ends = _map.GetFacilityNeighborRoads(to);
        HashSet<(int, int)> endSet = new(ends);
        HashSet<(int, int)> visited = new();
        Queue<(int, int)> queue = new();

        if (starts.Count == 0 || ends.Count == 0)
        {
            return false;
        }

        foreach (var start in starts)
        {
            queue.Enqueue(start);
            visited.Add(start);
        }

        while (queue.Count != 0)
        {
            (int x, int y) current = queue.Dequeue();

            if (endSet.Contains(current))
                return true;

            foreach (var neighbor in _map.GetTilesNeighborRoadsCoords(current.x, current.y))
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        return false;
    }
}
