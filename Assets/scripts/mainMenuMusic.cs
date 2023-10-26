using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class mainMenuMusic : MonoBehaviour
{
    public AudioSource[] allAudios;
    public AudioSource radioStatic;
    public int random;

    public static bool happened;
    // Start is called before the first frame update
    void Awake()
    {
        random = Random.Range(0, 6);
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("Music");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        
        radioStatic.playOnAwake = true;
        radioStatic.loop = true;
    }

    private void Start()
    {
        if(happened == false)
        {
            allAudios[random].Play();
            radioStatic.Play();
            EscapeMenu.isOpen = false;
            musicController.music = true;
            happened = true;
        }
        StartCoroutine(playMusic());
    }

    // Update is called once per frame
    void Update()
    {

        allAudios[random].volume = musicController.masterVolume;
        radioStatic.volume = musicController.masterVolume;
        allAudios[random].playOnAwake = true;
        allAudios[random].loop = true;

        if (musicController.music)
        {
            allAudios[random].mute = false;
            radioStatic.mute = false;
        }
        else
        {
            allAudios[random].mute = true;
            radioStatic.mute = true;

        }

        if (EscapeMenu.isOpen || handscript.noElectricity)
        {
            allAudios[random].Pause();
            radioStatic.Pause();
        }
        else
        {
            allAudios[random].UnPause();
            radioStatic.UnPause();
        }
    }

    IEnumerator playMusic()
    {
        allAudios[0].mute = true;
        allAudios[1].mute = true;
        allAudios[2].mute = true;
        allAudios[3].mute = true;
        allAudios[4].mute = true;
        allAudios[5].mute = true;
        random = Random.Range(0, 6);
        allAudios[random].mute = false;
        allAudios[random].Play();
        yield return new WaitForSeconds(allAudios[random].clip.length);
        StartCoroutine(playMusic());
    }
}
