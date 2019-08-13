using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Image healthBar;
    private Text text;
    private static PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        healthBar = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = playerController.health + "/100";
        healthBar.fillAmount = playerController.health/100.0f;
    }
}
