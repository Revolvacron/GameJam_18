using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EVE.SOF;

/// <summary>
/// Spawns random asteroids within a box volume.
/// </summary>
[ExecuteInEditMode]
public class AsteroidSpawner : MonoBehaviour
{
    public SOFInterface sofInterface = null;

    [Range(0, 99999)]
    [Tooltip("The number of asteroids to spawn at a time.")]
    public int nAsteroids = 100;

    [Tooltip("The minimum size of the asteroid to spawn.")]
    public float minSize = 1.0f;
    [Tooltip("The maximum size of the asteroid to spawn.")]
    public float maxSize = 100.0f;

    [Tooltip("The minimum dirt level of the asteroid to spawn.")]
    public float minDirt = 0.3f;
    [Tooltip("The maximum dirt level of the asteroid to spawn.")]
    public float maxDirt = 0.65f;

    [Tooltip("The minimum spin speed of the asteroid to spawn.")]
    public float minSpin = 1.0f;
    [Tooltip("The maximum spin speed of the asteroid to spawn.")]
    public float maxSpin = 10.0f;

    [Tooltip("The faction to use for the spawned asteroids.")]
    public string faction = "gneiss";
    [Tooltip("The race to use for the spawned asteroids.")]
    public string race = "asteroid";

    [Tooltip("The hulls to possibly use for the asteroids (doesn't strictly need to be an asteroid hull).")]
    public List<string> hulls = new List<string>()
    {
        "rock_01v1",
        "rock_01v2",
        "rock_01v3",
        "rock_01v4",
        "rock_02v1",
        "rock_02v2",
        "rock_02v3",
        "rock_02v4",
        "ice_01v1",
        "ice_01v2",
        "ice_01v3",
        "ice_01v4",
        "crystal_01v1",
        "crystal_01v2",
        "crystal_01v3",
        "crystal_01v4",
        "crystal_02v1",
        "crystal_02v2",
        "crystal_02v3",
        "crystal_02v4",
    };

    // Use this for initialization
    void Start()
    {
        Debug.Assert(sofInterface != null, "An item with a sof interface must be supplied.");
    }

    public Vector3 GenerateRandomAxis()
    {
        return new Vector3(
            Random.Range(-1.0f, 1.0f),
            Random.Range(-1.0f, 1.0f),
            Random.Range(-1.0f, 1.0f)
        );
    }

    public Vector3 GenerateRandomPosition()
    {
        return new Vector3(
            Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2),
            Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2),
            Random.Range(transform.position.z - transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2)
        );
    }

    /// <summary>
    /// Spawns one asteroid at random.
    /// </summary>
    public void SpawnSingle()
    {
        var dna = hulls[Random.Range(0, hulls.Count)] + ":" + faction + ":" + race;
        var size = Random.Range(minSize, maxSize);
        var dirtAmount = Random.Range(minDirt, maxDirt);
        var so = sofInterface.SpawnShip(dna, size, dirtAmount);

        so.transform.position = GenerateRandomPosition();
        var spin = so.AddComponent<Spin>();
        spin.axis = GenerateRandomAxis();
        spin.spinSpeed = Random.Range(minSpin, maxSpin);

        so.transform.SetParent(transform);
    }

    /// <summary>
    /// Spawn nAsteroids...
    /// </summary>
    public void Spawn()
    {
        for (int i = 0; i < nAsteroids; ++i)
        {
            SpawnSingle();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
