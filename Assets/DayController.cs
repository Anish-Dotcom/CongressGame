using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    public static int dayNum = 0;
    public static GameObject[] paperObjectsForNext;//turn these into sprite arrays
    public GameObject[] allPaperObjects;
    public static int[] approvalPercentageDemographics = new int[7];//The 1%, Middle class, Impoverished, Progressive, Conservative, Federalist, Anti-Federalist

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void NewDay() 
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
            Instantiate (paperObjectsForNext[i], new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
    }
}
