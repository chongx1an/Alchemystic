using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggeredAreaController : MonoBehaviour
{
    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ice")
        {

            enemyController.targetList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ice")
        {
            enemyController.targetList.Remove(collision.gameObject);
        }
    }
}
