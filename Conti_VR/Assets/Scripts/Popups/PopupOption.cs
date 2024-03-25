using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOption : MonoBehaviour, ILaserOption
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private Transform circleCanvas;
    [SerializeField]
    private Image circleFill;
    [SerializeField]
    private List<GameObject> otherOptions = new();
    public GameObject mainBox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            circleCanvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            circleFill.fillAmount = 0;
            circleCanvas.gameObject.SetActive(false);
        }
    }

    void OnEnable ()
    {
        circleCanvas.gameObject.SetActive(false);
        circleFill.fillAmount = 0;
    }
    void Update()
    {
        if (circleCanvas.gameObject.activeSelf)
        {
            circleFill.fillAmount += Time.deltaTime;
        }
        if (circleFill.fillAmount >= 1)
        {
            mainBox.SetActive(true);
            circleFill.fillAmount = 0;
            foreach (var option in otherOptions)
            {
                option.GetComponent<PopupOption>().mainBox.SetActive(false);
                option.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }

    //Laser Functions
    public void LaserClick()
    {
        if (PlayerPrefsManager.Load("Language") == "English")
        {
            switch (mainBox.name)
            {
                case "MID2":
                    AudioManager.Instance.PlayAudio("EMeterBasic");
                    break;
                case "MID3":
                    AudioManager.Instance.PlayAudio("EMeterAdvance");
                    break;
                case "in2visible":
                    AudioManager.Instance.PlayAudio("EMeterPremium");
                    break;
                case "CID1":
                    AudioManager.Instance.PlayAudio("ECIDBasic");
                    break;
                case "CID2":
                    AudioManager.Instance.PlayAudio("ECIDAdvance1");
                    break;
                case "CID_2b":
                    AudioManager.Instance.PlayAudio("ECIDAdvance2");
                    break;
                case "CID3":
                    AudioManager.Instance.PlayAudio("ECIDPremium");
                    break;
                default:
                    break;
            }
        }
        else if (PlayerPrefsManager.Load("Language") == "Japanese")
        {
            switch (mainBox.name)
            {
                case "MID2":
                    AudioManager.Instance.PlayAudio("JMeterBasic");
                    break;
                case "MID3":
                    AudioManager.Instance.PlayAudio("JMeterAdvance");
                    break;
                case "in2visible":
                    AudioManager.Instance.PlayAudio("JMeterPremium");
                    break;
                case "CID1":
                    AudioManager.Instance.PlayAudio("JCIDBasic");
                    break;
                case "CID2":
                    AudioManager.Instance.PlayAudio("JCIDAdvance1");
                    break;
                case "CID_2b":
                    AudioManager.Instance.PlayAudio("JCIDAdvance2");
                    break;
                case "CID3":
                    AudioManager.Instance.PlayAudio("JCIDPremium");
                    break;
                default:
                    break;
            }
        }

        mainBox.SetActive(true);
        circleFill.fillAmount = 0;
        foreach (var option in otherOptions)
        {
            option.GetComponent<PopupOption>().mainBox.SetActive(false);
            option.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void LaserEnter()
    {
        if (mainBox.name == "in2visible" || mainBox.name == "CID1")
        {
            meshRenderer.material.SetColor("_EmissionColor", new Color(1, 0.8f, 0) * 0.25f);
        }
        else
        {
            meshRenderer.material.SetColor("_EmissionColor", new Color(1, 0.8f, 0) * 1.5f);
        }
        meshRenderer.material.EnableKeyword("_EMISSION");
    }
    public void LaserExit()
    {
        meshRenderer.material.DisableKeyword("_EMISSION");
    }
}
