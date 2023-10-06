using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowTypeEffect : MonoBehaviour
{
    public float speed = 0.1f;
    public string fullText;
    private string currentText = "";
    public bool startText = false;
    // Start is called before the first frame update
    void Start()
    {
        if (startText)
        {
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++) {
            currentText = fullText.Substring(0,i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(speed);
        }
    }
}
