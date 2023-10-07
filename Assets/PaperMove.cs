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

    public int stampedType = 0;
    public int paperNumber;

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
        paperControllerObjects = GameObject.FindGameObjectsWithTag("PaperController");
        prevPapers = GameObject.FindGameObjectsWithTag("Paper");
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
        if (transform.position.x < -11f || transform.position.x > 11f || transform.position.y < -8f || transform.position.y > 8f)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
        if (paperShadow != null)
        {
            shadowX = pos.x;
            shadowY = pos.y;
            shadowPosition = paperShadow.transform.position;
            shadowPosition.x = shadowX - 0.5f;
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
            if (!paperpickedupinstance.instance.paperpickedup && hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject || hit.collider.gameObject.CompareTag("StampCheck") && hit.collider.gameObject == isStampedObj)
                {
                    isRightMouseDown = true;
                    transform.localScale = transform.localScale * 1.1f;
                    float randomRot = Random.Range(-3, 3);
                    transform.rotation = Quaternion.Euler(0f, 0f, randomRot);
                    paperShadow.SetActive(true);
                    renderer.sortingLayerName = paperpickedupsortinglayer;
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
                            stampObjects[i].GetComponent<Renderer>().sortingLayerName = paperputdownsortinglayer;
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
                    if (prevPapers[i] == Paper)
                    {
                        currentTop = i;
                        currentSortingOrder[i] = prevPapers.Length - 1;
                        prevPapers[i].GetComponent<Renderer>().sortingOrder = currentSortingOrder[i] * 2;// * 2 so we have spaces for the stamp layer
                        changeStampLayer();
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
            colorInt = 0.95f - (0.08f / prevPapers.Length) / (currentSortingOrder[i] + 1);
            UnityEngine.Debug.Log(colorInt);
            prevPapers[i].GetComponent<SpriteRenderer>().color = new Color(colorInt+ (0.025f / prevPapers.Length) * (currentSortingOrder[i] + 1), colorInt, colorInt + (0.025f / prevPapers.Length) / (currentSortingOrder[i] + 1), 1f);
        }
    }
    private void changeStampLayer() 
    {
        for (int i = 0; i < prevPapers.Length; i++)
        {
            if (stampObjects[i] != null && i == currentTop)
            {
                stampObjects[i].GetComponent<Renderer>().sortingLayerName = "StampUp";
                stampObjects[i].GetComponent<Renderer>().sortingOrder = prevPapers[i].GetComponent<Renderer>().sortingOrder + 1;
            }
            else if (stampObjects[i] != null)
            {
                stampObjects[i].GetComponent<Renderer>().sortingOrder = prevPapers[i].GetComponent<Renderer>().sortingOrder + 1;
            }
        }
    }
}