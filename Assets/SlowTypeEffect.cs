using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlowTypeEffect : MonoBehaviour
{

    public float speed = 0.1f;
    public string fullText;
    private string currentText = "";
    public bool startText = false;
    public LightsTransition lta;
    public LightsTransition ltb;
    public DayController DayController;
    public int dayNumreal ;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        dayNumreal = DayController.daynum1 - 1;
        
        fullText = "Day" + " "+ dayNumreal + " ";
        
        if (startText)
        {
            StopAllCoroutines();

            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        startText = false;
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<TMP_Text>().text = currentText;
            yield return new WaitForSeconds(speed);
        }
        yield return new WaitForSeconds(1);
        this.GetComponent<TMP_Text>().text = "";
        lta.transitionback= true;
        ltb.transitionback1 = true;
        
    }
}

