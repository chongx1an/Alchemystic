using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     * Player Stats
     */
    public float health = 100.0f;
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    /*
     * Player Mode
     */
    public enum PotionMode
    {
        Fire,
        Ice,
        Space
    }
    public enum AimTo
    {
        Left,
        Right
    }
    private enum FaceTo
    {
        Left,
        Right
    }

    public PotionMode potionMode;
    public bool isAiming;
    public AimTo aimTo;
    private FaceTo faceTo;


    private bool isGrounded = true;
    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManagerController>().StartMainSceneBackgroundMusic();

        potionMode = PotionMode.Fire;
        aimTo = AimTo.Right;
        faceTo = FaceTo.Right;

        health = Mathf.Clamp(health, 0, health);

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
    }

    // Update is called once per frame
    void Update()
    {
        
        SwitchPotion();
        Jump();
    }

    private void FixedUpdate()
    {
        Move(MovementInput());

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckEnterGround(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckEnterGround(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        CheckExitGround(collision);
    }

    private float MovementInput() //Get input for player horizontal movement
    {
        return Input.GetAxis("Horizontal");
    }
    private void Move (float moveHorizontal) //Control the movement of player
    {
        FlipPlayer(); //Detect and flip player based on condition
        if (isAiming)
        {
            if (aimTo == AimTo.Right)
            {

                if (moveHorizontal > 0)
                {
                    transform.Translate(Vector3.right * moveHorizontal * moveSpeed * Time.deltaTime);
                    //rb2d.MovePosition(Vector2.right * moveHorizontal * moveSpeed * Time.deltaTime);
                    animator.SetBool("isBackward", false);
                    animator.SetBool("isForward", true);

                    faceTo = FaceTo.Right;
                }
                else if (moveHorizontal < 0)
                {
                    transform.Translate(Vector3.right * moveHorizontal * moveSpeed * Time.deltaTime);
                    animator.SetBool("isForward", false);
                    animator.SetBool("isBackward", true);

                    faceTo = FaceTo.Right;
                }
                else
                {
                    transform.Translate(Vector3.right * moveHorizontal * moveSpeed * Time.deltaTime);
                    animator.SetBool("isForward", false);
                    animator.SetBool("isBackward", false);

                    faceTo = FaceTo.Right;
                }
            }
            else
            {
                if (moveHorizontal > 0)
                {
                    transform.Translate(Vector3.right * moveHorizontal * moveSpeed * Time.deltaTime);
                    animator.SetBool("isForward", false);
                    animator.SetBool("isBackward", true);

                    faceTo = FaceTo.Left;
                }
                else if (moveHorizontal < 0)
                {
                    transform.Translate(Vector3.right * moveHorizontal * moveSpeed * Time.deltaTime);
                    animator.SetBool("isBackward", false);
                    animator.SetBool("isForward", true);

                    faceTo = FaceTo.Left;
                }
                else
                {
                    animator.SetBool("isForward", false);
                    animator.SetBool("isBackward", false);

                    faceTo = FaceTo.Left;
                }
            }
        }
        else
        {
            if (moveHorizontal > 0)
            {
                //rb2d.MovePosition(rb2d.position + new Vector2(moveHorizontal, 0) * moveSpeed * Time.fixedDeltaTime);
                transform.Translate(Vector3.right * moveHorizontal * moveSpeed * Time.deltaTime);
                animator.SetBool("isBackward", false);
                animator.SetBool("isForward", true);

                faceTo = FaceTo.Right;
            }
            else if (moveHorizontal < 0)
            {
                transform.Translate(Vector3.right * moveHorizontal * moveSpeed * Time.deltaTime);
                animator.SetBool("isBackward", false);
                animator.SetBool("isForward", true);

                faceTo = FaceTo.Left;
            }
            else
            {
                animator.SetBool("isForward", false);
                animator.SetBool("isBackward", false);

            }
        }
    }    
    private void FlipPlayer() //Flip player to face the right direction
    {
        if(faceTo == FaceTo.Right)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = - Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
    private void SwitchPotion() //Detect input and switch Potion Mode
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetBool("isFire", true);
            animator.SetBool("isIce", false);
            animator.SetBool("isSpace", false);
            potionMode = PotionMode.Fire;

        }

        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetBool("isIce", true);
            animator.SetBool("isFire", false);
            animator.SetBool("isSpace", false);
            potionMode = PotionMode.Ice;

        }

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetBool("isSpace", true);
            animator.SetBool("isFire", false);
            animator.SetBool("isIce", false);
            potionMode = PotionMode.Space;

        }
    }
    private void Jump() //Detect input and let player jump
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = new Vector3(0, jumpForce, 0);
        }

        if (Mathf.Round(rb2d.velocity.y) > 0)
        {
            animator.SetBool("isJumping", true);
        }
        else if (Mathf.Round(rb2d.velocity.y) < 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }
    private void CheckEnterGround(Collision2D collision) //Check if player entered on ground
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("isFalling", false);
        }
    }
    private void CheckExitGround(Collision2D collision) //Check if player exit ground
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    public void TakeDamage(float dmg)
    {
        animator.SetTrigger("hurtFire");
        if(potionMode == PotionMode.Fire)
        {

        }
        StartCoroutine("WhiteSpriteAndBack");
        health -= dmg;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }

    }

    private IEnumerator WhiteSpriteAndBack()
    {
        spriteRenderer.material.shader = shaderGUItext;
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.01f);
        spriteRenderer.material.shader = shaderSpritesDefault;
        spriteRenderer.color = Color.white;
    }
}
