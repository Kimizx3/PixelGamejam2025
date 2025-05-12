using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Tiles tile;
    public int row, column;
    public Transform camPose;
    public Dictionary<Vector2, Tiles> tiles;

    private void Start()
    {
        GenerateTile();
    }

    private void GenerateTile()
    {
        tiles = new Dictionary<Vector2, Tiles>();
        for (int x = 0; x < row; ++x)
        {
            for (int y = 0; y < column; ++y)
            {
                var tileSet = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                // var offset = (x % 2) ^ (y % 2); // Bitwise version
                var offset = (x + y) % 2 != 0; // For readability
                tileSet.Init(offset);

                tiles[new Vector2(x, y)] = tileSet;
            }
        }

        camPose.position = new Vector3((float)row / 2 - 0.5f, (float)column / 2 - 0.5f, -10);
    }

    public Tiles GetTilePose(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
