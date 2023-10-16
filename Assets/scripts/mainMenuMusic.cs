using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class mainMenuMusic : MonoBehaviour
{
    public AudioSource source;
    // Start is called before the first frame update
    void Awake()
    {
        source.playOnAwake = true;
        source.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (musicController.instance != null)
        {
            if (musicController.instance.music)
            {
                source.Play();
            }
            else
            {
                source.Stop();
            }
        }
    }
}
