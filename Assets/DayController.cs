using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Array2DEditor;

public class DayController : MonoBehaviour
{
    public static int dayNum = 0;
    public int daynum1 = 0;
    public static Sprite[] paperObjectsForNext;
    public Sprite[] allPaperObjects;//0 = tutorial doc, 1-14 = random docs, 15 = media control, 16 = create laws, 17 = force reinstatement
    public static Sprite[] staticAllPaperObjects;
    public static bool[] isUsedPaper;
    public static int[] approvalPercentageDemographics = new int[7];//The 1%, Middle class, Impoverished, Progressive, Conservative, Federalist, Anti-Federalist
    [SerializeField]
    public Array2DInt demographicChangeAcc;
    [SerializeField]
    public Array2DInt demographicChangeDec;
    public GameObject paperPrefab;
    public static GameObject staticPaperPrefab;
    public GameObject bellunpushed;
    public GameObject bellpushed;
    public static bool bellIsPushed;
    public static int bellInt = 0;
    public LightsTransition lt;
    public LightsTransition lt1;
    public static GameObject DayConObj;
    public string[] AgentTextOnPickup;
    public string[] AgentTextOnStampAcc;
    public string[] AgentTextOnStampDec;

    public float speed = 1f;
    public string preFullText;
    public string fullText;
    private string currentText = "";
    public bool startText = true;
    public static bool textWriting = false;
    public static bool textWasInterupted = false;
    public static TMP_Text AgentText;

    public static SpriteRenderer Agent;
    public Sprite[] Agents;
    public int[] pickupIntAgent;
    public int[] stampAccAgent;
    public int[] stampDecAgent;

    public static bool stampedAll = false;

    public string daynumstring = "Day";
    public static bool citizensCanProposeLaws = false;

