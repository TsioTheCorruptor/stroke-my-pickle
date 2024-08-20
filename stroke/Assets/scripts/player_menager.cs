using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float damaged_timer = 2;
    bool damaged = false;
    public Animator animator;
    public Transform ballparent;
    public float jamEmptySpeed = 1.0f;
    public Transform jam;
    private float startJamYPos;
    float startY;
    float endY;

    public int maxAmmo = 5;
    private int ammoLeft = 5;
    private float jamAmount;

    public float startSize = 3.0f;
    public float sizeReduceAmount = 0.5f;
    public float sizeReduceSpeed = 1.0f;
    float startSizeY;
    float endSizeY;

    //public Transform Spoon;
    //public float spoonSpeed = 1.0f;
    //private float startSpoonRot;

    private float curJamYPos;

    public GameObject jamBall;
    public Transform jamBallSpawn;
    public float jamBallSpeed = 700f;

    // Start is called before the first frame update
    void Start()
    {
        startJamYPos = jam.localPosition.y;
        jamAmount = 0.55f / maxAmmo;

        ammoLeft = maxAmmo;

        
        StopCoroutine("SizeChange");

        startY = jam.localPosition.y;
        endY = startJamYPos;
       

        startSizeY = transform.localScale.y;
        endSizeY = startSize;
        StartCoroutine("SizeChange");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ammoLeft = maxAmmo;

            //StopCoroutine("JamAnimation");
            StopCoroutine("SizeChange");

            startY = jam.localPosition.y;
            endY = startJamYPos;
           // StartCoroutine("JamAnimation");

            startSizeY = transform.localScale.y;
            endSizeY = startSize;
            StartCoroutine("SizeChange");
        }

        //print(ammoLeft);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(damaged==false)
        {
 if(collision.tag== "tongue_damage")
        {
            ammoLeft =ammoLeft-3;
            if (ammoLeft >= 0)
            {
                    startdamagedstate();
                    Invoke("enddamagedstate", 2f);
                StopCoroutine("SizeChange");

                startY = jam.localPosition.y;
                endY = startJamYPos - (jamAmount * (maxAmmo - ammoLeft));
                // StartCoroutine("JamAnimation");

                startSizeY = transform.localScale.y;
                endSizeY = startSize - (sizeReduceAmount * (maxAmmo - ammoLeft));
                StartCoroutine("SizeChange");
            }
            else
                ammoLeft = 0;
          
        }
        }
       
    }
    void Shoot()
    {
        if (ammoLeft > 0)
        {
            
            GameObject curJamBall = Instantiate(jamBall,jamBallSpawn, false);
            // curJamBall.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(jamBallSpeed, 0));
            curJamBall.GetComponent<Rigidbody2D>().AddForce(jamBallSpawn.right * jamBallSpeed, 0);
            curJamBall.transform.SetParent(ballparent);
            Destroy(curJamBall, 5);
            --ammoLeft;

            //StopCoroutine("JamAnimation");
            StopCoroutine("SizeChange");

            startY = jam.localPosition.y;
            endY = startJamYPos - (jamAmount * (maxAmmo - ammoLeft));
           // StartCoroutine("JamAnimation");

            startSizeY = transform.localScale.y;
            endSizeY = startSize - (sizeReduceAmount * (maxAmmo - ammoLeft));
            StartCoroutine("SizeChange");
        }
    }

    IEnumerator SizeChange()
    {
        float i = 0.0f;
        Vector2 startVevtor = new Vector2(startSizeY, startSizeY);
        Vector2 endVevtor = new Vector2(endSizeY, endSizeY);
        while (i < 1.0f)
        {
            i += Time.deltaTime * sizeReduceSpeed;
            transform.localScale = Vector2.Lerp(startVevtor, endVevtor, i);
            yield return null;
        }
    }

    IEnumerator JamAnimation()
    {
        float i = 0.0f;
        Vector2 startPos = new Vector2(0.0f, startY);
        while (i < 1.0f)
        {
            i += Time.deltaTime * jamEmptySpeed;
            jam.localPosition = Vector2.Lerp(startPos, new Vector2(0.0f, endY), i);
            yield return null;
        }
    }
    void startdamagedstate()
    {
        damaged = true;
        animator.SetBool("damaged", true);
    }
    void enddamagedstate()
    {
        damaged = false;
        animator.SetBool("damaged", false);
    }
}