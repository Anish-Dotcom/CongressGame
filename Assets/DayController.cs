using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    public static int dayNum = 0;
    public static Sprite[] paperObjectsForNext;//turn these into sprite arrays
    public Sprite[] allPaperObjects;
    public static int[] approvalPercentageDemographics = new int[7];//The 1%, Middle class, Impoverished, Progressive, Conservative, Federalist, Anti-Federalist
    public GameObject paperPrefab;

    void Start()
    {
        paperObjectsForNext = new Sprite[allPaperObjects.Length];
        paperObjectsForNext[0] = allPaperObjects[0];
        PaperMove.prevPapers = GameObject.FindGameObjectsWithTag("Paper");
        NewDay();
    }
    void Update()
    {
        
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
            PaperMove.prevPapers[i].GetComponent<SpriteRenderer>().sprite = paperObjectsForNext[i];
        }
    }
}
