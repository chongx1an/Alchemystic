using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHeadController : MonoBehaviour
{
    private Animator animator;
    public GameObject lava;
    private Animator lavaAnimator;
    private bool isStop;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lavaAnimator = lava.GetComponent<Animator>();
        isStop = false;

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
            AudioManagerController.instance.Play("PotionBreak4");
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator StopLava()
    {
        if (!isStop)
        {
            AudioManagerController.instance.Play("LavaMechanism");
            yield return new WaitForSeconds(1f);
            lavaAnimator.SetBool("isStop", true);
            yield return new WaitForSeconds(lavaAnimator.GetCurrentAnimatorClipInfo(0).Length - 0.3f);
            lava.SetActive(false);
            isStop = true;
            animator.Play("lava_head_stop");
        }
        else
        {
            AudioManagerController.instance.Play("PotionBreak4");
        }
    }
    private IEnumerator StartLava()
    {
        if (isStop)
        {
            AudioManagerController.instance.Play("LavaMechanism");
            yield return new WaitForSeconds(0.5f);
            AudioManagerController.instance.Play("LavaHead");
            isStop = false;
            lava.SetActive(true);
            animator.Play("lava_head_flow");
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            AudioManagerController.instance.Play("PotionBreak4");
        }


    }
}
