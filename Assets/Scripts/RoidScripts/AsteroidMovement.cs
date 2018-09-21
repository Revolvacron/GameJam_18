using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public GameObject manager;
    public Rigidbody2D rb;
    public int size = 4;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 direction;
        if(size == 4)
        {
            direction = new Vector2(-transform.position.x * Random.Range(.01f, .02f), -transform.position.y * Random.Range(1.5f, 3f));
        } else
        {
            direction = Random.insideUnitCircle * 200;
        }

        rb.AddForce(direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.magnitude > 2000 || this.transform.position.magnitude < -2000)
        {
            Destroy(this.gameObject);
        }
        this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (size > 0)
        {
            size--;
            this.transform.localScale *= .65f;
            rb.velocity = Random.insideUnitCircle * 200;

            GameObject clone;
            clone = Instantiate(this.gameObject, this.transform.position + (Random.insideUnitSphere * 200), Random.rotation);
            clone.transform.localScale = this.transform.localScale;
            clone.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * 200;
            clone.GetComponent<AsteroidMovement>().size = size;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
