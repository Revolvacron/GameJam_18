using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Follow : MonoBehaviour
{
    public GameObject target = null;
    public float acceleration = 1.0f;
    public float maxSpeed = 1.0f;
    public float restitutionCoefficient = 1.0f;

    private Rigidbody _rb = null;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
		if (target)
        {
            var dir = target.transform.position - transform.position;
            var distance = dir.magnitude;
            dir.Normalize();

            if (distance > restitutionCoefficient)
            {
                _rb.velocity -= _rb.velocity.normalized * acceleration * Time.fixedDeltaTime; // avoid orbiting...
                _rb.velocity += dir * acceleration * 2 * Time.fixedDeltaTime;
                _rb.velocity = _rb.velocity.normalized * Mathf.Min(maxSpeed, distance, _rb.velocity.magnitude);
            }
            else
            {
                _rb.velocity = Vector3.zero;
            }
        }
	}
}
