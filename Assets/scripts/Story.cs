using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    public GameObject camera;
    Vector2 currentVelocity;
    public float smoothTime = 0.5f;
    public float maxImageFollowSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 movelocation = new Vector3(108.9f, 20.8f, 0f);
        Vector3 movescale = new Vector3(12.98783f, 13.80571f, 12.26504f);

        transform.position = Vector2.SmoothDamp(transform.position, movelocation, ref currentVelocity, smoothTime, maxImageFollowSpeed);
        transform.localScale = Vector2.SmoothDamp(transform.localScale, movescale, ref currentVelocity, smoothTime, maxImageFollowSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
