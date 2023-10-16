using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicController : MonoBehaviour
{
    public float masterVolume;
    public bool music;
    public bool effects;
    public Slider masterVolumeSlider;

    public static musicController instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        masterVolume = masterVolumeSlider.value;
    }
}
