using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    { // log icollectible 
        Debug.Log("works");
        Icollectible collectible = collision.gameObject.GetComponent<Icollectible>();
        if (collectible != null)
        {
           
            collectible.Collect();
        }
    }
}
