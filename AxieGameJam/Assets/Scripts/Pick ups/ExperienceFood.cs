using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceFood : Pickup
{
    public int experienceGranted;

    
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
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        
    }
   


}
