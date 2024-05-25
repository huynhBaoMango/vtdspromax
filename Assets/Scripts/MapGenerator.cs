using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapLength, mapWidth;
    public int roomWidthMin, roomLengthMin;
    public int maxIterations;
    public int corridorWidth;

    // Start is called before the first frame update
    void Start()
    {
        CreateDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateDungeon()
    {
        DungeonGenerator generator = new DungeonGenerator(mapLength, mapWidth);
        //var listOfRooms = generator.CalculateRooms(maxIterations, roomWidthMin, roomLengthMin);
    }
}
