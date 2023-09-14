using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightmoving : MonoBehaviour
{
    public GameObject light;
    public Vector3 pos;
    public float xaxis;

    public bool increasing;
    public bool decreasing;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        xaxis = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(xaxis == -2)
        {
            increasing = true;
            while (increasing == true)
            {
                StartCoroutine(Pause());
                pos = new Vector3(xaxis, -7.5f, 0f);
                transform.position = pos;
                xaxis++;
                if (xaxis == 2){
                    increasing = false;
                    decreasing = true;
                }
            }
        }

        if(xaxis == 2)
        {
            decreasing = true;
            while (decreasing == true)
            {
                StartCoroutine(Pause());
                pos = new Vector3(xaxis, -7.5f, 0f);
                transform.position = pos;
                xaxis--;
                if (xaxis == -2)
                {
                    decreasing = false;
                }
            }
        }

        
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
    }
}
