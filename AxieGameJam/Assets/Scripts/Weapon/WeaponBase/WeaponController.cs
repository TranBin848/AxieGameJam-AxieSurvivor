using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public WeaponScriptableObject weaponData;
    
    float currentCooldown;
    public int pierce;
    protected PlayerController pc;

    protected virtual void Start()
    {
        pc = FindAnyObjectByType<PlayerController>();
        currentCooldown = weaponData.CooldownDuration;
        
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0)
        {
            Attack();
        }
            
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration; 
    }
}
