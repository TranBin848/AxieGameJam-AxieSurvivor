using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private GameObject enemy;
    private Rigidbody rb;
    public WeaponScriptableObject spearData;
    
    private float timer;

    protected float currentDamage;

    void Awake()
    {
        currentDamage = spearData.Damage;

    }
    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10)
        {
            Destroy(gameObject);
        }
        
        Vector3 direction = enemy.transform.position - transform.position;
        float angle = Mathf.Atan2(-direction.z, -direction.x) * Mathf.Rad2Deg;

        rb.velocity = new Vector3(direction.x, 0, direction.z).normalized * spearData.Speed;

        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
            Destroy(gameObject) ;
        }
    }


}
