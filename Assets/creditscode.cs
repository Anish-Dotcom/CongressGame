using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditscode : MonoBehaviour
{
    public GameObject buttons;
    public float alpha = 0;
    public static bool wasOnOriginally = false;

    // Start is called before the first frame update
    void Start()
    {
        if(musicController.music == true)
        {
            wasOnOriginally = true;
            musicController.music = false;
        }
        else
        {
            wasOnOriginally = false;
        }
        StartCoroutine(waitThenGo());
    }

    // Update is called once per frame
    void Update()
    {
        CanvasGroup canvasGroup = buttons.GetComponent<CanvasGroup>();
        canvasGroup.alpha = alpha;
    }

    IEnumerator waitThenGo()
    {
        yield return new WaitForSeconds(10);
        alpha = 0.25f;
        yield return new WaitForSeconds(0.1f);
        alpha = 0.5f;
        yield return new WaitForSeconds(0.1f);
        alpha = 0.75f;
        yield return new WaitForSeconds(0.1f);
        alpha = 1f;
    }

    public void playAgain()
    {
        if (wasOnOriginally == true)
        {
            musicController.music = true;
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
