using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupOption : MonoBehaviour, ILaserOption
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    [SerializeField]
    private List<GameObject> otherOptions = new();
    public GameObject mainBox;
    private AudioManager audioManager;
    private bool firstTime = true;

    void OnEnable ()
    {
        audioManager = AudioManager.Instance;
    }

    //Laser Functions
    public void LaserClick()
    {
        PlaySelectedAudio();

        mainBox.SetActive(true);
        foreach (var option in otherOptions)
        {
            option.GetComponent<PopupOption>().mainBox.SetActive(false);
            option.SetActive(false);
        }
        gameObject.SetActive(false);
    }
    public void LaserEnter()
    {
        if (mainBox.name == "Mainin2visible" || mainBox.name == "CID1")
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

    //Audio Functions
    private void PlaySelectedAudio()
    {
        if (PlayerPrefsManager.Load("Language") == "English")
        {
            switch (mainBox.name)
            {
                case "MID2":
                    audioManager.PlayAudio("EMeterBasic");
                    break;
                case "MID3":
                    audioManager.PlayAudio("EMeterAdvance");
                    break;
                case "Mainin2visible":
                    audioManager.PlayAudio("EMeterPremium");
                    break;
                case "CID1":
                    audioManager.PlayAudio("ECIDBasic");
                    break;
                case "CID2":
                    if (firstTime)
                    {
                        List<string> audiosToPlay = new() { "ECIDAdvance1", "ECIDAdvanceQuestion" };
                        audioManager.QueueAudios(audiosToPlay);
                        firstTime = false;
                    }
                    else
                    {
                        audioManager.PlayAudio("ECIDAdvance1");
                    }
                    break;
                case "CID_2b":
                    audioManager.PlayAudio("ECIDAdvance2");
                    break;
                case "CID3":
                    audioManager.PlayAudio("ECIDPremium");
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
                    audioManager.PlayAudio("JMeterBasic");
                    break;
                case "MID3":
                    audioManager.PlayAudio("JMeterAdvance");
                    break;
                case "Mainin2visible":
                    audioManager.PlayAudio("JMeterPremium");
                    break;
                case "CID1":
                    audioManager.PlayAudio("JCIDBasic");
                    break;
                case "CID2":
                    if (firstTime)
                    {
                        List<string> audiosToPlay = new() { "JCIDAdvance1", "JCIDAdvanceQuestion" };
                        audioManager.QueueAudios(audiosToPlay);
                        firstTime = false;
                    }
                    else
                    {
                        audioManager.PlayAudio("JCIDAdvance1");
                    }
                    break;
                case "CID_2b":
                    audioManager.PlayAudio("JCIDAdvance2");
                    break;
                case "CID3":
                    audioManager.PlayAudio("JCIDPremium");
                    break;
                default:
                    break;
            }
        }
    }
}
