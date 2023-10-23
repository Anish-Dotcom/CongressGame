using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

public class Stamp : MonoBehaviour
{
    public AudioSource source;

    public float scaleFactor = 5f;
    public Vector3 originalScale;
    private Vector3 pos;
    private Quaternion rot;
    private SpriteRenderer spriteRenderer;
    private bool isRightMouseDown = false;
    public float maxPaperFollowSpeed = 20;
    public float smoothTime = 0.5f;
    public bool slowToStop = false;
    private Vector2 mousePositionSlow;
    private RaycastHit2D hit;
    public GameObject Stampdown;
    public bool Stamppicked = false;
    public LayerMask StampLayer;
    public bool OverPaper;
    private GameObject PaperObject;
    private GameObject controllerOfPaperObj;
    private Renderer ParentRender;
    public Renderer renderer;
    public string stamppickedupsortinglayer = "above all";
    public string stampputdownsortinglayer = "Stamp";
    public Vector3 returnPos;
    public float returnPosX;
    public float returnPosY;
    public int stampType;

    public GameObject hand;
    Vector2 currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        returnPos = new Vector3(returnPosX, returnPosY, 0f);
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = musicController.masterVolume;
        if (transform.position.x < -9.5f || transform.position.x > 9.5f || transform.position.y < -5.5f || transform.position.y > 5.5f)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetMouseButtonDown(0))
        {
            slowToStop = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (!paperpickedupinstance.paperpickedup && hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    hand.SetActive(true);
                    paperpickedupinstance.paperpickedup = true;
                    Stamppicked = true;
                    isRightMouseDown = true;
                    transform.localScale = transform.localScale * 1.1f;
                    renderer.sortingLayerName = stamppickedupsortinglayer;
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
                    hand.SetActive(false);
                    paperpickedupinstance.paperpickedup = false;
                    Stamppicked = false;
                    transform.localScale = originalScale;
                    renderer.sortingLayerName = stampputdownsortinglayer;
                    transform.position = returnPos;
                }
            }
            mousePositionSlow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (isRightMouseDown)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector2(mousePosition.x, mousePosition.y - 1f);
            transform.position = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, smoothTime, maxPaperFollowSpeed);
            pos = transform.position;
            rot = transform.rotation;
        }
        if (slowToStop)
        {
            transform.position = Vector2.SmoothDamp(transform.position, mousePositionSlow, ref currentVelocity, smoothTime, maxPaperFollowSpeed);
        }
        if(Input.GetMouseButtonDown(1) && Stamppicked || Input.GetKeyDown(KeyCode.Space) && Stamppicked)
        {
            GameObject paperO = PaperObject;
            if (OverPaper && !PaperObject.GetComponent<isStamped>().objIsStamped && controllerOfPaperObj.GetComponent<PaperMove>().paperNumber <= 17)
            {
                if (musicController.effects == true)
                {
                    source.Play();
                }
                GameObject Stamp = Instantiate(Stampdown, new Vector3(transform.position.x, transform.position.y - 0.5f, 1f), Quaternion.identity, PaperObject.transform);
                Stamp.transform.localScale = new Vector3(1f, 1f, 1f);
                Stamp.GetComponent<Renderer>().sortingOrder = ParentRender.sortingOrder + 1;
                PaperObject.GetComponent<isStamped>().objIsStamped = true;
                controllerOfPaperObj.GetComponent<PaperMove>().stampedType = stampType;
                if (controllerOfPaperObj.GetComponent<PaperMove>().stampedType == 2)
                {
                    DayController.DayConObj.GetComponent<DayController>().preFullText = DayController.DayConObj.GetComponent<DayController>().AgentTextOnStampAcc[controllerOfPaperObj.GetComponent<PaperMove>().paperNumber];
                    DayController.Agent.sprite = DayController.DayConObj.GetComponent<DayController>().Agents[DayController.DayConObj.GetComponent<DayController>().stampAccAgent[controllerOfPaperObj.GetComponent<PaperMove>().paperNumber]];
                    DayController.DayConObj.GetComponent<DayController>().showTextCall();
                }
                else if (controllerOfPaperObj.GetComponent<PaperMove>().stampedType == 1)
                {
                    DayController.DayConObj.GetComponent<DayController>().preFullText = DayController.DayConObj.GetComponent<DayController>().AgentTextOnStampDec[controllerOfPaperObj.GetComponent<PaperMove>().paperNumber];
                    DayController.Agent.sprite = DayController.DayConObj.GetComponent<DayController>().Agents[DayController.DayConObj.GetComponent<DayController>().stampDecAgent[controllerOfPaperObj.GetComponent<PaperMove>().paperNumber]];
                    DayController.DayConObj.GetComponent<DayController>().showTextCall();
                }
                transform.localScale = originalScale;
                StartCoroutine(pauseBetweenStampPutDownAndUp());
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("StampCheck")) 
        {
            PaperObject = col.gameObject;
            ParentRender = PaperObject.GetComponentInParent<Renderer>();
            OverPaper = true;
            controllerOfPaperObj = col.transform.parent.gameObject.transform.parent.gameObject;
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StampCheck"))
        {
            OverPaper = false;
        }
    }

    IEnumerator pauseBetweenStampPutDownAndUp()
    {
        yield return new WaitForSeconds(0.2f);
        transform.localScale = transform.localScale * 1.1f;
    }
}
