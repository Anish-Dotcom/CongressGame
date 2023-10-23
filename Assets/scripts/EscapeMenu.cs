using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public GameObject EscapeObject;
    public static bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isOpen)
            {
                isOpen = false;
                EscapeObject.SetActive(false);
            }
            else
            {
                isOpen = true;
                EscapeObject.SetActive(true);
            }
        }
    }

    public void loadMainMenu()
    {
        Application.Quit();
    }

    public void closeEscape()
    {
        isOpen = false;
        EscapeObject.SetActive(false);
    }
}
