using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
{
    public Transform p1;
    public Transform p2;
    public Transform p3;
    public Transform p4;

    [Tooltip("The person who shoots. Must be set by the firing script")]
    public Transform shooter;
    public Transform target;

    [Tooltip("Trail Particles")]
    public ParticleSystem trail;

    public Rigidbody2D rb;

    public float thrust;

    // Start is called before the first frame update
    void Start()
    {
        if (shooter != p1) target = p1;
        else if (shooter != p2) target = p2;

        if (Vector3.Distance(this.transform.position, p2.position) < Vector3.Distance(this.transform.position, target.position) && shooter != p2)
        {
            target = p2;
        }

        if (Vector3.Distance(this.transform.position, p3.position) < Vector3.Distance(this.transform.position, target.position) && shooter != p3)
        {
            target = p3;
        }

        if (Vector3.Distance(this.transform.position, p4.position) < Vector3.Distance(this.transform.position, target.position) && shooter != p4)
        {
            target = p4;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > 100)
        {
            // ... slow it down.
            rb.velocity *= .99f;
        }
        this.transform.LookAt(target, new Vector3(-1,-1,0));
        rb.AddForce(transform.forward * thrust);
        trail.Play();
    }
}
