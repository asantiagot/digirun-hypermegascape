using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStrip : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 5f;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
}
