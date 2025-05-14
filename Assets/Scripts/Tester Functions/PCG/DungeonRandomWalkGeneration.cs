using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class DungeonRandomWalkGeneration : AbstractDungeonGenerator
{
    [SerializeField] protected SO_RandomWalkGenerator randomWalkParameters;

    protected override void RunProceduralGeneration() 
    {
        HashSet<Vector2Int> floorPosition = RunRandomWalk(randomWalkParameters, startPose);
        tileMapVisualizer.ClearTiles();
        tileMapVisualizer.PaintFloorTiles(floorPosition);
        WallGenerator.CreateWalls(floorPosition, tileMapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SO_RandomWalkGenerator parameters, Vector2Int position)
    {
        var currentPose = position;
        HashSet<Vector2Int> floorPose = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkParameters.iteration; i++)
        {
            var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPose, randomWalkParameters.walkLength);
            floorPose.UnionWith(path); // hashset function, check online or GPT
            if (randomWalkParameters.startRandomlyEachIteration)
            {
                currentPose = floorPose.ElementAt(Random.Range(0, floorPose.Count));
            }
        }

        return floorPose;
    }
}
