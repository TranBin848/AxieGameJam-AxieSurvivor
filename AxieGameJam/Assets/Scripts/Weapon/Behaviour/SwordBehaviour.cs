using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : ProjectileWeaponBehaviours
{
    
    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime;
        
    }
}
