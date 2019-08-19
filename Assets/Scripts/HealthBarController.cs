using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private Image healthBar;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = PlayerController.instance.health + "/100";
        healthBar.fillAmount = PlayerController.instance.health /100.0f;
    }
}
