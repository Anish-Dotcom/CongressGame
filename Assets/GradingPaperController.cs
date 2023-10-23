using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradingPaperController : MonoBehaviour
{
    public AudioSource sourceUp;
    public AudioSource sourceDown;

    public float scaleFactor = 5f;
    public Vector3 originalScale;
    private Vector3 pos;
    private Quaternion rot;
    private bool isRightMouseDown = false;
    public float maxPaperFollowSpeed = 20;
    public float smoothTime = 0.5f;
    public bool slowToStop = false;
    private Vector2 mousePositionSlow;
    private RaycastHit2D hit;
    public GameObject isStampedObj;

    Vector2 currentVelocity;
    Vector3 shadowPosition;
    public GameObject paperShadow;
    public float shadowX;
    public float shadowY;
    public float subFromX;
    public Renderer renderer;
    public GameObject Paper;
    public GameObject hand;

    public GameObject[] barsDemographic;

    public static GameObject thisPaper;
    void Start()
    {
        thisPaper = GameObject.FindGameObjectWithTag("GradingCon"); ;
        originalScale = transform.localScale;
        paperpickedupinstance.instance.paperpickedup = false;

        float randomRot = Random.Range(-90, 90);
        float randomPosx = Random.Range(-8, 8);
        float randomPosy = Random.Range(-4, 4);

        Vector3 randomPos = new Vector3(randomPosx, randomPosy, 0f);
        transform.position = randomPos;
        transform.rotation = Quaternion.Euler(0f, 0f, randomRot);
        pos = transform.position;
        rot = transform.rotation;

        shadowX = pos.x;
        shadowY = pos.y;
        shadowPosition = paperShadow.transform.position;
        shadowPosition.y = shadowY + 0.25f;
        paperShadow.transform.position = shadowPosition;
        setDemoBarSize();
    }
    void Update()
    {
        sourceUp.volume = musicController.masterVolume;
        sourceDown.volume = musicController.masterVolume;
        if (transform.position.x < -11f || transform.position.x > 11f || transform.position.y < -8f || transform.position.y > 8f)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
        if (paperShadow != null)
        {
            shadowX = pos.x;
            shadowY = pos.y;
            shadowPosition = paperShadow.transform.position;
            subFromX = (shadowX - 0) / 5;
            shadowPosition.x = shadowX + subFromX;
            shadowPosition.y = shadowY + 0.25f;
            paperShadow.transform.position = shadowPosition;
        }
        else
        {
            Destroy(this);
        }
        if (Input.GetMouseButtonDown(0))
        {
            slowToStop = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (!paperpickedupinstance.instance.paperpickedup && hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject || hit.collider.gameObject.CompareTag("StampCheck") && hit.collider.gameObject == isStampedObj)
                {
                    if (musicController.effects == true)
                    {
                        sourceUp.Play();
                    }
                    hand.SetActive(true);
                    paperpickedupinstance.instance.paperpickedup = true;
                    isRightMouseDown = true;
                    transform.localScale = transform.localScale * 1.1f;
                    float randomRot = Random.Range(-3, 3);
                    transform.rotation = Quaternion.Euler(0f, 0f, randomRot);
                    paperShadow.SetActive(true);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRightMouseDown = false;
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject || hit.collider.gameObject.CompareTag("StampCheck") && hit.collider.gameObject == isStampedObj)
                {
                    if (musicController.effects == true)
                    {
                        sourceDown.Play();
                    }
                    paperpickedupinstance.instance.paperpickedup = false;
                    hand.SetActive(false);
                    slowToStop = true;
                    transform.localScale = originalScale;
                    paperShadow.SetActive(false);
                }
            }
            mousePositionSlow = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        if (isRightMouseDown)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.SmoothDamp(transform.position, mousePosition, ref currentVelocity, smoothTime, maxPaperFollowSpeed);
            pos = transform.position;
            rot = transform.rotation;
        }
        if (slowToStop)
        {
            transform.position = Vector2.SmoothDamp(transform.position, mousePositionSlow, ref currentVelocity, smoothTime, maxPaperFollowSpeed);
        }
    }
    public static void setDemoBarSize() 
    {
        for (int i = 0; i < GradingPaperController.thisPaper.GetComponent<GradingPaperController>().barsDemographic.Length; i++)
        {
            Vector3 barController = GradingPaperController.thisPaper.GetComponent<GradingPaperController>().barsDemographic[i].transform.localScale;
            barController.y = 0.072f * DayController.approvalPercentageDemographics[i];
            GradingPaperController.thisPaper.GetComponent<GradingPaperController>().barsDemographic[i].transform.localScale = barController;

            Vector3 barPos = GradingPaperController.thisPaper.GetComponent<GradingPaperController>().barsDemographic[i].transform.localPosition;
            barPos.y = (0.072f * DayController.approvalPercentageDemographics[i]) / 2f - 3.48f;
            GradingPaperController.thisPaper.GetComponent<GradingPaperController>().barsDemographic[i].transform.localPosition = barPos;
        }
    }
}
