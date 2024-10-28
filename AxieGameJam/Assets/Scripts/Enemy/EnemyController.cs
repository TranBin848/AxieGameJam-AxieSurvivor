using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyScriptableObjects enemyData;
    EnemyStats enemy;
    Transform player;
    public Transform left, right;
    private float distance, distanceleft, distanceright;
    
    private Animator anim;  
    private Rigidbody rb;
    public SpriteRenderer spriteRenderer;

    public LayerMask terrainLayer;
    

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + enemyData.groundDist;
                transform.position = movePos;
            }
        }

        


    
        distance = Vector3.Distance(player.transform.position, transform.position);
        distanceleft = Vector3.Distance(player.transform.position, left.position);
        distanceright = Vector3.Distance(player.transform.position, right.position);

        if (distanceleft < distanceright )
        {
            spriteRenderer.flipX = true;

        }
        else
        {
            spriteRenderer.flipX = false;

        }


        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);
        if (distance < 20)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }



    }
}
