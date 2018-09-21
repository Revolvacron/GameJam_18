using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    public float orbitSpeed;
    public Vector3 orbitDirection;
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        //Rotate the planet about its own z-axis
        transform.Rotate(axis: Vector3.forward, angle: Time.deltaTime*rotationSpeed);
        //Rotate the planet around the world's z-axis
        transform.RotateAround(Vector3.zero, orbitDirection, orbitSpeed * Time.deltaTime);
    }
}
