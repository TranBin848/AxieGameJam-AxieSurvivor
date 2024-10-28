using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
   
    public Vector3 moveInput;
    public Vector3 moveDir;
    public Vector3 lastMovedVector;
    public float groundDist;
    private float distance;
    private Rigidbody rb;
    private Animator anim;
    public SpriteRenderer spriteRenderer;

    public LayerMask terrainLayer;

    public CharacterScriptableObject characterData;
    PlayerStats player;
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        lastMovedVector = new Vector3(1, 0, 1);

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
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.z = Input.GetAxis("Vertical");
        moveDir = new Vector3(moveInput.x, 0, moveInput.z);             
        moveDir.Normalize();

        if(moveDir.x != 0)
        {
            float lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector3(lastHorizontalVector, 0, 0);
        }
        if(moveDir.z != 0)
        {
            float lastVerticalVector = moveDir.z;
            lastMovedVector = new Vector3(0, 0, lastVerticalVector);
        }
        if(moveDir.x != 0 && moveDir.z != 0)
        {
            float lastHorizontalVector = moveDir.x;
            float lastVerticalVector = moveDir.z;
            lastMovedVector = new Vector3(lastHorizontalVector, 0, lastVerticalVector);


        }

        rb.velocity = moveDir * player.CurrentMoveSpeed;

        

        if (moveInput.x != 0 && moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput.x != 0 && moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if(rb.velocity.x !=0 || rb.velocity.z != 0 )
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }


    }
    

}
