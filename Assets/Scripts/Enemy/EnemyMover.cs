using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    //List where all waypoints are stored for the enemy to follow;
    [Header("Waypoints stored for fixed Wapoints")]
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    //variables used to change the speed of the enemy
    [Header("Speed Settings")]
    [SerializeField][Range(0f, 10f)] float enemySpeed = 1f;
    private float lerpStart = 0f;

    //chached References
    private Enemy changeCurrency;

    // Start is called before the first frame update
    void OnEnable()
    {
        CachedReferences();
        EnableFuntions();
    }

    //references cached to the class
    private void CachedReferences()
    {
        changeCurrency = GetComponent<Enemy>();
    }

    //functions that gets called as the class is enabled
    private void EnableFuntions()
    {
        FindPath();
        StartPoint();
        StartCoroutine(DisplayPath());
    }

    //finds the waypoints tagged as the path and stores them in the list
    private void FindPath()
    {
        path.Clear();

        GameObject pathParent = GameObject.FindGameObjectWithTag(Tags.TAGS_PATH);

        foreach(Transform childTransform in pathParent.transform)
        {
            Waypoint waypoint = childTransform.GetComponent<Waypoint>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }

    }

    //get the startposition where the enemies must spawn
    private void StartPoint()
    {
        transform.position = path[0].transform.position;
    }

    //Let the enemy move from one tile to the next as set in the list
   IEnumerator DisplayPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
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
