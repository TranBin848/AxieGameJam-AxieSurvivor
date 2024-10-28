using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    
    public WeaponScriptableObject weaponData;
    public float destroyAfterSeconds;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentcooldownDuration;
    protected int currentPierce;

    private void Awake()
    {
        
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentcooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
        
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col .GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);    
        }
    }

   
    
}
