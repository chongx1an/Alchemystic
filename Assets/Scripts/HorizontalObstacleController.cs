using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacleController : MonoBehaviour
{
    public float maxRight;
    public float maxLeft;
    private bool isRight;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        isRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.transform.position.x < maxRight && isRight)
        {
            rb2d.transform.Translate(0.05f, 0, 0);

        }
        else
        {

            isRight = false;
            if (rb2d.transform.position.x > maxLeft && !isRight)
            {
                rb2d.transform.Translate(-0.05f, 0, 0);

            }
            else
            {
                isRight = true;

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
