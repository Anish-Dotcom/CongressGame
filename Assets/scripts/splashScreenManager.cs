using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class splashScreenManager : MonoBehaviour
{
    public float changeSpeed;
    public GameObject[] anim;
    public GameObject text;
    public int number;
    public bool loading;

    // Start is called before the first frame update
    void Start()
    {
        loading = true;
        StartCoroutine(beginAnim());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && loading == false)
        {
            transitionscript.runTransition = true;
        }
    }

    IEnumerator beginAnim()
    {
        if (number < 27)
        {
            anim[0].SetActive(false);
            anim[1].SetActive(false);
            anim[2].SetActive(false);
            anim[3].SetActive(false);
            anim[4].SetActive(false);
            anim[5].SetActive(false);
            anim[6].SetActive(false);
            anim[7].SetActive(false);
            anim[8].SetActive(false);
            anim[9].SetActive(false);
            anim[10].SetActive(false);
            anim[11].SetActive(false);
            anim[12].SetActive(false);
            anim[13].SetActive(false);
            anim[14].SetActive(false);
            anim[15].SetActive(false);
            anim[16].SetActive(false);
            anim[17].SetActive(false);
            anim[18].SetActive(false);
            anim[19].SetActive(false);
            anim[20].SetActive(false);
            anim[21].SetActive(false);
            anim[22].SetActive(false);
            anim[23].SetActive(false);
            anim[24].SetActive(false);
            anim[25].SetActive(false);
            anim[26].SetActive(false);
            anim[27].SetActive(false);
            anim[number].SetActive(true);
            yield return new WaitForSeconds(changeSpeed);
            if (number == 22)
            {
                yield return new WaitForSeconds(0.3f);
            }
            number = number + 1;
            StartCoroutine(beginAnim());
        }
        if(number == 27)
        {
            anim[26].SetActive(false);
            anim[27].SetActive(true);
            yield return new WaitForSeconds(0.2f);
            loading = false;
            text.SetActive(true);
        }
    }
}
