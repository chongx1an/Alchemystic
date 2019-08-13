using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private new SpriteRenderer renderer;
    public float fireDuration;
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("DistinguishFire");
        
    }

    private IEnumerator DistinguishFire()
    {
        yield return new WaitForSeconds(fireDuration);
        Color opacity = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
        renderer.color = opacity;
        animator.Play("fire_exit");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
