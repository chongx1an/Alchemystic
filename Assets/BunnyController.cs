using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    public float moveSpeed;
    private float destX;
    private bool isReachedPatrolPoint;
    private Animator animator;
    private bool isFacingRight;
    public float patrolMaxRight;
    public float patrolMaxLeft;

    private bool isPatroling;

    // Start is called before the first frame update
    void Start()
    {
        isReachedPatrolPoint = true;
        isFacingRight = true;
        isPatroling = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isPatroling)
        {

            StartCoroutine("Patrol");
        }
        else
        {
            isPatroling = false;
        }

    }
    private IEnumerator Patrol()
    {
        FlipBunny();
        if (!isReachedPatrolPoint)
        {

            if (Mathf.Round(destX) > Mathf.Round(transform.position.x))
            {

                transform.Translate(Vector3.right * moveSpeed / 5.0f * Time.deltaTime);
                animator.SetBool("isWalking", true);
                isFacingRight = true;
            }
            else if (Mathf.Round(destX) < Mathf.Round(transform.position.x))
            {
                transform.Translate(Vector3.left * moveSpeed / 5.0f * Time.deltaTime);
                animator.SetBool("isWalking", true);
                isFacingRight = false;
            }
            else
            {
                isReachedPatrolPoint = true;

            }
        }
        else
        {

            destX = Random.Range(patrolMaxLeft, patrolMaxRight);
            animator.SetBool("isWalking", false);
            yield return new WaitForSeconds(Random.Range(4, 8));
            isReachedPatrolPoint = false;
            isPatroling = true;



        }


    }

    private void FlipBunny()
    {
        if (isFacingRight)
        {
            Vector3 scale = transform.localScale;

            scale.x = Mathf.Abs(scale.x);

            transform.localScale = scale;

        }
        else
        {
            Vector3 scale = transform.localScale;

            scale.x = -Mathf.Abs(scale.x);

            transform.localScale = scale;
        }
    }
}
