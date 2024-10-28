using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObjects enemyData;
    public AudioSource takeDamageSound;

    //current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    private Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();    
    }
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
       
    }
    public void TakeDamage(float dmg)
    {
        
        
        takeDamageSound.Play();
        
        anim.SetTrigger("TakeDamage");

        currentHealth -= dmg;

        if(currentHealth <= 0) 
        {
            Kill();
        }

        else if(currentHealth > 0)
        {
            anim.SetTrigger("Alive");
        }

        
    }
     
    
    public void Kill()
    {
        Destroy(this.gameObject);


    }

    private void OnCollisionStay(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);

            

        }
      


    }

   
    private void OnDestroy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>(); 
        es.OnEnemyKilled();
    }
    

}
