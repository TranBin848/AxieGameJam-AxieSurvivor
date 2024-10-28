using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickup
{
    public int healthRestore;

    public override void Collect()
    {
        if (hasBeenCollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthRestore);
        
    }
}
