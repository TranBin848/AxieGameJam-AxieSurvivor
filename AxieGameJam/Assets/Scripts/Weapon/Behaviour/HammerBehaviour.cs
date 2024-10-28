using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBehaviour : MeleeWeaponBehaviour
{
    Transform player;
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<PlayerStats>().transform;
        

    }

    // Update is called once per frame
    void Update()
    {
        int degree = 360;
        transform.RotateAround(player.transform.position, Vector3.up, degree * Time.deltaTime); 
       
       
        
    }
}
