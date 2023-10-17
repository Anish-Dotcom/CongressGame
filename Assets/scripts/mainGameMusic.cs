using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainGameMusic : MonoBehaviour
{
    public AudioSource source;
    public AudioSource source2;

    public static bool happened;
    // Start is called before the first frame update
    void Awake()
    {
        source.playOnAwake = true;
        source.loop = true;
        source2.playOnAwake = true;
        source2.loop = true;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        source.volume = musicController.masterVolume;

        if (musicController.music)
        {
            source.mute = false;
            source2.mute = false;

        }
        else
        {
            source.mute = true;
            source2.mute = false;

        }
    }
}
