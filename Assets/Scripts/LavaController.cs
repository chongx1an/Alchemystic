using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(100f);
        }
        else if (collision.gameObject.tag == "Ice Potion" || collision.gameObject.tag == "Space Potion")
        {
            AudioManagerController.instance.Play("PotionBreak4");
            Destroy(collision.gameObject);
        }
    }

}
