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

    Vector2 currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit.collider != null)
            {
                if(hit.collider.gameObject == gameObject)
                {
                    if (!paperpickedupinstance.instance.paperpickedup) 
                    {
                        transform.position = Vector3.zero;
                        transform.rotation = Quaternion.identity;
                        transform.localScale = transform.localScale * scaleFactor;
                        paperpickedupinstance.instance.paperpickedup = true;
                    }
                    else if (paperpickedupinstance.instance.paperpickedup) 
                    {
                        transform.position = pos;
                        transform.rotation = rot;
                        transform.localScale = originalScale;
                        paperpickedupinstance.instance.paperpickedup = false;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            slowToStop = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (!paperpickedupinstance.instance.paperpickedup && hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isRightMouseDown = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRightMouseDown = false;
            if (hit.collider.gameObject == gameObject) 
            {
                slowToStop = true;
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