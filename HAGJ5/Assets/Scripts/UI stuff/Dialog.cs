using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisp;
    public string[] sentences;
    private int index = 0;
    public float typingSpeed = 0.2f;

    private bool isTyping = false;
    private Coroutine co;

    // Start is called before the first frame update
    void Start()
    {
        isTyping = false;
        co = StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        isTyping = true;
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisp.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void nextSentence()
    {
        if (!isTyping)
        {
            if (index < sentences.Length - 1)
            {
                index++;
                textDisp.text = "";
                co = StartCoroutine(Type());
            }
        }
        else
        {
            //load full sentences alr
            StopCoroutine(co);
            isTyping = false;
            textDisp.text = sentences[index];
        }
    }
}
