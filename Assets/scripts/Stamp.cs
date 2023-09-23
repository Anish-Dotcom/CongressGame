using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
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
    public GameObject Stampdown;
    public bool Stamppicked = false;
    public LayerMask StampLayer;
    static public bool OverPaper;
    private GameObject PaperObject;

    Vector2 currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Stamppicked = true;
            slowToStop = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (!paperpickedupinstance.instance.paperpickedup && hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isRightMouseDown = true;
                    transform.localScale = transform.localScale * 1.1f;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            Stamppicked = false;
            isRightMouseDown = false;
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    slowToStop = true;
                    transform.localScale = originalScale;
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
        if(Input.GetMouseButtonDown(1) && Stamppicked)
        {
            if (OverPaper && !PaperObject.GetComponent<isStamped>().objIsStamped)
            {
                GameObject Stamp = Instantiate(Stampdown, new Vector3(transform.position.x, transform.position.y, 1f), Quaternion.identity, PaperObject.transform);
                Stamp.transform.localScale = new Vector3(1f, 1f, 1f);
                PaperObject.GetComponent<isStamped>().stampRenderer = Stamp.GetComponent<Renderer>();
                PaperObject.GetComponent<isStamped>().objIsStamped = true;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("StampCheck")) 
        {
            PaperObject = col.gameObject;
            OverPaper = true;
        }
    }
    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StampCheck"))
        {
            OverPaper = false;
        }
    }
}
