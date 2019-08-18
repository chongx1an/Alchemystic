using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    private Animator animator;
    public GameObject portalTriggeredArea;
    public bool isSealed;
    public bool isEnterAfterSealedEra;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isSealed = false;
        isEnterAfterSealedEra = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isSealed && FindObjectsOfType<BossController>().Length == 0 && isEnterAfterSealedEra)
        {

            AudioManagerController.instance.StartCoroutine("StartAfterSealedBackgroundMusic");
            isEnterAfterSealedEra = false;
            FindObjectOfType<PortalTriggeredAreaController>().StartCoroutine("LighterBackground");
            EnemyController[] enemyList = FindObjectsOfType<EnemyController>();

            foreach(EnemyController enemy in enemyList)
            {
                enemy.Hurt(100);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Space Potion" && !isSealed)
        {
            animator.Play("space_portal_stop");
            Destroy(collision.gameObject);
            AudioManagerController.instance.Play("PortalSealed");
            
            isSealed = true;
            isEnterAfterSealedEra = true;
        }
        
    }
}
