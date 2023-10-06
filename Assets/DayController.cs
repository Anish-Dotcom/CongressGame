using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    public static int dayNum = 0;
    public static Sprite[] paperObjectsForNext;
    public Sprite[] allPaperObjects;//0 = tutorial doc, 1-14 = random docs, 15 = media control, 16 = create laws, 17 = force reinstatement
    public static Sprite[] staticAllPaperObjects;
    public static bool[] isUsedPaper;
    public static int[] approvalPercentageDemographics = new int[7];//The 1%, Middle class, Impoverished, Progressive, Conservative, Federalist, Anti-Federalist
    public GameObject paperPrefab;
    public static GameObject staticPaperPrefab;
    public GameObject bellunpushed;
    public GameObject bellpushed;
    public static bool bellIsPushed;
    public static int bellInt = 0;

    void Start()
    {
        staticPaperPrefab = paperPrefab;
        isUsedPaper = new bool[allPaperObjects.Length];
        staticAllPaperObjects = new Sprite[allPaperObjects.Length];
        paperObjectsForNext = new Sprite[allPaperObjects.Length];
        for (int i = 0; i < isUsedPaper.Length; i++) 
        {
            isUsedPaper[i] = false;
            staticAllPaperObjects[i] = allPaperObjects[i];
        }
        paperObjectsForNext[0] = allPaperObjects[0];//Tutorial doc
        Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        PaperMove.prevPapers = GameObject.FindGameObjectsWithTag("Paper");
        PapersForNext();
    }
    void Update()
    {
        if (bellIsPushed == true)
        {
            StartCoroutine(PushBell());
        }
    }
    public static void NewDay()
    {
        for (int i = 0; i < approvalPercentageDemographics.Length; i++) 
        {
            if (approvalPercentageDemographics[i] > 100) 
            {
                approvalPercentageDemographics[i] = 100;
            }
        }
        for (int i = 0; i < paperObjectsForNext.Length; i++)
        {
            if (paperObjectsForNext[i] !=  null)
            {
                PaperMove.prevPapers[i].GetComponent<SpriteRenderer>().sprite = paperObjectsForNext[i];//order of things happening is wrong which causes this not to work
            }
        }
        dayNum++;
    }
    public static void PapersForNext()//call this after inbetween day
    {
        bellIsPushed = true;
        if (dayNum == 0) 
        {
            NewDay();
        }
        else
        {
            if (dayNum >= 3)//makes algorithm more efficient :)
            { 
                if (dayNum == 3)
                {
                    paperObjectsForNext[15] = staticAllPaperObjects[15];
                }
                if (dayNum == 4)
                {
                    paperObjectsForNext[16] = staticAllPaperObjects[16];
                }
                if (dayNum == 5)
                {
                    paperObjectsForNext[17] = staticAllPaperObjects[17];
                }
                Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
            int numberOfPapers = UnityEngine.Random.Range(2, 4);
            for (int i = 0; i < numberOfPapers; i++)
            {
                int numberOfRandomPaper = UnityEngine.Random.Range(1, 15);
                if (!isUsedPaper[numberOfRandomPaper])
                {
                    paperObjectsForNext[i] = staticAllPaperObjects[numberOfRandomPaper];
                    isUsedPaper[numberOfRandomPaper] = true;
                    Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                }
            }
        }
        NewDay();
    }
    public static void InBetweenDay() //remove all papers and then summon news paper and madlibs politcal campain
    {
        GameObject[] PrevPapers = GameObject.FindGameObjectsWithTag("PaperController");
        for (int i = 0; i < PrevPapers.Length; i++) 
        {
            if (PrevPapers[i] != null) 
            {
                Destroy(PrevPapers[i]);
            }
        }
    }
    public static void BellPush()
    { 
        if (bellInt == 0) 
        {
            InBetweenDay();
            bellInt = 1;
        }
        else if (bellInt == 1) 
        {
            PapersForNext();
            bellInt = 0;
        }
    }
    IEnumerator PushBell()
    {
        bellunpushed.SetActive(false);
        bellpushed.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        bellunpushed.SetActive(true);
        bellpushed.SetActive(false);
        bellIsPushed = false;
    }
}