using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightmoving : MonoBehaviour
{
    public float intensityChangeSpeed = 0.2f;
    public float posChangeSpeed = 0.2f;
    public float intensityamount;
    public float maxintensity;
    public float minintensity;
    public float maxpos;
    public float minpos;
    public Vector3 pos;
    public float xLocation;
    public UnityEngine.Rendering.Universal.Light2D spotlight;
    // Start is called before the first frame update
    void Start()
    {
        spotlight.intensity = 1f;
        StartCoroutine(intensityChangedecrease());
        xLocation = 2f;
        pos = new Vector3(xLocation, -7.5f, 90);
        StartCoroutine(posChangedecrease());
    }

    // Update is called once per frame
    void Update()
    {
        pos = new Vector3(xLocation, -7.5f, 90);
        transform.position = pos;
        spotlight.intensity = intensityamount;
    }

    IEnumerator posChangedecrease()
    {
        yield return new WaitForSeconds(posChangeSpeed);
        xLocation = xLocation - 0.25f;
        if (xLocation == minpos)
        {
            StartCoroutine(posChangeincrease());
        }
        else
        {
            StartCoroutine(posChangedecrease());
        }
    }
    IEnumerator posChangeincrease()
    {
        yield return new WaitForSeconds(posChangeSpeed);
        xLocation = xLocation + 0.25f;
        if (xLocation == maxpos)
        {
            StartCoroutine(posChangedecrease());
        }
        else
        {
            StartCoroutine(posChangeincrease());
        }
    }



    IEnumerator intensityChangedecrease()
    {
        yield return new WaitForSeconds(intensityChangeSpeed);
        intensityamount = intensityamount - 0.01f;
        if (intensityamount == minintensity || intensityamount < minintensity)
        {
            StartCoroutine(intensityChangeincrease());
        }
        else
        {
            StartCoroutine(intensityChangedecrease());
        }
    }
    IEnumerator intensityChangeincrease()
    {
        yield return new WaitForSeconds(intensityChangeSpeed);
        intensityamount = intensityamount + 0.01f;
        if (intensityamount == maxintensity || intensityamount > maxintensity)
        {
            StartCoroutine(intensityChangedecrease());
        }
        else
        {
            StartCoroutine(intensityChangeincrease());
        }
    }
}
