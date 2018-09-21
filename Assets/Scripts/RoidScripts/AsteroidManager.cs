using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject asteroidPrefab;
    private Vector3 spawnPoint;
    public float timer;
    public float timerStart;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            timer = timerStart;
            spawnPoint = Random.insideUnitCircle.normalized;
            Instantiate(asteroidPrefab, spawnPoint * 1500, Random.rotation, this.transform);
        } else
        {
            timer--;
        }
    }
}
