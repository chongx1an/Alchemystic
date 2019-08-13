using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffectController : MonoBehaviour
{
    private float health = 100f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TakeDamage(float dmg)
    {
        health -= dmg/2.0f;

        if (health <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        else if (health <= 20)
        {
            animator.SetBool("is20", true);
        }
        else if (health <= 40)
        {
            animator.SetBool("is40", true);
        }
        else if (health <= 70)
        {

            animator.SetBool("is70", true);

        }
        return false;
    }
}
