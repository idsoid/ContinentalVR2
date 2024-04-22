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
    [SerializeField]
    private GameObject mainin2visible, in2visibleOff, in2visibleOn;
    [SerializeField]
    private List<MeshRenderer> meshRenderers = new();
    private bool heightSet = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        laserPointer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Reset player direction to face steering wheel and position
        if (vrCam.transform.localPosition != Vector3.zero && !heightSet)
        {
            float heightDiff = 1.1f - vrCam.transform.localPosition.y;
            transform.position = new Vector3(0, transform.position.y + heightDiff, 0);
            heightSet = true;
            laserPointer.active = false;
            gameManager.ResetOrientation();
        }

        //Laser
        if (!laserPointer.enabled)
        {
            laserPointer.enabled = gameManager.GetLaser();
        }
        else if (laserPointer.enabled)
        {
            laserPointer.active = gameManager.GetLaser();
        }

        //Turn off laser highlight when laser off
        if (!laserPointer.active)
        {
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material.DisableKeyword("_EMISSION");
            }
        }

        //Display change for in2visible
        if (mainin2visible.activeSelf)
        {
            in2visibleOff.SetActive(gameManager.Getin2visible());
            in2visibleOn.SetActive(!gameManager.Getin2visible());
        }

        UpdateHeight();

        //Return to MenuScene
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
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
