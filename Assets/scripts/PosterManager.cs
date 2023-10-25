using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterManager : MonoBehaviour
{
    public static bool blue = false;
    public static bool orange = false;
    public static bool yellow = false;
    public static bool red = false;
    public static bool green = false;
    public static bool gray = false;

    public static bool sloganOpt1 = false;
    public static bool sloganOpt2 = false;
    public static bool sloganOpt3 = false;
    public static bool sloganOpt4 = false;
    public static bool sloganOpt5 = false;

    public static bool motifopt1 = false;
    public static bool motifopt2 = false;
    public static bool motifopt3 = false;
    public static bool motifopt4 = false;

    public int demographicToChange;

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
        demographicToChange = 0;
        DayController.approvalPercentageDemographics[demographicToChange] += 2;
        RemoveAllColors();
        blue = true;
    }
    public void ColorOrange()
    {
        demographicToChange = 2;
        DayController.approvalPercentageDemographics[demographicToChange] += 2;
        RemoveAllColors();
        orange = true;
    }
    public void ColorYellow()
    {
        demographicToChange = 4;
        DayController.approvalPercentageDemographics[demographicToChange] += 2;
        RemoveAllColors();
        yellow = true;
    }
    public void ColorRed()
    {
        demographicToChange = 5;
        DayController.approvalPercentageDemographics[demographicToChange] += 2;
        RemoveAllColors();
        red = true;
    }
    public void ColorGreen()
    {
        demographicToChange = 1;
        DayController.approvalPercentageDemographics[demographicToChange] += 2;
        RemoveAllColors();
        green = true;
    }
    public void ColorGray()
    {
        demographicToChange = 2;
        DayController.approvalPercentageDemographics[demographicToChange] += 2;
        RemoveAllColors();
        gray = true;
    }

    public void slogan1() // slogan button
    {
        demographicToChange = 2;
        DayController.approvalPercentageDemographics[demographicToChange] += -2;
        RemoveAllSlogans();
        sloganOpt1 = true;
    }
    public void slogan2()
    {
        demographicToChange = 0;
        DayController.approvalPercentageDemographics[demographicToChange] += -2;
        RemoveAllSlogans();
        sloganOpt2 = true;
    }
    public void slogan3()
    {
        demographicToChange = 6;
        DayController.approvalPercentageDemographics[demographicToChange] += -2;
        RemoveAllSlogans();
        sloganOpt3 = true;
    }
    public void slogan4()
    {
        demographicToChange = 4;
        DayController.approvalPercentageDemographics[demographicToChange] += -2;
        RemoveAllSlogans();
        sloganOpt4 = true;
    }
    public void slogan5()
    {
        demographicToChange = 5;
        DayController.approvalPercentageDemographics[demographicToChange] += -2;
        RemoveAllSlogans();
        sloganOpt5 = true;
    }

    public void Motif1() // motif unfinshed button
    {
        demographicToChange = 0;
        DayController.approvalPercentageDemographics[demographicToChange] += 1;
        RemoveAllMotifs();
        motifopt1 = true;
    }
    public void Motif2()
    {
        demographicToChange = 5;
        DayController.approvalPercentageDemographics[demographicToChange] += 1;
        RemoveAllMotifs();
        motifopt2 = true;
    }
    public void Motif3()
    {
        demographicToChange = 1;
        DayController.approvalPercentageDemographics[demographicToChange] += 1;
        RemoveAllMotifs();
        motifopt3 = true;
    }
    public void Motif4()
    {
        demographicToChange = 1;
        DayController.approvalPercentageDemographics[demographicToChange] += 1;
        RemoveAllMotifs();
        motifopt4 = true;
    }

    public void RemoveAllColors() // reset colors
    {
        DayController.approvalPercentageDemographics[demographicToChange] += -2;
        blue = false;
        orange = false;
        yellow = false;
        red = false;
        green = false;
        gray = false;
    }
    public void RemoveAllSlogans() //reset slogans
    {
        DayController.approvalPercentageDemographics[demographicToChange] += 2;
        sloganOpt1 = false;
        sloganOpt2 = false;
        sloganOpt3 = false;
        sloganOpt4 = false;
        sloganOpt5 = false;
    }

    public void RemoveAllMotifs() //reset motifs
    {
        DayController.approvalPercentageDemographics[demographicToChange] += -1;
        motifopt1 = false;
        motifopt2 = false;
        motifopt3 = false;
        motifopt4 = false;
    }
}
