using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStrip : MonoBehaviour
{
    // This code enables the LightStrip following mechanism, disabling for now since LightStrips will be static
    // public List<Transform> waypoints;
    // public float speed = 5f;

    // private int currentWaypointIndex = 0;

    // void Update()
    // {
    //     if (currentWaypointIndex < waypoints.Count)
    //     {
    //         MoveAlongPath();
    //     }
    // }

    // void MoveAlongPath()
    // {
    //     Transform targetWaypoint = waypoints[currentWaypointIndex];
    //     Vector3 direction = targetWaypoint.position - transform.position;
    //     transform.position += direction.normalized * speed * Time.deltaTime;

    //     if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
    //     {
    //         currentWaypointIndex++;
    //     }
    // }

    // public Color touchedColor = new(255, 110, 4);
    public Material defaultMaterial;
    public Material onTouchMaterial;
    public ParticleSystem sparkleEffect;
    private ParticleSystem activeSparkleEffect;
    private Renderer lightStripRenderer;
    private bool isPlayerTouching;

    void Start()
    {
        lightStripRenderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTouching = true;
            lightStripRenderer.material = onTouchMaterial;

            if (activeSparkleEffect == null)
            {
                Vector3 collisionPoint = other.ClosestPoint(transform.position);
                activeSparkleEffect = Instantiate(sparkleEffect, collisionPoint, Quaternion.identity);
            }
            activeSparkleEffect.Play();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTouching = true;
            if (activeSparkleEffect != null)
            {
                Vector3 collisionPoint = other.ClosestPoint(transform.position);
                activeSparkleEffect.transform.position = collisionPoint;
                if (!activeSparkleEffect.isPlaying)
                {
                    activeSparkleEffect.Play();
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTouching = false;
            if (activeSparkleEffect != null)
            {
                activeSparkleEffect.Stop();
            }

            lightStripRenderer.material = defaultMaterial;
        }
    }
}
