using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    //List where all waypoints are stored for the enemy to follow;
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    //variables used to change the speed of the enemy
    [Header("Speed Settings")]
    [SerializeField] float timeToNextmove = 1f;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(DisplayPath());
    }

    //Displays the pathe that the enemy will take in the console
   IEnumerator DisplayPath()
    {
        foreach(Waypoint waypoint in path)
        {
            print(waypoint.name);
            FollowPath(waypoint);
            yield return new WaitForSeconds(timeToNextmove);
        }
    }

    //get the enemy to move from current pathe to new path
    void FollowPath(Waypoint newPos)
    {    
        transform.position = newPos.transform.position;
    }
}
