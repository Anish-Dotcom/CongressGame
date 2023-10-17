using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public static float masterVolume = 1f; // Default value

    public Slider masterVolumeSlider;

    void Start()
    {
        // Set the initial value of the slider
        masterVolumeSlider.value = masterVolume;
    }

    public void UpdateSliderValue(float value)
    {
        // Update the static variable when the slider value changes
        masterVolume = value;
    }
}
