using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    SphereCollider playerCollector;
    public float pullSpeed;
    

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<SphereCollider>();
        
    }

    void Update()
    {
        playerCollector.radius = player.CurrentMagnet;
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
            Vector3 forceDirection = (transform.position - col.transform.position).normalized;
            rb.AddForce(forceDirection * pullSpeed);






            collectible.Collect();
        }
        
    }
}
