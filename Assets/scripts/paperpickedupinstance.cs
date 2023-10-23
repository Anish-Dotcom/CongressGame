using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paperpickedupinstance : MonoBehaviour
{
    public bool paperpickedup;
    public static paperpickedupinstance instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
