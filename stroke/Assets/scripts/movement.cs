using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movement : MonoBehaviour
{
    int leftorright = 0;
    public Transform gunpos;
    bool once=false;
    bool once2 = false;
    public Animator animator;
    public float vel;
   public SpriteRenderer spriteRenderer;
   public float jumppower = 225f;
   public Rigidbody2D rb;
public int MovementSpeed= 3;
    public Transform pos;
   public BoxCollider2D bcl;
    bool JumpEnabled=false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       rb=GetComponent<Rigidbody2D>();
        pos = this.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    
    {
     
       


        if (rb.velocity.y<-1)
        {
            animator.SetBool("in air", true);
        }
        else
        {
            animator.SetBool("in air", false);
            animator.SetBool("jumped", false);
        }
        {
            
        }



        if ((Input.GetKey(KeyCode.LeftArrow) == true && Input.GetKey(KeyCode.RightArrow) == true)|| (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.LeftArrow) == false))
        {
            animator.SetBool("is running", false);
        }





        if(Input.GetKey(KeyCode.RightArrow)==true&& Input.GetKey(KeyCode.LeftArrow) ==false)
        {
            leftorright = 0;
            if (Input.GetKey(KeyCode.DownArrow) == true)
            {
                gunpos.rotation = Quaternion.Euler(0,0 ,-45 );
            }
            else
            {
                gunpos.rotation = Quaternion.Euler(0, 0, 0);
            }
      
            pos.position +=Vector3.right*MovementSpeed*Time.deltaTime;
            spriteRenderer.flipX =false;
           
           
            animator.SetBool("is running", true);
        }
      





        if (Input.GetKey(KeyCode.LeftArrow) == true&& Input.GetKey(KeyCode.RightArrow) == false)
        {
            leftorright = 1;
            if(Input.GetKey(KeyCode.DownArrow)==true)
            {
                gunpos.rotation = Quaternion.Euler(0, 0,225 );
            }
            else
            {
                gunpos.rotation = Quaternion.Euler(0,0, 180);
            }
            
            pos.position -= Vector3.right * MovementSpeed * Time.deltaTime;
            spriteRenderer.flipX = true;
            
          
            animator.SetBool("is running", true);
        }
      





        if (Input.GetKey(KeyCode.Space) == true|| Input.GetKey(KeyCode.UpArrow))
        {
            jump();
        }
      
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ground" && rb.velocity.y<=0) 
        {
            JumpEnabled= true;  
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ground") 
        {
            JumpEnabled = false;
        }
    }
    void jump()
    {
        if (JumpEnabled ==true)
        {
            rb.AddForce(Vector2.up*jumppower);
            animator.SetBool("jumped", true);
        }
    }
}
 
