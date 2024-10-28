using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : WeaponController
{
    
    public AudioSource attackSound;
    protected override void Start()
    {
        base.Start();
        
    }

    
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedHammer = Instantiate(weaponData.prefab);
        
        
        spawnedHammer.transform.localPosition = transform.position + new Vector3(2f, 0, 2f);
        spawnedHammer.transform.parent = transform;
        attackSound.Play();



    }
}
