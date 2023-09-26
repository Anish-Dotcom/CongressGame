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
    public float maxintensityused;
    public float minintensityused;
    public float uncertainty;
    public float maxpos;
    public float minpos;
    public Vector3 pos;
    public float xLocation;
    public float yLocation;
    public float zLocation;
    public UnityEngine.Rendering.Universal.Light2D spotlight;
    // Start is called before the first frame update
    void Start()
    {
        spotlight.intensity = 1f;
        StartCoroutine(intensityChangedecrease());
        pos = new Vector3(xLocation, yLocation, zLocation);
        StartCoroutine(posChangedecrease());
        StartCoroutine(posChangedecrease());
        uncertainty = (maxintensity - minintensity) / 2;
        maxintensityused = maxintensity;
        minintensityused = minintensity;
    }

    // Update is called once per frame
    void Update()
    {
        pos = new Vector3(xLocation, yLocation, zLocation);
        transform.position = pos;
        spotlight.intensity = intensityamount;
    }

    IEnumerator posChangedecrease()
    {
        yield return new WaitForSeconds(posChangeSpeed);
        xLocation = xLocation - 0.025f;
        if (xLocation == minpos || xLocation < minpos)
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
        xLocation = xLocation + 0.025f;
        if (xLocation == maxpos || xLocation > maxpos)
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
        if (intensityamount == minintensityused || intensityamount < minintensityused)
        {
            intensityChangeSpeed = Random.Range(0.01f, 0.07f);
            maxintensityused = Random.Range(maxintensity-uncertainty+0.1f, maxintensity+0.1f);
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
        if (intensityamount == maxintensityused || intensityamount > maxintensityused)
        {
            intensityChangeSpeed = Random.Range(0.01f, 0.07f);
            minintensityused = Random.Range(minintensity-0.1f, minintensity+uncertainty-0.1f);
            StartCoroutine(intensityChangedecrease());
        }
        else
        {
            StartCoroutine(intensityChangeincrease());
        }
    }
}
