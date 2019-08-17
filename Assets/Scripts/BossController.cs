using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private GameObject nearestTarget;
    public ArrayList targetList;
    private Animator animator;
    private Rigidbody2D rb2d;

    /*
     * Enemy Stats
     */
    public float health = 100f;
    public float moveSpeed = 7.5f;
    public float attackDmg = 20.0f;


    /* Enemy Chasing Direction Attributes
     * 
     */
    private bool isFacingRight;
    private bool isMoving;
    private bool isMovingRight;

    /*
     * Attack attributes
     */
    public GameObject attackPoint;
    public float areaOfAttackEffect;
    private float attackRange = 3.0f;
    private float attackCD = 4.0f;
    private float attackReadyTimer;

    /*
     * Attack Animation
     */

    private bool isAttacking;

    private float minTime;
    private float maxTime = 2.0f;

    public GameObject hurtingEffect;
    public float patrolMaxRight;
    public float patrolMaxLeft;
    private bool isReachedPatrolPoint;
    private float destX;
    // Start is called before the first frame update
    void Start()
    {
        minTime = 0.0f;

        rb2d = GetComponent<Rigidbody2D>();
        attackCD = 3.0f;
        attackReadyTimer = 0.0f;

        isFacingRight = true;

        animator = GetComponent<Animator>();
        targetList = new ArrayList();

        float restritedTransformX = Mathf.Clamp(transform.position.x, patrolMaxLeft, patrolMaxRight);

        transform.position = new Vector2(restritedTransformX, transform.position.y);
        isReachedPatrolPoint = false;
        destX = Random.Range(patrolMaxLeft, patrolMaxRight);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        transform.Translate(0, 0, 0.01f);
        transform.Translate(0, 0, -0.01f);


        if (targetList.Count > 0)
        {
            Chase();
        }
        else
        {
            nearestTarget = null;
            StartCoroutine("Patrol");
        }

    }


    private IEnumerator Patrol()
    {
        FlipEnemy();
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

        }


    }
    private void Chase()
    {
        FlipEnemy();
        nearestTarget = FindNearest(targetList);
        CheckReached();
        if (isMoving)
        {
            animator.SetBool("isWalking", true);
            if (isMovingRight)
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private GameObject FindNearest(ArrayList targetList)
    {
        Debug.Log(targetList.Count);
        GameObject nearest = (targetList[0] as GameObject);
        float nearestDistance = Mathf.Abs(nearest.transform.position.x - transform.position.x);
        float currentDistance;
        foreach (GameObject currentTarget in targetList)
        {
            currentDistance = Mathf.Abs(currentTarget.transform.position.x - transform.position.x);
            if (currentDistance < nearestDistance)
            {
                nearest = currentTarget;
                nearestDistance = Mathf.Abs(nearest.transform.position.x - transform.position.x);
            }
        }
        return nearest;
    }
    private void CheckReached()
    {

        if (nearestTarget)
        {
            if (transform.position.x < nearestTarget.transform.position.x - attackRange && !isAttacking)
            {
                animator.SetBool("isAttackingLeft", false);
                animator.SetBool("isAttackingRight", false);
                isMovingRight = true;
                isMoving = true;
                isFacingRight = true;
            }
            else if (transform.position.x > nearestTarget.transform.position.x + attackRange && !isAttacking)
            {
                animator.SetBool("isAttackingLeft", false);
                animator.SetBool("isAttackingRight", false);
                isMovingRight = false;
                isMoving = true;
                isFacingRight = false;
            }
            else
            {
                isMoving = false;
                if (attackReadyTimer > 0)
                {
                    attackReadyTimer -= Time.deltaTime;
                }
                else
                {
                    StartCoroutine("Attack");
                }

            }
        }

    }

    private void FlipEnemy()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {



    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Fire")
        {
            hurtingEffect.SetActive(true);

            if (minTime < 0)
            {
                Hurt(20);
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

        if (collision.gameObject.tag == "Fire")
        {
            hurtingEffect.SetActive(false);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, areaOfAttackEffect);
    }

    /*
    private void Attack()
    {



        if (isAttackReady)
        {
            animator.SetBool("isAttacking", true);
            if (attackDurTimer > 0)
            {
                attackDurTimer -= Time.deltaTime;
            }
            else
            {

                attackReadyTimer = attackCD;
                attackDurTimer = attackMaxDur;
                isAttackReady = false;
                animator.SetBool("isAttacking", false);
                isAttacking = false;
                Collider2D[] dmgList = Physics2D.OverlapCircleAll(attackPoint.transform.position, areaOfAttackEffect);
                DealDamage(dmgList);

            }
        }

    }*/

    private IEnumerator Attack()
    {
        isAttacking = true;

        float leftOrRight = Random.Range(0, 100);
        if(leftOrRight > 50)
        {
            animator.SetBool("isAttackingLeft", true);
        }
        else
        {
            animator.SetBool("isAttackingRight", true);
        }

        FindObjectOfType<AudioManagerController>().Play("BossAttack");
        attackReadyTimer = attackCD;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        Collider2D[] dmgList = Physics2D.OverlapCircleAll(attackPoint.transform.position, areaOfAttackEffect);
        DealDamage(dmgList);

        if (leftOrRight > 50)
        {
            animator.SetBool("isAttackingLeft", false);
        }
        else
        {
            animator.SetBool("isAttackingRight", false);
        }
        isAttacking = false;
    }
    private void DealDamage(Collider2D[] dmgList)
    {
        foreach (Collider2D target in dmgList)
        {
            if (target.gameObject.tag == "Ice")
            {
                if (target.gameObject.GetComponent<IceEffectController>().TakeDamage(attackDmg))
                {

                    targetList.Remove(target.gameObject);
                    nearestTarget = null;
                }

            }

            if (target.gameObject.tag == "Player")
            {
                target.gameObject.GetComponent<PlayerController>().TakeDamage(attackDmg);

                return;

            }
        }
    }


    private void Hurt(float damage)
    {

        health -= damage;

        if (health <= 0)
        {
            animator.Play("boss_death");
            moveSpeed = 0.0f;
            Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0)[0].clip.length + 1.5f);

        }
    }
}
