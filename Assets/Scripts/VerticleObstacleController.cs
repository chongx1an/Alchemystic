using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticleObstacleController : MonoBehaviour
{
    public float maxHeight;
    public float minHeight;
    private bool isUp;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        isUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.transform.position.y < maxHeight && isUp)
        {
            rb2d.transform.Translate(0, 0.05f, 0);
            
        }
        else
        {
            isUp = false;
            if (rb2d.transform.position.y > minHeight && !isUp)
            {
                rb2d.transform.Translate(0, -0.05f, 0);

            }
            else
            {
                isUp = true;

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
