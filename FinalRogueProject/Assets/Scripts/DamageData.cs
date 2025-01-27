using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Data", menuName = "Damage System/Damage Data")]
public class DamageData : ScriptableObject
{
    public int damageAmount = 10; // Damage amount
    public bool destroyOnHit = false; // Should the object be destroyed after hitting?
    public string damageType = "Physical"; // Damage type (e.g., Physical, Magical)
}
        