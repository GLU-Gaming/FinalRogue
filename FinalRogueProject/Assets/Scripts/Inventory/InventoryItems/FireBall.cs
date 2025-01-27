using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour, Icollectible
{
    public static event FireballCollected OnFireBall;
    public delegate void FireballCollected(ItemData itemdata);

    public ItemData fireballData;
    public void Collect()
    {
        OnFireBall?.Invoke(fireballData);
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
