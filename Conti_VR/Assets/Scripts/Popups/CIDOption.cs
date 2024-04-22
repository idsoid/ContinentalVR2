using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CIDOption : MonoBehaviour, ILaserOption
{
    //Hand variables
    private bool handReady = true;
    private bool handLeft = false;
    private float handPosY;

    //Object variables
    [SerializeField]
    private List<GameObject> cidOptions = new();
    [SerializeField]
    private List<MeshRenderer> cidMesh = new();
    [SerializeField]
    private List<GameObject> popupOptions = new();
    private int listIndicator = 0;
    private AudioManager audioManager;

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
        switch (PlayerPrefsManager.Load("CID"))
        {
            case "Basic":
                listIndicator = 0;
                break;
            case "Advance 1":
                listIndicator = 1;
                break;
            case "Advance 2":
                listIndicator = 2;
                break;
            case "Premium":
                listIndicator = 3;
                break;
            default:
                break;
        }
        cidOptions[listIndicator].SetActive(true);
        foreach (var popup in popupOptions)
        {
            popup.SetActive(false);
        }
        audioManager = AudioManager.Instance;
    }
    void Update()
    {
        for (int i = 0; i < cidOptions.Count; i++)
        {
            if (cidOptions[i].activeSelf)
            {
                listIndicator = i;
            }
        }
        Debug.Log("cid listindicator: " + listIndicator);
        Debug.Log("game manager first time: " + GameManager.Instance.cidFirstTime);
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
            cidOptions[listIndicator].SetActive(false);
            listIndicator++;
            if (listIndicator >= cidOptions.Count)
            {
                listIndicator = 0;
            }
            cidOptions[listIndicator].SetActive(true);
            PlaySelectedAudio();
        }
        else if (swipeDir < -0.04f)
        {
            Debug.Log("Swiped up");
            cidOptions[listIndicator].SetActive(false);
            listIndicator--;
            if (listIndicator < 0)
            {
                listIndicator = cidOptions.Count - 1;
            }
            cidOptions[listIndicator].SetActive(true);
            PlaySelectedAudio();
        }
    }
    IEnumerator HandReady()
    {
        yield return new WaitForSeconds(1.0f);
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
            var mat = cidMesh[listIndicator].material;
            if (listIndicator == 0)
            {
                mat.SetColor("_EmissionColor", new Color(1, 0.8f, 0) * 0.25f);
            }
            else
            {
                mat.SetColor("_EmissionColor", new Color(1, 0.8f, 0) * 1.5f);
            }
            mat.EnableKeyword("_EMISSION");
        }
    }
    public void LaserExit()
    {
        var mat = cidMesh[listIndicator].material;
        mat.DisableKeyword("_EMISSION");
    }

    //Audio Functions
    private void PlaySelectedAudio()
    {
        if (PlayerPrefsManager.Load("Language") == "English")
        {
            switch (listIndicator)
            {
                case 0:
                    audioManager.PlayAudio("ECIDBasic");
                    break;
                case 1:
                    if (GameManager.Instance.cidFirstTime)
                    {
                        List<string> audiosToPlay = new() { "ECIDAdvance1", "ECIDAdvanceQuestion" };
                        audioManager.QueueAudios(audiosToPlay);
                        GameManager.Instance.cidFirstTime = false;
                    }
                    else
                    {
                        audioManager.PlayAudio("ECIDAdvance1");
                    }
                    break;
                case 2:
                    audioManager.PlayAudio("ECIDAdvance2");
                    if (GameManager.Instance.cidFirstTime)
                    {
                        GameManager.Instance.cidFirstTime = false;
                    }
                    break;
                case 3:
                    audioManager.PlayAudio("ECIDPremium");
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
                    audioManager.PlayAudio("JCIDBasic");
                    break;
                case 1:
                    if (GameManager.Instance.cidFirstTime)
                    {
                        List<string> audiosToPlay = new() { "JCIDAdvance1", "JCIDAdvanceQuestion" };
                        audioManager.QueueAudios(audiosToPlay);
                        GameManager.Instance.cidFirstTime = false;
                    }
                    else
                    {
                        audioManager.PlayAudio("JCIDAdvance1");
                    }
                    break;
                case 2:
                    audioManager.PlayAudio("JCIDAdvance2");
                    if (GameManager.Instance.cidFirstTime)
                    {
                        GameManager.Instance.cidFirstTime = false;
                    }
                    break;
                case 3:
                    audioManager.PlayAudio("JCIDPremium");
                    break;
                default:
                    break;
            }
        }
    }
}
