using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightmoving : MonoBehaviour
{
    public float intensityChangeSpeed = 0.2f;
    public Vector3 pos;
    public float xLocation;
    public UnityEngine.Rendering.Universal.Light2D spotlight;
    // Start is called before the first frame update
    void Start()
    {
        spotlight.intensity = 1f;
        StartCoroutine(Pause1());
        xLocation = 2f;
        pos = new Vector3(xLocation, -7.5f, 90);
        StartCoroutine(posChange1());
    }

    // Update is called once per frame
    void Update()
    {
        pos = new Vector3(xLocation, -7.5f, 90);
        transform.position = pos;
    }

    IEnumerator posChange1()
    {
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 1.75f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 1.5f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 1.25f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 1f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 0.75f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 0.5f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 0.25f;
        StartCoroutine(posChange2());
    }
    IEnumerator posChange2()
    {
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 0.5f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 0.75f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 1f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 1.25f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 1.5f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 1.75f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        xLocation = 2f;
        StartCoroutine(posChange1());
    }

    IEnumerator Pause1()
    {
        yield return new WaitForSeconds(intensityChangeSpeed);
        spotlight.intensity = 0.9f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        spotlight.intensity = 0.8f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        spotlight.intensity = 0.7f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        spotlight.intensity = 0.6f;
        StartCoroutine(Pause2());
    }
    IEnumerator Pause2()
    {
        yield return new WaitForSeconds(intensityChangeSpeed);
        spotlight.intensity = 0.7f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        spotlight.intensity = 0.8f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        spotlight.intensity = 0.9f;
        yield return new WaitForSeconds(intensityChangeSpeed);
        spotlight.intensity = 1f;
        StartCoroutine(Pause1());
    }
}
