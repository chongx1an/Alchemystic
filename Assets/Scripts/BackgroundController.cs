using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, transform.position.z);
        Vector2 offset = new Vector2(Time.time * 5.0f - Camera.main.transform.position.x, 0);

    }
}
