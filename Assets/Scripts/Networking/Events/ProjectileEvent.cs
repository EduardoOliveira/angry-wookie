using UnityEngine;
using System.Collections;

public class ProjectileEvent {
    
    public delegate void Projectile(Message m);

    public static event Projectile onShootingMasterSimple;
    public static event Projectile onShootingSlaveSimple;

    public static void FireOnShootindMasterSimple(Message m)
    {
        if (onShootingMasterSimple != null)
            onShootingMasterSimple(m);
    }

    public static void FireOnShootindSlaveSimple(Message m)
    {
        if (onShootingSlaveSimple != null)
            onShootingSlaveSimple(m);
    }
}
