using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClusterOption : MonoBehaviour, ILaserOption
{
    //Hand variables
    private bool handReady = true;
    private bool handLeft = false;
    private float handInTimer = 0.0f;
    private float handPosY;

    //Object variables
    [SerializeField]
    private List<GameObject> clusterOptions = new();
    private int listIndicator = 0;

    [SerializeField]
    private ParticleSystem laserParticle;
    [SerializeField]
    private Transform circleCanvas;
    [SerializeField]
    private Image circleFill;
    [SerializeField]
    private List<GameObject> popupOptions = new();
    [SerializeField]
    private GameObject defaultStand;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && handReady)
        {
            handReady = false;
            SaveHandPos(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand") && !handReady && !popupOptions[0].activeSelf && !circleCanvas.gameObject.activeSelf)
        {
            if (handInTimer > 0)
            {
                handInTimer -= Time.deltaTime;
            }
            else if (handInTimer <= 0)
            {
                handInTimer = 1.25f;
                circleCanvas.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand") && !popupOptions[0].activeSelf && !handLeft)
        {
            handLeft = true;
            handInTimer = 1.25f;
            circleFill.fillAmount = 0;
            circleCanvas.gameObject.SetActive(false);
            CheckSwipe(other);
            StartCoroutine(HandReady());
        }
    }

    void Start()
    {
        circleCanvas.gameObject.SetActive(false);
        circleFill.fillAmount = 0;
        handInTimer = 1.25f;
        switch (PlayerPrefsManager.Load("Cluster"))
        {
            case "Affordable":
                clusterOptions[0].SetActive(true);
                listIndicator = 0;
                break;
            case "Advanced":
                clusterOptions[1].SetActive(true);
                listIndicator = 1;
                break;
            case "Premium":
                clusterOptions[2].SetActive(true);
                listIndicator = 2;
                break;
            default:
                break;
        }
        foreach (var popup in popupOptions)
        {
            popup.SetActive(false);
        }
    }
    void Update()
    {
        Debug.Log("handInTimer: " + handInTimer);

        if (circleCanvas.gameObject.activeSelf)
        {
            circleFill.fillAmount += Time.deltaTime;
        }
        if (circleFill.fillAmount >= 1)
        {
            circleCanvas.gameObject.SetActive(false);
            circleFill.fillAmount = 0;
            for (int i = 0; i < popupOptions.Count; i++)
            {
                popupOptions[i].SetActive(true);
            }
        }

        for (int i = 0; i < clusterOptions.Count; i++)
        {
            if (clusterOptions[i].activeSelf)
            {
                listIndicator = i;
            }
        }
    }

    //Swipe functions
    private void SaveHandPos(Collider hand)
    {
        handPosY = hand.transform.position.y;
    }
    private void CheckSwipe(Collider hand)
    {
        float swipeDir = handPosY - hand.transform.position.y;
        if (swipeDir > 0.08f)
        {
            Debug.Log("Swiped down");
            clusterOptions[listIndicator].SetActive(false);
            listIndicator++;
            if (listIndicator >= clusterOptions.Count)
            {
                listIndicator = 0;
            }
            clusterOptions[listIndicator].SetActive(true);
        }
        else if (swipeDir < -0.08f)
        {
            Debug.Log("Swiped up");
            clusterOptions[listIndicator].SetActive(false);
            listIndicator--;
            if (listIndicator < 0)
            {
                listIndicator = clusterOptions.Count - 1;
            }
            clusterOptions[listIndicator].SetActive(true);
        }
    }
    IEnumerator HandReady()
    {
        yield return new WaitForSeconds(1.1f);
        handReady = true;
        handLeft = false;
    }

    //Laser functions
    public void LaserClick()
    {
        for (int i = 0; i < popupOptions.Count; i++)
        {
            popupOptions[i].SetActive(true);
        }
    }
    public void LaserEnter()
    {
        if (!popupOptions[0].activeSelf)
        {
            laserParticle.Play();
        }
    }
    public void LaserExit()
    {
        laserParticle.Stop();
    }
}
