using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Array2DEditor;

namespace Array2DEditor
{
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
            }
            if (dayNum > 1)
            {
                lt1.transition1 = true;
                lt.transition = true;
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
                        approvalPercentageDemographics[b] += demographicChangeDec.GetCell(paperConObj[i].GetComponent<PaperMove>().paperNumber, b);
                    }
                }
                if (paperConObj[i].GetComponent<PaperMove>().stampedType == 2) //accepted --------------------------------------------------------------------------------------------------
                {
                    for (int b = 0; b < 7; b++)
                    {
                        approvalPercentageDemographics[b] += demographicChangeAcc.GetCell(paperConObj[i].GetComponent<PaperMove>().paperNumber, b);
                    }
                }
                if (paperConObj[i].GetComponent<PaperMove>().stampedType == 2) //accepted --------------------------------------------------------------------------------------------------
                {
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 1)//Federal sales tax
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 2)//Nuclear power investments
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 3)//taxing business expences and ghost bills
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 4)//fossil fuel investments
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 5)//Increase minimum wage
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 6)//Increase gun regulation
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 7)//decrease gun reg
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 8)//decrease drug abuse sentence
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 9)//alcohol ban
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 10)//ruduce police funding
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 11)//reduce homlessness
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 12)//material help for staved citizens
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 13)//funding for education
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 14)//Ban insider trading by gov officials
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 15)//enactment of media controls
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 16)//right to propose laws
                    {

                    }
                    if (paperConObj[i].GetComponent<PaperMove>().paperNumber == 17)//Force reinstatement
                    {

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
                            Instantiate(staticPaperPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
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
        IEnumerator transition()
        {
            lt.transition = true;
            yield return new WaitForSeconds(0);
        }

    }
}