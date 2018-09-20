using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EveFlyByVelocity : MonoBehaviour
{
    private Rigidbody _rb = null;

    /// <summary>
    /// A smoothing value on the turning animation. Lower = Slower.
    /// </summary>
    [Range(0.0f, 10.0f)]
    public float turnSmoothing = 0.01f;
    /// <summary>
    /// The amount the asset tilts on the z axis when implying turning.
    /// </summary>
    [Range(0.0f, 90.0f)]
    public float tiltAmount = 1.0f;

	// Use this for initialization
	private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	// Update is called once per frame
	private void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.identity;
        if (_rb.velocity.magnitude != 0)
            targetRotation = Quaternion.LookRotation(_rb.velocity.normalized, Vector3.up);
        else
            targetRotation = Quaternion.LookRotation(new Vector3(transform.forward.x, 0, transform.forward.z), Vector3.up);

        var desiredTilt = (transform.forward.AngleAboutAxis(_rb.velocity.normalized, Vector3.up) / 180.0f) * tiltAmount;
        targetRotation *= Quaternion.AngleAxis(desiredTilt, -Vector3.forward);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, Time.deltaTime * turnSmoothing);
    }
}
