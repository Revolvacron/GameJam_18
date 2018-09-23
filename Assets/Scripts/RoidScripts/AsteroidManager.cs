using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [Tooltip("The asteroids this manager spawns")]
    public GameObject asteroidPrefab;
    private Vector3 spawnPoint;
    [Tooltip("The time (in milliseconds) between asteroid spawns.")]
    public float timerStart;
    float timer;
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
            // spawn point is determined first as a direction from center
            spawnPoint = Random.insideUnitCircle.normalized;
            // when instantiated, this direction (which has a magnitude of 1) is multiplied to give an exact position
            Instantiate(asteroidPrefab, spawnPoint * 1500, Random.rotation, this.transform);
        } else
        {
            timer--;
        }
    }
}
