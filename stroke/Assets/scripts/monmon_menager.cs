using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monmon_menager : MonoBehaviour
{
    public CapsuleCollider2D monbcl1;
    public CapsuleCollider2D monbcl2;
    public bool stopmovement = false;
public Animator animator;   
    public int rage_distance = 5;
    public Transform monsterpos;
    public Transform playerpos;
   public SpriteRenderer spriteRenderer;
    public Transform rightpoint;
    public Transform leftpoint;
    int rightorleft = 1;
    public float movespeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(playerpos.position,monsterpos.position)<=rage_distance)
        {
            movespeed = 8;
            animator.SetBool("chase", true);
        }
        else
        {
            animator.SetBool("chase", false);
            movespeed = 5;
        }
        if(stopmovement==false)
        {
 switch (rightorleft)
        {
            case 0:
                transform.position = new Vector3(transform.position.x + (Time.deltaTime * movespeed), transform.position.y, transform.position.z);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x - (Time.deltaTime * movespeed), transform.position.y, transform.position.z);
                break;
        }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "rightwall")
        {
            rightorleft = 1;
            spriteRenderer.flipX = false;
        }
        if (collision.tag == "leftwall")
        {
            rightorleft = 0;
            spriteRenderer.flipX = true;
        }
        if (collision.tag == "Player")
        {
            monbcl1.enabled = false;
            monbcl2.enabled= false;
            stopmovement = true;
            animator.SetBool("attack", true);
        }
    }
}

