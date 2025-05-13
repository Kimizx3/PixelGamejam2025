using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField] private int iter = 10;
    [SerializeField] public int walkLength = 10;
    [SerializeField] public bool startRandomlyEachIter = true;

    [SerializeField] private TileMapVisualizer tileMapVisualizer;

    public void RunProceduralGenerator()
    {
        HashSet<Vector2Int> floorPosition = RunRandomWalk();
        tileMapVisualizer.ClearTiles();
        tileMapVisualizer.PaintFloorTiles(floorPosition);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPose = startPosition;
        HashSet<Vector2Int> floorPose = new HashSet<Vector2Int>();
        for (int i = 0; i < iter; i++)
        {
            var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPose, walkLength);
            floorPose.UnionWith(path); // hashset function, check online or GPT
            if (startRandomlyEachIter)
            {
                currentPose = floorPose.ElementAt(Random.Range(0, floorPose.Count));
            }
        }

        return floorPose;
    }
}
