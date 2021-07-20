using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridmanager : MonoBehaviour
{
    //variable decalred at the start
    [Header("Grid Settings")]
    [SerializeField] private Vector2Int gridSize;
    [Tooltip("Must be same as unity editor settings")]
    [SerializeField] private int unityGridSize = 10;

    //store all of the node as a datastructure in the grid
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    //Properties
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    public int UnityGridSize { get { return unityGridSize; } }

    //first function to call as soon as the gameobject comes into play
    private void Awake()
    {
        CreateGrid();
    }

    //getters and setters(encapsulation)
    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    //create the grid by going through every x, and y co-ordinate, and store at as the base for each node in the dictionary
    private void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }

    //Blocks the node if there is any ibstacles on the node, or the enemy cant move on it
    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    //get the coordinates from the position as a veector 3 and return it as an int vector2
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    //takes the current 2d coordiantes and returns it as 3d world coordinates
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();

        position.x = coordinates.x * unityGridSize;
        position.x = coordinates.y * unityGridSize;

        return position;
    }
}
