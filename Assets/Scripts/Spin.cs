using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [Tooltip("The axis to rotate about.")]
    public Vector3 axis = Vector3.up;
    [Tooltip("Spin speed (degrees per second).")]
    public float spinSpeed = 1.0f;

    private Rigidbody _rb = null;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rb)
        {
            _rb.angularVelocity = axis.normalized * spinSpeed * Mathf.Deg2Rad;
        }
        else
        {
            transform.Rotate(axis.normalized, spinSpeed * Time.fixedDeltaTime);
        }
    }
}
