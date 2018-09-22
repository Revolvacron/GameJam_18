using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Blaster : MonoBehaviour
{
    public Transform blasterProjectileSpawn;
    public GameObject projectile;
    public float fireRate;
    //Variable definitions for multiplayer input support
    public string ShootButton = "Shoot_P1";
    private float nextShot;
    public InputDevice controller1;
    public InputDevice controller2;


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controller1 = InputManager.ActiveDevice;
        if (controller1.RightTrigger.IsPressed && Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;

            Instantiate(projectile, blasterProjectileSpawn.position, blasterProjectileSpawn.rotation);
        }
    }
}
