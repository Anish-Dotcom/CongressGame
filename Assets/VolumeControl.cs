using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    private void Start()
    {
        // Make sure you assign the AudioSource and VolumeSlider in the Inspector.
        if (audioSource == null || volumeSlider == null)
        {
            Debug.LogError("Please assign the AudioSource and VolumeSlider in the Inspector.");
            return;
        }

        // Set the initial volume based on the slider value.
        audioSource.volume = volumeSlider.value;

    }

    public void UpdateVolume()
    {
        // Update the audio source volume based on the slider value.
        audioSource.volume = volumeSlider.value;

        Debug.Log(volumeSlider.value);
    }
}