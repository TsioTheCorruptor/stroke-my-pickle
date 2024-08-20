using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class functions : MonoBehaviour
{
    public Transform monster;
    public CapsuleCollider2D monbcl1;
    public CapsuleCollider2D monbcl2;
    public monmon_menager monmon_Menager_script;
    public Animator mon_animator;
    public Transform monpos;
    public GameObject damagebox;
    public GameObject smalldamagebox;
    // Start is called before the first frame update
    public void createdamage()
    {
        
      GameObject temp=  Instantiate(damagebox, monpos.position, monpos.rotation);
        Destroy(temp, 0.05f);
    }
    public void stopattackanim()

    {
        monbcl2.enabled = true; 
        monbcl1.enabled=true;
 monmon_Menager_script.stopmovement = false;
        mon_animator.SetBool("attack", false);
       
    }
    public void createdamageJUMP()
    {

        GameObject temp = Instantiate(smalldamagebox, monpos.position, monpos.rotation);
      //  temp.GetComponent<BoxCollider2D>().isTrigger = true;
        temp.transform.SetParent(monster.transform);
        Destroy(temp, 1.3f);
    }
    public void stopdamageJUMP()
    {
        monbcl2.enabled = true;
        monbcl1.enabled = true;
        mon_animator.SetBool("jump", false);
        monmon_Menager_script.disablelickattack = false;
    }
}
