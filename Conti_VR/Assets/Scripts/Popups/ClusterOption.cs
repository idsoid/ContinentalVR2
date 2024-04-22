using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClusterOption : MonoBehaviour, ILaserOption
{
    private GameManager gameManager;

    //Hand variables
    private bool handReady = true;
    private bool handLeft = false;
    private float handPosY;

    //Object variables
    [SerializeField]
    private List<GameObject> clusterOptions = new();
    [SerializeField]
    private List<GameObject> popupOptions = new();
    [SerializeField]
    private GameObject defaultStand;
    private int listIndicator = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && handReady)
        {
            handReady = false;
            SaveHandPos(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand") && !popupOptions[0].activeSelf && !handLeft)
        {
            handLeft = true;
            CheckSwipe(other);
            StartCoroutine(HandReady());
        }
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        switch (PlayerPrefsManager.Load("Cluster"))
        {
            case "Basic":
                listIndicator = 0;
                break;
            case "Advance":
                listIndicator = 1;
                break;
            case "Premium":
                listIndicator = 2;
                defaultStand.SetActive(false);
                break;
            default:
                break;
        }
        clusterOptions[listIndicator].SetActive(true);
        foreach (var popup in popupOptions)
        {
            popup.SetActive(false);
        }
    }
    void Update()
    {
        for (int i = 0; i < clusterOptions.Count; i++)
        {
            if (clusterOptions[i].activeSelf)
            {
                listIndicator = i;
            }

            if (listIndicator == 2)
            {
                defaultStand.SetActive(false);
            }
            else
            {
                defaultStand.SetActive(true);
            }
        }
        Debug.Log("cluster listindicator: " + listIndicator);
    }

    //Swipe functions
    private void SaveHandPos(Collider hand)
    {
        handPosY = hand.transform.position.y;
    }
    private void CheckSwipe(Collider hand)
    {
        float swipeDir = handPosY - hand.transform.position.y;
        if (swipeDir > 0.04f)
        {
            Debug.Log("Swiped down");
            clusterOptions[listIndicator].SetActive(false);
            listIndicator++;
            if (listIndicator >= clusterOptions.Count)
            {
                listIndicator = 0;
            }
            clusterOptions[listIndicator].SetActive(true);
            PlaySelectedAudio();
        }
        else if (swipeDir < -0.04f)
        {
            Debug.Log("Swiped up");
            clusterOptions[listIndicator].SetActive(false);
            listIndicator--;
            if (listIndicator < 0)
            {
                listIndicator = clusterOptions.Count - 1;
            }
            clusterOptions[listIndicator].SetActive(true);
            PlaySelectedAudio();
        }
    }
    IEnumerator HandReady()
    {
        yield return new WaitForSeconds(1.1f);
        handReady = true;
        handLeft = false;
    }

    //Laser Functions
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
            Material mat;
            if (listIndicator == 2)
            {
                if (gameManager.Getin2visible())
                {
                    mat = clusterOptions[listIndicator].transform.GetChild(1).GetComponent<MeshRenderer>().material;
                }
                else
                {
                    mat = clusterOptions[listIndicator].transform.GetChild(0).GetComponent<MeshRenderer>().material;
                }
                mat.SetColor("_EmissionColor", new Color(1, 0.8f, 0) * 0.25f);
            }
            else
            {
                mat = clusterOptions[listIndicator].GetComponentInChildren<MeshRenderer>().material;
                mat.SetColor("_EmissionColor", new Color(1, 0.8f, 0) * 1.5f);
            }
            mat.EnableKeyword("_EMISSION");
        }
    }
    public void LaserExit()
    {
        Material mat;
        if (listIndicator == 2)
        {
            if (gameManager.Getin2visible())
            {
                mat = clusterOptions[listIndicator].transform.GetChild(1).GetComponent<MeshRenderer>().material;
            }
            else
            {
                mat = clusterOptions[listIndicator].transform.GetChild(0).GetComponent<MeshRenderer>().material;
            }
        }
        else
        {
            mat = clusterOptions[listIndicator].GetComponentInChildren<MeshRenderer>().material;
        }
        mat.DisableKeyword("_EMISSION");
    }

    //Audio functions
    private void PlaySelectedAudio()
    {
        if (PlayerPrefsManager.Load("Language") == "English")
        {
            switch (listIndicator)
            {
                case 0:
                    AudioManager.Instance.PlayAudio("EMeterBasic");
                    break;
                case 1:
                    AudioManager.Instance.PlayAudio("EMeterAdvance");
                    break;
                case 2:
                    AudioManager.Instance.PlayAudio("EMeterPremium");
                    break;
                default:
                    break;
            }
        }
        else if (PlayerPrefsManager.Load("Language") == "Japanese")
        {
            switch (listIndicator)
            {
                case 0:
                    AudioManager.Instance.PlayAudio("JMeterBasic");
                    break;
                case 1:
                    AudioManager.Instance.PlayAudio("JMeterAdvance");
                    break;
                case 2:
                    AudioManager.Instance.PlayAudio("JMeterPremium");
                    break;
                default:
                    break;
            }
        }
    }
}
