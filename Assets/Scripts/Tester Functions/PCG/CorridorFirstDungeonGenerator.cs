using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorFirstDungeonGenerator : DungeonRandomWalkGeneration
{
    [SerializeField] private int corridorLength = 14, corridorCount = 5;
    [SerializeField] [Range(0.1f, 1)] private float roomPercent = 0.8f;
    [SerializeField] private SO_RandomWalkGenerator roomGenerationParameters;
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        CreateCorridor(floorPositions);
        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }

    private void CreateCorridor(HashSet<Vector2Int> floorPosition)
    {
        var currentPosition = startPose;
        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            floorPosition.UnionWith(corridor);
        }
    }
}
