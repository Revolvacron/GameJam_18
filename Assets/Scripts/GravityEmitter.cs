using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEmitter : MonoBehaviour {
  [Tooltip("Mass used to determine gravitational pull.")]
  public float mass;

  [Tooltip("The distance under which an object will not have a force applied to it.")]
  public float gravityWellBoundary = 10;

  // Gravitational constant, the value doesn't really matter just might need to be adjusted to make things feel nice
  // with preferred mass values.
  private static float gConstant = 10;

  // Performs on every update.
  public void Update() {
    // Find all objects with a rigid body component, we need to add a force to them.
    Rigidbody2D[] bodies = Object.FindObjectsOfType<Rigidbody2D>();

    // Determine the two-dimensional position of the emitter.
    Vector2 emitter = this.transform.position;

    // Calculate the force and apply it to each rigid body.
    // TODO: determine if this can be easily done in parallel.
    foreach(Rigidbody2D body in bodies) {
      // Determine the gravitational pull vector.
      Vector2 direction = (emitter - body.position);

      // Perform a gravitational well check, if the object is under the set distance, skip it.
      if(direction.magnitude < this.gravityWellBoundary) {
        continue;
      }

      // Determine the magnitude of the force.
      float force = GravityEmitter.gConstant * (body.mass * this.mass) / (Mathf.Pow(direction.magnitude, 2));

      // Apply the force.
      body.AddForce(force * direction.normalized);
    }
  }
}
