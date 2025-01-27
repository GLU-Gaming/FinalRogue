using UnityEngine;

public static class CollisionLayer
{
    public static readonly string Player = "Player";
    public static readonly string Enemy = "Enemy";
    public static readonly string PlayerProjectile = "PlayerProjectile";
    public static readonly string EnemyProjectile = "EnemyProjectile";

    // Helper method to get layer number
    public static int GetLayerNumber(string layerName)
    {
        return LayerMask.NameToLayer(layerName);
    }
}