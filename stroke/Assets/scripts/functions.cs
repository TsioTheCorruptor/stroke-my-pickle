using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class functions : MonoBehaviour
{
    public CapsuleCollider2D monbcl1;
    public CapsuleCollider2D monbcl2;
    public monmon_menager monmon_Menager_script;
    public Animator mon_animator;
    public Transform monpos;
    public GameObject damagebox;
    // Start is called before the first frame update
   public void createdamage()
    {
        
      GameObject temp=  Instantiate(damagebox, monpos.position, monpos.rotation);
        Destroy(temp, 0.1f);
    }
    public void stopattackanim()

    {
        monbcl2.enabled = true; 
        monbcl1.enabled=true;
 monmon_Menager_script.stopmovement = false;
        mon_animator.SetBool("attack", false);
       
    }
    }
