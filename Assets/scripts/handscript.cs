using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handscript : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D spotlight;
    public GameObject LightOn;
    public GameObject LightOff;

    public GameObject hands;
    public GameObject rightHandEmpty;
    public GameObject leftHand;
    public GameObject lights;

    public static bool lightsOut;

    public bool lightOutForLight;
    public bool happened;
    public int numberOfFlickers;

    // Start is called before the first frame update
    void Start()
    {
        happened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (paperpickedupinstance.paperpickedup == false)
        {
            rightHandEmpty.SetActive(true);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector2(mousePosition.x, mousePosition.y);
            hands.transform.position = mousePosition;
        }
        else
        {
            rightHandEmpty.SetActive(false);
        }

        if (lightsOut)
        {
            if (happened == false)
            {
                StartCoroutine(lightFlicker());
                happened = true;
            }
            if (lightOutForLight == true)
            {
                spotlight.intensity = 0f;
            }
        }
        else
        {
            lightOutForLight = false;
            happened = false;
            spotlight.intensity = 1;
            LightOff.SetActive(false);
            LightOn.SetActive(true);
            leftHand.SetActive(false);
        }
    }

    IEnumerator lightFlicker()
    {
        while (numberOfFlickers > 0)
        {
            lights.SetActive(false);
            yield return new WaitForSeconds(0.08f);
            spotlight.intensity = 0.75f;
            lights.SetActive(true);
            yield return new WaitForSeconds(0.12f);
            lights.SetActive(false);
            yield return new WaitForSeconds(0.24f);
            spotlight.intensity = 0.5f;
            lights.SetActive(true);
            yield return new WaitForSeconds(0.08f);
            lights.SetActive(false);
            yield return new WaitForSeconds(0.22f);
            spotlight.intensity = 0.25f;
            lights.SetActive(true);
            yield return new WaitForSeconds(0.14f);
            spotlight.intensity = 0f;
            numberOfFlickers = numberOfFlickers - 1;
        }
        lightOutForLight = true;
        spotlight.intensity = 0;
        LightOn.SetActive(false);
        LightOff.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        DayController.DayConObj.GetComponent<DayController>().preFullText = "Seems like the power went out. Heres a candle for convienience.";
        yield return new WaitForSeconds(0.2f);
        leftHand.SetActive(true);
    }

}
