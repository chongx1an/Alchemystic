using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffectController : MonoBehaviour
{
    public float health = 100f;
    private Animator animator;
    private bool isDestroy;

    private float destroyTimer;
    private float destroyMaxTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        destroyTimer = 0;
        isDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isDestroy)
        {

            if (destroyTimer < destroyMaxTime)
            {
                destroyTimer += Time.deltaTime;
            }
            else
            {
                if (health > 0)
                {
                    TakeDamage(20);
                }
                else
                {
                    isDestroy = false;
                }
                destroyTimer = 0;
            }
        }
    }
    public bool TakeDamage(float dmg)
    {
       
        health -= dmg/2.0f;

        if (health <= 0)
        {
            AudioManagerController.instance.Play("IceBreaking");
            Destroy(gameObject);

            return true;
        }
        else if (health <= 20)
        {
            AudioManagerController.instance.Play("IceBreaking");
            animator.SetBool("is20", true);

        }
        else if (health <= 40)
        {
            AudioManagerController.instance.Play("IceBreaking");
            animator.SetBool("is40", true);

        }
        else if (health <= 70)
        {

            AudioManagerController.instance.Play("IceBreaking");
            animator.SetBool("is70", true);


        }
        return false;
    }

    public void Suicide()
    {
        isDestroy = true;

    }
}
