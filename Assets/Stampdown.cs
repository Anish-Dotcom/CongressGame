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
    
}
