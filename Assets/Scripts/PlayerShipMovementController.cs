using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxVelocity;
    public float maxThrust;
    public float turnAgility;
    private float thrustInput;
    private float turnInput;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector2.up * thrustInput, ForceMode2D.Impulse);

        if (rb.angularVelocity < turnAgility)
        {
            rb.AddTorque(turnInput * -5.0f);
        }
    }
}
