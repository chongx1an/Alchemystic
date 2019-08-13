using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHeadController : MonoBehaviour
{
    private Animator animator;
    public GameObject lava;
    private Animator lavaAnimator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lavaAnimator = lava.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ice Potion")
        {
            
            Destroy(collision.gameObject);
            lavaAnimator.SetBool("isStop", true);
            StartCoroutine("StopLava");

        }
        else if (collision.gameObject.tag == "Fire Potion")
        {
            Destroy(collision.gameObject);
            lavaAnimator.SetBool("isStop", false);
            StartCoroutine("StartLava");

        }
        else if (collision.gameObject.tag == "Space Potion")
        {
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator StopLava()
    {
        yield return new WaitForSeconds(lavaAnimator.GetCurrentAnimatorClipInfo(0).Length-0.3f);
        lava.SetActive(false);
        animator.Play("lava_head_stop");
    }
    private IEnumerator StartLava()
    {
       
        lava.SetActive(true);
        animator.Play("lava_head_flow");
        yield return new WaitForSeconds(0.3f);
    }
}
