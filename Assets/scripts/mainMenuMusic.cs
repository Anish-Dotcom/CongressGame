using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class mainMenuMusic : MonoBehaviour
{
    public AudioSource source;
    public bool happened;
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
            musicController.effects = true;
            musicController.music = true;
            happened = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
