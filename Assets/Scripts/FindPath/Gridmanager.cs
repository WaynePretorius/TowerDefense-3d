using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridmanager : MonoBehaviour
{
    //variable decalred at the start
    [SerializeField] private Vector2Int gridSize;

    //store all of the node as a datastructure in the grid
    private Dictionary<Vector2, Node> grid = new Dictionary<Vector2, Node>();

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
                print(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable);
            }
        }
    }
}
