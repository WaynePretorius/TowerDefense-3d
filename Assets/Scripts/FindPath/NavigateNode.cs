using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateNode : MonoBehaviour
{
    //variables declared at the start for later use
    private Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down };
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int endCoordinates;

    //cashed references
    private Node startNode;
    private Node endNode;
    private Node currentSearchNode;

    private Gridmanager gridManager;

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    private Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    private Queue<Node> nodeQueu = new Queue<Node>();

    //first function that gets called as soon as the object comes into play;
    private void Awake()
    {
        StartupCaches();
    }
    
    //function that declares the startup function
    private void StartupCaches()
    {
        gridManager = FindObjectOfType<Gridmanager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        StartUpFunctions();
        BreadthFirstSearch();
        BuildPath();
    }

    //function that will be processed after everything else has been instantiated when the game start
    private void StartUpFunctions()
    {

        startNode = gridManager.Grid[startCoordinates];
        endNode = gridManager.Grid[endCoordinates];
    }

    //Function that looks for the neighbouring nodes and stores it in a list
    private void LookForNeighbourNode()
    {
        List<Node> neighbours = new List<Node>();

        //look through all the nodes in each of the directions specified
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighbourCoordinates))
            {
                neighbours.Add(grid[neighbourCoordinates]);
            }
        }

        //look through the nodes, if they havent been added yet and no obstruction on them, then add it to the queu
        foreach (Node neighbour in neighbours)
        {
            if(!reached.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                neighbour.connectNode = currentSearchNode;
                reached.Add(neighbour.coordinates, neighbour);
                nodeQueu.Enqueue(neighbour);
            }
        }
    }

    //function that will loop through the tiles and see what path will run best to the end destination
    private void BreadthFirstSearch()
    {
        bool isSearching = true;

        nodeQueu.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while(nodeQueu.Count > 0 && isSearching)
        {
            currentSearchNode = nodeQueu.Dequeue();
            currentSearchNode.isExplored = true;
            LookForNeighbourNode();
            if(currentSearchNode.coordinates == endCoordinates)
            {
                isSearching = false;
            }
        }
    }

    //funtion that iterates through the path, and connects it together, reverses the path and then gives the result of the path
    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectNode != null)
        {
            currentNode = currentNode.connectNode;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }
}
