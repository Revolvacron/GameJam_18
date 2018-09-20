﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 directionInput;

    public float agility;
    public float maxSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get the player's desired direction
        directionInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        // Get a rotation based on the desired direction
        float desiredDirection = DetermineDesiredRotation();

        // Check to see if the ship is pointed in the player's desired direction
        if (!Mathf.Approximately(rb.rotation, desiredDirection))
        {
            // Find the delta angle from current direction to desired direction
            float deltaRotation = desiredDirection - rb.rotation;

            // Check to see if the desired direction is further than the ship can rotate this update
            if (Mathf.Abs(deltaRotation) > agility)
            {
                // Rotate the ship based on its agility
                // But first check to see which direction we have to go
                rb.MoveRotation(rb.rotation + Mathf.Sign(deltaRotation) * agility);

                /* if (deltaRotation > 0)
                {
                    rb.MoveRotation(rb.rotation + agility);
                }
                else
                {
                    rb.MoveRotation(rb.rotation - agility);
                } */
            }
            else
            {
                // Rotate the ship to the specified direction
                rb.MoveRotation(desiredDirection);
            }
        }

        print(rb.rotation);
        print(desiredDirection);
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