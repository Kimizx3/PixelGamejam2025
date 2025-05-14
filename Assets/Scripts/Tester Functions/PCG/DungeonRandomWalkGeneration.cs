using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class DungeonRandomWalkGeneration : AbstractDungeonGenerator
{
    [SerializeField] private SO_RandomWalkGenerator roomGenerationParameters;

    protected override void RunProceduralGeneration() 
    {
        HashSet<Vector2Int> floorPosition = RunRandomWalk(roomGenerationParameters);
        tileMapVisualizer.ClearTiles();
        tileMapVisualizer.PaintFloorTiles(floorPosition);
        WallGenerator.CreateWalls(floorPosition, tileMapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SO_RandomWalkGenerator parameters)
    {
        var currentPose = startPose;
        HashSet<Vector2Int> floorPose = new HashSet<Vector2Int>();
        for (int i = 0; i < roomGenerationParameters.iteration; i++)
        {
            var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPose, roomGenerationParameters.walkLength);
            floorPose.UnionWith(path); // hashset function, check online or GPT
            if (roomGenerationParameters.startRandomlyEachIteration)
            {
                currentPose = floorPose.ElementAt(Random.Range(0, floorPose.Count));
            }
        }

        return floorPose;
    }
}
