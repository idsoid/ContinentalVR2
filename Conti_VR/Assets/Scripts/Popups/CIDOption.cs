using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CIDOption : MonoBehaviour, ILaserOption
{
    //Hand variables
    private bool handReady = true;
    private bool handLeft = false;
    private float handInTimer = 0.0f;
    private float handPosY;

    //Object variables
    [SerializeField]
    private List<GameObject> cidOptions = new();
    [SerializeField]
    private List<MeshRenderer> cidMesh = new();
    private int listIndicator = 0;
    
    [SerializeField]
    private Transform circleCanvas;
    [SerializeField]
    private Image circleFill;
    [SerializeField]
    private List<GameObject> popupOptions = new();

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
        switch (PlayerPrefsManager.Load("CID"))
        {
            case "Affordable":
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

        for (int i = 0; i < cidOptions.Count; i++)
        {
            if (cidOptions[i].activeSelf)
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
            cidOptions[listIndicator].SetActive(false);
            listIndicator++;
            if (listIndicator >= cidOptions.Count)
            {
                listIndicator = 0;
            }
            cidOptions[listIndicator].SetActive(true);
        }
        else if (swipeDir < -0.08f)
        {
            Debug.Log("Swiped up");
            cidOptions[listIndicator].SetActive(false);
            listIndicator--;
            if (listIndicator < 0)
            {
                listIndicator = cidOptions.Count - 1;
            }
            cidOptions[listIndicator].SetActive(true);
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
            var mat = cidMesh[listIndicator].material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", new Color(1, 0.8f, 0, 0.25f));
        }
    }
    public void LaserExit()
    {
        var mat = cidMesh[listIndicator].material;
        mat.DisableKeyword("_EMISSION");
    }
}
