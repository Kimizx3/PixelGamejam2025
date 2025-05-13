using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject endRoom;
    [SerializeField] private GameObject[] roomPrefabs;
    [SerializeField] private int roomCount = 10;
    [SerializeField] private Vector2 gridSize = new Vector2(10, 10);

    private HashSet<Vector2> occupiedPositions = new HashSet<Vector2>();
    private List<Vector2> roomPositions = new List<Vector2>();
    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        // Spawn start room
        Vector2 startPos = Vector2.zero; // change this later to be randomized
        Instantiate(startRoom, startPos, Quaternion.identity);
        occupiedPositions.Add(startPos);
        roomPositions.Add(startPos);

        // Generate body rooms
        for (int i = 0; i < roomCount; i++)
        {
            Vector2 newPos;
            do
            {
                newPos = roomPositions[Random.Range(0, roomPositions.Count)] +
                         directions[Random.Range(0, directions.Length)];
            } while (occupiedPositions.Contains(newPos) || !IsWithinGrid(newPos));

            GameObject room = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)], newPos,
                Quaternion.identity);
            occupiedPositions.Add(newPos);
            roomPositions.Add(newPos);
        }

        Vector2 endPos;
        do
        {
            endPos = roomPositions[Random.Range(0, roomPositions.Count)] +
                     directions[Random.Range(0, directions.Length)];
        } while (occupiedPositions.Contains(endPos) || !IsWithinGrid(endPos));

        Instantiate(endRoom, endPos, Quaternion.identity);
    }

    bool IsWithinGrid(Vector2 position)
    {
        return Mathf.Abs(position.x) < gridSize.x / 2 && Mathf.Abs(position.y) < gridSize.y / 2;
    }
}
