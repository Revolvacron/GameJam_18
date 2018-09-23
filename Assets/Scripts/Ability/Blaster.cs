using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    public PlayerShipMovementController playerController = null;
    public Transform blasterProjectileSpawn;
    public GameObject projectile;
    public float fireRate;

    private float nextShot;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(playerController != null, "ERROR: A player controller must be attached.");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.inputDevice != null && playerController.inputDevice.RightTrigger.IsPressed && Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            Instantiate(projectile, blasterProjectileSpawn.position, blasterProjectileSpawn.rotation);
        }
    }
}
