using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class papercode : MonoBehaviour
{
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
    public bool istouchingpaper = false;
    public Collider2D colliders;
    public float shadowX;
    public float shadowY;
    public GameObject paperShadow;
    public string paperpickedupsortinglayer = "above all";
    public string paperputdownsortinglayer = "paper";
    public Renderer renderer;
    Vector2 currentVelocity;
    public GameObject isStampedObj;
    // Start is called before the first frame update
    void Start()//--------------------------------------Note for later - make paper rougly straight up when you pick it up and make each paper on start have a slightly different color so they stand out from one another when they are overlapping, currently cant pick up paper where stamps are
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        originalScale = transform.localScale;
        paperpickedupinstance.instance.paperpickedup = false;

        float randomRot = Random.Range(0, 360);
        float randomPosx = Random.Range(-8, 8);
        float randomPosy = Random.Range(-4, 4);

        Vector3 randomPos = new Vector3(randomPosx, randomPosy, 0f);
        transform.position = randomPos;
        transform.rotation = Quaternion.Euler(0f, 0f, randomRot);
        pos = transform.position;
        rot = transform.rotation;
        shadowX = pos.x;
        shadowY = pos.y;
        Vector3 shadowPosition = paperShadow.transform.position;
        shadowPosition.x = shadowX - 0.5f;
        shadowPosition.y = shadowY + 0.25f;
        paperShadow.transform.position = shadowPosition;
    }

    // Update is called once per frame
    void Update()
    {

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
            if (hit.collider.gameObject == gameObject) 
            {
                slowToStop = true;
                transform.localScale = originalScale;
                paperShadow.SetActive(false);
                renderer.sortingLayerName = paperputdownsortinglayer;
                if (isStampedObj.GetComponent<isStamped>().stampRenderer != null) 
                {
                    isStampedObj.GetComponent<isStamped>().stampRenderer.GetComponent<Renderer>().sortingLayerName = "Stamp";
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
        }
        if (slowToStop) 
        {
            transform.position = Vector2.SmoothDamp(transform.position, mousePositionSlow, ref currentVelocity, smoothTime, maxPaperFollowSpeed);
        }
    }
}