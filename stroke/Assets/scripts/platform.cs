using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    BoxCollider2D bcl;
   public Transform playerpos;
    Transform maxpos;
    // Start is called before the first frame update
    void Start()
    {
        bcl= GetComponent<BoxCollider2D>();
        maxpos= GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerpos.position.y <maxpos.position.y)
        {
            bcl.enabled = false;
        }
        else
            bcl.enabled = true;
    }
}