    void Start()
    {
        Agent = GameObject.FindGameObjectWithTag("Agent").GetComponent<SpriteRenderer>();
        AgentText = GameObject.FindGameObjectWithTag("AgentText").GetComponent<TMP_Text>();
        DayConObj = GameObject.FindGameObjectWithTag("DayCon");
        staticPaperPrefab = paperPrefab;
        isUsedPaper = new bool[allPaperObjects.Length];
        staticAllPaperObjects = new Sprite[allPaperObjects.Length];
        paperObjectsForNext = new Sprite[allPaperObjects.Length];
        for (int i = 0; i < staticAllPaperObjects.Length; i++)
        {
            isUsedPaper[i] = false;
            staticAllPaperObjects[i] = allPaperObjects[i];
        }
        paperObjectsForNext[0] = allPaperObjects[0];//Tutorial doc
        GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Paper.GetComponent<PaperMove>().paperNumber = 0;
        PaperMove.prevPapers = GameObject.FindGameObjectsWithTag("Paper");
        PapersForNext();
    }
    void Update()
    {
        daynum1 = dayNum;
        if (bellIsPushed == true)
        {
            StartCoroutine(PushBell());
            if (dayNum > 1)
            {

                lt1.transition1 = true;
                lt.transition = true;
                UnityEngine.Debug.Log("start");
            }
        }
        
    }
    public static void NewDay()
    {
        PaperMove.prevPapers = GameObject.FindGameObjectsWithTag("Paper");
        for (int i = 0; i < approvalPercentageDemographics.Length; i++)
        {
            if (approvalPercentageDemographics[i] > 100)
            {
                approvalPercentageDemographics[i] = 100;
            }
            if (approvalPercentageDemographics[i] < 0)
            {
                approvalPercentageDemographics[i] = 0;
            }
        }
        for (int i = 0; i < paperObjectsForNext.Length; i++)
        {
            if (paperObjectsForNext[i] != null)
            {
                PaperMove.prevPapers[i].GetComponent<SpriteRenderer>().sprite = paperObjectsForNext[i];
            }
        }
        dayNum++;
    }
    public static void PapersForNext()//call this after inbetween day
    {
        GameObject[] paperConObj = GameObject.FindGameObjectsWithTag("PaperController");
        for (int i = 0; i < paperConObj.Length; i++)
        {
            if (paperConObj[i].GetComponent<PaperMove>().stampedType == 1)//decclined -----------------------------------------------------------------------------------------------
            {
                for (int b = 0; b < 7; b++)
                {
                    approvalPercentageDemographics[b] += DayConObj.GetComponent<DayController>().demographicChangeDec.GetCell(b, paperConObj[i].GetComponent<PaperMove>().paperNumber);
                }
            }
            if (paperConObj[i].GetComponent<PaperMove>().stampedType == 2) //accepted --------------------------------------------------------------------------------------------------
            {
                for (int b = 0; b < 7; b++)
                {
                    approvalPercentageDemographics[b] += DayConObj.GetComponent<DayController>().demographicChangeAcc.GetCell(b, paperConObj[i].GetComponent<PaperMove>().paperNumber);
                    if (dayNum == 4 && paperConObj[i].GetComponent<PaperMove>().paperNumber == 16) 
                    {
                        citizensCanProposeLaws = true;
                    }
                }
            }
        }
        bellIsPushed = true;
        if (dayNum == 0)
        {
            NewDay();
        }
        else
        {
            if (dayNum == 3)
            {
                paperObjectsForNext[0] = staticAllPaperObjects[15];
                GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                Paper.GetComponent<PaperMove>().paperNumber = 15;
            }
            if (dayNum == 4)
            {
                paperObjectsForNext[0] = staticAllPaperObjects[16];
                GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                Paper.GetComponent<PaperMove>().paperNumber = 16;
            }
            if (dayNum == 5)
            {
                paperObjectsForNext[0] = staticAllPaperObjects[17];
                GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                Paper.GetComponent<PaperMove>().paperNumber = 17;
            }
            int numberOfPapers = UnityEngine.Random.Range(2, 4);
            if (dayNum >= 3)
            {
                for (int i = 0; i < numberOfPapers; i++)
                {
                    int numberOfRandomPaper = UnityEngine.Random.Range(1, 15);
                    if (!isUsedPaper[numberOfRandomPaper])
                    {
                        paperObjectsForNext[i + 1] = staticAllPaperObjects[numberOfRandomPaper];
                        isUsedPaper[numberOfRandomPaper] = true;
                        GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Paper.GetComponent<PaperMove>().paperNumber = numberOfRandomPaper;
                        if (numberOfRandomPaper == 6 || numberOfRandomPaper == 7)
                        {
                            isUsedPaper[6] = true;
                            isUsedPaper[7] = true;
                        }
                    }
                    else
                    {
                        i = i - 1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < numberOfPapers; i++)
                {
                    int numberOfRandomPaper = UnityEngine.Random.Range(1, 15);
                    if (!isUsedPaper[numberOfRandomPaper])
                    {
                        paperObjectsForNext[i] = staticAllPaperObjects[numberOfRandomPaper];
                        isUsedPaper[numberOfRandomPaper] = true;
                        GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Paper.GetComponent<PaperMove>().paperNumber = numberOfRandomPaper;
                        if (numberOfRandomPaper == 6 || numberOfRandomPaper == 7) 
                        {
                            isUsedPaper[6] = true;
                            isUsedPaper[7] = true;
                        }
                    }
                    else
                    {
                        i = i - 1;
                    }
                }
            }
            NewDay();
        }
    }
    public static void NewsPaperForBetween() 
    {
        int newspaperInt;
        if (citizensCanProposeLaws) 
        {
            newspaperInt = dayNum + 19;
        }
        else 
        {
            newspaperInt = dayNum + 18;
        }
        GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Paper.GetComponent<PaperMove>().paperNumber = newspaperInt;
        PaperMove.prevPapers = GameObject.FindGameObjectsWithTag("Paper");
        paperObjectsForNext[0] = DayConObj.GetComponent<DayController>().allPaperObjects[newspaperInt];
        for (int i = 0; i < paperObjectsForNext.Length; i++)
        {
            if (paperObjectsForNext[i] != null)
            {
                PaperMove.prevPapers[i].GetComponent<SpriteRenderer>().sprite = paperObjectsForNext[i];
            }
        }
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
        for (int i = 0; i < paperObjectsForNext.Length; i++)
        {
            paperObjectsForNext[i] = null;
        }
        NewsPaperForBetween();
    }
    public static void BellPush()
    {
        if (bellInt == 0)
        {
            GameObject[] paperConObj = GameObject.FindGameObjectsWithTag("PaperController");
            stampedAll = true;
            for (int i = 0; i < paperConObj.Length; i++)
            {
                if (paperConObj[i].GetComponent<PaperMove>().stampedType == 0)
                {
                    DayConObj.GetComponent<DayController>().preFullText = "You haven't stamped all papers.";
                    Agent.sprite = DayConObj.GetComponent<DayController>().Agents[3];
                    DayConObj.GetComponent<DayController>().showTextCall();
                    stampedAll = false;
                }
            }
            if (stampedAll)
            {
                InBetweenDay();
                bellInt = 1;
                RemoveText();
            }
        }
        else if (bellInt == 1)
        {
            GameObject[] PrevPapers = GameObject.FindGameObjectsWithTag("PaperController");
            for (int i = 0; i < PrevPapers.Length; i++)
            {
                if (PrevPapers[i] != null)
                {
                    Destroy(PrevPapers[i]);
                }
            }
            PapersForNext();
            bellInt = 0;
            RemoveText();
        }
    }
    IEnumerator PushBell()
    {
        bellIsPushed = false;
        bellunpushed.SetActive(false);
        bellpushed.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        bellunpushed.SetActive(true);
        bellpushed.SetActive(false);
        
    }
    IEnumerator transition()
    {
        lt.transition = true;
        yield return new WaitForSeconds(0);
    }
    public void showTextCall() 
    {
        StartCoroutine(ShowText());
    }
    IEnumerator ShowText()
    {
        if (currentText == fullText || textWasInterupted)
        {
            textWriting = false;
        }
        else
        {
            textWriting = true;
        }
        if (!textWriting)
        {
            fullText = preFullText;
        }
        else
        {
            fullText = currentText + "�";
        }
        startText = false;
        if (!textWriting)
        {
            textWriting = true;
            textWasInterupted = false;
            for (int i = 0; i < fullText.Length + 1; i++)
            {
                currentText = fullText.Substring(0, i);
                AgentText.text = currentText;
                yield return new WaitForSeconds(speed);
                if (i < fullText.Length + 1 && currentText == fullText && fullText != preFullText)
                {
                    fullText = preFullText;
                    textWriting = false;
                    textWasInterupted = true;
                    StartCoroutine(ShowText());
                    break;
                }
            }
        }
    }
    public static void RemoveText() 
    {
        DayController.DayConObj.GetComponent<DayController>().preFullText = " ";
        DayController.DayConObj.GetComponent<DayController>().showTextCall();
    }
}