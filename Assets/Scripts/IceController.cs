using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceController : MonoBehaviour
{
    public GameObject iceEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Ground")
        {
            GameObject[] iceWalls = GameObject.FindGameObjectsWithTag("Ice");

            if (iceWalls.Length > 4)
            {
                iceWalls[0].GetComponent<IceEffectController>().Suicide();
            }

            AudioManagerController.instance.Play("PotionBreak4");
            //col.GetComponent<BoxCollider2D>().transform.position.y + 2.75f * col.GetComponent<BoxCollider2D>().size.y
            Vector2 pos = new Vector2(transform.position.x, transform.position.y + 1.0f);
            GameObject ice = Instantiate(iceEffectPrefab, pos, transform.rotation);
            Destroy(gameObject);
            //Destroy(ice, 5);
        }
        else if (col.gameObject.tag == "Wall")
        {
            AudioManagerController.instance.Play("PotionBreak4");
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Boundary")
        {
            AudioManagerController.instance.Play("PotionBreak4");
            Destroy(gameObject);
        }
    }

    
}
