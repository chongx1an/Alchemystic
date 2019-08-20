using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    public GameObject spaceStartEffectPrefab;
    public GameObject spaceAfterEffectPrefab;
    private static GameObject player;
    private GameObject attackPoint;
    private PotionController potionController;
    private Vector2 previousPos;
    private Vector2 previospreviousPos;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        potionController = player.GetComponentInChildren<PotionController>();
        previousPos = transform.position;
        previospreviousPos = transform.position;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        previospreviousPos = previousPos;
        previousPos = transform.position;

    }

    private RaycastHit2D CheckRayCast(Vector2 direction)
    {
        Vector2 startingPos = new Vector2(transform.position.x, transform.position.y - 1f);

        Debug.DrawRay(startingPos, -direction, Color.green);

        return Physics2D.Raycast(startingPos, transform.up, direction.y);
    }

    
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Wall")
        {

            Vector2 blinkToPos;

            blinkToPos = previousPos; 

            Vector2 blinkFromPos = player.transform.position;
            GameObject spaceStartEffect = Instantiate(spaceStartEffectPrefab, blinkFromPos, transform.rotation);
            Destroy(gameObject);

            Vector3 translation = new Vector3(blinkToPos.x, blinkToPos.y) - player.transform.position;
 
            player.transform.Translate(translation);

            Vector2 afterEffectPos = new Vector2(player.transform.position.x, transform.position.y + 0.2f);
            AudioManagerController.instance.Play("SpaceSound");
            GameObject spaceAfterEffect = Instantiate(spaceAfterEffectPrefab, afterEffectPos, player.transform.rotation);
            Destroy(spaceStartEffect, spaceStartEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            Destroy(spaceAfterEffect, spaceAfterEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            
        }
        else if (col.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }

    }
    
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {

            Vector2 blinkToPos;

            blinkToPos = previousPos;

            Vector2 blinkFromPos = player.transform.position;
            GameObject spaceStartEffect = Instantiate(spaceStartEffectPrefab, blinkFromPos, transform.rotation);
            Destroy(gameObject);

            Vector3 translation = new Vector3(blinkToPos.x, blinkToPos.y) - player.transform.position;

            player.transform.Translate(translation);

            Vector2 afterEffectPos = new Vector2(player.transform.position.x, transform.position.y + 0.2f);
            AudioManagerController.instance.Play("SpaceSound");
            GameObject spaceAfterEffect = Instantiate(spaceAfterEffectPrefab, afterEffectPos, player.transform.rotation);
            Destroy(spaceStartEffect, spaceStartEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            Destroy(spaceAfterEffect, spaceAfterEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        }
        else if (collision.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
    */
    
}
