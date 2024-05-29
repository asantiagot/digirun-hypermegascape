using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float bpm = 120.0f;
    private float beatInterval;
    private float nextBeatTime;

    void Start()
    {
        beatInterval = 60.0f / bpm;
        nextBeatTime = Time.time + beatInterval;
    }

    void Update()
    {
        if (Time.time >= nextBeatTime)
        {
            OnBeat();
            nextBeatTime += beatInterval;
        }
    }

    void OnBeat()
    {
        // Debug.Log("Beat");
        // SpawnObstacle();
    }

    void SpawnObstacle()
    {
        // GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }

    public GameObject obstaclePrefab;
    public Vector3 spawnPosition;
}
