using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
     
    public static event Action HealthPickupCollected;
    [SerializeField] public PowerUpEffect healthBuff;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the Player tag
        if (collision.CompareTag("Player"))
        {
            HealthPickupCollected?.Invoke();
            healthBuff.Apply(collision.gameObject);
            Debug.Log("Health Pickup collected!");
            Destroy(gameObject);
        }

    }
}
