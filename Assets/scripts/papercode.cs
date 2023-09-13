using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class papercode : MonoBehaviour
{
    public float scaleFactor = 5f;
    public bool paperpickedup;
    private Vector3 pos;
    private Quaternion rot;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        paperpickedup = false;

        float randomRot = Random.Range(0, 360);

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
            if(hit.collider != null && paperpickedup == false)
            {
                if(hit.collider.gameObject == gameObject)
                {
                    transform.position = Vector3.zero;
                    transform.rotation = Quaternion.identity;
                    transform.localScale = transform.localScale * scaleFactor;
                    paperpickedup = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = pos;
            transform.rotation = rot;
            transform.localScale = transform.localScale / scaleFactor;
            paperpickedup = false;
        }
    }
}
