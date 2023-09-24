using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class PaperMove : MonoBehaviour
{
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
    public static int[] currentSortingOrder;
    public static int currentTop;
    public static bool[] firstChange;

    private float colorInt;
    void Start()
    {
        originalScale = transform.localScale;
        paperpickedupinstance.instance.paperpickedup = false;

        float randomRot = Random.Range(-90, 90);
        float randomPosx = Random.Range(-8, 8);
        float randomPosy = Random.Range(-4, 4);

        Vector3 randomPos = new Vector3(randomPosx, randomPosy, 0f);
        transform.position = randomPos;
        transform.rotation = Quaternion.Euler(0f, 0f, randomRot);
        pos = transform.position;
        rot = transform.rotation;
        shadowX = pos.x;
        shadowY = pos.y;
        shadowPosition = paperShadow.transform.position;
        shadowPosition.x = shadowX - 0.5f;
        shadowPosition.y = shadowY + 0.25f;
        paperShadow.transform.position = shadowPosition;

        prevPapers = GameObject.FindGameObjectsWithTag("Paper");
        paperControllerObjects = GameObject.FindGameObjectsWithTag("PaperController");
        currentSortingOrder = new int[prevPapers.Length];
        firstChange = new bool[prevPapers.Length];
        stampObjects = new GameObject[prevPapers.Length];
        for (int i = 0; i < prevPapers.Length; i++) 
        {
            firstChange[i] = true;
            currentSortingOrder[i] = i;
            prevPapers[i].GetComponent<Renderer>().sortingOrder = currentSortingOrder[i];
            changePaperColor();
        }
    }
    void Update()
    {
        shadowX = pos.x;
        shadowY = pos.y;
        shadowPosition = paperShadow.transform.position;
        shadowPosition.x = shadowX - 0.5f;
        shadowPosition.y = shadowY + 0.25f;
        paperShadow.transform.position = shadowPosition;
        if (Input.GetMouseButtonDown(0))
        {
            slowToStop = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (!paperpickedupinstance.instance.paperpickedup && hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isRightMouseDown = true;
                    transform.localScale = transform.localScale * 1.05f;
                    float randomRot = Random.Range(-5, 5);
                    transform.rotation = Quaternion.Euler(0f, 0f, randomRot);//Make ease to position instead of this
                    paperShadow.SetActive(true);
                    renderer.sortingLayerName = paperpickedupsortinglayer;
                    if (isStampedObj.GetComponent<isStamped>().stampRenderer != null)
                    {
                        isStampedObj.GetComponent<isStamped>().stampRenderer.GetComponent<Renderer>().sortingLayerName = "StampUp";
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRightMouseDown = false;
            if (hit.collider != null) 
            {
                if (hit.collider.gameObject == gameObject)
                {
                    slowToStop = true;
                    transform.localScale = originalScale;
                    paperShadow.SetActive(false);
                    renderer.sortingLayerName = paperputdownsortinglayer;
                    for (int i = 0; i < prevPapers.Length; i++)
                    {
                        firstChange[i] = true;
                    }
                    for (int i = 0; i < prevPapers.Length; i++)
                    {
                        if (stampObjects[i] != null)
                        {
                            isStampedObj.GetComponent<isStamped>().stampRenderer.GetComponent<Renderer>().sortingLayerName = paperputdownsortinglayer;
                            stampObjects[i].GetComponent<Renderer>().sortingOrder = prevPapers[i].GetComponent<Renderer>().sortingOrder + 1;
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
            for (int i = 0; i < prevPapers.Length; i++)
            {
                if (firstChange[i]) 
                {
                    if (prevPapers[i] == Paper)//set the paper you are picking up to the highest paper
                    {
                        currentTop = i;
                        currentSortingOrder[i] = prevPapers.Length - 1;
                        prevPapers[i].GetComponent<Renderer>().sortingOrder = currentSortingOrder[i] * 2;// * 2 so we have spaces for the stamp layer
                        changePaperColor();
                    }
                    if (prevPapers[i] != Paper && currentSortingOrder[i] > currentTop)
                    {
                        currentSortingOrder[i] -= 1;
                        prevPapers[i].GetComponent<Renderer>().sortingOrder = currentSortingOrder[i] * 2;
                        changePaperColor();
                    }
                    firstChange[i] = false;
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
            colorInt = 0.9f - (0.08f / prevPapers.Length) / (currentSortingOrder[i] + 1);
            UnityEngine.Debug.Log(colorInt);
            prevPapers[i].GetComponent<SpriteRenderer>().color = new Color(colorInt+ (0.025f / prevPapers.Length) * (currentSortingOrder[i] + 1), colorInt, colorInt + (0.025f / prevPapers.Length) / (currentSortingOrder[i] + 1), 1f);
        }
    }
}