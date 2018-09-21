using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    public Transform blasterProjectileSpawn;
    public GameObject projectile;
    public float fireRate;

    private float nextShot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Shoot") && Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;

            Instantiate(projectile, blasterProjectileSpawn.position, blasterProjectileSpawn.rotation);
        }
    }
}
