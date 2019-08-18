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
 
            Debug.Log(translation);
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


    private void RaycastCheckUpdateDown()
    {
        float distance = 0.5f;
        float offsetDown = 0.2f;

        Vector2 startingPosDown = new Vector2(transform.position.x, transform.position.y - offsetDown);
        Vector2 directionDown = new Vector2(0, -distance);
        RaycastHit2D hitDown = Physics2D.Raycast(startingPosDown, transform.up, directionDown.y);
        Debug.DrawRay(startingPosDown, -directionDown, Color.green);

        if (hitDown.collider.tag == "Ground")
        {
            Debug.Log("Hit down : " + hitDown.collider.name);
            Vector2 blinkToPos = new Vector2(transform.position.x, transform.position.y + 0.3f);

            Vector2 blinkFromPos = player.transform.position;
            GameObject spaceStartEffect = Instantiate(spaceStartEffectPrefab, blinkFromPos, transform.rotation);
            Destroy(gameObject);


            player.transform.position = blinkToPos;
            //col.GetComponent<BoxCollider2D>().transform.position.y + 1.2f * col.GetComponent<BoxCollider2D>().size.y
            Vector2 afterEffectPos = new Vector2(player.transform.position.x, transform.position.y + 0.2f);
            AudioManagerController.instance.Play("SpaceSound");
            GameObject spaceAfterEffect = Instantiate(spaceAfterEffectPrefab, afterEffectPos, player.transform.rotation);
            Destroy(spaceStartEffect, spaceStartEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            Destroy(spaceAfterEffect, spaceAfterEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }


        float offsetRight = 0.1f;
        Vector2 startingPosRight = new Vector2(transform.position.x + offsetRight, transform.position.y);
        Vector2 directionRight = new Vector2(distance, 0);
        RaycastHit2D hitRight = Physics2D.Raycast(startingPosRight, transform.right, -directionRight.x);
        Debug.DrawRay(startingPosRight, directionRight, Color.green);

        if (hitRight.collider.tag == "Ground" || hitRight.collider.tag == "Wall")
            Debug.Log("Hit right : " + hitRight.collider.name);


        float offsetLeft = 0.2f;
        Vector2 startingPosLeft = new Vector2(transform.position.x - offsetLeft, transform.position.y);
        Vector2 directionLeft = new Vector2(-distance, 0);
        RaycastHit2D hitLeft = Physics2D.Raycast(startingPosLeft, transform.right, directionLeft.x);
        Debug.DrawRay(startingPosLeft, -directionLeft, Color.green);

        if (hitLeft.collider.tag == "Ground" || hitLeft.collider.tag == "Wall")
            Debug.Log("Hit left : " + hitLeft.collider.name);
        /*
        Vector2 directionRight = new Vector2(offset, 0);
        RaycastHit2D hitRight = Physics2D.Raycast(startingPos, transform.right, directionRight.x);
        Debug.DrawRay(startingPos, -directionRight, Color.green);

        if (hitRight.collider.tag == "Ground")
            Debug.Log("Hit right : " + hitDown.collider.name);
            */
    }

    private void RaycastCheckUpdateUp()
    {
        float distance = 0.5f;

        float offsetUp = 0.14f;
        Vector2 startingPosUp = new Vector2(transform.position.x, transform.position.y + offsetUp);
        Vector2 directionUp = new Vector2(0, distance);
        RaycastHit2D hitUp = Physics2D.Raycast(startingPosUp, transform.up, -directionUp.y);
        Debug.DrawRay(startingPosUp, directionUp, Color.green);

        if (hitUp.collider.tag == "Ground")
        {
            Debug.Log("Hit up : " + hitUp.collider.name);
            Vector2 blinkToPos = new Vector2(hitUp.transform.position.x, hitUp.transform.position.y - 5f);
            Vector2 blinkFromPos = player.transform.position;
            Destroy(gameObject);
            GameObject spaceStartEffect = Instantiate(spaceStartEffectPrefab, blinkFromPos, transform.rotation);



            player.transform.position = blinkToPos;
            //col.GetComponent<BoxCollider2D>().transform.position.y + 1.2f * col.GetComponent<BoxCollider2D>().size.y
            Vector2 afterEffectPos = new Vector2(player.transform.position.x, transform.position.y + 0.2f);
            AudioManagerController.instance.Play("SpaceSound");
            GameObject spaceAfterEffect = Instantiate(spaceAfterEffectPrefab, afterEffectPos, player.transform.rotation);
            Destroy(spaceStartEffect, spaceStartEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            Destroy(spaceAfterEffect, spaceAfterEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }

    }

    private void RaycastCheckUpdateRight()
    {
        float distance = 0.5f;
        float offsetRight = 0.1f;
        Vector2 startingPosRight = new Vector2(transform.position.x + offsetRight, transform.position.y);
        Vector2 directionRight = new Vector2(distance, 0);
        RaycastHit2D hitRight = Physics2D.Raycast(startingPosRight, transform.right, -directionRight.x);
        Debug.DrawRay(startingPosRight, directionRight, Color.green);

        if (hitRight.collider.tag == "Ground" || hitRight.collider.tag == "Wall")
        {
            
            Debug.Log("Hit right : " + hitRight.collider.name);
            Vector2 blinkToPos = new Vector2(transform.position.x, transform.position.y - 2f);
            Vector2 blinkFromPos = player.transform.position;
            Destroy(gameObject);
            GameObject spaceStartEffect = Instantiate(spaceStartEffectPrefab, blinkFromPos, transform.rotation);



            player.transform.position = blinkToPos;
            //col.GetComponent<BoxCollider2D>().transform.position.y + 1.2f * col.GetComponent<BoxCollider2D>().size.y
            Vector2 afterEffectPos = new Vector2(player.transform.position.x, transform.position.y + 0.2f);
            AudioManagerController.instance.Play("SpaceSound");
            GameObject spaceAfterEffect = Instantiate(spaceAfterEffectPrefab, afterEffectPos, player.transform.rotation);
            Destroy(spaceStartEffect, spaceStartEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            Destroy(spaceAfterEffect, spaceAfterEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
            

    }
}
