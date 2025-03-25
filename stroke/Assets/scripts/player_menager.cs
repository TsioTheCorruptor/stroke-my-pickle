using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public Animator death_animator;
    public float damaged_timer = 2;
    bool damaged = false;
    public Animator animator;
    public Transform ballparent;

    public TextMeshProUGUI ammoText;

    public float jamEmptySpeed = 1.0f;
    public float jamRefillSpeed = 1.0f;
    public Transform jam;
    public float maxJamYPos;
    public float minJamYPos;
    float startY;
    float endY;

    public int ammoFromOrb = 1;
    public int maxAmmo = 5;
    private int ammoLeft = 5;
    private float jamAmount;
    private float sizeAmount;

    public float jamManMaxSize = 3.0f;
    public float jamManMinSize = 0.5f;
    public float sizeReduceSpeed = 1.0f;
    public float sizeEnlargeSpeed = 1.0f;
    float startSizeY;
    float endSizeY;

    public GameObject jamBall;
    public Transform jamBallSpawn;
    public float jamBallSpeed = 700f;

    public Image toastJam;
    public GameObject orb;

    // Start is called before the first frame update
    void Start()
    {
        ammoLeft = maxAmmo;
        ammoText.text = ammoLeft + " / " + maxAmmo;
        toastJam.fillAmount = (float)ammoLeft / (float)maxAmmo;

        jamAmount = (Mathf.Abs(maxJamYPos - minJamYPos)) / maxAmmo;
        sizeAmount = (Mathf.Abs(jamManMaxSize - jamManMinSize)) / maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(ammoLeft<=0)
        {
            death_animator.SetBool("dead", true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ammoLeft = maxAmmo;

        //    ammoText.text = ammoLeft + " / " + maxAmmo;
        //    toastJam.fillAmount = (float)ammoLeft / (float)maxAmmo;

        //    StopCoroutine("JamAnimation");
        //    StopCoroutine("SizeChange");

        //    startY = jam.localPosition.y;
        //    endY = maxJamYPos;
        //    StartCoroutine("JamAnimation");

        //    startSizeY = transform.localScale.y;
        //    endSizeY = jamManMaxSize;
        //    StartCoroutine("SizeChange");
        //}

        //print(ammoLeft);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Orb"))
        {
            if (ammoLeft < maxAmmo)
            {
                orb.SetActive(false);
                Invoke("setjamreload", 5);

                ammoLeft = Mathf.Min(ammoLeft + ammoFromOrb, maxAmmo);

                ammoText.text = ammoLeft + " / " + maxAmmo;
                toastJam.fillAmount = (float)ammoLeft / (float)maxAmmo;

                StopCoroutine("JamAnimation");
                StopCoroutine("SizeChange");

                startY = jam.localPosition.y;
                endY = maxJamYPos - (jamAmount * (maxAmmo - ammoLeft));
                StartCoroutine("JamAnimation");

                startSizeY = transform.localScale.y;
                endSizeY = jamManMaxSize - (sizeAmount * (maxAmmo - ammoLeft));
                StartCoroutine("SizeChange");

                //collision.gameObject.SetActive(false);
            }
        }
        if (damaged == false)
        {
            if (collision.tag == "tongue_damage")
            {
                ammoLeft = Mathf.Max(ammoLeft - 3, 0);

                startdamagedstate();
                Invoke("enddamagedstate", 2f);


                ammoText.text = ammoLeft + " / " + maxAmmo;
                toastJam.fillAmount = (float)ammoLeft / (float)maxAmmo;

                StopCoroutine("JamAnimation");
                StopCoroutine("SizeChange");

                startY = jam.localPosition.y;
                endY = maxJamYPos - (jamAmount * (maxAmmo - ammoLeft));
                StartCoroutine("JamAnimation");

                startSizeY = transform.localScale.y;
                endSizeY = jamManMaxSize - (sizeAmount * (maxAmmo - ammoLeft));
                StartCoroutine("SizeChange");
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
            ammoText.text = ammoLeft + " / " + maxAmmo;
            toastJam.fillAmount = (float)ammoLeft / (float)maxAmmo;

            StopCoroutine("JamAnimation");
            StopCoroutine("SizeChange");

            startY = jam.localPosition.y;
            endY = maxJamYPos - (jamAmount * (maxAmmo - ammoLeft));
            StartCoroutine("JamAnimation");

            startSizeY = transform.localScale.y;
            endSizeY = jamManMaxSize - (sizeAmount * (maxAmmo - ammoLeft));
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
    void setjamreload()
    {
        orb.SetActive(true);
    }
}