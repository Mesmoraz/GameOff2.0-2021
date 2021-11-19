using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit something");

        if (other.tag == "Player")
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
    }
}
