using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject nearestTarget;
    public ArrayList targetList;
    private Animator animator;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

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
    private float attackRange = 2.0f;
    private float attackCD = 2.0f;
    private float attackReadyTimer;

    /*
     * Attack Animation
     */

    private bool isAttacking;

    private float minTime;
    private float maxTime = 1.0f;

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
        attackCD = 2.0f;
        attackReadyTimer = 0.0f;

        isFacingRight = true;

        animator = GetComponent<Animator>();
        targetList = new ArrayList();

        float restritedTransformX = Mathf.Clamp(transform.position.x, patrolMaxLeft, patrolMaxRight);
        
        transform.position = new Vector2(restritedTransformX, transform.position.y);
        isReachedPatrolPoint = false;
        destX = Random.Range(patrolMaxLeft, patrolMaxRight);

        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = new Color(1, 0, 0, spriteRenderer.color.a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(0, 0, 0.01f);
        transform.Translate(0, 0, -0.01f);

        if (isAttacking && PlayerController.instance.isAlive)
        {
            if (isFacingRight)
            {
                transform.Translate(0.02f, 0, 0);
                transform.Translate(0.03f, 0.03f, 0);
            }
            else
            {
                transform.Translate(-0.02f, 0, 0);
                transform.Translate(-0.03f, 0.03f, 0);
            }
            
        
        }

        if (targetList.Count > 0 && PlayerController.instance.isAlive)
        {
            Chase();
        }
        else
        {
            nearestTarget = null;
            StartCoroutine("Patrol");
        }

        if (spriteRenderer.color.g < 1.0f)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g + 0.05f, spriteRenderer.color.b + 0.05f);
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
                animator.SetBool("isAttacking", false);
                isMovingRight = true;
                isMoving = true;
                isFacingRight = true;
            }
            else if (transform.position.x > nearestTarget.transform.position.x + attackRange && !isAttacking)
            {
                animator.SetBool("isAttacking", false);
                isMovingRight = false;
                isMoving = true;
                isFacingRight = false;
            }
            else
            {
                if (attackReadyTimer > 0)
                {
                    attackReadyTimer -= Time.deltaTime;
                }
                else
                {

                    StartCoroutine("Attack");

                }

                isMoving = false;
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

        animator.SetBool("isAttacking", true);
        
        isAttacking = true;

        attackReadyTimer = attackCD;
        AudioManagerController.instance.Play("EnemyAttack");
        yield return new WaitForSeconds(0.4f);

        Collider2D[] dmgList = Physics2D.OverlapCircleAll(attackPoint.transform.position, areaOfAttackEffect);
        DealDamage(dmgList);

        
        animator.SetBool("isAttacking", false);
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


    public void Hurt(float damage)
    {

        health -= damage;

        if (health <= 0)
        {
            animator.Play("enemy_death");
            moveSpeed = 0.0f;
            Destroy(gameObject, 1f);

        }
    }

}
