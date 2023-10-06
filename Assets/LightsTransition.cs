using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //2019 VERSIONS

public class LightsTransition : MonoBehaviour
{
    public bool transition;
    public float decayspeed = 1f;
    public GameObject Mainlight;
    public GameObject Offlight;
    public UnityEngine.Rendering.Universal.Light2D light;
    // Start is called before the first frame update
    void Start()
    {
        if (transition)
        {
            StartCoroutine(SlowLightDown());
        }
    }

    IEnumerator  SlowLightDown()
    {
        Mainlight.SetActive(false);
        Offlight.SetActive(true);
        for (int i = 0; i < 20;i++)
        {
            light.intensity = light.intensity - decayspeed;
            
            yield return new WaitForSeconds(0.05f);
        }
    }
}
