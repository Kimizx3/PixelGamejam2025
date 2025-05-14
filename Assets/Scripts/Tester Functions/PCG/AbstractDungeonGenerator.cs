using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField] protected TileMapVisualizer tileMapVisualizer = null;
    [SerializeField] protected Vector2Int startPose = Vector2Int.zero;

    public void GenerateDungeon()
    {
        tileMapVisualizer.ClearTiles();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
