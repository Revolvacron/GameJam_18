﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class PlayerShipMovementController : MonoBehaviour
{
    [Tooltip("The instanteous angular velocity of the vehicle.")]
    public float agility;

    [Tooltip("The maximum velocity of the vehicle.")]
    public float maxSpeed;

    [Tooltip("The maximum acceleration of the vehicle.")]
    public float maxAcceleration;

    [Tooltip("Booster Particles")]
    public ParticleSystem boosterTrail;

    public InputDevice inputDevice;

    // Internal reference to the object's rigid body component.
    private Rigidbody2D mRigidBody;

    // Internal reference to the desired orientation of the vehicle.
    private Vector2 mDesiredOrientation;

    // Internal reference to the desired state of the vehicle's boost.
    private bool mBoost;

    private void Start() {
      this.mRigidBody = this.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // Gather user inputs.
        if (inputDevice == null)
        return;
        this.mDesiredOrientation = inputDevice.LeftStick.Vector.normalized;
        this.mBoost = inputDevice.LeftTrigger.IsPressed;
    }

    private void FixedUpdate() {
      // TODO: implement maximum speed limit (speed of light).
      if(this.mBoost) {
        this.mRigidBody.AddRelativeForce(Vector2.up * this.maxAcceleration);
        boosterTrail.Play();
      }
      else {
        boosterTrail.Stop();
      }

      // Update the vehicle's orientation.
      this.UpdateVehicleOrientation();
    }

    /**
     *  Performs an update of the vehicle's orientation based on the desired orientation of the controller.
     */
    private void UpdateVehicleOrientation() {
      // Exit early if we don't need to rotate.
      if(this.mDesiredOrientation.Equals(Vector2.zero)) {
        return;
      }

      // Get Vector2 of vehicle orientation.
      float rotation = this.mRigidBody.rotation * Mathf.Deg2Rad;
      Vector2 orientation = new Vector2(-Mathf.Sin(rotation), Mathf.Cos(rotation));

      // Get angle from orientation towards desination.
      float delta = Vector2.SignedAngle(orientation, this.mDesiredOrientation);

      // Determine how much to rotate by.
      float step = agility;
      if(Mathf.Abs(delta) < Mathf.Abs(agility)) {
        step = Mathf.Abs(delta);
      }
      step *= Mathf.Sign(delta);

      // Apply the incremental rotation.
      this.mRigidBody.rotation += step;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<PlayerShipMovementController>()) {
            Destroy(this.gameObject);
        }
    }

}
