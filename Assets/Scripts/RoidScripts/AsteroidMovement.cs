using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [Tooltip("This should be filled with the asteroid's own rigidbody.")]
    public Rigidbody2D rb;
    [Tooltip("How many times it can change size")]
    public int size = 4;
    [Tooltip("For checking speed of asteroid. Read-only.")]
    public float velocity;
    [Tooltip("How many times it can bump another asteroid")]
    public int life = 3;

    void Start()
    {
        // On start, provide this asteroid with an initial velocity that points toward the sun
        // plus some noise to not necessarily make it hit, of course
        Vector2 direction;
        direction = new Vector2(-transform.position.x * Random.Range(1.5f, 3f), -transform.position.y * Random.Range(3f, 6f));
        rb.AddForce(direction*5);
    }

    // Update is called once per frame
    void Update()
    {
        // figure out the speed of the asteroid
        velocity = rb.velocity.magnitude;

        // if it's going too fast...
        if (velocity > 100)
        {
            // ... slow it down.
            rb.velocity *= .99f;
        }

        // If it's well out of bounds...
        if (this.transform.position.magnitude > 2000 || this.transform.position.magnitude < -2000)
        {
            // ... kill the asteroid
            Destroy(this.gameObject);
        }
        // honestly, this was originally to keep things at z=0, but we moved to 2d,
        // and I'm afraid to change this in case it breaks something.
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if we didn't collide with another asteroid, or if we have no remaining life left
        if (!collision.gameObject.GetComponent<AsteroidMovement>() || life == 1)
        {
            // if we are not at the smallest possible size point, split.
            if (size > 0)
            {
                // reset life, and reduce size by 1
                life = 3;
                size--;
                this.transform.localScale *= .65f;
                
                // move perpedicular to your old movement.
                Vector3 newMove = new Vector3(rb.velocity.y, rb.velocity.x);
                rb.velocity = newMove;

                // create a clone of yourself
                GameObject clone;
                clone = Instantiate(this.gameObject, this.transform.position + (Random.insideUnitSphere * 60 * (1 + size)), Random.rotation);
                clone.transform.localScale = this.transform.localScale;
                clone.GetComponent<Rigidbody2D>().velocity = -rb.velocity; // new roid's velocity is the opposite of this one.
                clone.GetComponent<AsteroidMovement>().size = size;
            }
            else // if we were at the smallest possible size point, kill this asteroid
            {
                Destroy(this.gameObject);
            }
        }
        else // if we hit another asteroid and have remaining life left, just reduce our life.
        {
            life--;
        }
    }
}
