using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    //List where all waypoints are stored for the enemy to follow;
    [SerializeField] List<Node> path = new List<Node>();

    //variables used to change the speed of the enemy
    [Header("Speed Settings")]
    [SerializeField][Range(0f, 10f)] float enemySpeed = 1f;
    private float lerpStart = 0f;

    //chached References
    private Enemy changeCurrency;
    private Gridmanager gridManager;
    private NavigateNode pathFinder;

    //first function that is called as soon as the object comes into play
    private void Awake()
    {
        CachedReferences();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        EnableFuntions();
    }

    //references cached to the class
    private void CachedReferences()
    {
        changeCurrency = GetComponent<Enemy>();
        gridManager = FindObjectOfType<Gridmanager>();
        pathFinder = FindObjectOfType<NavigateNode>();
    }

    //functions that gets called as the class is enabled
    private void EnableFuntions()
    {
        StartPoint();
        RecalculatePath(true);
    }

    //finds the waypoints tagged as the path and stores them in the list
    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = CheckIfStart(resetPath);

        StopAllCoroutines();

        path.Clear();
        path = pathFinder.FindNewPath(coordinates);

        StartCoroutine(DisplayPath());
    }

    //see if it is the start coordinates, or new coordinates
    private Vector2Int CheckIfStart(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        return coordinates;
    }

    //get the startposition where the enemies must spawn
    private void StartPoint()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    //Let the enemy move from one tile to the next as set in the list
   IEnumerator DisplayPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(pathFinder.EndCoordinates);
            float travelpercent = lerpStart;

            transform.LookAt(endPos);

            while (travelpercent < 1)
            {
                travelpercent += Time.deltaTime * enemySpeed;
                transform.position = Vector3.Lerp(startPos, endPos, travelpercent);

                yield return new WaitForEndOfFrame();
            }
        }

        EndOfLine();
    }

    //Runs all function once the enemy finds it path to the end
   private void EndOfLine()
    {
        gameObject.SetActive(false);
        changeCurrency.DeductCurrency();
    }
}
