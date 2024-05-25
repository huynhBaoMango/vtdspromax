using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    
    List<RoomNode> allSpaceNodes = new List<RoomNode> ();
    private int mapLength;
    private int mapWidth;

    public DungeonGenerator(int mapLength, int mapWidth)
    {
        this.mapLength = mapLength;
        this.mapWidth = mapWidth;
    }

    public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(mapWidth, mapLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
    }
}