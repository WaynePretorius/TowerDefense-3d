using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//data class that will sotre the waypoints as nodes that can be used to get the pathe ot the end point
[System.Serializable]
public class Node
{
    public Vector2Int coordinates;

    public bool isWalkable;
    public bool isExplored;
    public bool isPath;

    public Node connectNode;

    //constructor to get the position of the node, and see if the enemy can move to the node
    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
