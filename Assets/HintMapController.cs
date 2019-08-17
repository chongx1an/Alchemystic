using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintMapController : MonoBehaviour
{
    public Image hintMap;
    public Image hintMapCover;

    private PlayerController playerController;

    private bool isShow;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        isShow = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isShow && hintMap.color.a <= 1.0f)
        {
            float opacity = hintMap.color.a + 0.01f;
            Color newColor = new Color(hintMap.color.r, hintMap.color.g, hintMap.color.b, opacity);
            hintMap.color = newColor;
        }
        else if (!isShow && hintMap.color.a >= 0.0f)
        {
            float opacity = hintMap.color.a - 0.01f;
            Color newColor = new Color(hintMap.color.r, hintMap.color.g, hintMap.color.b, opacity);
            hintMap.color = newColor;
        }

        if (isShow && hintMapCover.color.a <= 0.5f)
        {
            float opacity = hintMapCover.color.a + 0.01f;
            Color newColor = new Color(hintMapCover.color.r, hintMapCover.color.g, hintMapCover.color.b, opacity);
            hintMapCover.color = newColor;
        }
        else if (!isShow && hintMapCover.color.a >= 0.0f)
        {
            float opacity = hintMapCover.color.a - 0.01f;
            Color newColor = new Color(hintMapCover.color.r, hintMapCover.color.g, hintMapCover.color.b, opacity);
            hintMapCover.color = newColor;
        }

        transform.Translate(0, 0, 0.0001f);
        transform.Translate(0, 0, -0.0001f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
