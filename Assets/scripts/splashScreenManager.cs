using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class splashScreenManager : MonoBehaviour
{
    public GameObject anim1;
    public GameObject anim2;
    public GameObject anim3;
    public GameObject anim4;
    public GameObject anim5;
    public GameObject anim6;
    public GameObject anim7;
    public GameObject anim8;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(beginAnim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator beginAnim()
    {
        yield return new WaitForSeconds(0.5f);
        anim1.SetActive(false);
        anim2.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        anim2.SetActive(false);
        anim3.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        anim3.SetActive(false);
        anim4.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        anim4.SetActive(false);
        anim5.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        anim5.SetActive(false);
        anim6.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        anim6.SetActive(false);
        anim7.SetActive(true);
        yield return new WaitForSeconds(2f);
        anim7.SetActive(false);
        anim8.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("MainMenu");
    }
}
