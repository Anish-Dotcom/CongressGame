using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject Pic1;
    public GameObject Pic2;
    public GameObject Pic3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Nextpic());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Nextpic()
    {
        Pic1.SetActive(true);
        yield return new WaitForSeconds(1);
        StartCoroutine(Nextpic1());
    }
    IEnumerator Nextpic1()
    {
        Pic2.SetActive(true);
        yield return new WaitForSeconds(5);
        StartCoroutine(Nextpic2());
    }
    IEnumerator Nextpic2()
    {
        Pic3.SetActive(true);
        yield return new WaitForSeconds(10);
    }
}
