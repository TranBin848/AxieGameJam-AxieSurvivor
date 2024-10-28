using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
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
     
        Attack();   
        

    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
