using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPose, TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPose, Direction2D.cardinalDirList);
        foreach (var wallPosition in basicWallPositions)
        {
            tileMapVisualizer.PaintSingleBasicWall(wallPosition);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var floorPosition in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = floorPosition + direction;
                if (floorPositions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }

        return wallPositions;
    }
}
