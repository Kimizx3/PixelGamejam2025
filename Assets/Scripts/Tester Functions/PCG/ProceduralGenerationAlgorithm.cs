using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithm
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var prePose = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPose = prePose + Direction2D.GetRandCardinalDir();
            path.Add(newPose);
            prePose = newPose;
        }

        return path;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), // Up
        new Vector2Int(1, 0), // Right
        new Vector2Int(0, -1),// Down
        new Vector2Int(-1, 0) // Left
    };

    public static Vector2Int GetRandCardinalDir()
    {
        return cardinalDirList[Random.Range(0, cardinalDirList.Count)];
    }
}
