using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public WeaponScriptableObject spearData;
    public AudioSource attackSound;
    private GameObject enemy;

    private float timer;

    void Start()
    {
        
    }


    void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        timer += Time.deltaTime;

        if (timer > spearData.CooldownDuration)
        {
            timer = 0;
            Shoot();
        }

    }
    void Shoot()
    {
        if (enemy != null)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
            attackSound.Play();
        }

    }
}
