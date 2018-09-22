using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("asset in game area");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<AsteroidMovement>())
        {
            Debug.Log("Collison occurs");
            Destroy(other.gameObject);
        }
    }
}
