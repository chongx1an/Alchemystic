﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject fireEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Ground")
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y + 0.5f);
            GameObject fire = Instantiate(fireEffectPrefab, pos, transform.rotation);
            fire.transform.SetParent(col.gameObject.transform);
            FindObjectOfType<AudioManagerController>().Play("FireLightUp");
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }

    }
}
