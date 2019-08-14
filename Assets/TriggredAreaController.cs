using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggredAreaController : MonoBehaviour
{
    private BossController bossController;

    // Start is called before the first frame update
    void Start()
    {
        bossController = GetComponentInParent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ice")
        {
            bossController.targetList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ice")
        {
            bossController.targetList.Remove(collision.gameObject);
        }
    }
}
