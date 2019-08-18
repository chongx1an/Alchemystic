using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFountainController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private PlayerController playerController;
    private Rigidbody2D playerRB2D;
    public GameObject healingEffect;
    private float minTime = 0.0f;
    private float maxTime = 2.0f;
    private bool isHealing;
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRB2D = player.GetComponent<Rigidbody2D>();

        isHealing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        playerRB2D.transform.Translate(0, 0, 0.001f);
        playerRB2D.transform.Translate(0, 0, -0.001f);

        if (isHealing)
        {
            AudioManagerController.instance.Play("HealingSound");
            healingEffect.SetActive(true);
        }
        else
        {
            healingEffect.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (minTime < 0)
            {
                Heal(20);
                minTime = maxTime;
            }
            else
            {
                minTime -= Time.deltaTime;
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isHealing = false;
    }

    private void Heal(int amount)
    {
        if(playerController.health < 100)
        {
            isHealing = true;
            playerController.health += amount;
        }
        else
        {
            isHealing = false;
        }


    }
}
