using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStripFollower : MonoBehaviour
{
    public Transform lightStrip;
    public int pointsPerSecond = 10;
    private float accumulatedPoints = 0f;

    private bool isFollowing = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightStrip"))
        {
            isFollowing = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightStrip"))
        {
            isFollowing = false;
        }
    }

    void Update()
    {
        if (isFollowing)
        {
            AwardPoints();
        }
    }

    void AwardPoints()
    {
        if (ScoreManager.instance != null)
        {
            accumulatedPoints += pointsPerSecond * Time.deltaTime;
            int pointsToAdd = Mathf.FloorToInt(accumulatedPoints);
            if (pointsToAdd > 0)
            {
                ScoreManager.instance.AddScore(pointsToAdd);
                accumulatedPoints -= pointsToAdd;
            }
        }
    }
}
