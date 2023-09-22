using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public static float volumeSlider;
    public static AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void volumeup(float sliderValue)
    {
        volumeSlider = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("Volume", volumeSlider);
    }
}
