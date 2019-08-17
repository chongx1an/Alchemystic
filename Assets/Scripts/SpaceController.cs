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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        potionController = player.GetComponentInChildren<PotionController>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Wall")
        {
            float angle = potionController.angle;
            float x_pos;
            if (angle >= 90)
            {
                x_pos = transform.position.x + 0.3f;
            }
            else
            {
                x_pos = transform.position.x - 0.3f;
            }

            //col.GetComponent<CompositeCollider2D>().transform.position.y + 2f * col.GetComponent<CompositeCollider2D>().size.y
            Vector2 blinkToPos = new Vector2(x_pos, transform.position.y + 0.3f);

            Vector2 blinkFromPos = player.transform.position;
            GameObject spaceStartEffect = Instantiate(spaceStartEffectPrefab, blinkFromPos, transform.rotation);
            Destroy(gameObject);


            player.transform.position = blinkToPos;
            //col.GetComponent<BoxCollider2D>().transform.position.y + 1.2f * col.GetComponent<BoxCollider2D>().size.y
            Vector2 afterEffectPos = new Vector2(player.transform.position.x, transform.position.y + 0.2f);
            FindObjectOfType<AudioManagerController>().Play("SpaceSound");
            GameObject spaceAfterEffect = Instantiate(spaceAfterEffectPrefab, afterEffectPos, player.transform.rotation);
            Destroy(spaceStartEffect, spaceStartEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            Destroy(spaceAfterEffect, spaceAfterEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
        else if (col.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }

    }
}
