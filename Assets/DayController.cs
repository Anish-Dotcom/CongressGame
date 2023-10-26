using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Array2DEditor;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class DayController : MonoBehaviour
{
    public AudioSource source;

    public static int dayNum = 0;
    public int daynum1 = 0;
    public static Sprite[] paperObjectsForNext;
    public Sprite[] allPaperObjects;//0 = tutorial doc, 1-14 = random docs, 15 = media control, 16 = create laws, 17 = force reinstatement
    public static Sprite[] staticAllPaperObjects;
    public static bool[] isUsedPaper;
    public static int[] approvalPercentageDemographics = new int[7];//The 1%, Middle class, Impoverished, Progressive, Conservative, Federalist, Anti-Federalist
    public int[] approvalPercentageDemographicsTest = new int[7];
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

    public static bool nextDayPowerOut;
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

    public int requiredAvgAprovalPercent;
    public GameObject GradingPaper;

    public static bool colorOn;
    public static bool sloganOn;
    public static bool motifOn;
    public GameObject posterColor;
    public GameObject posterSlogan;
    public GameObject posterMotif;
    public static bool posterShow = false;
    public int numberOfTimesPressed = 0;

    public GameObject posterCanvas;
    public static int anotherPickedUp = 0;
    public static bool agentTalking = false;
    public static bool OnLastPressed = false;
    public GameObject agentObject;
    public GameObject agentObjectText;

    void Start()
    {
        colorOn = false;
        sloganOn = false;
        motifOn = false;
        lt1.transition1 = true;
        lt.transition = true;
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
        for (int i = 0; i < approvalPercentageDemographics.Length; i++)
        {
            approvalPercentageDemographics[i] = approvalPercentageDemographicsTest[i];
        }
        Paper.GetComponent<PaperMove>().paperNumber = 0;
        PaperMove.prevPapers = GameObject.FindGameObjectsWithTag("Paper");
        PapersForNext();
    }
    void Update()
    {
        if (agentTalking)
        {
            agentObject.SetActive(true);
            agentObjectText.SetActive(true);
            anotherPickedUp = anotherPickedUp + 1;
            if (OnLastPressed == false)
            {
                OnLastPressed = true;
                StartCoroutine(AgentDisappear());
            }
            agentTalking = false;
        }

        daynum1 = dayNum - 1;
        if(numberOfTimesPressed == 12)
        {
            if(Input.GetMouseButton(0) || Input.GetMouseButton(0))
            {
                posterCanvas.SetActive(false);
                PapersForNext();
            }
        }
        if (bellIsPushed == true)
        {
            StartCoroutine(PushBell());
        }

        if(colorOn == true)
        {
            posterColor.SetActive(true);
        }
        else
        {
            posterColor.SetActive(false);
        }

        if(sloganOn == true)
        {
            posterSlogan.SetActive(true);
        }
        else
        {
            posterSlogan.SetActive(false);
        }

        if(motifOn == true)
        {
            posterMotif.SetActive(true);
        }
        else
        {
            posterMotif.SetActive(false);
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
        bellIsPushed = true;
        if (dayNum == 0)
        {
            NewDay();
        }
        else
        {
            DayConObj.GetComponent<DayController>().lt1.transition1 = true;
                DayConObj.GetComponent<DayController>().lt.transition = true;
            if (dayNum == 3)
            {
                paperObjectsForNext[1] = staticAllPaperObjects[15];
                GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                Paper.GetComponent<PaperMove>().paperNumber = 15;
                
            }
            if (dayNum == 4)
            {
                paperObjectsForNext[1] = staticAllPaperObjects[16];
                GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                Paper.GetComponent<PaperMove>().paperNumber = 16;
            }
            if (dayNum == 5)
            {
                paperObjectsForNext[1] = staticAllPaperObjects[17];
                GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                Paper.GetComponent<PaperMove>().paperNumber = 17;
            }
            
            if (dayNum >= 3 && dayNum < 6)
            {
                int numberOfPapers = UnityEngine.Random.Range(2, 3);
                for (int i = 0; i < numberOfPapers; i++)
                {
                    int numberOfRandomPaper = UnityEngine.Random.Range(1, 15);
                    if (!isUsedPaper[numberOfRandomPaper])
                    {
                        paperObjectsForNext[i + 2] = staticAllPaperObjects[numberOfRandomPaper];
                        isUsedPaper[numberOfRandomPaper] = true;
                        GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Paper.GetComponent<PaperMove>().paperNumber = numberOfRandomPaper;
                        if (numberOfRandomPaper == 6 || numberOfRandomPaper == 7)
                        {
                            isUsedPaper[6] = true;
                            isUsedPaper[7] = true;
                        }
                        if (numberOfRandomPaper == 2 || numberOfRandomPaper == 4)
                        {
                            isUsedPaper[2] = true;
                            isUsedPaper[4] = true;
                        }
                    }
                    else
                    {
                        i = i - 1;
                    }
                }
            }
            else if (dayNum < 3)
            {
                int numberOfPapers = UnityEngine.Random.Range(2, 4);
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
                        if (numberOfRandomPaper == 2 || numberOfRandomPaper == 4)
                        {
                            isUsedPaper[2] = true;
                            isUsedPaper[4] = true;
                        }
                    }
                    else
                    {
                        i = i - 1;
                    }
                }
            }
            else if (dayNum == 6)//summon grading paper
            {
                Instantiate(DayConObj.GetComponent<DayController>().GradingPaper, new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
            NewDay();
        }
    }
    public static void NewsPaperForBetween() 
    {
        bellIsPushed = true;
        if (dayNum != 6)
        {
            int newspaperInt;
            if (citizensCanProposeLaws)
            {
                newspaperInt = dayNum + 18;
            }
            else
            {
                newspaperInt = dayNum + 17;
            }
            GameObject Paper = Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            Paper.GetComponent<PaperMove>().paperNumber = newspaperInt;
            Paper.GetComponent<PaperMove>().Paper.GetComponent<Transform>().localScale = Paper.GetComponent<PaperMove>().Paper.GetComponent<Transform>().localScale * 1.3f;
            Paper.GetComponent<PaperMove>().hand.transform.position = new Vector2(3.68f, Paper.GetComponent<PaperMove>().hand.transform.position.y);
            Paper.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = staticAllPaperObjects[newspaperInt];
            if (newspaperInt == 18)
            {
                colorOn = true;
            }
            if (newspaperInt == 19)
            {
                sloganOn = true;
            }
            if (newspaperInt == 20)
            {
                motifOn = true;
            }
        }
        else 
        {
            
        }
    }
    public static void InBetweenDay() //remove all papers and then summon news paper and madlibs politcal campain
    {
        GameObject[] paperConObj = GameObject.FindGameObjectsWithTag("PaperController");
        nextDayPowerOut = false;
        for (int i = 0; i < paperConObj.Length; i++)
        {
            if (paperConObj[i].GetComponent<PaperMove>().stampedType == 1)//decclined -----------------------------------------------------------------------------------------------
            {
                for (int b = 0; b < 7; b++)
                {
                    approvalPercentageDemographics[b] += DayConObj.GetComponent<DayController>().demographicChangeDec.GetCell(b, paperConObj[i].GetComponent<PaperMove>().paperNumber);
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 2 || paperConObj[i].GetComponent<PaperMove>().paperNumber == 4)
                    {
                        nextDayPowerOut = true;
                    }
                    else
                    {
                        handscript.lightsOut = false;
                    }
                }
            }
            if (paperConObj[i].GetComponent<PaperMove>().stampedType == 2) //accepted --------------------------------------------------------------------------------------------------
            {
                for (int b = 0; b < 7; b++)
                {
                    approvalPercentageDemographics[b] += DayConObj.GetComponent<DayController>().demographicChangeAcc.GetCell(b, paperConObj[i].GetComponent<PaperMove>().paperNumber);
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 16)
                    {
                        citizensCanProposeLaws = true;
                    }
                }
            }
        }
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
        colorOn = false;
        sloganOn = false;
        motifOn = false;
        if (bellInt == 0)
        {
            GameObject[] stampedCheckObj = GameObject.FindGameObjectsWithTag("StampCheck");
            stampedAll = true;
            for (int i = 0; i < stampedCheckObj.Length; i++)
            {
                if (stampedCheckObj[i].GetComponent<isStamped>().objIsStamped == false)
                {
                    DayConObj.GetComponent<DayController>().preFullText = "You haven't stamped all papers.";
                    Agent.sprite = DayConObj.GetComponent<DayController>().Agents[3];
                    DayConObj.GetComponent<DayController>().showTextCall();
                    agentTalking = true;
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
        numberOfTimesPressed = numberOfTimesPressed + 1;
        bellIsPushed = false;
        bellunpushed.SetActive(false);
        bellpushed.SetActive(true);
        source.Play();
        if (numberOfTimesPressed == 12)
        {
            DayConObj.GetComponent<DayController>().preFullText = "Seems like there were some budget cuts for the poster!";
            Agent.sprite = Agents[2];
            StartCoroutine(AgentDisappear());
            anotherPickedUp = anotherPickedUp + 1;
            posterCanvas.SetActive(true);
        }
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
        if (currentText == fullText && fullText != preFullText || textWasInterupted)
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
        else if (fullText != preFullText)
        {
            fullText = currentText + "—";
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
    public static void calculateEnding()//this is called when you press the bell after grading day
    {
        int totalApproval = 0;
        for (int i = 0; i < approvalPercentageDemographics.Length; i++)
        {
            totalApproval += approvalPercentageDemographics[i];
        }
        if (totalApproval / approvalPercentageDemographics.Length >= DayConObj.GetComponent<DayController>().requiredAvgAprovalPercent) //win
        {
            SceneManager.LoadScene("SucceedScene");
        }
        else //lose
        {
            SceneManager.LoadScene("DeathScene");
        }
    }

    IEnumerator AgentDisappear()
    {
        while (OnLastPressed)
        {
            yield return new WaitForSeconds(8f);
            if (anotherPickedUp > 1)
            {
                anotherPickedUp = 0;
                StartCoroutine(AgentDisappear());
            }
            else
            {
                agentObject.SetActive(false);
                agentObjectText.SetActive(false);
                OnLastPressed = false;
            }
        }
    }
}