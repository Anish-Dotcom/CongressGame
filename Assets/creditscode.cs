using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditscode : MonoBehaviour
{
    public GameObject agent;
    public GameObject agenttext;
    public GameObject playagainbutton;
    public GameObject buttons;
    public float alpha = 0;
    public bool raycast = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitThenGo());
    }

    // Update is called once per frame
    void Update()
    {
        CanvasGroup canvasGroup = buttons.GetComponent<CanvasGroup>();
        canvasGroup.alpha = alpha;
        canvasGroup.blocksRaycasts = raycast;
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
        raycast = false;
    }

    public void playAgain()
    {
        agent.SetActive(true);
        agenttext.SetActive(true);
        playagainbutton.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
