using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using Valve.VR;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        clusterDisplay = cidDisplay = 0;
        cidFirstTime = true;
        clusterText.text = PlayerPrefsManager.Load("Cluster");
        cidText.text = PlayerPrefsManager.Load("CID");

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public Transform steamCamera;
    public Transform cameraRig;
    
    public SteamVR_Action_Vector2 heightInput;
    public SteamVR_Action_Boolean toggleLaser;
    public SteamVR_Action_Boolean togglein2visible;

    public TMP_Text clusterText, cidText;
    public bool cidFirstTime = true;
    [SerializeField]
    private List<GameObject> clusterOptions, cidOptions = new();
    private int clusterDisplay, cidDisplay = 0;

    //SteamVR Inputs
    #region
    //Height
    public Vector2 GetHeight()
    {
        return heightInput.axis;
    }

    //Laser
    public bool GetLaser()
    {
        return toggleLaser.GetState(SteamVR_Input_Sources.Any);
    }

    //in2visible
    public bool Getin2visible()
    {
        return togglein2visible.GetState(SteamVR_Input_Sources.Any);
    }
    #endregion

    //UI Functions
    #region
    public void ResetOrientation()
    {
        if ((steamCamera != null) && (cameraRig != null))
        {
            //ROTATION
            // Get current head heading in scene (y-only, to avoid tilting the floor)
            float offsetAngle = steamCamera.rotation.eulerAngles.y;
            // Now rotate CameraRig in opposite direction to compensate
            cameraRig.Rotate(0f, -offsetAngle, 0f);

            //POSITION
            // Calculate postional offset between CameraRig and Camera
            Vector3 offsetPos = steamCamera.position - cameraRig.position;
            // Reposition CameraRig to desired position minus offset
            cameraRig.position = (new Vector3(0, 1.1f, 0.125f) - offsetPos);

            Debug.Log("Seat recentered!");
        }
        else
        {
            Debug.Log("Error: SteamVR objects not found!");
        }
    }
    public void EndSimulation()
    {
        Application.Quit();
    }
    public void OperatorClusterUI(string arrow)
    {
        if (arrow == "LEFT")
        {
            clusterDisplay--;
            if (clusterDisplay < 0)
            {
                clusterDisplay = 2; 
            }
        }
        else if (arrow == "RIGHT")
        {
            clusterDisplay++;
            if (clusterDisplay > 2)
            {
                clusterDisplay = 0;
            }
        }

        switch (clusterDisplay)
        {
            case 0:
                clusterText.text = "Basic";
                break;
            case 1:
                clusterText.text = "Advance";
                break;
            case 2:
                clusterText.text = "Premium";
                break;
            default:
                break;
        }
    }
    public void OperatorCIDUI(string arrow)
    {
        if (arrow == "LEFT")
        {
            cidDisplay--;
            if (cidDisplay < 0)
            {
                cidDisplay = 3;
            }
        }
        else if (arrow == "RIGHT")
        {
            cidDisplay++;
            if (cidDisplay > 3)
            {
                cidDisplay = 0;
            }
        }

        switch (cidDisplay)
        {
            case 0:
                cidText.text = "Basic";
                break;
            case 1:
                cidText.text = "Advance 1";
                break;
            case 2:
                cidText.text = "Advance 2";
                break;
            case 3:
                cidText.text = "Premium";
                break;
            default:
                break;
        }
    }
    public void ConfirmUI(string display)
    {
        if (display == "CLUSTER")
        {
            if (PlayerPrefsManager.Load("Language") == "English")
            {
                switch (clusterDisplay)
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
                switch (clusterDisplay)
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

            foreach (var option in clusterOptions)
            {
                if (option.activeSelf)
                {
                    option.SetActive(false);
                }
            }
            clusterOptions[clusterDisplay].SetActive(true);
        }
        else if (display == "CID")
        {
            if (PlayerPrefsManager.Load("Language") == "English")
            {
                switch (cidDisplay)
                {
                    case 0:
                        AudioManager.Instance.PlayAudio("ECIDBasic");
                        break;
                    case 1:
                        if (cidFirstTime)
                        {
                            List<string> audiosToPlay = new() { "ECIDAdvance1", "ECIDAdvanceQuestion" };
                            AudioManager.Instance.QueueAudios(audiosToPlay);
                            cidFirstTime = false;
                        }
                        else
                        {
                            AudioManager.Instance.PlayAudio("ECIDAdvance1");
                        }
                        break;
                    case 2:
                        AudioManager.Instance.PlayAudio("ECIDAdvance2");
                        if (cidFirstTime)
                        {
                            cidFirstTime = false;
                        }
                        break;
                    case 3:
                        AudioManager.Instance.PlayAudio("ECIDPremium");
                        break;
                    default:
                        break;
                }
            }
            else if (PlayerPrefsManager.Load("Language") == "Japanese")
            {
                switch (cidDisplay)
                {
                    case 0:
                        AudioManager.Instance.PlayAudio("JCIDBasic");
                        break;
                    case 1:
                        if (cidFirstTime)
                        {
                            List<string> audiosToPlay = new() { "JCIDAdvance1", "JCIDAdvanceQuestion" };
                            AudioManager.Instance.QueueAudios(audiosToPlay);
                            cidFirstTime = false;
                        }
                        else
                        {
                            AudioManager.Instance.PlayAudio("JCIDAdvance1");
                        }
                        break;
                    case 2:
                        AudioManager.Instance.PlayAudio("JCIDAdvance2");
                        if (cidFirstTime)
                        {
                            cidFirstTime = false;
                        }
                        break;
                    case 3:
                        AudioManager.Instance.PlayAudio("JCIDPremium");
                        break;
                    default:
                        break;
                }
            }

            foreach (var option in cidOptions)
            {
                if (option.activeSelf)
                {
                    option.SetActive(false);
                }
            }
            cidOptions[cidDisplay].SetActive(true);
        }
        Debug.Log("gamemanager cluster: " + clusterDisplay);
        Debug.Log("gamemanager cid: " + cidDisplay);
    }
    #endregion
}
