using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSpriteChange : MonoBehaviour
{
    private SpriteMask mask;
    private SpriteRenderer spriteRend;

    private Sprite maskSprite, rendSprite;

    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponent<SpriteMask>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        maskSprite = mask.sprite;
        rendSprite = spriteRend.sprite;

        if (maskSprite != rendSprite)
        {
            mask.sprite = spriteRend.sprite;
        }
    }
}
