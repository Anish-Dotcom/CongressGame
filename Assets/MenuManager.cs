using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static int volume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void volumeup(float slidervalue)
    {
      mixer.Setfloat("Volume", Mathf.Log10 (slidervalue) *20 )
    }
}
