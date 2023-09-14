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
                    if (!paperPickedUp) 
                    {
                        transform.position = Vector3.zero;
                        transform.rotation = Quaternion.identity;
                        transform.localScale = transform.localScale * scaleFactor;
                        paperpickedupinstance.instance.paperpickedup = true;
                    }
                    else if (paperPickedUp) 
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
            isRightMouseDown = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRightMouseDown = false;
        }

        if (isRightMouseDown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null && !paperpickedupinstance.instance.paperpickedup)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0;
                    transform.position = Vector3.MoveTowards(transform.position, mousePosition, Time.deltaTime * 1000);
                    pos = transform.position;
                    rot = transform.rotation;
                }
            }
            
        }

    }
}
