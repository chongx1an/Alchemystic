using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public float fireDuration;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("DistinguishFire");
        
    }

    private IEnumerator DistinguishFire()
    {
        yield return new WaitForSeconds(fireDuration);
        Color opacity = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        spriteRenderer.color = opacity;
        animator.Play("fire_exit");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    
}
