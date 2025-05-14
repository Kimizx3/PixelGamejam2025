using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTileMap, wallTileMap;
    [SerializeField] private TileBase floorTile, wallTop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPose)
    {
        PaintTiles(floorPose, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }
    
    public void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTileMap, wallTop, position);
    }

    public void ClearTiles()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }

    
}
