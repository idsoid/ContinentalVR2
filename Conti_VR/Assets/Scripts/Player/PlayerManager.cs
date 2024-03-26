using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private Transform vrCam;
    [SerializeField]
    private SteamVR_LaserPointer laserPointer;
    private bool heightSet = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }

        if (vrCam.transform.localPosition != Vector3.zero && !heightSet)
        {
            float heightDiff = 1.1f - vrCam.transform.localPosition.y;
            transform.position = new Vector3(0, transform.position.y + heightDiff, 0);
            heightSet = true;
            gameManager.ResetOrientation();
        }

        laserPointer.active = gameManager.GetLaser();
        UpdateHeight();
    }

    private void UpdateHeight()
    {
        if (gameManager.GetHeight().magnitude < 0.1f)
        {
            return;
        }

        Vector3 translateVector = 0.2f * Time.deltaTime * new Vector3(0, 1, 0);
        if (gameManager.GetHeight().y > 0.5f)
        {
            transform.position += translateVector;
        }
        else if (gameManager.GetHeight().y < -0.5f)
        {
            transform.position -= translateVector;
        }
    }
}
