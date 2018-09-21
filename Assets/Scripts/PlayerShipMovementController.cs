using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovementController : MonoBehaviour
{
    private Vector2 directionInput;
    private bool speedInput;

    [Tooltip("The instanteous angular velocity of the vehicle.")]
    public float agility;

    [Tooltip("The maximum velocity of the vehicle.")]
    public float maxSpeed;

    [Tooltip("The maximum acceleration of the vehicle.")]
    public float maxAcceleration;

    // Internal reference to the object's rigid body component.
    private Rigidbody2D mRigidBody;

    // Update is called once per frame
    private void Update()
    {
        // Get the player's desired direction
        directionInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        speedInput = Input.GetKey("Booster");
    }

    void FixedUpdate()
    {
        // Get a rotation based on the desired direction
        float desiredDirection = DetermineDesiredRotation();
    private void Start() {
      this.mRigidBody = this.GetComponent<Rigidbody2D>();
    }

        // Check to see if the ship is pointed in the player's desired direction
        if (!Mathf.Approximately(rb.rotation, desiredDirection))
        {
            // Find the delta angle from current direction to desired direction
            float deltaRotation = desiredDirection - rb.rotation;

            // Check to see if the desired direction is further than the ship can rotate this update
            if (Mathf.Abs(deltaRotation) > agility)
            {
                // Rotate the ship based on its agility
                rb.MoveRotation(rb.rotation + Mathf.Sign(deltaRotation) * agility);
            }
            else
            {
                // Rotate the ship to the specified direction
                rb.MoveRotation(desiredDirection);
            }
        }

        // Move forward!
        if (speedInput)
        {
            rb.AddRelativeForce(transform.forward * maxAcceleration);
        }
    }

    // How far does the ship need to rotate
    private float DetermineDesiredRotation()
    {
        if (directionInput != Vector2.zero)
        {
            // Find out how far the ship needs to rotate to match the direction the player wants to go
            float desiredDirection = Vector2.SignedAngle(Vector2.up, directionInput);

            if (desiredDirection < 0)
            {
                desiredDirection += 360;
            }

            return desiredDirection;
        }

        return rb.rotation;
    }
}