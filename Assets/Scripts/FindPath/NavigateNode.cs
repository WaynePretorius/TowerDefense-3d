using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateNode : MonoBehaviour
{
    //variables declared at the start for later use
    private Vector2Int[] directions = { Vector2Int.right, Vector2Int.up, Vector2Int.left, Vector2Int.down };
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int endCoordinates;

    //properties for the variables declared(encapsulation)
    public Vector2Int StartCoordinates { get { return startCoordinates; } }
    public Vector2Int EndCoordinates { get { return endCoordinates; } }

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
            GridSettings();
        }

    }

    //set the dictionary of the grid to gridmanager and set up the start and enpoints of the grid.
    private void GridSettings()
    {
        grid = gridManager.Grid;
        startNode = grid[startCoordinates];
        endNode = grid[endCoordinates];
    }

    // Start is called before the first frame update
    void Start()
    {
        FindNewPath();
    }

    //Finds and builds the new path after an obstruction has been set in place
    public List<Node> FindNewPath()
    {
        return FindNewPath(startCoordinates);
    }

    //method overload, needs coordinates of where the original doesnt
    public List<Node> FindNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
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
    private void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        endNode.isWalkable = true;

        nodeQueu.Clear();
        reached.Clear();

        bool isSearching = true;

        nodeQueu.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

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

    //returns true or false depending if a new obstruction has been placed on the tiles
    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = FindNewPath();
            grid[coordinates].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                FindNewPath();
                return true;
            }
        }

        return false;
    }

    //calls to all to say recalculate path, if they can they will, otherwise, it just send the message, even if there is no recievers
    public void NotifyRecievers()
    {
        BroadcastMessage(Tags.METHOD_RECALCULATE, false, SendMessageOptions.DontRequireReceiver);
    }
}
