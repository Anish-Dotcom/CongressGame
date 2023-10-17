using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicController : MonoBehaviour
{
    public static float masterVolume = 1f;
    public static bool music = true;
    public static bool effects = true;
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

    public void UpdateSliderValue(float value)
    {
        masterVolume = value;
    }

    // Update is called once per frame
    void Update()
    {
        masterVolumeSlider.value = masterVolume;
        musicToggleInspector.isOn = music;
        effectsToggleInspector.isOn = effects;
    }
}
