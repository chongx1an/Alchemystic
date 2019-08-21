using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnigTextController : MonoBehaviour
{
    public float delay = 0.2f;
    private string fullText;
    private string currentText;
    private Text textObject;

    // Start is called before the first frame update
    void Start()
    {
        textObject = GetComponent<Text>();
        fullText = textObject.text;
        textObject.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShowText()
    {
        yield return new WaitForSeconds(5.0f);
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textObject.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(5.0f);
        while(textObject.color.a > 0)
        {
            textObject.color = new Color(textObject.color.r, textObject.color.g, textObject.color.b, textObject.color.a - 0.01f);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
