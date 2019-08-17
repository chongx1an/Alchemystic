using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StorySceneController : MonoBehaviour
{
    public GameObject blackFade;

    private ArrayList animators;

    public float delay = 0.1f;
    private string fullText;
    private string currentText;
    private Text textObject;
    // Start is called before the first frame update
    void Start()
    {
        textObject = GetComponent<Text>();
        fullText = textObject.text;
        StartCoroutine("ShowText");

        animators = new ArrayList
        {
            GetComponent<Animator>(),
            blackFade.GetComponent<Animator>()
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            foreach (Animator theAnimator in animators)
            {
                theAnimator.SetTrigger("LoadNextScene");
            }
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("MainScene");

    }

    private IEnumerator ShowText()
    {
        for(int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textObject.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

}
