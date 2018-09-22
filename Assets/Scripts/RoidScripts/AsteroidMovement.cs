using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public int size = 4;
    public float velocity;

    void Start()
    {
        Vector2 direction;
        direction = new Vector2(-transform.position.x * Random.Range(1.5f, 3f), -transform.position.y * Random.Range(3f, 6f));
        rb.AddForce(direction*5);
    }

    // Update is called once per frame
    void Update()
    {
        
        velocity = rb.velocity.magnitude;
        if (velocity > 100)
        {
            rb.velocity *= .99f;
        }

        if (this.transform.position.magnitude > 2000 || this.transform.position.magnitude < -2000)
        {
            Destroy(this.gameObject);
        }
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<AsteroidMovement>())
        {
        
            if (size > 0)
            {
                size--;
                this.transform.localScale *= .65f;
                // rb.velocity += Random.insideUnitCircle * 50;
                Vector3 newMove = new Vector3(rb.velocity.y, rb.velocity.x);
                rb.velocity = newMove;


                GameObject clone;
                clone = Instantiate(this.gameObject, this.transform.position + (Random.insideUnitSphere * 60 * (1 + size)), Random.rotation);
                clone.transform.localScale = this.transform.localScale;
                clone.GetComponent<Rigidbody2D>().velocity = -rb.velocity;
                clone.GetComponent<AsteroidMovement>().size = size;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
