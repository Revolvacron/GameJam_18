using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 directionInput;

    public float agility = 0.5f;
    public float maxSpeed = 10;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        directionInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // thrustInput = Input.GetAxis("Vertical");
        // turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        print(rb.rotation);
        print(directionInput.rotation);
//        rb.AddTorque (rb.velocity, directionInput * maxSpeed, agility);
        
    }
}
