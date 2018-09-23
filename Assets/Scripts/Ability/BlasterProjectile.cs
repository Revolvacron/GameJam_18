using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterProjectile : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public float projectileVelocityMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed * projectileVelocityMultiplier;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
