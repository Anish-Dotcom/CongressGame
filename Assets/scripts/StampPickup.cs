using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampPickup : MonoBehaviour
{
    public bool stamppickedup;
    public static StampPickup instance;
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
