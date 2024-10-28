using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ProjectileWeaponBehaviours : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;

    //current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentcooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentcooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
        
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float dirz = direction.z;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;
        if (dirx == 0 && dirz < 0) //down
        {
            rotation.y = 90f;
        }
        else if (dirx < 0 && dirz == 0) //left 
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        
        else if (dirx == 0 && dirz > 0) //up
        {
            rotation.y = -90f;
        }
        else if(dirx > 0 && dirz > 0) //rightup
        {
            rotation.y = -45f;
        }
        else if(dirx > 0 && dirz < 0) //rightdown
        {
            rotation.y = 45f;
        }
        else if(dirx < 0 && dirz > 0) //leftup
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.y = 45f;

        }
        else if(dirx < 0 && dirz < 0) //leftdown
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.y = -45f;

        }
        transform.localScale = scale;   
        transform.rotation = Quaternion.Euler(rotation);

    }
    protected void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
            ReducePierce();
        }
        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }

    }
    void ReducePierce()
    {
        currentPierce--;
        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
