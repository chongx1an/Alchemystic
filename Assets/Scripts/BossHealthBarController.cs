using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour
{
    public GameObject healthBar;
    private SpriteRenderer healthBarRenderer;
    public GameObject boss;
    private BossController bossController;
    // Start is called before the first frame update
    void Start()
    {
        healthBarRenderer = healthBar.GetComponent<SpriteRenderer>();
        bossController = boss.GetComponent<BossController>();
        

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 newSize = new Vector2(bossController.health / 300.0f * 5.12f, 0.32f);

        healthBarRenderer.size = newSize;
    }
}
