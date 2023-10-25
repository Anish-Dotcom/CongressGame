using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterManager : MonoBehaviour
{
    public static bool blue = true;
    public static bool orange = false;
    public static bool yellow = false;
    public static bool red = false;
    public static bool green = false;
    public static bool gray = false;

    public static bool sloganOpt1 = true;
    public static bool sloganOpt2 = false;
    public static bool sloganOpt3 = false;
    public static bool sloganOpt4 = false;
    public static bool sloganOpt5 = false;

    public static bool motifopt1 = true;
    public static bool motifopt2 = false;
    public static bool motifopt3 = false;
    public static bool motifopt4 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorBlue() // colors of poster for button
    {
        RemoveAllColors();
        blue = true;
    }
    public void ColorOrange()
    {
        RemoveAllColors();
        orange = true;
    }
    public void ColorYellow()
    {
        RemoveAllColors();
        yellow = true;
    }
    public void ColorRed()
    {
        RemoveAllColors();
        red = true;
    }
    public void ColorGreen()
    {
        RemoveAllColors();
        green = true;
    }
    public void ColorGray()
    {
        RemoveAllColors();
        gray = true;
    }

    public void slogan1() // slogan button
    {
        RemoveAllSlogans();
        sloganOpt1 = true;
    }
    public void slogan2()
    {
        RemoveAllSlogans();
        sloganOpt2 = true;
    }
    public void slogan3()
    {
        RemoveAllSlogans();
        sloganOpt3 = true;
    }
    public void slogan4()
    {
        RemoveAllSlogans();
        sloganOpt4 = true;
    }
    public void slogan5()
    {
        RemoveAllSlogans();
        sloganOpt5 = true;
    }

    public void Motif1() // motif unfinshed button
    {
        RemoveAllMotifs();
        motifopt1 = true;
    }
    public void Motif2()
    {
        RemoveAllMotifs();
        motifopt2 = true;
    }
    public void Motif3()
    {
        RemoveAllMotifs();
        motifopt3 = true;
    }
    public void Motif4()
    {
        RemoveAllMotifs();
        motifopt4 = true;
    }

    public void RemoveAllColors() // reset colors
    {
        blue = false;
        orange = false;
        yellow = false;
        red = false;
        green = false;
        gray = false;
    }
    public void RemoveAllSlogans() //reset slogans
    {
        sloganOpt1 = false;
        sloganOpt2 = false;
        sloganOpt3 = false;
        sloganOpt4 = false;
        sloganOpt5 = false;
    }

    public void RemoveAllMotifs() //reset motifs
    {
        motifopt1 = false;
        motifopt2 = false;
        motifopt3 = false;
        motifopt4 = false;
    }
}
