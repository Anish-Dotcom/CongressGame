using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handscript : MonoBehaviour
{
    public GameObject rightHandEmpty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (paperpickedupinstance.instance.paperpickedup == false)
        {
            rightHandEmpty.SetActive(true);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector2(mousePosition.x, mousePosition.y);
            rightHandEmpty.transform.position = mousePosition;
        }
        else
        {
            rightHandEmpty.SetActive(false);
        }
    }
}
