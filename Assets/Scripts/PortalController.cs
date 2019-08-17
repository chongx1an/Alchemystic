using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    private Animator animator;
    public GameObject portalTriggeredArea;
    private bool isSealed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isSealed = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Space Potion" && !isSealed)
        {
            animator.Play("space_portal_stop");
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManagerController>().Play("PortalSealed");
            portalTriggeredArea.SetActive(false);
            isSealed = true;
        }
        
    }
}
