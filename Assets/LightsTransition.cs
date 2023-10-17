using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class LightsTransition : MonoBehaviour
{
    
    public bool transition;

    public bool transition1;
    public bool transitionback;

    public GameObject agent;

    

    public bool transitionback1;
    public float decayspeed = 1f;
    public GameObject Mainlight;
    public GameObject Offlight;
    public SlowTypeEffect Slow;
    public UnityEngine.Rendering.Universal.Light2D light;
    // Start is called before the first frame update


    
    void Start()
    {
         
    }

    void Update()
    {
        
        if (transition)
        {
            StartCoroutine(SlowLightDown());
        }
        if (transition1)
        {
            StartCoroutine(SlowLightDown1());
        }
        if (transitionback)
        {
            StartCoroutine(BringLightup());
        }
        if (transitionback1)
        {
            StartCoroutine(BringLightup1());


            transitionback1 = false;
            UnityEngine.Debug.Log("zsdfafs");
        }
    }

    IEnumerator  SlowLightDown()
    {

        agent.SetActive(false);
        
        Mainlight.SetActive(false);
        Offlight.SetActive(true);
        for (int i = 0; i < 10;i++)
        {
            light.intensity = light.intensity - decayspeed;
            
            yield return new WaitForSeconds(0.05f);
        }

        Slow.startText = true;
        
        transition = false;
    }
    IEnumerator SlowLightDown1()
    {

        agent.SetActive(false);
        Mainlight.SetActive(false);
        Offlight.SetActive(true);
        for (int i = 0; i < 10; i++)
        {
            light.intensity = light.intensity - decayspeed;

            yield return new WaitForSeconds(0.05f);
        }

        transition1 = false;
    }
    IEnumerator BringLightup()
    {

        
        for (int i = 0; i < 20; i++)
        {
            if (light.intensity < 0.3) {
                light.intensity = light.intensity + decayspeed;

                yield return new WaitForSeconds(0.05f);
            }
        }


        transitionback = false;
        Mainlight.SetActive(true);
        Offlight.SetActive(false);
        
        agent.SetActive(true);

    }
    IEnumerator BringLightup1()
    {


        UnityEngine.Debug.Log("fsdaf");

        for (int i = 0; i < 20; i++)
        {
            if (light.intensity < 1)
            {
                light.intensity = light.intensity + decayspeed+0.1f;

                UnityEngine.Debug.Log("rewrw");
                yield return new WaitForSeconds(0.05f);
            }

            
        }


        UnityEngine.Debug.Log("vxcvsf");
        Mainlight.SetActive(true);
        Offlight.SetActive(false);

        agent.SetActive(true);
    }
}
