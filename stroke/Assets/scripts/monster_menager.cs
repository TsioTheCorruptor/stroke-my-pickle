using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_menager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Transform rightpoint;
    public Transform leftpoint;
    int rightorleft = 0;
    public float movespeed=5;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(rightorleft)
        {
            case 0:
transform.position =new Vector3(transform.position.x+(Time.deltaTime*movespeed),transform.position.y,transform.position.z);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x - (Time.deltaTime * movespeed), transform.position.y, transform.position.z);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="rightwall")
        {
            rightorleft = 1;
        }
        if (collision.tag == "leftwall")
        {
            rightorleft = 0;
        }
    }
}
