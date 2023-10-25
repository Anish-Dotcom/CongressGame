using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterScript : MonoBehaviour
{
    public GameObject BluePoster;
    public GameObject OrangePoster;
    public GameObject YellowPoster;
    public GameObject RedPoster;
    public GameObject GreenPoster;
    public GameObject GrayPoster;

    public GameObject Slogan1;
    public GameObject Slogan2;
    public GameObject Slogan3;
    public GameObject Slogan4;
    public GameObject Slogan5;

    public GameObject lion;
    public GameObject lightningbolt;
    public GameObject money;
    public GameObject bear;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PosterManager.blue == true) // color change
        {
            BluePoster.SetActive(true);
        }
        else
        {
            BluePoster.SetActive(false);
        }
        if (PosterManager.orange == true)
        {
            OrangePoster.SetActive(true);
        }
        else
        {
            OrangePoster.SetActive(false);
        }
        if (PosterManager.yellow == true)
        {
            YellowPoster.SetActive(true);
        }
        else
        {
            YellowPoster.SetActive(false);
        }
        if (PosterManager.red == true)
        {
            RedPoster.SetActive(true);
        }
        else
        {
            RedPoster.SetActive(false);
        }
        if (PosterManager.green == true)
        {
            GreenPoster.SetActive(true);
        }
        else
        {
            GreenPoster.SetActive(false);
        }
        if (PosterManager.gray == true)
        {
            GrayPoster.SetActive(true);
        }
        else
        {
            GrayPoster.SetActive(false);
        }

        if (PosterManager.sloganOpt1 == true) // slogan change
        {
            Slogan1.SetActive(true);
        }
        else
        {
            Slogan1.SetActive(false);
        }
        if (PosterManager.sloganOpt2 == true)
        {
            Slogan2.SetActive(true);
        }
        else
        {
            Slogan2.SetActive(false);
        }
        if (PosterManager.sloganOpt3 == true)
        {
            Slogan3.SetActive(true);
        }
        else
        {
            Slogan3.SetActive(false);
        }
        if (PosterManager.sloganOpt4 == true)
        {
            Slogan4.SetActive(true);
        }
        else
        {
            Slogan4.SetActive(false);
        }
        if (PosterManager.sloganOpt5 == true)
        {
            Slogan5.SetActive(true);
        }
        else
        {
            Slogan5.SetActive(false);
        }
        if (PosterManager.motifopt1 == true) // motif change
        {
            lion.SetActive(true);
        }
        else
        {
            lion.SetActive(false);
        }
        if (PosterManager.motifopt2 == true)
        {
            lightningbolt.SetActive(true);
        }
        else
        {
            lightningbolt.SetActive(false);
        }
        if (PosterManager.motifopt3 == true)
        {
            money.SetActive(true);
        }
        else
        {
            money.SetActive(false);
        }
        if (PosterManager.motifopt4 == true)
        {
            bear.SetActive(true);
        }
        else
        {
            bear.SetActive(false);
        }
    }
}
