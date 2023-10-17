using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicController : MonoBehaviour
{
    public static bool music;
    public static bool effects;
    public Slider masterVolumeSlider;
    public Toggle musicToggleInspector;
    public Toggle effectsToggleInspector;

    public static bool happened;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void Start()
    {
        if (!SliderManager.masterVolume.Equals(0))
        {
            // Set the slider value from the SliderManager
            masterVolumeSlider.value = SliderManager.masterVolume;
        }
        if (happened == false)
        {
            music = true;
            effects = true;
            happened = true;
        }
    }

    public void musicToggle(bool musicTog)
    {
        Debug.Log(musicTog);
        music = musicTog;
    }
    public void effectsToggle(bool effectsTog)
    {
        Debug.Log(effectsTog);
        effects = effectsTog;
    }

    // Update is called once per frame
    void Update()
    {
        masterVolumeSlider.value = SliderManager.masterVolume;
        musicToggleInspector.isOn = music;
        effectsToggleInspector.isOn = effects;
    }
}
