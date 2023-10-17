using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class mainMenuMusic : MonoBehaviour
{
    public AudioSource source;
    public static bool happened;
    // Start is called before the first frame update
    void Awake()
    {
        source.playOnAwake = true;
        source.loop = true;
    }

    private void Start()
    {
        if(happened == false)
        {
            musicController.music = true;
            happened = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = musicController.masterVolume;

        if (musicController.music)
        {
            source.mute = false;
        }
        else
        {
            source.mute = true;
        }
    }
}
