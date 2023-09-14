using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public void loadMainTable() //Use this for start in main menu at the moment, change for an intro/office later
    {
        SceneManager.LoadScene("Table");
    }
    public void loadSettings() 
    {
        SceneManager.LoadScene("Settings");
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void quitGame() 
    {
        Application.Quit();
    }
}
