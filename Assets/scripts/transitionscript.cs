using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitionscript : MonoBehaviour
{
    public Animator transition;
    public static bool runTransition = false;
    public static bool mainMenuLoaded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (runTransition)
        {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
}
