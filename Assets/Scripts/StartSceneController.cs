using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneController : MonoBehaviour
{
    public GameObject title;
    public GameObject blackFade;

    private ArrayList animators;
    private Text text;
    private bool isReducing;
    // Start is called before the first frame update
    void Start()
    {
        animators = new ArrayList();
        text = GetComponent<Text>();
        animators.Add(GetComponent<Animator>());
        animators.Add(title.GetComponent<Animator>());
        animators.Add(blackFade.GetComponent<Animator>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {

            foreach(Animator theAnimator in animators)
            {
                theAnimator.SetTrigger("LoadNextScene");
            }

        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("StoryScene");
    }

    private void FixedUpdate()
    {
        if (text.color.a < 1.0f && !isReducing)
        {
            Color newColor = new Color(text.color.r, text.color.r, text.color.b, text.color.a + 0.01f);
            text.color = newColor;
        }
        else
        {
            isReducing = true;
        }

        if (text.color.a > 0.5f && isReducing)
        {
            Color newColor = new Color(text.color.r, text.color.r, text.color.b, text.color.a - 0.01f);
            text.color = newColor;
        }
        else
        {
            isReducing = false;
        }
    }


}
