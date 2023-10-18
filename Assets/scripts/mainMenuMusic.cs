using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class mainMenuMusic : MonoBehaviour
{
    public AudioSource source;
    public AudioSource source2;

    public static bool happened;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("Music");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        source.playOnAwake = true;
        source.loop = true;
        source2.playOnAwake = true;
        source2.loop = true;
    }

    private void Start()
    {
        if(happened == false)
        {
            EscapeMenu.isOpen = false;
            musicController.music = true;
            happened = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = musicController.masterVolume;
        source2.volume = musicController.masterVolume;

        if (musicController.music)
        {
            source.mute = false;
            source2.mute = false;

        }
        else
        {
            source.mute = true;
            source2.mute = true;

        }

        if (EscapeMenu.isOpen)
        {
            source.Pause();
            source2.Pause();
        }
        else
        {
            source.UnPause();
            source2.UnPause();
        }
    }
}
