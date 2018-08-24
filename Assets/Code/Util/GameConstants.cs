using UnityEngine;

public static class GameConstants
{
    public static readonly string ProjectileLayerName = "Projectile";
    public static readonly LayerMask ProjectileLayerIndex = LayerMask.NameToLayer(ProjectileLayerName);
    public static readonly int ProjectileLayer = 1 << ProjectileLayerIndex;
}
