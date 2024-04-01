using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
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

    public SteamVR_Action_Boolean swipeEnabled;
    public SteamVR_Action_Vector2 movementInput;
    public SteamVR_Action_Vector2 heightInput;
    public SteamVR_Action_Boolean toggleLaser;
    public SteamVR_Action_Boolean togglein2visible;

    private int languageOption, clusterOption, cidOption;

    //SteamVR Inputs
    #region
    //Height
    public Vector2 GetHeight()
    {
        return heightInput.axis;
    }

    //Movement
    public Vector2 GetMovement()
    {
        return movementInput.axis;
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

    //Menu Choices
    #region
    //Language
    public int GetLanguage()
    {
        return languageOption;
    }
    public void SetLanguage(int choice)
    {
        languageOption = choice;
    }

    //Cluster
    public int GetCluster()
    {
        return clusterOption;
    }
    public void SetCluster(int choice)
    {
        clusterOption = choice;
    }

    //CID
    public int GetCID()
    {
        return cidOption;
    }
    public void SetCID(int choice)
    {
        cidOption = choice;
    }
    #endregion

    //Misc Functions
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
}
