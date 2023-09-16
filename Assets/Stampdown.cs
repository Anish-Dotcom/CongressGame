using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stampdown : MonoBehaviour
{
    public bool istouchingpaper = false;
    void Update()
    {
        if(istouchingpaper == true)
        {
            Debug.Log("Tp");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Paper")
        {
            bool istouchingpaper = true;

            Debug.Log("Tz");
        }
    }
}
