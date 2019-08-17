using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHintController : MonoBehaviour
{
    public GameObject hintWord;
    private SpriteRenderer hintRenderer;
    private PlayerController playerController;

    private bool isShow;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        hintRenderer = hintWord.GetComponent<SpriteRenderer>();
        isShow = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isShow && hintRenderer.color.a <= 1.0f)
        {
            float opacity = hintRenderer.color.a + 0.01f;
            Color newColor = new Color(hintRenderer.color.r, hintRenderer.color.g, hintRenderer.color.b, opacity);
            hintRenderer.color = newColor;
        }
        else if(!isShow && hintRenderer.color.a >= 0.0f)
        {
            float opacity = hintRenderer.color.a - 0.01f;
            Color newColor = new Color(hintRenderer.color.r, hintRenderer.color.g, hintRenderer.color.b, opacity);
            hintRenderer.color = newColor;
        }

        transform.Translate(0, 0, 0.0001f);
        transform.Translate(0, 0, -0.0001f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && playerController.potionMode == PlayerController.PotionMode.Fire)
        {
            isShow = true;
        }
        else
        {
            isShow = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isShow = false;
        }
    }


}
