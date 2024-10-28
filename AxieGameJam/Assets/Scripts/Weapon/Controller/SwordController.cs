    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordController : WeaponController
{
    public AudioSource attackSound;
    
    protected override void Start()
    {
        base.Start();
    }

    
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedSwords = Instantiate(weaponData.prefab);
        spawnedSwords.transform.position = transform.position;
        spawnedSwords.GetComponent<SwordBehaviour>().DirectionChecker(pc.lastMovedVector);
        attackSound.Play();

    }
}
