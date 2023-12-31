using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class PaperMove : MonoBehaviour
{
    public AudioSource sourceUp;
    public AudioSource sourceDown;

    public float scaleFactor = 5f;
    public Vector3 originalScale;
    private Vector3 pos;
    private Quaternion rot;
    private bool isRightMouseDown = false;
    public float maxPaperFollowSpeed = 20;
    public float smoothTime = 0.5f;
    public bool slowToStop = false;
    private Vector2 mousePositionSlow;
    private RaycastHit2D hit;
    public float shadowX;
    public float shadowY;
    public GameObject paperShadow;
    public GameObject isStampedObj;
    public string paperpickedupsortinglayer = "above all";
    public string paperputdownsortinglayer = "paper";
    public Renderer renderer;
    Vector2 currentVelocity;
    Vector3 shadowPosition;

    public GameObject Paper;
    public static GameObject[] prevPapers;
    public static GameObject[] stampObjects;
    public static GameObject[] paperControllerObjects;
    public int currentSortingOrder;
    public bool isCurrentTop;
    public static int currentTop;
    public static bool firstChange;
    public float subFromX;
    public GameObject hand;
    public int stampedType = 0;
    public int paperNumber;

    private float colorInt;
    void Start()
    {
        originalScale = transform.localScale;
        paperpickedupinstance.paperpickedup = false;

        float randomRot = Random.Range(-90, 90);
        float randomPosx = Random.Range(-8, 1.1f);
        float randomPosy = Random.Range(-4, 4);

        Vector3 randomPos = new Vector3(randomPosx, randomPosy, 0f);
        transform.position = randomPos;
        transform.rotation = Quaternion.Euler(0f, 0f, randomRot);
        pos = transform.position;
        rot = transform.rotation;

        shadowX = pos.x;
        shadowY = pos.y;
        shadowPosition = paperShadow.transform.position;
        shadowPosition.y = shadowY + 0.25f;
        paperShadow.transform.position = shadowPosition;
        prevPapers = GameObject.FindGameObjectsWithTag("Paper");
        paperControllerObjects = new GameObject[prevPapers.Length];
        for (int i = 0; i < prevPapers.Length; i++)
        {
            paperControllerObjects[i] = prevPapers[i].transform.parent.gameObject;
        }
        stampObjects = new GameObject[prevPapers.Length];
        firstChange = true;
        for (int i = 0; i < prevPapers.Length; i++) 
        {
            paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder = i - 1;
            if (paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder < 0) 
            {
                paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder = 0;
            }
            prevPapers[i].GetComponent<Renderer>().sortingOrder = paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder * 2;
            changePaperColor();
        }
    }
    void Update()
    {
        sourceUp.volume = musicController.masterVolume;
        sourceDown.volume = musicController.masterVolume;

        if (transform.position.x < -11f || transform.position.x > 11f || transform.position.y < -8f || transform.position.y > 8f)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
        if (paperShadow != null)
        {
            shadowX = pos.x;
            shadowY = pos.y;
            shadowPosition = paperShadow.transform.position;
            subFromX = (shadowX - 0) / 5;
            shadowPosition.x = shadowX + subFromX;
            shadowPosition.y = shadowY + 0.25f;
            paperShadow.transform.position = shadowPosition;
        }
        else 
        {
            Destroy(this);
        }
        if (Input.GetMouseButtonDown(0))
        {
            slowToStop = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (!paperpickedupinstance.paperpickedup && hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject || hit.collider.gameObject.CompareTag("StampCheck") && hit.collider.gameObject == isStampedObj)
                {
                    if (musicController.effects == true)
                    {
                        sourceUp.Play();
                    }
                    hand.SetActive(true);
                    paperpickedupinstance.paperpickedup = true;
                    firstChange = true;
                    isRightMouseDown = true;
                    transform.localScale = transform.localScale * 1.1f;
                    float randomRot = Random.Range(-3, 3);
                    transform.rotation = Quaternion.Euler(0f, 0f, randomRot);
                    paperShadow.SetActive(true);
                    renderer.sortingLayerName = paperpickedupsortinglayer;
                    if (DayController.nextDayPowerOut && paperNumber <= 17)
                    {
                        UnityEngine.Debug.Log(DayController.nextDayPowerOut);
                        DayController.nextDayPowerOut = false;
                        handscript.lightsOut = true;
                    }
                    if (hit.collider.gameObject.CompareTag("StampCheck"))
                    {
                        DayController.agentStartedTalking = true;
                        DayController.DayConObj.GetComponent<DayController>().preFullText = DayController.DayConObj.GetComponent<DayController>().AgentTextOnPickup[hit.collider.gameObject.GetComponent<isStamped>().paperControllerObject.GetComponent<PaperMove>().paperNumber];
                        DayController.Agent.sprite = DayController.DayConObj.GetComponent<DayController>().Agents[DayController.DayConObj.GetComponent<DayController>().pickupIntAgent[hit.collider.gameObject.GetComponent<isStamped>().paperControllerObject.GetComponent<PaperMove>().paperNumber]];
                        DayController.DayConObj.GetComponent<DayController>().showTextCall();
                    }
                    else 
                    {
                        DayController.agentStartedTalking = true;
                        DayController.DayConObj.GetComponent<DayController>().preFullText = DayController.DayConObj.GetComponent<DayController>().AgentTextOnPickup[hit.collider.gameObject.GetComponent<PaperMove>().paperNumber];
                        DayController.Agent.sprite = DayController.DayConObj.GetComponent<DayController>().Agents[DayController.DayConObj.GetComponent<DayController>().pickupIntAgent[hit.collider.gameObject.GetComponent<PaperMove>().paperNumber]];
                        DayController.DayConObj.GetComponent<DayController>().showTextCall();
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRightMouseDown = false;
            if (hit.collider != null) 
            {
                if (hit.collider.gameObject == gameObject || hit.collider.gameObject.CompareTag("StampCheck") && hit.collider.gameObject == isStampedObj)
                {
                    if (musicController.effects == true)
                    {
                        sourceDown.Play();
                    }
                    hand.SetActive(false);
                    paperpickedupinstance.paperpickedup = false;
                    slowToStop = true;
                    transform.localScale = originalScale;
                    paperShadow.SetActive(false);
                    renderer.sortingLayerName = paperputdownsortinglayer;
                    stampObjects = GameObject.FindGameObjectsWithTag("Stamp");
                    for (int i = 0; i < stampObjects.Length; i++)
                    {
                        if (stampObjects[i] != null)
                        {
                            prevPapers[i].GetComponentInParent<PaperMove>().isCurrentTop = false;
                            stampObjects[i].GetComponent<Renderer>().sortingLayerName = paperputdownsortinglayer;
                            stampObjects[i].GetComponent<Renderer>().sortingOrder = stampObjects[i].GetComponentInParent<isStamped>().paperControllerObject.GetComponent<PaperMove>().Paper.GetComponent<Renderer>().sortingOrder + 1;
                        }
                    }
                    
                }
            }
            mousePositionSlow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (isRightMouseDown)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, smoothTime, maxPaperFollowSpeed);
            pos = transform.position;
            rot = transform.rotation;
            if (firstChange) 
            {
                firstChange = false;
                prevPapers = GameObject.FindGameObjectsWithTag("Paper");
                for (int i = 0; i < prevPapers.Length; i++)
                {
                    paperControllerObjects[i] = prevPapers[i].transform.parent.gameObject;
                    paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder = prevPapers[i].GetComponent<Renderer>().sortingOrder / 2;
                    if (prevPapers[i] == Paper) 
                    {
                        prevPapers[i].GetComponentInParent<PaperMove>().isCurrentTop = true;
                        currentTop = paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder;
                    }
                    else
                    {
                        prevPapers[i].GetComponentInParent<PaperMove>().isCurrentTop = false;
                    }
                }
                for (int i = 0; i < prevPapers.Length; i++)
                {
                    if (prevPapers[i] != null) 
                    {
                        if (prevPapers[i] == Paper)
                        {
                            paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder = prevPapers.Length - 1;
                            prevPapers[i].GetComponent<Renderer>().sortingOrder = paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder * 2;
                            changeStampLayer();
                            changePaperColor();
                        }
                        else
                        {
                            if (paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder >= currentTop && paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder > 0)
                            {
                                paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder -= 1;
                                prevPapers[i].GetComponent<Renderer>().sortingOrder = paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder * 2;
                                changePaperColor();
                            }
                        }
                    }
                }
            }
        }
        if (slowToStop)
        {
            transform.position = Vector2.SmoothDamp(transform.position, mousePositionSlow, ref currentVelocity, smoothTime, maxPaperFollowSpeed);
        }
    }
    private void changePaperColor() 
    {
        for (int i = 0; i < prevPapers.Length; i++) 
        {
            if (prevPapers[i] != null) 
            {
                colorInt = 0.95f - (0.08f / prevPapers.Length) / (paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder + 1);

                prevPapers[i].GetComponent<SpriteRenderer>().color = new Color(colorInt + (0.025f / prevPapers.Length) * (paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder + 1), colorInt, colorInt + (0.025f / prevPapers.Length) / (paperControllerObjects[i].GetComponent<PaperMove>().currentSortingOrder + 1), 1f);
            }
        }
    }
    private void changeStampLayer() 
    {
        stampObjects = GameObject.FindGameObjectsWithTag("Stamp");
        for (int i = 0; i < stampObjects.Length; i++)
        {
            if (stampObjects[i] != null && stampObjects[i].GetComponentInParent<isStamped>().paperControllerObject.GetComponent<PaperMove>().isCurrentTop)
            {
                stampObjects[i].GetComponent<Renderer>().sortingLayerName = "StampUp";
                stampObjects[i].GetComponent<Renderer>().sortingOrder = stampObjects[i].GetComponentInParent<isStamped>().paperControllerObject.GetComponent<PaperMove>().Paper.GetComponent<Renderer>().sortingOrder + 1;
            }
            else if (stampObjects[i] != null)
            {
                stampObjects[i].GetComponent<Renderer>().sortingOrder = stampObjects[i].GetComponentInParent<isStamped>().paperControllerObject.GetComponent<PaperMove>().Paper.GetComponent<Renderer>().sortingOrder + 1;
            }
        }
    }
}