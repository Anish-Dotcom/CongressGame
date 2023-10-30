using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        musicController.music = false;
        StartCoroutine(beginAnim());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator beginAnim()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Credits");
    }
}
